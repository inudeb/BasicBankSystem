using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain
{
    public class AccountEntity
    {
        public AccountEntity()
        {
            Transactions = new List<TransactionEntity>();
        }
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }

        public UserEntity User { get; set; }

        public List<TransactionEntity> Transactions { get; set; }
    }
}
