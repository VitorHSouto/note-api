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

        public async Task Teste2(T entity)
        {
            string updateQuery = $"UPDATE {_tableName} SET {_mergedColumns} WHERE id = @id";

            var parameters = new ExpandoObject() as IDictionary<string, object?>;
            foreach (var property in _allProperties)
            {
                if (property.PropertyType.IsEnum)
                    parameters[property.Name] = property.GetValue(entity)?.ToString();
                else if (property.PropertyType == typeof(Guid?))
                {
                    var value = property.GetValue(entity);
                    parameters[property.Name.ToLower()] = !Equals(value, Guid.Empty) ? (Guid?)value : null;
                }
                else if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
                    var value = underlyingType.IsEnum ? property.GetValue(entity)?.ToString() : property.GetValue(entity);
                    var defaultValue = GetDefault(underlyingType);
                    parameters[property.Name] = Equals(value, defaultValue) ? null : value;
                }
                else
                {
                    parameters[property.Name.ToLower()] = property?.GetGetMethod()?.Invoke(entity, null);
                }
            }

            await _dbContext.Database.ExecuteSqlRawAsync(updateQuery, parameters);
            await _dbContext.SaveChangesAsync();
        }

        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }

}
