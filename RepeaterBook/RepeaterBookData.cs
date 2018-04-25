using System;
using System.Collections.Generic;

namespace RepeaterBook
{
    public class RepeaterBookData
    {
        public List<Entry> Entries { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string Hash { get; set; }
    }
}