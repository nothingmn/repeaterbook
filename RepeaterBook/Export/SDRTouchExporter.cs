using System.Collections.Generic;
using System.Text;

namespace RepeaterBook.Export
{
    public class SDRTouchExporter : IExport
    {
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
            ExportFolders(filename, data);
        }

        public void ExportFolders(string filename, IDictionary<double, Entry> data)
        {
            var index = 0;
            var sb = new StringBuilder();
            sb.Append(
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?><sdr_presets version=\"1\"><category id=\"99\" name=\"Imported\">");
            foreach (var key in data.Keys)
            {
                var entry = data[key];
                sb.Append(
                    $"<preset id=\"{index}\" name=\"{entry.Call}\" freq=\"{ (entry.RX * 1000 * 1000).ToString("#########")}\" centfreq=\"{(entry.RX * 1000 * 1000).ToString("#########")}\" offset=\"0\" order=\"{index}\" filter=\"70000\" dem=\"1\"/>");
                index++;
            }

            sb.Append("</category></sdr_presets>");

            System.IO.File.WriteAllText(filename, sb.ToString(), Encoding.ASCII);
        }
    }
}