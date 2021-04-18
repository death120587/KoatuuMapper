using System;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Collections.Generic;
using FluentNHibernate.Conventions.Helpers;
using Common.Model;

namespace ProcuctDatabase
{
    class MsSqlSessionFactory
    {
        private static readonly Dictionary<(string ipAddress, string portNumber, string sid, string userName), ISessionFactory>
            _sessionFactories = new Dictionary<(string ipAddress, string portNumber, string sid, string userName), ISessionFactory>();

        public static int MaxThreadCount;

        protected static ISessionFactory BuildSessionFactory(string ipAddress, string portNumber, string sid, string userName,
            string userPassword)
        {
            MaxThreadCount = System.Environment.ProcessorCount * 2;
            MaxThreadCount = MaxThreadCount > 10 ? 10 : MaxThreadCount;

            var factory = Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012.ConnectionString(c => c
                        .Server(ipAddress)
                        .Database(sid)
                        .Username(userName)
                        .Password(userPassword)
                        )//.ShowSql()
                )
                .ExposeConfiguration(c =>
                {
                    c.DataBaseIntegration(db =>
                    {
                        db.Batcher<NHibernate.AdoNet.GenericBatchingBatcherFactory>();
                        db.BatchSize = 1000;
                    });
                })
                .Mappings(m => m.FluentMappings
                    .Conventions.Setup(x => x.Add(AutoImport.Never()))
                    .AddFromAssemblyOf<MsSqlSessionFactory>())
                .BuildConfiguration()
                .AddProperties(new Dictionary<string, string>
                {
                    {
                        "query.substitutions", "true 1, false 0, yes 'Y', no 'N'"
                    }
                })
                .BuildSessionFactory();

            _sessionFactories.Add(ValueTuple.Create(ipAddress, portNumber, sid, userName), factory);

            return factory;
        }

        public static ISessionFactory GetSessionFactory(MapperConfig config)
        {
            var key = ValueTuple.Create(
                config.Database.Host,
                config.Database.Port,
                config.Database.DatabaseName,
                config.Database.User
            );
            if (_sessionFactories.TryGetValue(key, out var sessionFct))
            {
                return sessionFct;
            }
            return BuildSessionFactory(
                config.Database.Host,
                config.Database.Port,
                config.Database.DatabaseName,
                config.Database.User,
                config.Database.Password
            );
        }
    }
}
