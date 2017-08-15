using System;
using System.Data;
using System.IO;
using System.Text;
using DataSetReader.Tests.Properties;
using NUnit.Framework;

namespace DataSetReader.Tests
{
    [TestFixture]
    public class DataSetReaderTests
    {
        [TestCase]
        public void WhenStreamIsNull_ThrowsArgumentNullException()
        {
            Stream testStream = null;

            Assert.Throws<ArgumentNullException>(() => new DataSetReader(testStream));
        }

        [TestCase]
        public void WhenStreamIsAnOpenXmlExcelFileAndNotEmpty_AsDataSet_ReturnsDataFromStreamAsADataSet()
        {
            DataSet result = null;
            using (var testStream = new MemoryStream(Resources.OpenXmlExcelKeypad))
            {
                var reader = new DataSetReader(testStream);
                result = reader.AsDataSet();
            }

            Assert.IsNotNull(result);
            Assert.Greater(result.Tables.Count, 0);
        }

        [TestCase]
        public void WhenStreamIsABinaryExcelFileAndNotEmpty_AsDataSet_ReturnsDataFromStreamAsADataSet()
        {
            DataSet result = null;
            using (var testStream = new MemoryStream(Resources.BinaryExcelKeypad))
            {
                var reader = new DataSetReader(testStream);
                result = reader.AsDataSet();
            }

            Assert.IsNotNull(result);
            Assert.Greater(result.Tables.Count, 0);
        }

        [TestCase]
        public void WhenStreamIsACsvFileAndNotEmpty_AsDataSet_ReturnsDataFromStreamAsADataSet()
        {
            DataSet result = null;
            using (var testStream = new MemoryStream(Encoding.UTF8.GetBytes(Resources.CsvKeypad)))
            {
                var reader = new DataSetReader(testStream);
                result = reader.AsDataSet();
            }

            Assert.IsNotNull(result);
            Assert.Greater(result.Tables.Count, 0);
        }
    }
}
