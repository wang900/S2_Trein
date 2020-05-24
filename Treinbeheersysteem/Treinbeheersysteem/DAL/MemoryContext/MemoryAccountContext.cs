using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.MemoryContext
{
    public class MemoryAccountContext : BaseMemoryContext, IAccountContext
    {
        #region Account
        public bool DeleteAccount(long id)
        {
            Accounts.FirstOrDefault(ac => ac.Id == id).Actief = false;
            return true;
        }

        public long CreateAccount(Account a)
        {
            Accounts.Add(a);
            return a.Id;
        }

        public Account GetAccountbyId(long id)
        {
            Account account = Accounts.FirstOrDefault(a => a.Id == id);
            return account;
        }

        public List<Account> GetAccountsbyAchternaam(string achternaam)
        {
            return new List<Account>(Accounts.FindAll(a => a.Persoon.Achternaam == achternaam));
        }

        public List<Account> GetAllBeheerders()
        {
            return new List<Account>(Accounts.FindAll(a => a.Rol.Naam == "Beheerder"));
        }

        public List<Account> GetAllTreinverkeersleiders()
        {
            return new List<Account>(Accounts.FindAll(a => a.Rol.Naam == "Treinverkeersleider"));
        }

        public bool UpdateAccount(Account a)
        {
            int index = Accounts.FindIndex(t => t.Id == a.Id);
            Accounts[index] = a;
            return true;
        }
        #endregion
    }
}
