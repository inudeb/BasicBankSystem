using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain
{
    public class CreateAccountRequest
    {
        public int UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
