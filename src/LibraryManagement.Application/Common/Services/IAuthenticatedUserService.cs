namespace LibraryManagement.Application.Common.Services
{
    public interface IAuthenticatedUserService
    {
        Guid UserId { get; }
    }
}