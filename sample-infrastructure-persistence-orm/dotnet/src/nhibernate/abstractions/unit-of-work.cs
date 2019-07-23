using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using Sample.Domain.Abstractions;
using Sample.Infrastructure.Persistence.ORM.DependencyInjection;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Abstractions
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {

        private readonly ISessionFactory _sessionFactory;

        public UnitOfWorkBase(ISessionFactory sessionFactory) => _sessionFactory = sessionFactory;

        public void Dispose() { }

        public Task<string> BeginAsync() => throw new System.NotImplementedException();

        public Task CommitAsync() => throw new System.NotImplementedException();

        public Task RollbackAsync() => throw new System.NotImplementedException();
    }

    public abstract class UnitOfWorkFactoryBase<TUnitOfWork> : IUnitOfWorkFactory<TUnitOfWork> where TUnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;

        public UnitOfWorkFactoryBase(IOptions options)
        {
            // Entity mapping
            var mapper = new ModelMapper();
            OnEntityMapping(mapper);
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            // Session factory
            var configuration = new Configuration().DataBaseIntegration(db => 
            {
                db.ConnectionString = BuildConnectionString(options.Postgres.GetValueOrDefault());
                db.Dialect<PostgreSQLDialect>();
                db.Driver<NpgsqlDriver>();
            });
            configuration.AddMapping(mapping);
			_sessionFactory = configuration.BuildSessionFactory();
        }

        protected virtual void OnEntityMapping(ModelMapper mapper) { }

        private string BuildConnectionString(PostgresConnection postgres)  => 
             $"Server=${postgres.Host};database=${postgres.Database};user id=${postgres.Username};password=${postgres.Password}";

        public TUnitOfWork Create() => CreateInstance(_sessionFactory);

        protected abstract TUnitOfWork CreateInstance(ISessionFactory sessionFactory);
    }
}