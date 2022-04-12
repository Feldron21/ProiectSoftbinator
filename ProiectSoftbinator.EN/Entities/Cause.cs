using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectSoftbinator.EN.Entities
{
    public class Cause
    {
        public Cause()
        {
            this.Donations = new HashSet<Donation>();
        }
        public int Id { get; set; } 
        public string Name { get; set; }
        public decimal Funds { get; set; }
        public string Website { get; set; }

        public virtual ICollection<Donation> Donations { get; set; }
    }
}
