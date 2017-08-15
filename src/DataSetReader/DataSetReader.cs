using System;
using System.Data;
using System.IO;
using ExcelDataReader;

namespace DataSetReader
{
    public class DataSetReader
    {
        private readonly Stream fileStream = null;

        public DataSetReader(Stream fileStream)
        {
            this.fileStream = fileStream ?? throw new ArgumentNullException("fileStream");
        }

        public DataSet AsDataSet()
        {
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(this.fileStream))
            {
                return excelReader.AsDataSet();
            }
        }
    }
}
