using System;
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
            Stream testStream = null;

            Assert.Throws<ArgumentNullException>(() => new DataSetReader(testStream));
        }
    }
}
