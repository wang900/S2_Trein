using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreinbeheersysteemMain.Models.Authentication
{
    public class AccountViewModel
    {
        public List<AccountDetailViewModel> Accounts { get; set; }
        
        public AccountViewModel()
        {
            Accounts = new List<AccountDetailViewModel>();
        }
    }
}
