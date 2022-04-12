using ProiectSoftbinator.EN;
using ProiectSoftbinator.EN.Entities;
using ProiectSoftbinator.EN.Models.Role;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectSoftbinator.Services.RoleServices
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;
        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoleGetModel>> GetAll()
        {
            return await _context.Roles.Select(x => new RoleGetModel
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToListAsync();
        }

        public async Task AddRole(RolePostModel model)
        {
            var role = new Role()
            {
                Name = model.Name,
            };

            await _context.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRole(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);

            if (role == null)
            {
                return;
            }

            _context.Roles.Remove(role);

            await _context.SaveChangesAsync();
        }
    }
}
