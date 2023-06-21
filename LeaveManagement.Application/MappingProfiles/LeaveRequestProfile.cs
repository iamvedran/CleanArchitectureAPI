using LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequestCommand;
using LeaveManagement.Domain.Models;
using AutoMapper;
using LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

namespace LeaveManagement.Application.MappingProfiles;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequestListDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequestDetailsDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestDetailsDto>();
        CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
        CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
    }
}