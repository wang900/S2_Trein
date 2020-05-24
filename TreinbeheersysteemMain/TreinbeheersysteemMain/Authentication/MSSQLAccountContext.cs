using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace TreinbeheersysteemMain.Authentication
{
    public class MSSQLAccountContext : IUserStore<Account>, IUserPasswordStore<Account>, IUserEmailStore<Account>, IUserRoleStore<Account>
    {
        private readonly string _connectionString;
        public MSSQLAccountContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Task AddToRoleAsync(Account account, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(Account account, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(
                                                "EXECUTE @executable" +
                                                "@Gebruikersnaam = @gebruikersnaam" +
                                                ",@Wachtwoord = @wachtwoord" +
                                                ",@Actief = @actief" +
                                                ",@Voornaam = @voornaam" +
                                                ",@Achternaam = @achternaam" +
                                                ",@Email = @email", connection);
                    if (account.Rol.Id == 2)
                    {
                        sqlCommand.Parameters.AddWithValue("@executable", "[dbo].[InsertBeheerder]");
                    }
                    else if (account.Rol.Id == 3)
                    {
                        sqlCommand.Parameters.AddWithValue("@executable", "[dbo].[InsertTreinVerkeersleider]");
                    }
                    sqlCommand.Parameters.AddWithValue("@gebruikersNaam", account.Gebruikersnaam);
                    sqlCommand.Parameters.AddWithValue("@wachtwoord", account.Wachtwoord);
                    sqlCommand.Parameters.AddWithValue("@actief", account.Actief);
                    sqlCommand.Parameters.AddWithValue("@voornaam", account.Persoon.Voornaam);
                    sqlCommand.Parameters.AddWithValue("@achternaam", account.Persoon.Achternaam);
                    sqlCommand.Parameters.AddWithValue("@email", account.Persoon.Email);

                    account.Id = Convert.ToInt64(sqlCommand.ExecuteScalar());
                    return Task.FromResult<IdentityResult>(IdentityResult.Success);
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        public Task<IdentityResult> DeleteAsync(Account account, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("update Account set Active = @active where Id = @id");

                    List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
                    {
                    new KeyValuePair<string, string>("id", account.Id.ToString()),
                    new KeyValuePair<string, string>("active", "0")
                    };

                    sqlCommand.ExecuteNonQuery();
                    return Task.FromResult(IdentityResult.Success);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {

        }

        public Task<Account> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand accountSqlCommand = new SqlCommand("SELECT a.[id], a.[gebruikersnaam], a.[persoonId], p.[email] FROM[Account] a INNER JOIN[Persoon] p ON p.[Id] = a.[persoonId] WHERE p.[Email] = @email AND a.[Actief] = @actief", connection);
                accountSqlCommand.Parameters.AddWithValue("@email", normalizedEmail);
                accountSqlCommand.Parameters.AddWithValue("@actief", 1);

                using (SqlDataReader sqlDataReader = accountSqlCommand.ExecuteReader())
                {
                    Account account = default(Account);
                    if (sqlDataReader.Read())
                    {
                        account = new Account(Convert.ToInt64(sqlDataReader["id"].ToString()), sqlDataReader["gebruikersnaam"].ToString(), new Persoon(Convert.ToInt64(sqlDataReader["persoonId"].ToString()), sqlDataReader["email"].ToString()));
                    }
                    return Task.FromResult(account);
                }
            }

        }

        public Task<Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    SqlCommand sqlCommand = new SqlCommand("SELECT a.[id], a.[gebruikersnaam], p.[voornaam], p.[achternaam], p.[email], r.[naam] FROM [Account] a " +
                        "INNER JOIN [Persoon] p ON p.[Id] = a.[persoonId] " +
                        "INNER JOIN [AccountRol] ar ON a.[Id] = ar.[accountId]" +
                        "INNER JOIN [Rol] r ON ar.[rolId] = r.[Id]" +
                        "WHERE a.Id = @Id AND a.[Actief] = @actief", connection);
                    sqlCommand.Parameters.AddWithValue("@id", userId);
                    sqlCommand.Parameters.AddWithValue("@actief", 1);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        Account account = default(Account);
                        if (sqlDataReader.Read())
                        {
                            account = new Account(Convert.ToInt64(sqlDataReader["id"].ToString()), sqlDataReader["gebruikersnaam"].ToString(), new Rol { Naam = sqlDataReader["naam"].ToString() }, new Persoon(sqlDataReader["voornaam"].ToString(), sqlDataReader["achternaam"].ToString(), sqlDataReader["email"].ToString()), true);
                        }
                        return Task.FromResult(account);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public Task<Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM GetAccountInfo WHERE [gebruikersNaam] = @gebruikersnaam AND [Actief] = @actief", connection);
                    sqlCommand.Parameters.AddWithValue("@gebruikersnaam", normalizedUserName);
                    sqlCommand.Parameters.AddWithValue("@actief", 1);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        Account account = default(Account);
                        if (sqlDataReader.Read())
                        {
                            account = new Account(Convert.ToInt64(sqlDataReader["id"].ToString()), sqlDataReader["gebruikersnaam"].ToString(), new Persoon(Convert.ToInt64(sqlDataReader["persoonId"].ToString()), sqlDataReader["email"].ToString()));
                            account.SetPassword(sqlDataReader["wachtwoord"].ToString());
                        }
                        return Task.FromResult(account);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<string> GetEmailAsync(Account account, CancellationToken cancellationToken)
        {
            return Task.FromResult(account.Persoon.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(Account account, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(Account account, CancellationToken cancellationToken)
        {
            return Task.FromResult(account.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(Account account, CancellationToken cancellationToken)
        {
            return Task.FromResult(account.NormalizedGebruikersnaam);
        }

        public Task<string> GetPasswordHashAsync(Account account, CancellationToken cancellationToken)
        {
            return Task.FromResult(account.Wachtwoord);
        }

        public Task<IList<string>> GetRolesAsync(Account account, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("SELECT r.[naam] FROM [Rol] r INNER JOIN [AccountRol] ar ON ar.[rolId] = r.id INNER JOIN [Account] a ON a.Id = ar.accountId WHERE ar.accountId = @accountId AND a.[actief] = @actief", connection);
                    sqlCommand.Parameters.AddWithValue("@accountId", account.Id);
                    sqlCommand.Parameters.AddWithValue("@actief", 1);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        IList<string> roles = new List<string>();
                        while (sqlDataReader.Read())
                        {
                            roles.Add(sqlDataReader["Naam"].ToString());
                        }

                        return Task.FromResult(roles);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Task<string> GetUserIdAsync(Account account, CancellationToken cancellationToken)
        {
            return Task.FromResult(account.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Account account, CancellationToken cancellationToken)
        {
            return Task.FromResult(account.Gebruikersnaam);
        }

        public Task<IList<Account>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM GetAccountInfo WHERE Naam != 'Administrator'", connection);
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        IList<Account> accounts = new List<Account>();
                        while (sqlDataReader.Read())
                        {
                            accounts.Add(new Account(Convert.ToInt64(sqlDataReader["id"].ToString()), sqlDataReader["gebruikersnaam"].ToString(), new Rol { Naam = sqlDataReader["naam"].ToString() }, new Persoon(sqlDataReader["voorNaam"].ToString(), sqlDataReader["achterNaam"].ToString(), sqlDataReader["email"].ToString()), Convert.ToBoolean(Convert.ToByte(sqlDataReader["actief"]))));
                        }

                        return Task.FromResult(accounts);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> HasPasswordAsync(Account account, CancellationToken cancellationToken)
        {
            return Task.FromResult(account.Wachtwoord != null);
        }

        public Task<bool> IsInRoleAsync(Account account, string roleName, CancellationToken cancellationToken)
        {
            try
            {

                cancellationToken.ThrowIfCancellationRequested();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("SELECT [id] FROM [Rol] WHERE [naam] = @normalizedName", connection);
                    sqlCommand.Parameters.AddWithValue("@normalizedName", roleName.ToUpper());
                    long? rolId = sqlCommand.ExecuteScalar() as long?; 

                    SqlCommand sqlCommandUserRole = new SqlCommand("SELECT COUNT(*) FROM [AccountRol] WHERE [accountId] = @accountId AND [rolId] = @rolId", connection);
                    sqlCommandUserRole.Parameters.AddWithValue("@accountId", account.Id);
                    sqlCommandUserRole.Parameters.AddWithValue("@rolId", rolId);

                    int? roleCount = sqlCommandUserRole.ExecuteScalar() as int?;

                    return Task.FromResult(roleCount > 0);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task RemoveFromRoleAsync(Account account, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(Account account, string email, CancellationToken cancellationToken)
        {
            account.Persoon.SetEmail(email);
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(Account account, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(Account account, string normalizedEmail, CancellationToken cancellationToken)
        {
            account.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetNormalizedUserNameAsync(Account account, string normalizedName, CancellationToken cancellationToken)
        {
            account.NormalizedGebruikersnaam = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(Account account, string passwordHash, CancellationToken cancellationToken)
        {
            account.Wachtwoord = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(Account account, string userName, CancellationToken cancellationToken)
        {
            account.Gebruikersnaam = userName;
            return Task.FromResult(0);
        }

        public Task<IdentityResult> UpdateAsync(Account account, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("EXEC [dbo].[UpdateAccount] " +
                                                           "@id" +
                                                           ",@voornaam" +
                                                           ",@achternaam" +
                                                           ",@email" +
                                                           ",@rolId" +
                                                           ",@actief" +
                                                           ",@wachtwoord",
                                                            connection);
                    
                    sqlCommand.Parameters.AddWithValue("@id", account.Id.ToString());
                    sqlCommand.Parameters.AddWithValue("@voornaam", account.Persoon.Voornaam);
                    sqlCommand.Parameters.AddWithValue("@achternaam", account.Persoon.Achternaam);
                    sqlCommand.Parameters.AddWithValue("@email", account.Persoon.Email);
                    sqlCommand.Parameters.AddWithValue("@rolId", account.Rol.Id);
                    sqlCommand.Parameters.AddWithValue("@actief", account.Actief ? 1 : 0);
                    sqlCommand.Parameters.AddWithValue("@wachtwoord", account.Wachtwoord);
                    sqlCommand.ExecuteNonQuery();
                    return Task.FromResult(IdentityResult.Success);
                }
            }
            catch(Exception)
            {
                throw;
            }

        }

        public Task<IList<Account>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM GetAccountInfo", connection);
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        IList<Account> accounts = new List<Account>();
                        while (sqlDataReader.Read())
                        {
                            accounts.Add(new Account(Convert.ToInt64(sqlDataReader["id"].ToString()), sqlDataReader["gebruikersnaam"].ToString(),new Rol { Naam = sqlDataReader["rol.naam"].ToString() } , new Persoon(sqlDataReader["voornaam"].ToString(), sqlDataReader["acternaam"].ToString(), sqlDataReader["email"].ToString()), Convert.ToBoolean(Convert.ToByte(sqlDataReader["actief"]))));
                        }

                        return Task.FromResult(accounts);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
