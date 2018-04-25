using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepeaterBook.Export
{
    public class ChirpExporter : IExport
    {
        public static Dictionary<double, Entry> ConvertToDictionary(IEnumerable<Entry> data)
        {
            int index = 0;
            var dictionary = new Dictionary<double, Entry>();
            foreach (var i in data)
            {
                dictionary.Add(index, i);
                index++;
            }

            return dictionary;
        }

        public void Export(string filename, IEnumerable<Entry> data)
        {
            Export(filename, ConvertToDictionary(data));
        }

        public void ExportFolders(string filename, IEnumerable<Entry> data)
        {
            ExportFolders(filename, ConvertToDictionary(data));
        }

        public void Export(string filename, IDictionary<double, Entry> data)
        {
            ExportFolders(filename, data);
        }

        public void ExportFolders(string filename, IDictionary<double, Entry> data)
        {
            var chirpEntries = new List<ChirpEntry>();
            var index = 0;
            foreach (var key in data.Keys)
            {
                var entry = data[key];
                var o = entry.Offset;
                Duplex d = Duplex.None;
                decimal off = 0;
                decimal rx = entry.RX;
                decimal tx = entry.TX;
                if (rx != tx)
                {
                    d = Duplex.Positive;
                    if (rx > tx) d = Duplex.Negative;
                    off = Math.Abs(rx - tx);
                }

                #region might consider later

                //if (!string.IsNullOrEmpty(o))
                //{
                //    if (o.Contains("-"))
                //    {
                //        sign = "-";
                //    }
                //    else
                //    {
                //        sign = "+";
                //    }
                //    var basic = o.Trim().Replace(sign, "").Trim();
                //    var left = basic.Substring(0, o.IndexOf(" ")).Trim();
                //    var right = basic.Substring(o.IndexOf(" ")).Trim();
                //    if (!string.IsNullOrEmpty(left))
                //    {
                //        if (decimal.TryParse(left, out off))
                //        {
                //            if (right.ToLower() == "khz")
                //            {
                //                off += 1000;
                //            }
                //        }
                //    }
                //}

                #endregion might consider later

                var c = new ChirpEntry()
                {
                    Location = index,
                    Name = entry.Call,
                    Frequency = entry.RX,
                    Mode = entry.Band,
                    Offset = off,
                    Duplex = d,
                    rToneFreq = entry.CTCSS,
                    Tone = ToneMode.Tone
                };

                if (!string.IsNullOrEmpty(entry.Location))
                {
                    c.Comment = c.Comment + entry.Location + ", ";
                }
                if (!string.IsNullOrEmpty(entry.Region))
                {
                    c.Comment = c.Comment + entry.Region + ", ";
                }
                if (!string.IsNullOrEmpty(entry.Province))
                {
                    c.Comment = c.Comment + entry.Province + ", ";
                }
                if (!string.IsNullOrEmpty(entry.County))
                {
                    c.Comment = c.Comment + entry.County + ", ";
                }
                if (!string.IsNullOrEmpty(entry.Country))
                {
                    c.Comment = c.Comment + entry.Country + ", ";
                }
                if (!string.IsNullOrEmpty(entry.NotesAccess))
                {
                    c.Comment = c.Comment + "Access: " + entry.NotesAccess + ", ";
                }
                if (!string.IsNullOrEmpty(entry.NotesLinks))
                {
                    c.Comment = c.Comment + "Links: " + entry.NotesLinks + ", ";
                }
                if (!string.IsNullOrEmpty(entry.NotesFeatures))
                {
                    c.Comment = c.Comment + "Features: " + entry.NotesFeatures + ", ";
                }
                c.Comment = c.Comment + " (" + key + ")";

                c.Comment = c.Comment.Trim();

                chirpEntries.Add(c);
                index++;
            }

            var sb = new StringBuilder();
            sb.Append(
                "Location,Name,Frequency,Duplex,Offset,Tone,cToneFreq,DtcsCode,DtcsPolarity,Mode,TStep,Skip,Comment\r\n");
            foreach (var ce in chirpEntries)
            {
                var dup = "";
                if (ce.Duplex == Duplex.Negative) dup = "-";
                if (ce.Duplex == Duplex.Positive) dup = "+";

                var rTone = "";
                if (ce.rToneFreq != 0) rTone = ce.rToneFreq.ToString("000.0");

                var tone = "";
                if (!string.IsNullOrEmpty(rTone) && ce.Tone != ToneMode.None) tone = ce.Tone.ToString();

                var line =
                    $"{ce.Location},{ce.Name},{ce.Frequency.ToString("###.000000")},{dup},{ce.Offset.ToString("##0.000000")},{tone},{rTone},023,NN,{ce.Mode},5.00,,\"{ce.Comment}\"\r\n";
                sb.Append(line);
                Console.WriteLine(line);
            }
            System.IO.File.WriteAllText(filename, sb.ToString(), Encoding.ASCII);
        }
    }

    public enum ToneMode
    {
        None, Tone, TSQL, DTCS, DTCSR, TSQLR, Cross
    }

    public enum Duplex
    {
        None, Positive, Negative
    }

    public enum DtsPolarity
    {
        None,
        NN,
        RN,
        NR,
        RR
    }

    public class ChirpEntry
    {
        //Location,Name,Frequency,Duplex,Offset,Tone,rToneFreq,cToneFreq,DtcsCode,DtcsPolarity,
        //Mode,TStep,Skip,Comment,URCALL,RPT1CALL,RPT2CALL,DVCODE
        public int Location { get; set; }

        public string Name { get; set; }

        public decimal Frequency { get; set; }
        public Duplex Duplex { get; set; }  //+ or -
        public decimal Offset { get; set; }
        public ToneMode Tone { get; set; } //

        public decimal rToneFreq { get; set; }
        public decimal cToneFreq { get; set; }
        public decimal DtcsCode { get; set; }
        public DtsPolarity DtcsPolarity { get; set; }
        public Band Mode { get; set; }
        public decimal TStep { get; set; }
        public bool Skip { get; set; }
        public string Comment { get; set; }
        public decimal URCall { get; set; }
        public decimal RPT1Call { get; set; }
        public decimal RPT2Call { get; set; }
        public decimal DVCode { get; set; }
    }
}