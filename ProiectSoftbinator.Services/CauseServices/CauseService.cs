using ProiectSoftbinator.EN;
using ProiectSoftbinator.EN.Entities;
using ProiectSoftbinator.EN.Models.Cause;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectSoftbinator.Services.CauseServices
{
    public class CauseService : ICauseService
    {
        private readonly AppDbContext _context;
        public CauseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CauseGetModel>> GetAll()
        {
            return await _context.Causes.Select(x => new CauseGetModel
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Funds = x.Funds.ToString(),
                Website = x.Website
            }).ToListAsync();
        }

        public async Task<CauseGetModel> GetById(int id)
        {
            var cause = await _context.Causes.FirstOrDefaultAsync(x => x.Id == id);

            var causeGetModel = new CauseGetModel
            {
                Id = cause.Id.ToString(),
                Name = cause.Name,
                Funds = cause.Funds.ToString(),
                Website = cause.Website
            };

            return causeGetModel;
        }

        public async Task AddCause(CausePostModel model)
        {
            var cause = new Cause()
            {
                Name = model.Name,
                Website = model.Website
            };

            await _context.AddAsync(cause);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCause(CausePutModel model, int id)
        {
            try
            {
                var cause = await _context.Causes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                if (cause == null)
                {
                    return;
                }

                cause.Name = model.Name;
                cause.Website = model.Website;

                _context.Causes.Update(cause);
                await _context.SaveChangesAsync();

            }

            catch (Exception ex)
            {

            }
        }

        public async Task DeleteCause(int id)
        {
            var cause = await _context.Causes.FirstOrDefaultAsync(x => x.Id == id);

            if (cause == null)
            {
                return;
            }

            _context.Causes.Remove(cause);

            await _context.SaveChangesAsync();
        }

        public async Task<CauseGetModel> GetMaxCause()
        {
            var causes = _context.Causes.Include(i => i.Donations).ToList();
            var orderedCausesByDonations = causes.OrderByDescending(i => i.Donations.Sum(donation => donation.Amount)).Take(1);
            var cause = orderedCausesByDonations.FirstOrDefault();
            decimal fund = _context.Donations.Where(i => i.CauseId == cause.Id).Sum(donation => donation.Amount);
            var causeGetModel = new CauseGetModel
            {
                Id = cause.Id.ToString(),
                Name = cause.Name,
                Funds = fund.ToString(),
                Website = cause.Website
            };
            return causeGetModel;
        }

    }
}
