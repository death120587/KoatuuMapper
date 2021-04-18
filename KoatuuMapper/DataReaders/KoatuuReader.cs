using OfficeOpenXml;
using System.Collections.Generic;
using KoatuuMapper.Mapping;

namespace KoatuuMapper.DataReaders
{
    public class KoatuuReader
    {
        private readonly XlsxReader _reader;
        public KoatuuReader(XlsxReader reader)
        {
            _reader = reader;
        }

        public List<ClassifierItem> Read(string file)
        {
            return _reader.ReadData(file, "Кодифікатор", 4, DesirializeData);
        }

        private ClassifierItem DesirializeData(ExcelRange data, int rowIndex)
        {
            var type = (string)data[rowIndex, 6].Value;

            return new ClassifierItem
            {
                Name = (string)data[rowIndex, 7].Value,
                Lavel1 = (string)data[rowIndex, 1].Value,
                Lavel2 = (string)data[rowIndex, 2].Value,
                Lavel3 = (string)data[rowIndex, 3].Value,
                Lavel4 = (string)data[rowIndex, 4].Value,
                Lavel5 = (string)data[rowIndex, 5].Value,
                Type = !string.IsNullOrEmpty(type) ? type[0] : ' ',
                Classifier = GetClassifier(data, 5, rowIndex)
            };
        }

        private string GetClassifier(ExcelRange excelRange, int startCellIndex, int rowIndex)
        {
            var value = string.Empty;

            if (startCellIndex > 0) 
            {
                value = (string)excelRange[rowIndex, startCellIndex].Value;
                if (string.IsNullOrEmpty(value))
                {
                    return GetClassifier(excelRange, startCellIndex - 1, rowIndex);
                }
            }

            return value;
        }
    }
}
