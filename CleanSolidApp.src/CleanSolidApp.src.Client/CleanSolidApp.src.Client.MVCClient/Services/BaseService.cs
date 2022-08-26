using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Client.MVCClient.Interfaces;
using CleanSolidApp.src.Client.MVCClient.Models;
using Newtonsoft.Json;

namespace CleanSolidApp.src.Client.MVCClient.Services;

public class BaseService: IBaseService
{
    public IHttpClientFactory httpClient { get; set; }

    public BaseService(IHttpClientFactory httpClient) 
    {
        this.httpClient = httpClient;
    }

    public async Task<T> SendAsync<T>(ApiRequest apiRequest)
    {
        try 
        {
            var client = httpClient.CreateClient("CleanSolidApp");
            
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(apiRequest.Url);
            client.DefaultRequestHeaders.Clear();
            
            if (apiRequest.Data != null) 
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), System.Text.Encoding.UTF8, "application/json");
            }

            if (!string.IsNullOrWhiteSpace(apiRequest.Token)) 
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiRequest.Token);
            }

            HttpResponseMessage apiResponse = null;
            
            switch (apiRequest.ApiType) 
            {
                case SD.ApiType.Get:
                    message.Method = HttpMethod.Get;
                    break;
                case SD.ApiType.Post:
                    message.Method = HttpMethod.Post;
                    break;
                case SD.ApiType.Put:
                    message.Method = HttpMethod.Put;
                    break;
                case SD.ApiType.Delete:
                    message.Method = HttpMethod.Delete;
                    break;
                default: 
                    message.Method = HttpMethod.Get;
                    break;
            }

            apiResponse = await client.SendAsync(message);

            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            var apiResponseDTO = JsonConvert.DeserializeObject<T>(apiContent);

            return apiResponseDTO;
        }
        catch(Exception ex) 
        {
            var dto = new  
            {
                Message = "Error",
                ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                IsSuccess = false
            };

            var res = JsonConvert.SerializeObject(dto);
            var apiResponseDTO = JsonConvert.DeserializeObject<T>(res);

            return apiResponseDTO;
        }
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
}