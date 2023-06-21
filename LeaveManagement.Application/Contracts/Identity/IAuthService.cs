using LeaveManagement.Application.Models.Identity;

namespace LeaveManagement.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest authRequest);
}