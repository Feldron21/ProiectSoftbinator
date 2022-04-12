using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectSoftbinator.EN.Models.Donation
{
    public class DonationPostModel
    {
        public decimal Amount { get; set; }
        public DateTime DateOfDonation { get; set; }
        public int UserId { get; set; }
        public int CauseId { get; set; }
    }
}
