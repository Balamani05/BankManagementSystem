using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Servicess.Models
{
    public class CustomerModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? MobileNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? AccountHolder { get; set; }
        public string? IFSCCode { get; set; }
        public string? BankName { get; set; }
        public string? Branch { get; set; }
    }
}
