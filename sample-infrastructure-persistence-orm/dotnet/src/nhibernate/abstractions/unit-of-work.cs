using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using Sample.Domain.Abstractions;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Abstractions
{
    public abstract class NHUnitOfWorkBase : IUnitOfWork
    {

        private readonly ISessionFactory _sessionFactory;

        public NHUnitOfWorkBase(ISessionFactory sessionFactory) => _sessionFactory = sessionFactory;

        public void Dispose() { }

        public Task<string> BeginAsync() => throw new System.NotImplementedException();

        public Task CommitAsync() => throw new System.NotImplementedException();

        public Task RollbackAsync() => throw new System.NotImplementedException();
    }

    public abstract class NHUnitOfWorkFactoryBase<TUnitOfWork> : IUnitOfWorkFactory<TUnitOfWork> where TUnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly Configuration _nhConfiguration;

        public Configuration NHConfiguration => _nhConfiguration;

        public NHUnitOfWorkFactoryBase(PostgresConnection postgres)
        {
            // Entity mapping
            var mapper = new ModelMapper();
            OnEntityMapping(mapper);
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            // Session factory
            _nhConfiguration = new Configuration().DataBaseIntegration(db => 
            {
                db.ConnectionString = postgres?.ToConnectionString();
                db.Dialect<PostgreSQLDialect>();
                db.Driver<NpgsqlDriver>();
            });
            _nhConfiguration.AddMapping(mapping);
			_sessionFactory = _nhConfiguration.BuildSessionFactory();
        }

        protected virtual void OnEntityMapping(ModelMapper mapper) { }

        public TUnitOfWork Create() => CreateInstance(_sessionFactory);

        protected abstract TUnitOfWork CreateInstance(ISessionFactory sessionFactory);
    }
}