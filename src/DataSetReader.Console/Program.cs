using System;
using System.Configuration;
using System.Data;
using System.IO;
using AppConsole = System.Console;

namespace DataSetReader.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"\\UDSERVER\Development\UDMatrix Imports\_____Matrix Files - All Clients\Total Gas and Power\2017-08-16\UD Group\TPI Matrix 16.08.17.xlsx";
            try
            {
                DataSet result = ConvertToDataSet(path);
                AppConsole.WriteLine("Success");
            }
            catch (Exception ex)
            {
                PrintExceptionMessage(ex);
            }

            AppConsole.WriteLine("\nPress any key to continue...");
            AppConsole.ReadKey();
        }

        private static DataSet ConvertToDataSet(string path)
        {
            using (FileStream testStream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                var reader = new DataSetReader(testStream);
                return reader.AsDataSet();
            }
        }

        private static void PrintExceptionMessage(Exception ex)
        {
            AppConsole.ForegroundColor = ConsoleColor.Red;
            AppConsole.WriteLine(ex.Message);

            if (ex.InnerException != null)
                PrintExceptionMessage(ex.InnerException);

            AppConsole.ForegroundColor = ConsoleColor.White;
        }
    }
}
