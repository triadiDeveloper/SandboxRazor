using SandboxRazor.BaseEntity;
using SandboxRazor.Helper;
using Microsoft.EntityFrameworkCore;

namespace SandboxRazor.Service
{
    public class EntityService<T> where T : class
    {
        private readonly CurrentUserHelper _currentUserHelper;
        private readonly SandboxRazor.Models.PersistenceDbContext _context;

        public EntityService(CurrentUserHelper currentUserHelper, SandboxRazor.Models.PersistenceDbContext context)
        {
            _currentUserHelper = currentUserHelper;
            _context = context;
        }

        // Method to retrieve an entity by ID
        public async Task<T?> GetEntityByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        // Method to check if an entity exists by ID
        public async Task<bool> EntityExistsAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id) != null;
        }

        // Method to create a new entity
        public async Task CreateEntityAsync(T entity)
        {
            if (entity is IAuditable auditableEntity)
            {
                auditableEntity.SetAuditCreatedFields(_currentUserHelper);
            }

            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        // Method to update an existing entity
        public async Task UpdateEntityAsync(T entity)
        {
            if (entity is IAuditable auditableEntity)
            {
                auditableEntity.SetAuditModifiedFields(_currentUserHelper);
            }

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        // Method to delete an entity by ID
        public async Task DeleteEntityAsync(int id)
        {
            var entity = await GetEntityByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Method to check for duplicate by property
        public async Task<bool> IsDuplicateAsync(string propertyName, string propertyValue)
        {
            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), "e");
            var propertyExpression = System.Linq.Expressions.Expression.Property(parameter, propertyName);
            var valueExpression = System.Linq.Expressions.Expression.Constant(propertyValue);

            var condition = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(
                System.Linq.Expressions.Expression.Equal(propertyExpression, valueExpression),
                parameter);

            return await _context.Set<T>().AnyAsync(condition);
        }

        // Method to Paginated

        public async Task<PaginatedListHelper<T>> GetPaginatedAndFilteredAsync(
    Func<IQueryable<T>, IQueryable<T>> filterQuery,
    int pageNumber,
    int pageSize)
        {
            IQueryable<T> query = _context.Set<T>();

            // Apply filtering logic
            if (filterQuery != null)
            {
                query = filterQuery(query);
            }

            // Return paginated results
            return await PaginatedListHelper<T>.CreateAsync(query, pageNumber, pageSize);
        }

    }
}