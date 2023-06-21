using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(DatabaseContext.LeaveDatabaseContext context) : base(context)
    {
    }

    public  async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        var leaveRequest = await _context.LeaveRequests
            .Include(x => x.LeaveType)
            .FirstOrDefaultAsync(x => x.Id == id);
        return leaveRequest;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails()
    {
        var leaveRequests = await _context.LeaveRequests
            .Include(x => x.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string userId)
    {
        var leaveRequests = await _context.LeaveRequests
            .Where(x=>x.RequestindEmployeeId == userId)
            .Include(x => x.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }
}