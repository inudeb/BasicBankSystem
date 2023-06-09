using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain
{
    public class UserEntity
    {

        public UserEntity()
        {
            UserAccounts = new HashSet<AccountEntity>();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Salt { get; set; }

        public virtual IEnumerable<AccountEntity> UserAccounts { get; set; }
    }
}
