using System;
using RepeaterBook;
using RepeaterBook.Export;

namespace RepeaterBookConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var manager = new DataManager();
            manager.Initialize();
            var coordinates = new Coordinates(49.2633935, -122.9734937);
            var filterByLocation = manager.FilterByLocation(coordinates, 50, UnitOfLength.Kilometers);

            var exporter = new ChirpExporter();
            exporter.ExportFolders(@"C:\Users\rchartier\Desktop\burnaby.csv", filterByLocation);

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}