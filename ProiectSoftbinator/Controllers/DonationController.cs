using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProiectSoftbinator.EN.Models.Donation;
using ProiectSoftbinator.Services.DonationServices;

namespace ProiectSoftbinator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        [HttpGet("getAllDonations")]
        public async Task<ActionResult> GetAllDonations()
        {
            var donation = await _donationService.GetAll();

            return Ok(donation);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var donation = await _donationService.GetById(id);

            return Ok(donation);
        }

        [HttpPost("add-donation")]
        public async Task<ActionResult> AddDonation([FromBody] DonationPostModel model)
        {
            await _donationService.AddDonation(model);

            return Ok();
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateDonation([FromBody] DonationPutModel model, int id)
        {
            await _donationService.UpdateDonation(model, id);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDonation(int id)
        {
            await _donationService.DeleteDonation(id);

            return Ok();
        }
    }
}