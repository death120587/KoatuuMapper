using MoreLinq;
using NHibernate;
using System.Linq;
using KoatuuMapper.Models;
using KoatuuMapper.Mapping;
using System.Collections.Generic;
using SqlLiteDatabase.Mapping;

namespace KoatuuMapper.Repository
{
    public class DecentralizationRepository
    {
        private readonly ISession _session;
        public DecentralizationRepository(ISession session)
        {
            _session = session;
        }

        public void SaveAll<T>(IEnumerable<T> items, int batchCount)
        {
            foreach (var batch in items.Batch(batchCount))
            {
                SaveAll(batch);
            }
        }

        public void SaveAll<T>(IEnumerable<T> items)
        {
            using (var transaction = _session.BeginTransaction())
            {
                foreach (var item in items)
                {
                    _session.Save(item);
                }
                transaction.Commit();
            }
        }

        public List<T> GetAllItems<T>() where T : class
        {
            return _session.QueryOver<T>()
                .List()
                .ToList();
        }


        public RefAdc GetElementByClassifier(string classifier)
        {
            return _session.Query<RefAdc>().FirstOrDefault(x => x.Classifier.Contains(classifier));
        }
    }
}
