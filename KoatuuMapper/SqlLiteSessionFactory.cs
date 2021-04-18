using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace SqlLiteDatabase
{
    class SqlLiteSessionFactory
    {
        private ISession _session;
        private readonly string _filePath;
        private string _connfigrationString;
        
        public SqlLiteSessionFactory(string filePath)
        {
            _filePath = filePath;
            _connfigrationString = $"Data Source={_filePath};Version=3;PRAGMA {_filePath}.synchronous=off;PRAGMA {_filePath}.journal_mode=OFF;";
        }

        private ISessionFactory CreateFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard
                      .ConnectionString(_connfigrationString)
                      //.ShowSql()
                )
                .Mappings(m => m.FluentMappings
                    .Conventions.Setup(x => x.Add(AutoImport.Never()))
                    .AddFromAssemblyOf<SqlLiteSessionFactory>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                .BuildConfiguration()
                .BuildSessionFactory();
        }

        public ISession GetSession()
        {
            if (_session is null)
            {
                using (var sessionFactory = CreateFactory())
                {
                    _session = sessionFactory.OpenSession();
                }
            }

            return _session;
        }        
    }
}
