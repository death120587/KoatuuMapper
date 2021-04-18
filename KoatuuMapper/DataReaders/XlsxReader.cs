using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KoatuuMapper.DataReaders
{
    public class XlsxReader
    {
        public List<U> ReadData<U>(string path, string workSheetName, int startRowIndex, Func<ExcelRange, int, U> dataDeserialization) where U : new()
        {
            List<U> data = new List<U>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var reader = new StreamReader(path))
            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Load(reader.BaseStream);

                var workSheet = excelPackage.Workbook.Worksheets[workSheetName];
                int lastRowIndex = workSheet.Dimension.End.Row;
                for (int i = startRowIndex; i <= lastRowIndex; i++)
                {
                    data.Add(dataDeserialization(workSheet.Cells, i));
                }
            }

            return data;
        }
    }
}
