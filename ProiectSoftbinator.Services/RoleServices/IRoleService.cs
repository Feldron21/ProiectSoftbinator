using ProiectSoftbinator.EN.Models.Role;

namespace ProiectSoftbinator.Services.RoleServices
{
    public interface IRoleService
    {
        Task AddRole(RolePostModel model);
        Task DeleteRole(int id);
        Task<List<RoleGetModel>> GetAll();
    }
}