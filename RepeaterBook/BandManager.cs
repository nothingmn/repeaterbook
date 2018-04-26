using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepeaterBook
{
    public class BandManager
    {
        private static IList<Band> Bands { get; set; }

        static BandManager()
        {
            Bands = new List<Band>()
            {
                new Band()
                {
                    Name = "Extremely Low Frequency",
                    Abbreviation = "ELF",
                    BandId = 1,
                    LowFrequency = 3,
                    HighFrequency = 30,
                    LowWaveLength = 99930.8 * 1000,
                    HighWaveLength = 9993.1 * 1000,
                    ExampleUses = "Communication with submarines",
                    BandPlan = BandPlan.ITU
                },
                new Band()
                {
                    Name = "Super Low Frequency",
                    Abbreviation = "SLF",
                    BandId = 2,
                    LowFrequency = 30,
                    HighFrequency = 300,
                    LowWaveLength = 9993.1 * 1000,
                    HighWaveLength = 999.3 * 1000,
                    ExampleUses = "Communication with submarines",
                    BandPlan = BandPlan.ITU
                },
                new Band()
                {
                    Name = "Ultra Low Frequency",
                    Abbreviation = "ULF",
                    BandId = 3,
                    LowFrequency = 300,
                    HighFrequency = 3000,
                    LowWaveLength = 999.3 * 1000,
                    HighWaveLength = 99.9 * 1000,
                    ExampleUses = "Submarine communication, communication within mines",
                    BandPlan = BandPlan.ITU
                },
                new Band()
                {
                    Name = "Very Low Frequency",
                    Abbreviation = "VLF",
                    BandId = 4,
                    LowFrequency = 3 * 1000,
                    HighFrequency = 30 * 1000,
                    LowWaveLength = 99.9 * 1000,
                    HighWaveLength = 10 * 1000,
                    ExampleUses = "Navigation, time signals, submarine communication, wireless heart rate monitors, geophysics",
                    BandPlan = BandPlan.ITU
                },
                new Band()
                {
                    Name = "Low Frequency",
                    Abbreviation = "LF",
                    BandId = 5,
                    LowFrequency = 30 * 1000,
                    HighFrequency = 300 * 1000,
                    LowWaveLength = 10 * 1000,
                    HighWaveLength = 1 * 1000,
                    ExampleUses = "Navigation, time signals, AM longwave broadcasting (Europe and parts of Asia), RFID, amateur radio",
                    BandPlan = BandPlan.ITU
                },
                new Band()
                {
                    Name = "Medium Frequency",
                    Abbreviation = "MF",
                    BandId = 6,
                    LowFrequency = 300 * 1000,
                    HighFrequency = 3000 * 1000,
                    LowWaveLength = 1 * 1000,
                    HighWaveLength = .1 * 1000,
                    ExampleUses = "AM (medium-wave) broadcasts, amateur radio, avalanche beacons",
                    BandPlan = BandPlan.ITU
                },
                new Band()
                {
                    Name = "High Frequency",
                    Abbreviation = "HF",
                    BandId = 7,
                    LowFrequency = 3 * 1000000,
                    HighFrequency = 30 * 1000000,
                    LowWaveLength = 99.9,
                    HighWaveLength = 10,
                    ExampleUses = "Shortwave broadcasts, citizens band radio, amateur radio and over-the-horizon aviation communications, RFID, over-the-horizon radar, automatic link establishment (ALE) / near-vertical incidence skywave (NVIS) radio communications, marine and mobile radio telephony",
                    BandPlan = BandPlan.ITU
                },
                new Band()
                {
                    Name = "Very High Frequency",
                    Abbreviation = "VHF",
                    BandId = 8,
                    LowFrequency = 30 * 1000000,
                    HighFrequency = 300 * 1000000,
                    LowWaveLength = 10,
                    HighWaveLength = 1,
                    ExampleUses = "FM, television broadcasts, line-of-sight ground-to-aircraft and aircraft-to-aircraft communications, land mobile and maritime mobile communications, amateur radio, weather radio.",
                    BandPlan = BandPlan.ITU
                },
                new Band()
                {
                    Name = "Ultra High Frequency",
                    Abbreviation = "UHF",
                    BandId = 9,
                    LowFrequency = 300 * 1000000,
                    HighFrequency = 3000f * 1000000f,
                    LowWaveLength = 1,
                    HighWaveLength = .1,
                    ExampleUses = "Television broadcasts, microwave oven, microwave devices/communications, radio astronomy, mobile phones, wireless LAN, Bluetooth, ZigBee, GPS and two-way radios such as land mobile, FRS and GMRS radios, amateur radio, satellite radio, Remote control Systems, ADSB.",
                    BandPlan = BandPlan.ITU
                },
                new Band()
                {
                    Name = "Super High Frequency",
                    Abbreviation = "SHF",
                    BandId = 10,
                    LowFrequency = 3 * 1000000000f,
                    HighFrequency = 30f * 1000000000f,
                    LowWaveLength = 0.0999,
                    HighWaveLength = 0.01,
                    ExampleUses = "Radio astronomy, microwave devices/communications, wireless LAN, DSRC, most modern radars, communications satellites, cable and satellite television broadcasting, DBS, amateur radio, satellite radio",
                    BandPlan = BandPlan.ITU
                },
                new Band()
                {
                    Name = "Extremely High Frequency",
                    Abbreviation = "EHF",
                    BandId = 11,
                    LowFrequency = 30 * 1000000000f,
                    HighFrequency = 300f * 1000000000f,
                    LowWaveLength = 0.01,
                    HighWaveLength = 0.001,
                    ExampleUses = "Radio astronomy, high-frequency microwave radio relay, microwave remote sensing, amateur radio, directed-energy weapon, millimeter wave scanner, wireless LAN (802.11ad).",
                    BandPlan = BandPlan.ITU
                },
                new Band()
                {
                    Name = "Tremendously High Frequency",
                    Abbreviation = "THF",
                    BandId = 12,
                    LowFrequency = 300 * 1000000000f,
                    HighFrequency = 3000f * 1000000000f,
                    LowWaveLength = 0.001,
                    HighWaveLength = 0.0001,
                    ExampleUses = "Experimental medical imaging to replace X-rays, ultrafast molecular dynamics, condensed-matter physics, terahertz time-domain spectroscopy, terahertz computing/communications, remote sensing, amateur radio.",
                    BandPlan = BandPlan.ITU
                }
            };
        }

        public Band BandForFrequency(double frequencyInHz, BandPlan plan = BandPlan.ITU)
        {
            return (from b in Bands where b.LowFrequency <= frequencyInHz && b.HighFrequency >= frequencyInHz && b.BandPlan == plan select b)?.FirstOrDefault();
        }

        private const double LIGHTSPEED = 2.9979e10;

        public double WaveLengthForFrequencyInMeters(double frequencyInHz)
        {
            var wv = (LIGHTSPEED / frequencyInHz) / 100;
            return Math.Round(wv, 4);
        }
    }

    /// <summary>
    /// ITU Definition
    /// https://en.wikipedia.org/wiki/Radio_spectrum
    /// </summary>
    public class Band
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int BandId { get; set; }
        public double LowFrequency { get; set; }
        public double HighFrequency { get; set; }
        public double LowWaveLength { get; set; }
        public double HighWaveLength { get; set; }
        public string ExampleUses { get; set; }
        public BandPlan BandPlan { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Abbreviation})";
        }
    }

    public enum BandPlan
    {
        ITU,
        ARRL
    }
}