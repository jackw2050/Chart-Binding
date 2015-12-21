using System;

namespace ChartBinding
{
    internal class ConfigData
    {

        public static double beamScale;// BSCALE   BEAM SCALE FACTOR
        public static Int16 numAuxChan;// NAUX     NUMBER OF AUXILIARY ANALOG CHANNELS
        public static string meterNumber;// METERNO  METER NUMBER
        public static Int16 linePrinterSwitch;// LPTSW    LINE PRINTER SWITCH
        public static Int16 fileNameSwitch;// NAMESW   FILE NAME SWITCH
        public static Int16 hardDiskSwitch;// IDSKSW   HARD DISK SWITCH
        public static string engPassword;// MAGIC    PASSWORD FOR HIDDEN MENU
        public static Int16 monitorDisplaySwitch;// ISCSW MONITOR DISPLAY SWITCH
        public static double crossPeriod;// PERX CROSS-AXIS PERIOD
        public static double longPeriod;// PERL LONG-AXIS PERIOD
        public static double crossDampFactor;// DAMPX CROSS-AXIS DAMPING
        public static double longDampFactor;// DAMPL LONG-AXIS DAMPING
        public static double crossGain;// AGAINX CROSS-AXIS GAIN
        public static double longGain;// AGAINL LONG-AXIS GAIN
        public static Int16 serialPortSwitch;// IFORM SERIAL PORT FORMAT SWITCH
        public static Int16 digitalInputSwitch;// ISTSW DIGITAL INPUT SWITCH
        public static Int16 printerEmulationSwitch;// MULATE PRINTER EMULATION
        public static Int16 serialPortOutputSwitch;// ICOMSW SERIAL PORT OUTPUT SWITCH
        public static Int16 alarmSwitch;// LARMSW ALARM SWITCH
        public static double crossLead;// ALEADX CROSS-AXIS LEAD
        public static double longLead;// ALEADL LONG-AXIS LEAD
        public static double springTensionMax;// STMAX MAXIMUM SPRING TENSION VALUE
        public static Int16 modeSwitch;// MODESW MODE SWITCH
        public static double iAuxGain;// IAUXGAIN -> not sure what this is
        public static double crossBias;// GXBIAS CROSS-AXIS BIAS
        public static double longBias;// GLBIAS LONG-AXIS BIAS
        public static double[] crossCouplingFactors = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };// CCFACT  CROSS COUPLING FACTORS
        public static double[] analogFilter = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };// ANALOG FILTER PARAMETERS



    }
}
