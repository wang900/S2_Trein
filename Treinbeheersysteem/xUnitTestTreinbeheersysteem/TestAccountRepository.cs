using System;
using Xunit;
using Treinbeheersysteem.BLL.Repositories;
using System.Collections.Generic;
using Treinbeheersysteem.DAL.Model;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.MemoryContext;

namespace xUnitTestTreinbeheersysteem
{
    public class TestAccountRepository : RemoveData
    {
        readonly IAccountContext context = new MemoryAccountContext();
        AccountRepository accountRepository;

        [Fact]
        public void GetAllBeheerders()
        {
            EmptyLists();

            accountRepository = new AccountRepository(context);

            Assert.True(accountRepository.GetAllBeheerders().Exists(a => a.Rol.Naam == "Beheerder"));
            Assert.False(accountRepository.GetAllBeheerders().Exists(a => a.Rol.Naam == "Treinverkeersleider"));
            Assert.Equal(2, accountRepository.GetAllBeheerders().Count);
        }

        [Fact]
        public void GetAllTreinverkeersleiders()
        {
            EmptyLists();

            accountRepository = new AccountRepository(context);

            Assert.True(accountRepository.GetAllTreinverkeersleiders().Exists(a => a.Rol.Naam == "Treinverkeersleider"));
            Assert.False(accountRepository.GetAllTreinverkeersleiders().Exists(a => a.Rol.Naam == "Beheerder"));
            Assert.Equal(3, accountRepository.GetAllTreinverkeersleiders().Count);
        }

        [Fact]
        public void GetAccountbyId()
        {
            EmptyLists();

            accountRepository = new AccountRepository(context);
            long id = 1;

            accountRepository.GetAccountbyId(id);

            Assert.Equal(id, accountRepository.GetAccountbyId(id).Id);
        }

        [Fact]
        public void GetAccountsbyAchternaam()
        {
            EmptyLists();

            accountRepository = new AccountRepository(context);
            string naam = "achternaam";

            Assert.Equal(5, accountRepository.GetAccountsbyAchternaam(naam).Count);
        }

        [Fact]
        public void CreateAccount()
        {
            EmptyLists();

            accountRepository = new AccountRepository(context);

            Account account = new Account(7, "gebruikersnaam", new Rol(), new Persoon("voornaam", "achternaam", "email"), true);

            Assert.Equal(7, accountRepository.CreateAccount(account));
        }

        [Fact]
        public void UpdateAccount()
        {
            EmptyLists();

            accountRepository = new AccountRepository(context);
            string nieuwgebruikersnaam = "Wow";
            Account account = new Account(1, nieuwgebruikersnaam, new Rol(), new Persoon("voornaam", "achternaam", "email"), true);
            
            Assert.True(accountRepository.UpdateAccount(account));
        }

        [Fact]
        public void DeleteAccount ()
        {
            EmptyLists();

            accountRepository = new AccountRepository(context);
            Account account = new Account(1, "naam", new Rol(), new Persoon("voornaam", "achternaam", "email"), true);
            
            Assert.True(accountRepository.DeleteAccount(account.Id));
        }
    }
}
