using System;
using SqlLiteDatabase;
using KoatuuMapper.DataReaders;
using KoatuuMapper.Repository;
using Newtonsoft.Json;
using System.Linq;
using SqlLiteDatabase.Mapping;
using System.IO;
using KoatuuMapper.Models;
using MoreLinq;

namespace KoatuuMapper
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(DateTime.Now);

            var configReader = new ConfigReader(Environment.CurrentDirectory);
            var config = configReader.GetConfig();
            
            var classifiersTypes = new ClassifierTypes();

            if (config.Sqllite.RecreateDb)
            {
                File.Delete(config.Sqllite.Path);
            }

            var sqlSession = new SqlLiteSessionFactory(config.Sqllite.Path);
            var session =  sqlSession.GetSession();
            
            var repository = new DecentralizationRepository(session);
            var procesor = new DataProccesor(config, repository, classifiersTypes);
            procesor.Procces();

            // Export data
            var items = repository.GetAllItems<RefAdc>();
            var batchCounter = 0;
            
            foreach (var item in items.Batch(1000))
            {
                var json = JsonConvert.SerializeObject(item, Formatting.None);
                File.WriteAllText($"{Environment.CurrentDirectory}\\sqs_classifiers_{batchCounter}.json", json);
                batchCounter++;
            }
            
            Console.WriteLine(DateTime.Now);
        }

        
    }
}
