using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudantDotNet.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
    }
}
