using NHibernate;
using System.Linq;
using ProcuctDatabase.Mapping;
using System.Collections.Generic;
using Common.Model;

namespace ProcuctDatabase
{
    public class ProductDatabaseManager
    {
        private readonly ISession _session;
        MapperConfig _config;
        public ProductDatabaseManager(MapperConfig config)
        {
            _config = config;
            _session = Initialize();
        }

        private ISession Initialize()
        { 
            var sessionFactory = MsSqlSessionFactory.GetSessionFactory(_config);
            return sessionFactory.OpenSession();
        }

        public List<CurrentRefAdm> GetCurrentData()
        {
            return _session.QueryOver<CurrentRefAdm>()
                    .List()
                    .ToList();
        }

        public List<CurrentArchivedAdm> GetArchivedData()
        {
            return _session.QueryOver<CurrentArchivedAdm>()
                    .List()
                    .ToList();
        }
    }
}
