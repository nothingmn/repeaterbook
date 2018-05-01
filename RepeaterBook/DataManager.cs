using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RepeaterBook.CallSigns;

namespace RepeaterBook
{
    public class DataManager
    {
        private RepeaterBookData RepeaterBookData;

        private AmateurRadioCallSignExport CanadianCallSigns;

        public void Initialize()
        {
            var asm = typeof(DataManager).Assembly;

            var resource1 = (from m in asm.GetManifestResourceNames()
                             where m.Contains("callsigns.canada")
                             select m)?.FirstOrDefault();

            using (var stm = asm.GetManifestResourceStream(resource1))
            {
                byte[] buffer = new byte[stm.Length];
                stm.Read(buffer, 0, buffer.Length);
                CanadianCallSigns = JsonConvert.DeserializeObject<AmateurRadioCallSignExport>(System.Text.Encoding.UTF8.GetString(buffer));
            }

            var resource = (from m in asm.GetManifestResourceNames()
                            where m.Contains("repeaterbookworld")
                            select m)?.FirstOrDefault();

            var bandManager = new BandManager();
            using (var stm = asm.GetManifestResourceStream(resource))
            {
                byte[] buffer = new byte[stm.Length];
                stm.Read(buffer, 0, buffer.Length);
                RepeaterBookData = JsonConvert.DeserializeObject<RepeaterBookData>(System.Text.Encoding.UTF8.GetString(buffer));
                foreach (var entry in RepeaterBookData?.Entries)
                {
                    entry.Coordinates = new Coordinates(entry.Lat, entry.Lng);
                    var hz = ((double)entry.TX) * 1000 * 1000;
                    entry.Band = bandManager.BandForFrequency(hz);
                    entry.WaveLength = bandManager.WaveLengthForFrequencyInMeters(hz);

                    if (!string.IsNullOrEmpty(entry.Call) && CanadianCallSigns.CallSigns.ContainsKey(entry.Call))
                    {
                        entry.CallSign = CanadianCallSigns.CallSigns[entry.Call];
                    }
                }
            }
        }

        public int? Count
        {
            get { return RepeaterBookData?.Entries?.Count; }
        }

        public IEnumerable<Entry> FindAll(Predicate<Entry> searchPredicate)
        {
            return RepeaterBookData?.Entries.FindAll(searchPredicate);
        }

        public Entry Find(Predicate<Entry> searchPredicate)
        {
            return RepeaterBookData?.Entries.Find(searchPredicate);
        }

        public SortedDictionary<double, Entry> FilterByLocation(Coordinates center, double distance = 5, UnitOfLength length = null)
        {
            if (length == null) length = UnitOfLength.Kilometers;

            SortedDictionary<double, Entry> result = new SortedDictionary<double, Entry>();

            foreach (var entry in RepeaterBookData.Entries)
            {
                var d = entry.Coordinates.DistanceTo(center, length);
                if (d <= distance)
                {
                    while (result.ContainsKey(d))
                    {
                        //add an arbitrarily tiny amount, just to keep our keys unique
                        d = d + 0.0000000001;
                    }

                    result.Add(d, entry);
                }
            }

            return result;
        }
    }
}