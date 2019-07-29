using System;
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

        private readonly ISession _session;
        private Guid? _transactionId;

        public NHUnitOfWorkBase(ISession session) => _session = session;

        public void Dispose() { }

        public Task<string> BeginAsync()
        {
            if (_transactionId.HasValue)
                throw new InvalidOperationException("A transaction has already begun");
            _session.Transaction.Begin();
            _transactionId = Guid.NewGuid();
            return Task.FromResult(_transactionId.ToString());
        }

        public async Task CommitAsync()
        {
            if (!_transactionId.HasValue)
                throw new InvalidOperationException("No transaction to commit");
            await _session.Transaction.CommitAsync();
            _transactionId = null;
        }

        public async Task RollbackAsync()
        {
            if (!_transactionId.HasValue)
                throw new InvalidOperationException("No transaction to rollback");
            await _session.Transaction.RollbackAsync();
            _transactionId = null;
        }
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

        public TUnitOfWork Create() => CreateInstance(_sessionFactory.OpenSession());

        protected abstract TUnitOfWork CreateInstance(ISession session);
    }
}