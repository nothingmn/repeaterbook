namespace RepeaterBook
{
    public class Entry
    {
        public int Id { get; set; }
        public string Call { get; set; }
        public Band Band { get; set; }
        public decimal RX { get; set; }
        public decimal TX { get; set; }
        public string Offset { get; set; }
        public decimal Services { get; set; }
        public decimal Access { get; set; }
        public decimal CTCSS { get; set; }
        public string DCS { get; set; }
        public decimal IRLP_node { get; set; }
        public decimal EchoLink_node { get; set; }
        public string DStar_node { get; set; }
        public string AllStar_node { get; set; }
        public string Location { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Province { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Country { get; set; }
        public string Url { get; set; }
        public string NotesFeatures { get; set; }
        public string NotesAccess { get; set; }
        public string Updated { get; set; }
        public string By { get; set; }
        public string RBID { get; set; }
        public decimal OpStatus { get; set; }
        public string DMR_text { get; set; }
        public string NotesLinks { get; set; }
        public string DTMF { get; set; }
        public string Locator { get; set; }
        public string Region { get; set; }
        public string ASL { get; set; }
        public string Power { get; set; }

        public Coordinates Coordinates { get; set; }
    }
}