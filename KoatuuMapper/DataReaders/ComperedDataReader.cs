using System.IO;
using OfficeOpenXml;
using KoatuuMapper.Mapping;
using System.Collections.Generic;

namespace KoatuuMapper.DataReaders
{
    public class ComperedDataReader
    {
        private readonly XlsxReader _reader;
        public ComperedDataReader(XlsxReader reader)
        {
            _reader = reader;
        }

        public List<CompareItem> Read(string file)
        {
            return _reader.ReadData(file, "Лист1", 4, DesirializeData);
        }

        private CompareItem DesirializeData(ExcelRange data, int rowIndex)
        {
            return new CompareItem
            {
                Сlassifier = (string)data[rowIndex, 1].Value,
                Koatuu = (string)data[rowIndex, 2].Value,
                Type = !string.IsNullOrEmpty((string)data[rowIndex, 3].Value) ? ((string)data[rowIndex, 3].Value).ToCharArray()[0] : ' ',
                Name = (string)data[rowIndex, 4].Value,
            };
        }
    }
}
