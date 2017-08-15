using System;
using System.Data;
using System.IO;
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
        public void WhenStreamIsNotEmpty_AsDataSet_ReturnsDataFromStreamAsADataSet()
        {
            DataSet result = null;
            using (var testStream = new MemoryStream(Resources.ExcelKeypad))
            {
                var reader = new DataSetReader(testStream);
                result = reader.AsDataSet();
            }

            Assert.IsNotNull(result);
            Assert.Greater(result.Tables.Count, 0);
        }
    }
}
