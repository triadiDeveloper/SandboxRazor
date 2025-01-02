namespace SandboxRazor.Service
{
    public interface IEntityServiceFactory
    {
        EntityService<T> GetService<T>() where T : class;
    }

    public class EntityServiceFactory : IEntityServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public EntityService<T> GetService<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<EntityService<T>>();
        }
    }

}