using System;
using System.Collections.Generic;
using System.Linq;
using RepeaterBook;
using RepeaterBook.Export;

namespace RepeaterBookConsole
{
    internal class Program
    {
        private static DataManager DataManager = new DataManager();

        private static void Main(string[] args)
        {
            DataManager.Initialize();

            if (args != null && args.Any() && args[0] == "custom")
            {
                var coordinates = new Coordinates(49.2634752, -122.9738316);
                var filterByLocation = DataManager.FilterByLocation(coordinates, 50, UnitOfLength.Kilometers);

                //var filterByLocation = DataManager.FindAll(entry => entry.Province.Equals("BC", StringComparison.InvariantCultureIgnoreCase) || entry.Province.Equals("AB", StringComparison.InvariantCultureIgnoreCase));
                var kml = new KMLExporter();
                kml.ExportFolders(@"C:\Users\rchartier\Desktop\exported_kml.kml", filterByLocation);
                var chrip = new ChirpExporter();
                chrip.ExportFolders(@"C:\Users\rchartier\Desktop\exported_chirp.csv", filterByLocation);

                //ExportCustomCSV(filterByLocation);

                //ExportChirp(filterByLocation);

                //ExportKML(filterByLocation);

                Console.WriteLine("Done");
                Console.ReadLine();
            }
            else
            {
                ExportByLocation();
            }
        }

        private static void ExportKML(IEnumerable<Entry> filterByLocation)
        {
            var kml = new KMLExporter();
            kml.ExportFolders(@"C:\Users\rchartier\Desktop\exported_kml.kml", filterByLocation);
        }

        private static void ExportChirp(IEnumerable<Entry> filterByLocation)
        {
            var chirp = new ChirpExporter();
            chirp.ExportFolders(@"C:\Users\rchartier\Desktop\exported_chrip.csv", filterByLocation);
        }

        private static void ExportCustomCSV(IEnumerable<Entry> entries)
        {
            var customCsv = new CustomCSV()
            {
                Header = () => "Name,Frequency\r\n",
                Body = entry => $"{entry.Call},{entry.TX}\r\n",
            };
            customCsv.Export(@"C:\Users\rchartier\Desktop\exported_custom.csv", entries);
        }

        private static double GetDoubleFromUser(string title)
        {
            while (true)
            {
                Console.WriteLine(title);
                var latInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(latInput))
                {
                    double input = 0;
                    if (double.TryParse(latInput, out input))
                    {
                        if (input != 0)
                        {
                            return input;
                        }
                    }
                }
                Console.WriteLine("Invalid input.  Try again.");
            }
        }

        private static string GetStringFromUser(string title, string[] allowedInputs = null)
        {
            var input = "";
            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine(title);
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Invalid input, Try again.");
                    continue;
                }

                if (allowedInputs != null)
                {
                    if (!allowedInputs.Contains(input))
                    {
                        Console.WriteLine("Invalid input, Try again.");
                        input = "";
                        continue;
                    }
                }
            }

            return input;
        }

        private static void ExportByLocation()
        {
            var lat = GetDoubleFromUser("Latitude?");
            var lon = GetDoubleFromUser("Longitude?");
            var distance = GetDoubleFromUser("Radius (in kilometers)?");
            var fileName = GetStringFromUser("Filename?");
            var format = GetStringFromUser("Format (KML or CHIRP)?");

            var coordinates = new Coordinates(lat, lon);

            Console.WriteLine($"Filtering data by: Latitude:{lat}, Longitude:{lon} with a radius of {distance}.");

            var filterByLocation = DataManager.FilterByLocation(coordinates, distance, UnitOfLength.Kilometers);

            Console.WriteLine($"Filtering data complete.  Found:{filterByLocation.Count} locations");

            if (filterByLocation.Count <= 0)
            {
                Console.WriteLine("Nothing to export.  Aborting...");
                return;
            }

            if (format.Equals("KML", StringComparison.InvariantCultureIgnoreCase))
            {
                var kml = new KMLExporter();
                kml.ExportFolders(fileName, filterByLocation);
            }
            else
            {
                var chirp = new ChirpExporter();
                chirp.ExportFolders(fileName, filterByLocation);
            }

            Console.WriteLine($"Done writing data to the file:{fileName}, exiting.");
        }
    }
}