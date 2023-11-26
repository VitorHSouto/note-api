using Microsoft.EntityFrameworkCore;
using note_api.Data;
using note_api.Domain.Entities;
using note_api.DTOs.Auth;
using Npgsql;

namespace note_api.Domain.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity>
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext, "public.user")
        {

        }

        public async Task<UserEntity> GetByLogin(string email, string password)
        {
            var sql = @$"SELECT {_allColumns} FROM {_tableName} 
                WHERE email = @email AND password = @password";

             var parameters = new { Email = email, Password = password };

            var user = await QueryFirstOrDefaultAsync(sql, parameters);
            return user;
        }
    }
}
