using BankSystem.Domain;

namespace BankSystem.WebApp
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Accounts = new List<AccountEntity>();
        }
        public string AccountOwner { get; set; }
        public List<AccountEntity> Accounts { get; set; }

    }
}
