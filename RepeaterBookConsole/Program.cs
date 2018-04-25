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
            var coordinates = new Coordinates(49.8875989, -119.4910348);
            var filterByLocation = manager.FilterByLocation(coordinates, 20, UnitOfLength.Kilometers);

            var exporter = new KMLExport();
            exporter.ExportFolders(@"C:\Users\rchartier\Desktop\kelowna.kml", filterByLocation);

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}