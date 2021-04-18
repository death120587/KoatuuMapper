using Common.Model;
using ProcuctDatabase;
using KoatuuMapper.Models;
using ProcuctDatabase.Mapping;
using System.Collections.Generic;
using KoatuuMapper.Repository;
using System.Linq;
using Common;
using System;
using KoatuuMapper.Mapping;
using System.Threading.Tasks;
using MoreLinq;
using KoatuuMapper.DataReaders;
using SqlLiteDatabase.Mapping;
using Konsole;

namespace KoatuuMapper
{
    public class DataProccesor
    {
        private List<CurrentArchivedAdm> archiveItems;
        private List<CurrentRefAdm> currentItems;
        private List<AdmItem> admItems;
        private XlsxReader xlsReader;
        private List<ClassifierItem> classifierItems;
        private List<CompareItem> compareItems;
        private readonly DecentralizationRepository _repository;
        private readonly ProductDatabaseManager procuctManager;
        private readonly ClassifierTypes _classifierTypes;
        private ProgressBar progressBar;

        public DataProccesor(MapperConfig config, DecentralizationRepository repository, ClassifierTypes classifierTypes)
        {
            _repository = repository;
            _classifierTypes = classifierTypes;
            admItems = new List<AdmItem>();
            currentItems = new List<CurrentRefAdm>();
            procuctManager = new ProductDatabaseManager(config);
            xlsReader = new XlsxReader();
        }
        
        public void Procces()
        {
            Console.WriteLine("Parsing xlsx sources");
            ReadXlsxDataSources();
            Console.WriteLine("Reading archived data");
            archiveItems = procuctManager.GetArchivedData();
            Console.WriteLine("Reading current data");
            currentItems = procuctManager.GetCurrentData();
            Console.WriteLine("Transforming archived data to temporary model");
            TransformArchivedData();
            Console.WriteLine("Transforming current data to temporary model");
            TransformCurrentData();
            Console.WriteLine("Transforming new classifiers to temporary model");
            TransformNewClassifiers();
            Console.WriteLine("Save data");
            TemporaryModelSave();
        }

        private void ReadXlsxDataSources()
        {
            // Ініціалізація вичитки даних
            var koatuuReader = new KoatuuReader(xlsReader);
            var comperedReader = new ComperedDataReader(xlsReader);

            // Вичитка даних нових класифікаторів
            Console.WriteLine(" - Parsing new classifiers");
            classifierItems = koatuuReader.Read(@"D:\Кодифікатор_до_наказу.xlsx");
            // Збереження даних нович класифікаторів
            _repository.SaveAll(classifierItems);

            // Вичитка даних порівняльної таблиці
            Console.WriteLine(" - Parsing compared table");
            compareItems = comperedReader.Read(@"D:\Порівняльна_таблиця.xlsx");
            // Збереження даних порівняльної таблиці
            _repository.SaveAll(compareItems);
        }

        private void TransformArchivedData()
        {
            progressBar = new ProgressBar(archiveItems.Count);

            foreach (var item in archiveItems)
            {
                var adcItem = GetItemByKoatuu(item.AdmId);
                var dateFrom = archiveItems.OrderByDescending(x => x.DateTo).FirstOrDefault(x => x.AdmId == item.AdmId && x.DateTo < item.DateTo);
                var levels = GetAmdLevels(item.AdmId, true);

                admItems.Add(new AdmItem
                {
                    Koatuu = item.AdmId,
                    Name = item.Name.Replace(" район", "").Replace(" область", ""),
                    FullName = item.FullName,
                    Uid = GetUniqueId(item.AdmId),
                    Lavel1 = levels.Level1.Id,
                    Lavel1Name = levels.Level1.Name,
                    Lavel1Suffix = levels.Level1.Type,
                    Lavel2 = levels.Level2.Id,
                    Lavel2Name = levels.Level2.Name,
                    Lavel2Suffix = levels.Level2.Type,
                    Lavel3 = levels.Level3.Id,
                    Lavel3Name = levels.Level3.Name,
                    Lavel3Suffix = levels.Level3.Type,
                    Lavel4 = item.AdmId,
                    Lavel4Name = item.Name,
                    Lavel4Prefix = item.Type,
                    /*Lavel5 = adcItem.ClassifierItem.Lavel5,
                    Lavel5Name = GetNameByClassifier(adcItem.ClassifierItem.Lavel5),
                    Lavel5Suffix = GetTypeByClassifier(adcItem.ClassifierItem.Lavel5),*/
                    Selectable = adcItem.ClassifierItem.Lavel5 != null || adcItem.ClassifierItem.Lavel4 != null,
                    DateStart = dateFrom != null ? dateFrom.DateTo : DateTime.Parse("01.01.1900 00:00"),
                    DateEnd = item.DateTo,
                    IsMy = false,
                    Created_At = item.UpdatedAt,
                    Created_By = item.UpdatedBy,
                    District = item.District,
                    Area = item.Area,
                    Type = item.Type,
                    IsArchived = true
                });

                progressBar.Next($" - {item.Name} ");
            }
        }

        private void TransformCurrentData()
        {
            progressBar = new ProgressBar(currentItems.Count);
            foreach (var item in currentItems)
            {
                if (item.Selectable)
                {
                    var adcItem = GetItemByKoatuu(item.Id);
                    var dateFrom = admItems.OrderByDescending(x => x.DateEnd).FirstOrDefault(x => x.Koatuu == item.Id);

                    var levels = GetAmdLevels(item.Id);

                    admItems.Add(new AdmItem
                    {
                        Koatuu = item.Id,
                        Name = item.Name.Replace(" район", "").Replace(" область", ""),
                        FullName = item.FullName,
                        Uid = GetUniqueId(item.Id),
                        Lavel1 = levels.Level1.Id,
                        Lavel1Name = levels.Level1.Name,
                        Lavel1Suffix = levels.Level1.Type,
                        Lavel2 = levels.Level2.Id,
                        Lavel2Name = levels.Level2.Name,
                        Lavel2Suffix = levels.Level2.Type,
                        Lavel3 = levels.Level3.Id,
                        Lavel3Name = levels.Level3.Name,
                        Lavel3Suffix = levels.Level3.Type,
                        Lavel4 = item.Id,
                        Lavel4Name = item.Name,
                        Lavel4Prefix = item.Type,
                        /*Lavel5 = adcItem.ClassifierItem.Lavel5,
                        Lavel5Name = GetNameByClassifier(adcItem.ClassifierItem.Lavel5),
                        Lavel5Suffix = GetTypeByClassifier(adcItem.ClassifierItem.Lavel5),*/
                        Selectable = adcItem.ClassifierItem.Lavel5 != null || adcItem.ClassifierItem.Lavel4 != null,
                        DateStart = dateFrom != null ? (DateTime)dateFrom.DateEnd : DateTime.Parse("01.01.1900 00:00"),
                        DateEnd = DateTime.Parse("17.08.2020 00:00"),
                        IsMy = item.My,
                        Created_At = item.UpdatedAt,
                        Created_By = item.UpdatedBy,
                        District = item.District,
                        Area = item.Area,
                        Type = item.Type,
                        IsArchived = true
                    });
                }

                progressBar.Next($" - {item.Name} ");
            }
        }

        private void TransformNewClassifiers()
        {
            Func<string,string> getKoatuu = (id) => {
                var item = compareItems.FirstOrDefault(c => c.Сlassifier == id);
                return item != null && item.Koatuu != "Новий район" ? item.Koatuu : null;
            };

            var result = classifierItems.Select(x => new AdmItem {
                Koatuu = getKoatuu(x.Classifier),
                Classifier = x.Classifier,
                Name = x.Name,
                FullName = "",
                Uid = GetUniqueId(getKoatuu(x.Classifier)),
                Lavel1 = x.Lavel1,
                Lavel1Name = GetNameByClassifier(x.Lavel1),
                Lavel1Suffix = GetTypeByClassifier(x.Lavel1),
                Lavel2 = x.Lavel2,
                Lavel2Name = GetNameByClassifier(x.Lavel2),
                Lavel2Suffix = GetTypeByClassifier(x.Lavel2),
                Lavel3 = x.Lavel3,
                Lavel3Name = GetNameByClassifier(x.Lavel3),
                Lavel3Suffix = GetTypeByClassifier(x.Lavel3),
                Lavel4 = x.Lavel4,
                Lavel4Name = GetNameByClassifier(x.Lavel4),
                Lavel4Prefix = GetTypeByClassifier(x.Lavel4),
                Lavel5 = x.Lavel5,
                Lavel5Name = GetNameByClassifier(x.Lavel5),
                Lavel5Suffix = GetTypeByClassifier(x.Lavel5),
                Selectable = x.Lavel5 != null || x.Lavel4 != null,
                DateStart = DateTime.Parse("17.08.2020 00:00"),
                DateEnd = DateTime.Parse("01.01.9999 00:00"),
                Type = x.Type.ToString(),
                IsArchived = false
            });

            admItems.AddRange(result);
        }

        private void TemporaryModelSave()
        {
            var itemsToSave = admItems.Select(x => new RefAdc {
                Classifier = x.IsArchived ? x.Koatuu : x.Classifier,
                Name = x.Name,
                FullName = x.FullName,
                Uid = x.Uid,
                Lavel1 = x.Lavel1,
                Lavel1Name = x.Lavel1Name,
                Lavel1Prefix = x.Lavel1Prefix,
                Lavel1Suffix = x.Lavel1Suffix,
                Lavel1Included = true,
                Lavel2 = x.Lavel2,
                Lavel2Name = x.Lavel2Name,
                Lavel2Prefix = x.Lavel2Prefix,
                Lavel2Suffix = x.Lavel2Suffix,
                Lavel2Included = x.Lavel5 is null,
                Lavel3 = x.Lavel3,
                Lavel3Name = x.Lavel3Name,
                Lavel3Prefix = x.Lavel3Prefix,
                Lavel3Suffix = x.Lavel3Suffix,
                Lavel3Included = HasDublicats(x.Name, x.Lavel2Name, x.Lavel1Name, x.DateStart, (DateTime)x.DateEnd),
                Lavel4 = x.Lavel4,
                Lavel4Name = x.Lavel4Name,
                Lavel4Prefix = x.Lavel4Prefix,
                Lavel4Suffix = x.Lavel4Suffix,
                Lavel4Included = x.Lavel5 != null,
                Lavel5 = x.Lavel5,
                Lavel5Name = x.Lavel5Name,
                Lavel5Prefix = x.Lavel5Prefix,
                Lavel5Suffix = x.Lavel5Suffix,
                Selectable = x.Selectable,
                DateStart = x.DateStart,
                DateEnd = x.DateEnd,
                Type = x.Type,
                Created_At = x.Created_At != null ? DateTime.Parse(((DateTime)x.Created_At).ToString("dd.MM.yyyy H:mm")) : DateTime.Parse("14.01.2021 00:00"),
                Created_By = 1
            });

            _repository.SaveAll(itemsToSave, 1000);
        }

        private long GetUniqueId(string koatuu)
        {
            var item = admItems.FirstOrDefault(x => x.Koatuu == koatuu);
            if (item != null)
            {
                return item.Uid;
            }

            return NumericGidGenerator.GetIdentifier();
        }

        private AdcItem GetItemByKoatuu(string koatuu)
        {
            ClassifierItem classifierItem = new ClassifierItem();
            var comparedItem = compareItems.FirstOrDefault(x => x.Koatuu == koatuu);

            if (comparedItem != null)
            {
                classifierItem = classifierItems.FirstOrDefault(x => x.Classifier == comparedItem.Сlassifier);
            }

            return new AdcItem
            {
                CompareItem = comparedItem ?? new CompareItem(),
                ClassifierItem = classifierItem
            };
        }

        private bool HasDublicats(string townName, string district, string region, DateTime dateStart, DateTime dateEnd)
        {
            return admItems.Where(x => x.Lavel1Name == region && x.Lavel1Name == district && x.Name == townName).Count() > 1;
        }

        private string GetNameByClassifier(string classifier)
        {
            var result = classifierItems.FirstOrDefault(x => x.Classifier == classifier);
            return result != null ? result.Name : string.Empty;
        }

        private string GetTypeByClassifier(string classifier)
        {
            var result = classifierItems.FirstOrDefault(x => x.Classifier == classifier);
            return result != null ? _classifierTypes.FindType(result.Type.ToString()) : string.Empty;
        }

        private AdmLevelNames GetAmdLevels(string koatuu, bool isArchived = false)
        {
            var level1 = archiveItems.FirstOrDefault(x => x.AdmId == koatuu.Substring(0, 2) + "00000000")?.Name ?? currentItems.FirstOrDefault(x => x.Id == koatuu.Substring(0, 2) + "00000000")?.Name;
            var level2 = archiveItems.FirstOrDefault(x => x.AdmId == koatuu.Substring(0, 5) + "00000")?.Name ?? currentItems.FirstOrDefault(x => x.Id == koatuu.Substring(0, 5) + "00000")?.Name;
            var level3 = archiveItems.FirstOrDefault(x => x.AdmId == koatuu.Substring(0, 7) + "00")?.Name ?? currentItems.FirstOrDefault(x => x.Id == koatuu.Substring(0, 7) + "00")?.Name;

            return new AdmLevelNames {
                Level1 = new AdmLevel 
                {
                    Name = level1,
                    Id = level1 != null? koatuu.Substring(0, 2) + "00000000" : null,
                    Type = level1 != null ? "обл." : null
                },
                Level2 = new AdmLevel
                {
                    Name = level2,
                    Id = level2 != null ? koatuu.Substring(0, 5) + "00000" : null,
                    Type = level2 != null ? "р-н" : null
                },
                Level3 = new AdmLevel 
                { 
                    Name = level3,
                    Id = level3 != null ? koatuu.Substring(0, 7) + "00" : null,
                    Type = null
                } 
            };
        }
    }
}
