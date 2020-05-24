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
    public class MSSQLRolContext : IRoleStore<Rol>
    {
        private readonly string _connectionString;
        public MSSQLRolContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Task<IdentityResult> CreateAsync(Rol role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(Rol role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        public Task<Rol> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Rol", connection);
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        Rol rol = default(Rol);
                        if (sqlDataReader.Read())
                        {
                            rol = new Rol();
                        }
                        return Task.FromResult<Rol>(rol);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Rol> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(Rol role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(Rol role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(Rol role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(Rol role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(Rol role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Rol role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
