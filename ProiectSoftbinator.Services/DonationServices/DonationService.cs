using ProiectSoftbinator.EN;
using ProiectSoftbinator.EN.Entities;
using ProiectSoftbinator.EN.Models.Donation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectSoftbinator.Services.DonationServices
{
    public class DonationService : IDonationService
    {
        private readonly AppDbContext _context;
        public DonationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DonationGetModel>> GetAll()
        {
            return await _context.Donations.Select(x => new DonationGetModel
            {
                Id = x.Id.ToString(),
                Amount = x.Amount.ToString(),
                DateOfDonation = x.DateOfDonation.ToString(),
                UserId = x.UserId.ToString(),
                CauseId = x.CauseId.ToString()
            }).ToListAsync();
        }

        public async Task<DonationGetModel> GetById(int id)
        {
            var donation = await _context.Donations.FirstOrDefaultAsync(x => x.Id == id);

            var donationGetModel = new DonationGetModel
            {
                Id = donation.Id.ToString(),
                Amount = donation.Amount.ToString(),
                DateOfDonation = donation.DateOfDonation.ToString(),
                UserId = donation.UserId.ToString(),
                CauseId = donation.CauseId.ToString()
            };

            return donationGetModel;
        }

        public async Task AddDonation(DonationPostModel model)
        {
            var donation = new Donation()
            {
                Amount = model.Amount,
                UserId = model.UserId,
                CauseId = model.CauseId
            };

            await _context.AddAsync(donation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDonation(DonationPutModel model, int id)
        {
            try
            {
                var donation = await _context.Donations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                if (donation == null)
                {
                    return;
                }

                donation.Amount = model.Amount;
                donation.UserId = model.UserId;
                donation.CauseId = model.CauseId;

                _context.Donations.Update(donation);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {

            }
        }

        public async Task DeleteDonation(int id)
        {
            var donation = await _context.Donations.FirstOrDefaultAsync(x => x.Id == id);

            if (donation == null)
            {
                return;
            }

            _context.Donations.Remove(donation);

            await _context.SaveChangesAsync();
        }
    }
}
