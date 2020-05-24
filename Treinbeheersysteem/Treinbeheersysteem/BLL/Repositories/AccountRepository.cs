using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.BLL.Repositories
{
    public class AccountRepository
    {
        IAccountContext Context;
        public AccountRepository(IAccountContext context)
        {
            Context = context;
        }

        public List<Account> GetAllBeheerders()
        {
            return Context.GetAllBeheerders();
        }

        public List<Account> GetAllTreinverkeersleiders()
        {
            return Context.GetAllTreinverkeersleiders();
        }

        public Account GetAccountbyId(long id)
        {
            return Context.GetAccountbyId(id);
        }

        public List<Account> GetAccountsbyAchternaam(string naam)
        {
            return Context.GetAccountsbyAchternaam(naam);
        }

        public long CreateAccount(Account a)
        {
            return Context.CreateAccount(a);
        }

        public bool UpdateAccount(Account a)
        {
            return Context.UpdateAccount(a);
        }

        public bool DeleteAccount(long id)
        {
            return Context.DeleteAccount(id);
        }
    }
}
