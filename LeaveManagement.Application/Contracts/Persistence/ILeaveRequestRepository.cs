﻿using LeaveManagement.Domain.Models;

namespace LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetails(int id);
    Task<List<LeaveRequest>> GetLeaveRequestWithDetails();
    Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string userId);

}