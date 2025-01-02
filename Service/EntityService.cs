using SandboxRazor.BaseEntity;
using SandboxRazor.Helper;

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

        // Method to create a new entity
        public async Task CreateEntityAsync(T entity)
        {
            if (entity is IAuditable auditableEntity)
            {
                auditableEntity.SetAuditFields(_currentUserHelper);
            }

            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        // Method to update an existing entity
        public async Task UpdateEntityAsync(T entity)
        {
            if (entity is IAuditable auditableEntity)
            {
                auditableEntity.SetAuditFields(_currentUserHelper);
            }

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}