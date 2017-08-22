using System;
using System.Data;
using System.IO;
using ExcelDataReader;
using LumenWorks.Framework.IO.Csv;

namespace DataSetReader
{
    public class DataSetReader
    {
        private readonly FileStream fileStream = null;

        public DataSetReader(FileStream fileStream)
        {
            this.fileStream = fileStream ?? throw new ArgumentNullException("fileStream");
        }

        public DataSet AsDataSet()
        {
            var dataSet = new DataSet();

            try
            {
                switch (GetFileExtension())
                {
                    case ".xls":
                    case ".xlb":
                    case ".xlm":
                    case ".xlsx":
                    case ".xlsb":
                    case ".xlsm":
                    default:
                        dataSet = ReadExcelToDataSet();
                        break;
                    case ".csv":
                    case ".txt":
                        dataSet = ReadCsvToDataSet();
                        break;
                }
                
            }
            catch (NotSupportedException)
            {
                dataSet = ReadCsvToDataSet();
            }

            return dataSet;
        }

        private string GetFileExtension()
        {
            return Path.GetExtension(this.fileStream.Name);
        }

        private DataSet ReadExcelToDataSet()
        {
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(this.fileStream))
                return excelReader.AsDataSet();
        }

        private DataSet ReadCsvToDataSet()
        {
            using (var streamReader = new StreamReader(this.fileStream))
            using (var csvReader = new CsvReader(streamReader, false))
            {
                var dataSet = new DataSet();
                DataTable table = CreateDataTableFromCsv(csvReader);

                dataSet.Tables.Add(table);
                return dataSet;
            }
        }

        private DataTable CreateDataTableFromCsv(CsvReader dataReader)
        {
            var table = new DataTable();

            int fieldCount = dataReader.FieldCount;
            table = AddColumns(table, fieldCount);
            table = LoadData(table, dataReader);

            return table;
        }

        private DataTable AddColumns(DataTable table, int count)
        {
            for (int i = 0; i < count; i++)
                table.Columns.Add($"Column{i}", typeof(string));

            return table;
        }

        private DataTable LoadData(DataTable table, CsvReader dataReader)
        {
            while (dataReader.ReadNextRecord())
                table = AddRow(table, dataReader);

            return table;
        }

        private DataTable AddRow(DataTable table, CsvReader dataReader)
        {
            DataRow newRow = CreateRowWithData(table, dataReader);
            table.Rows.Add(newRow);

            return table;
        }

        private DataRow CreateRowWithData(DataTable table, CsvReader dataReader)
        {
            DataRow newRow = table.NewRow();
            newRow = AddDataToRow(newRow, dataReader, table.Columns.Count);

            return newRow;
        }

        private DataRow AddDataToRow(DataRow row, CsvReader dataReader, int fieldCount)
        {
            for (int i = 0; i < fieldCount; i++)
                row[i] = dataReader[i];

            return row;
        }
    }
}
