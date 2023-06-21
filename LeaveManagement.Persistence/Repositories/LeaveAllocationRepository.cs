using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(DatabaseContext.LeaveDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        var leaveAllocation = await _context.LeaveAllocations
            .Include(x => x.LeaveType)
            .FirstOrDefaultAsync(x => x.Id == id);

        return leaveAllocation;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
    {
        var leaveAllocations = await _context.LeaveAllocations
            .Include(x => x.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }

    public Task<LeaveAllocation> GetLeaveAllocationWithDetails(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations.AnyAsync(x =>
            x.EmployeeId == userId && 
            x.LeaveTypeId == leaveTypeId && 
            x.Period == period);
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _context.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }

    public Task<LeaveAllocation> GetUsersAllocations(string userId, int leaveTypeId)
    {
        throw new NotImplementedException();
    }
}