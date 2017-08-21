using System;
using System.Configuration;
using System.Data;
using System.IO;
using NUnit.Framework;

namespace DataSetReader.Tests
{
    [TestFixture]
    public class DataSetReaderTests
    {
        [TestCase]
        public void WhenStreamIsNull_ThrowsArgumentNullException()
        {
            FileStream testStream = null;

            Assert.Throws<ArgumentNullException>(() => new DataSetReader(testStream));
        }

        [TestCase]
        public void WhenStreamIsAnOpenXmlExcelFileAndNotEmpty_AsDataSet_ReturnsDataFromStreamAsADataSet()
        {
            AssertDataSetIsNotEmpty("keypad.xlsx");
        }

        private void AssertDataSetIsNotEmpty(string fileName)
        {
            DataSet result = null;
            string path = GetFilePathFromResources(fileName);
            using (FileStream testStream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                var reader = new DataSetReader(testStream);
                result = reader.AsDataSet();
            }

            Assert.IsNotNull(result);
            Assert.Greater(result.Tables.Count, 0);
        }

        private string GetFilePathFromResources(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["TestResourcesPath"], fileName);
        }

        [TestCase]
        public void WhenStreamIsABinaryExcelFileAndNotEmpty_AsDataSet_ReturnsDataFromStreamAsADataSet()
        {
            AssertDataSetIsNotEmpty("keypad.xls");
        }

        [TestCase]
        public void WhenStreamIsACsvFileAndNotEmpty_AsDataSet_ReturnsDataFromStreamAsADataSet()
        {
            AssertDataSetIsNotEmpty("keypad.csv");
        }

        [TestCase]
        public void WhenStreamIsAnExcelFileWithARandomExtensionAndNotEmpty_AsDataSet_ReturnsDataFromStreamAsADataSet()
        {
            AssertDataSetIsNotEmpty("keypad.xlsx.foo");
        }

        //[TestCase]
        public void SmokeTest()
        {
            string path = @"\\UDSERVER\Development\UDMatrix Imports\_____Matrix Files - All Clients\British Gas\2017-08-01\BG Gas G36.xlsx";
            AssertDataSetIsNotEmpty(path);
        }
    }
}
