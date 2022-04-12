using ProiectSoftbinator.EN.Entities;
using ProiectSoftbinator.EN.Models.Cause;

namespace ProiectSoftbinator.Services.CauseServices
{
    public interface ICauseService
    {
        Task<List<CauseGetModel>> GetAll();
        Task<CauseGetModel> GetById(int id);
        Task AddCause(CausePostModel model);
        Task UpdateCause(CausePutModel model, int id);
        Task DeleteCause(int id);
        Task<CauseGetModel> GetMaxCause();
    }
}
