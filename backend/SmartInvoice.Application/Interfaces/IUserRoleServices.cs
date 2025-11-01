namespace SmartInvoice.Application.Interfaces
{
    public interface IUserRoleServices
    {
        Task AddToRoleAsync(User user, string rolename);
    }
}