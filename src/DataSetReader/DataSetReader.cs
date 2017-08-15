using System;
using System.Data;
using System.IO;

namespace DataSetReader
{
    public class DataSetReader
    {
        public DataSetReader(Stream fileStream)
        {
            if (fileStream == null)
                throw new ArgumentNullException("fileStream");
        }

        public DataSet AsDataSet()
        {
            return new DataSet();
        }
    }
}
