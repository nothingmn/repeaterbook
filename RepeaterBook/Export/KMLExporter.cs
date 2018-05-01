using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreLinq;

namespace RepeaterBook.Export
{
    public class KMLExporter : IExport
    {
        public void ExportFolders(string filename, IDictionary<double, Entry> data)
        {
            string placemarkTemplate = "<Placemark><name>{name}</name><description>{description}</description><Point><coordinates>{lng},{lat},0</coordinates></Point></Placemark>";
            string fileTemplate = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><kml xmlns=\"http://www.opengis.net/kml/2.2\">  <Document>{Placemarks}</Document></kml>";
            if (data == null || !data.Any()) return;
            var sb = new StringBuilder();

            foreach (var country in from e in data.Values.DistinctBy(e => e.Country) orderby e.Country select e.Country)
            {
                sb.Append("<Folder><name>{country}</name>".Replace("{country}", country));
                var countryEntries = from e in data.Values
                                     where e.Country.Equals(country, StringComparison.InvariantCultureIgnoreCase)
                                     select e;

                foreach (var entry in from p in countryEntries.DistinctBy(p => p.Province) orderby p.Province select p)
                {
                    sb.Append("<Folder><name>{province}</name>".Replace("{province}", entry.Province));
                    foreach (var item in from v in data.Values
                                         where v.Country.Equals(country, StringComparison.InvariantCultureIgnoreCase) &&
                                               v.Province.Equals(entry.Province, StringComparison.InvariantCultureIgnoreCase)
                                         select v)
                    {
                        var txt = placemarkTemplate;
                        txt = txt.Replace("{name}", item.Call);

                        var desc = "";
                        foreach (var p in item.GetType().GetProperties())
                        {
                            if (p.Name == "CallSign" || p.Name == "Coordinates") continue;
                            var v = p.GetValue(item);
                            if (v != null)
                            {
                                var value = v.ToString();
                                if (!string.IsNullOrEmpty(value))
                                {
                                    value = value.Replace("&", "&amp;");
                                    value = value.Replace("<", "&lt;");
                                    value = value.Replace(">", "&gt;");
                                    desc += $"{p.Name}={value}\n";
                                }
                            }
                        }

                        if (item.CallSign != null)
                        {
                            desc = desc + "Call Sign Information:\n";
                            desc = desc + "Name:" + item.CallSign.GivenNames + " " + item.CallSign.SurName + "\n";
                            desc = desc + "Qualifications:" + item.CallSign.Qualifications + "\n";
                            desc = desc + "Address:" + item.CallSign.Address + "\n";
                            desc = desc + "City:" + item.CallSign.City + "\n";
                            desc = desc + "Province:" + item.CallSign.Province + "\n";
                            desc = desc + "Postal Code:" + item.CallSign.PostalCode + "\n";
                            desc = desc + "Club Information:\n";
                            desc = desc + "Name:" + item.CallSign.ClubName + " " + item.CallSign.SecondClubName + "\n";
                            desc = desc + "Address:" + item.CallSign.ClubAddress + "\n";
                            desc = desc + "City:" + item.CallSign.ClubCity + "\n";
                            desc = desc + "Province:" + item.CallSign.ClubProvince + "\n";
                            desc = desc + "Postal Code:" + item.CallSign.ClubPostalCode + "\n";
                        }

                        txt = txt.Replace("{description}", desc);
                        txt = txt.Replace("{lat}", item.Lat.ToString());
                        txt = txt.Replace("{lng}", item.Lng.ToString());

                        sb.Append(txt);
                    }

                    sb.Append("</Folder>");
                }
                sb.Append("</Folder>");
            }

            var full = fileTemplate;
            full = full.Replace("{Placemarks}", sb.ToString());
            System.IO.File.WriteAllText(filename, full);
        }

        public void Export(string filename, IEnumerable<Entry> data)
        {
            Export(filename, ChirpExporter.ConvertToDictionary(data));
        }

        public void ExportFolders(string filename, IEnumerable<Entry> data)
        {
            ExportFolders(filename, ChirpExporter.ConvertToDictionary(data));
        }

        public void Export(string filename, IDictionary<double, Entry> data)
        {
            string placemarkTemplate = "<Placemark><name>{name}</name><description>{description}</description><Point><coordinates>{lng},{lat},0</coordinates></Point></Placemark>";
            string fileTemplate = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><kml xmlns=\"http://www.opengis.net/kml/2.2\">  <Document>{Placemarks}</Document></kml>";

            if (data == null || !data.Any()) return;
            var sb = new StringBuilder();
            foreach (var entry in data)
            {
                var item = placemarkTemplate;
                item = item.Replace("{name}", entry.Value.Call);

                var desc = "";
                foreach (var p in entry.Value.GetType().GetProperties())
                {
                    var v = p.GetValue(entry.Value);
                    if (v != null)
                    {
                        var value = v.ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            value = value.Replace("&", "&amp;");
                            value = value.Replace("<", "&lt;");
                            value = value.Replace(">", "&gt;");
                            desc += $"{p.Name}={value}\n";
                        }
                    }
                }

                item = item.Replace("{description}", desc);
                item = item.Replace("{lat}", entry.Value.Lat.ToString());
                item = item.Replace("{lng}", entry.Value.Lng.ToString());
                sb.Append(item);
            }

            var full = fileTemplate;
            full = full.Replace("{Placemarks}", sb.ToString());
            System.IO.File.WriteAllText(filename, full);
        }
    }
}