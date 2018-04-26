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
            var coordinates = new Coordinates(53.582710, -123.407596);
            var filterByLocation = manager.FilterByLocation(coordinates, 100, UnitOfLength.Kilometers);

            var chirp = new ChirpExporter();
            chirp.ExportFolders(@"C:\Users\rchartier\Desktop\fingerlake.csv", filterByLocation);

            var kml = new KMLExporter();
            kml.ExportFolders(@"C:\Users\rchartier\Desktop\fingerlake.kml", filterByLocation);

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}