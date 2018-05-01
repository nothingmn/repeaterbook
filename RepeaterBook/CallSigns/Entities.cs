using System;
using System.Collections.Generic;
using System.Text;

namespace RepeaterBook.CallSigns
{
    [Flags]
    public enum Qualifications
    {
        Basic = 1,
        Morse5Wpm = 2,
        Morse12Wpm = 4,
        Advanced = 8,
        BasicWithHonours = 16,
    }

    public class AmateurRadioCallSignExport
    {
        public Dictionary<string, AmateurRadioCallSign> CallSigns { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }

    public class AmateurRadioCallSign
    {
        public string CallSign { get; set; }
        public string GivenNames { get; set; }
        public string SurName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public Qualifications Qualifications { get; set; }
        public string ClubName { get; set; }
        public string SecondClubName { get; set; }
        public string ClubAddress { get; set; }
        public string ClubCity { get; set; }
        public string ClubProvince { get; set; }
        public string ClubPostalCode { get; set; }

        public override string ToString()
        {
            return $"{CallSign} {GivenNames} {SurName} {Address} {City} {Province} {PostalCode} {Qualifications}";
        }
    }
}