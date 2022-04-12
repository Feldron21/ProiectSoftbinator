using ProiectSoftbinator.EN;
using ProiectSoftbinator.EN.Entities;
using ProiectSoftbinator.EN.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProiectSoftbinator.EN.Models.Role;

namespace ProiectSoftbinator.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserGetModel>> GetAll()
        {
            return await _context.Users.Select(x => new UserGetModel
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Email = x.Email,
                Password = x.Password
    }).ToListAsync();
        }

        public async Task<UserGetModel> GetById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            var userGetModel = new UserGetModel
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };

            return userGetModel;
        }

        public async Task AddUser(UserPostModel model, List<RoleGetModel> roles)
        {
            var userRoles = new List<Role>();

            foreach (var role in roles)
            {
                userRoles.Add(new Role
                {
                   Name = role.Name
                }
                ); 
            }
               
            var user = new User()
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Roles = userRoles
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(UserPutModel model, int id)
        {
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    return;
                }

                user.Name = model.Name;
                user.Email = model.Email;
                user.Password = model.Password;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {

            }
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return;
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Role>> GetUserRole(int userId)
        {

            var roles = _context.Users.Include(i => i.Roles).Where(i => i.Id == userId).Select(i => i.Roles).FirstOrDefault().ToList();
            return roles;

        }

    }
}
