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
                using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(this.fileStream))
                    dataSet = excelReader.AsDataSet();
            }
            catch (NotSupportedException)
            {
                using (var csvReader = new CsvReader(new StreamReader(this.fileStream), false))
                {
                    var dt = new DataTable();

                    int fieldCount = csvReader.FieldCount;
                    dt = AddColumns(dt, fieldCount);
                    dt = LoadData(dt, csvReader);

                    dataSet.Tables.Add(dt);
                }
            }

            return dataSet;
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
