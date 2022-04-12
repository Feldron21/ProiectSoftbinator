using ProiectSoftbinator.EN.Entities;
using ProiectSoftbinator.EN.Models.Role;
using ProiectSoftbinator.EN.Models.User;

namespace ProiectSoftbinator.Services.UserServices
{
    public interface IUserService
    {
        Task AddUser(UserPostModel model, List<RoleGetModel> role);
        Task DeleteUser(int id);
        Task<List<UserGetModel>> GetAll();
        Task<UserGetModel> GetById(int id);
        Task UpdateUser(UserPutModel model, int id);
        Task<List<Role>> GetUserRole(int userId);
    }
}