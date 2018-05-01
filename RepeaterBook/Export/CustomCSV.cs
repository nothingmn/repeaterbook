using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepeaterBook.Export
{
    public class CustomCSV : IExport
    {
        public Func<string> Header { get; set; }
        public Func<Entry, string> Body { get; set; }

        public void Export(string filename, IEnumerable<Entry> data)
        {
            ExportFolders(filename, data);
        }

        public void Export(string filename, IDictionary<double, Entry> data)
        {
            ExportFolders(filename, data);
        }

        public void ExportFolders(string filename, IEnumerable<Entry> data)
        {
            ExportFolders(filename, ChirpExporter.ConvertToDictionary(data));
        }

        public void ExportFolders(string filename, IDictionary<double, Entry> data)
        {
            var sb = new StringBuilder();
            if (Header != null)
            {
                var h = Header();
                if (!string.IsNullOrEmpty(h)) sb.Append(h);
            }

            if (Body != null)
            {
                foreach (var item in data.Values)
                {
                    var b = Body(item);
                    if (!string.IsNullOrEmpty(b)) sb.Append(b);
                }
            }
            System.IO.File.WriteAllText(filename, sb.ToString());
        }
    }
}