using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectSoftbinator.EN.Models.Donation
{
    public class DonationGetModel
    {
        public string Id { get; set; }
        public string Amount { get; set; }
        public string DateOfDonation { get; set; }
        public string UserId { get; set; }
        public string CauseId { get; set; }
    }
}
