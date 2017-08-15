using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetReader
{
    public class DataSetReader
    {
        public DataSetReader(Stream fileStream)
        {
            if (fileStream == null)
                throw new ArgumentNullException("fileStream");
        }
    }
}
