using ProiectSoftbinator.EN.Models.Donation;

namespace ProiectSoftbinator.Services.DonationServices
{
    public interface IDonationService
    {
        Task AddDonation(DonationPostModel model);
        Task DeleteDonation(int id);
        Task<List<DonationGetModel>> GetAll();
        Task<DonationGetModel> GetById(int id);
        Task UpdateDonation(DonationPutModel model, int id);
    }
}