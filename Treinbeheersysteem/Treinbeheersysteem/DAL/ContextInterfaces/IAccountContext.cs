using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.Contexts
{
    public interface IAccountContext
    {

        Account GetAccountbyId(long id);
        List<Account> GetAccountsbyAchternaam(string voornaam);
        List<Account> GetAllBeheerders();
        List<Account> GetAllTreinverkeersleiders();
        long CreateAccount(Account a);
        bool UpdateAccount(Account a);
        bool DeleteAccount(long id);
    }
}
