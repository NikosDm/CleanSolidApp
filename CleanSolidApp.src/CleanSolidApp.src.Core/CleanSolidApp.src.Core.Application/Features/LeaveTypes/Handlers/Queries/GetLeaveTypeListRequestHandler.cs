using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;
using CleanSolidApp.src.Core.Application.Features.LeaveTypes.Requests.Queries;
using CleanSolidApp.src.Core.Application.Persistence.Contracts;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveTypes.Handlers.Queries;

public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, List<LeaveTypeDTO>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public GetLeaveTypeListRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeDTO>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
    {
        var leaveTypes = await _leaveTypeRepository.GetAllAsync();

        return _mapper.Map<List<LeaveTypeDTO>>(leaveTypes);
    }
}
