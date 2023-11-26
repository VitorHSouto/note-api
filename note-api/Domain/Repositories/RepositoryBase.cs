using Dapper;
using Microsoft.EntityFrameworkCore;
using note_api.Data;
using note_api.Entities;
using Npgsql;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Dynamic;
using System.Reflection;

namespace note_api.Domain.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : IEntityBase
    {
        public readonly ApplicationDbContext _dbContext;
        public DbSet<T> table;

        protected string _tableName;
        protected List<string> _columns;
        protected string _allColumns;
        protected string _allVariables;
        protected string _mergedColumns;
        protected List<PropertyInfo> _allProperties;

        public RepositoryBase(ApplicationDbContext dbContext, string tableName)
        {
            _dbContext = dbContext;
            table = _dbContext.Set<T>();

            _tableName = tableName.ToLower();
            _allProperties = typeof(T).GetProperties().ToList();
            _columns = _allProperties.Select(p => p.Name.ToLower()).ToList();
            _allColumns = string.Join(", ", _columns).ToLower();
            _allVariables = string.Join(", ", _columns.Select(i => $"@{i}")).ToLower();
            _mergedColumns = string.Join(", ", _columns.Select(i => $"{i} = @{i}")).ToLower();
        }

        public async Task<bool> Delete(Guid id)
        {
            T entity = await table.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return false;

            table.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<T?> GetById(Guid id)
        {
            return await table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<T>> ListAll()
        {
            return await table.OrderByDescending(item => item.UpdatedAt).ToListAsync();
        }

        public async Task<T> Save(T entity)
        {
            table.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            var updateEntity = await GetById(entity.Id);
            if (updateEntity == null)
                throw new Exception("Não é foi possível encontrar a entidade com esse Id.");

            updateEntity = entity;
            updateEntity.UpdatedAt = DateTime.UtcNow;

            string updateQuery = $"UPDATE {_tableName} SET {_mergedColumns} WHERE id = @id";

            var parameters = new List<NpgsqlParameter>();
            parameters.Add(new NpgsqlParameter("@id", NpgsqlDbType.Uuid) { Value = updateEntity.Id });

            foreach (var property in _allProperties)
            {
                parameters.Add(new NpgsqlParameter($"@{property.Name.ToLower()}", property.GetValue(updateEntity)));
            }

            await _dbContext.Database.ExecuteSqlRawAsync(updateQuery, parameters);
            await _dbContext.SaveChangesAsync();

            return updateEntity;
        }

        public async Task<T> QueryFirstOrDefaultAsync(string query, object parameters = null)
        {
            using (var connection = new NpgsqlConnection(_dbContext.Database.GetConnectionString()))
            {
                connection.Open();
                return (await connection.QueryFirstOrDefaultAsync<T>(query, parameters));
            }
        }

        public async Task<List<T>> QueryAsync(string query, object parameters = null)
        {
            using (var connection = new NpgsqlConnection(_dbContext.Database.GetConnectionString()))
            {
                connection.Open();
                return (await connection.QueryAsync<T>(query, parameters)).ToList();
            }
        }
    }

}
