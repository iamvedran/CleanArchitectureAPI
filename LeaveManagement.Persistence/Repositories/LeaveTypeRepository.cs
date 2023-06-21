using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(DatabaseContext.LeaveDatabaseContext context) : base(context)
    {
    }

    public async Task<bool> IsLeaveTypeUnique(string name)
    {
        return await _context.LeaveTypes.AnyAsync(x => x.Name == name) == false;
    }
}