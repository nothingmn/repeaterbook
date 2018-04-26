using System;

namespace RepeaterBook
{
    [Flags]
    public enum Mode
    {
        Auto = 0,
        WFM = 1,
        FM = 2,
        NFM = 4,
        AM = 8,
        NAM = 16,
        DV = 32,
        USB = 64,
        LSB = 128,
        CW = 256,
        RTTY = 512,
        DIG = 1024,
        PKT = 2048,
        NCW = 4096,
        NCWR = 8192,
        CWR = 16384,
        P25 = 32768,
        RTTYR = 65536,
        FSK = 131072,
        FSKR = 262144,
        DMR = 524288
    }
}