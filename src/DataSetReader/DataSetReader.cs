using System;
using System.Data;
using System.IO;
using ExcelDataReader;
using ExcelDataReader.Exceptions;
using LumenWorks.Framework.IO.Csv;

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
            try
            {
                using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(this.fileStream))
                {
                    return excelReader.AsDataSet();
                }
            }
            catch (NotSupportedException)
            {
                using (var csvReader = new CsvReader(new StreamReader(this.fileStream), true))
                {
                    var ds = new DataSet();
                    var dt = new DataTable();
                    dt.Load(csvReader);
                    ds.Tables.Add(dt);
                    return ds;
                }
            }            
        }
    }
}
