using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CleanSolidApp.src.Client.MVCClient.Interfaces;
using CleanSolidApp.src.Client.MVCClient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CleanSolidApp.src.Client.MVCClient.Services;

public class AuthService : BaseService, IAuthService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILocalStorageService _localStorage;
    private JwtSecurityTokenHandler _tokenHandler;

    public AuthService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor, ILocalStorageService localStorage) : base(clientFactory)
    {
        _clientFactory = clientFactory;
        _localStorage = localStorage;
        _httpContextAccessor = httpContextAccessor;
        this._tokenHandler = new JwtSecurityTokenHandler();
    }

    public async Task<bool> Authenticate(string email, string password)
    {
        try
        {
            AuthRequest authenticationRequest = new() { Email = email, Password = password };
            var authenticationResponse = await this.SendAsync<AuthResponse>(new ApiRequest 
            { 
                ApiType = SD.ApiType.Post,
                Url = SD.ApiUrl + "/api/Account/register",
                Data = authenticationRequest
            });;

            if (!string.IsNullOrWhiteSpace(authenticationResponse.Token))
            {
                //Get Claims from token and Build auth user object
                var tokenContent = _tokenHandler.ReadJwtToken(authenticationResponse.Token);
                var claims = ParseClaims(tokenContent);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                _localStorage.SetStorageValue("token", authenticationResponse.Token);

                return true;
            }
            return false;
        }
        catch 
        {
            return false;
        }
    }

    public async Task Logout()
    {
        _localStorage.ClearStorage(new List<string> { "token" });
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task<bool> Register(RegisterViewModel registration)
    {
        var response = await this.SendAsync<RegistrationResponse>(new ApiRequest 
        { 
            ApiType = SD.ApiType.Post,
            Url = SD.ApiUrl + "/api/Account/login",
            Data = registration
        });;

        if (!string.IsNullOrEmpty(response.UserID))
        {
            await Authenticate(registration.Email, registration.Password);
            return true;
        }
        return false;
    }

    private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
    {
        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }
}
