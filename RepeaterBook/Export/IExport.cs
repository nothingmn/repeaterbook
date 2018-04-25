using System.Collections.Generic;

namespace RepeaterBook.Export
{
    public interface IExport
    {
        void Export(string filename, IEnumerable<Entry> data);

        void ExportFolders(string filename, IEnumerable<Entry> data);

        void Export(string filename, IDictionary<double, Entry> data);

        void ExportFolders(string filename, IDictionary<double, Entry> data);
    }
}