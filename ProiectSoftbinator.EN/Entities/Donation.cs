using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectSoftbinator.EN.Entities
{
    public class Donation
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfDonation { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int CauseId { get; set; }
        public virtual Cause Cause { get; set; }
    }
}
