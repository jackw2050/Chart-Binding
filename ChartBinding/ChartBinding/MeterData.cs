using System;
using System.IO;
using System.Windows.Forms;


namespace ChartBinding
{
    internal class SystemVoltages
    {
        public double p28V;
        public double n28V;
        public double p24V;
        public double p15V;
        public double n15V;
        public double p5V;
    }

    internal class MeterData
    {
        private SystemVoltages SystemVoltages = new SystemVoltages();

        //       private double[] data1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        //       private double[] data2 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        //       private double[] data3 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        //       private double[] data4 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};


        public static double[] data1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] data2 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] data3 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] data4 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double beam;
        public static double oldBeam = 0;
        public static double beamFirstDifference;
        public static double totalCorrection;
        public static double rAwg;
        public static double gravity;
        public static int year, day, Hour, Min, Sec;
        public static double i4Par;
        public static DateTime dataTime = new DateTime(2015, 1,1);


        public void CheckMeterData(byte[] meterBytes)
        {
            Boolean validData = false;  // ASSUME DATA IS GARBAGE
            byte[] tempByte = { 0, 0, 0, 0 };
            byte[] tempByte2 = { 0, 0 };
            byte tempByte1 = 0;
            int dataLen;

            tempByte1 = meterBytes[0];
            dataLen =  Convert.ToInt32(meterBytes[0]); //BitConverter.ToInt32(tempByte, 0);

            //   Console.Write("Length per data " + dataLen + "\t Calculated length " +( meterBytes.Length - 1) + "\n");
            if (dataLen != meterBytes.Length)
            {
                // error invalid data.  increment error counter.  drop ot of loop
                validData = true;//  change later
            }
            else
            {
                // Data length is correct.  Now check for corrupt data - checksum
                validData = true;

            }

            if (validData)
            {
                validData = true;
            }
            else
            {
                //exit
            }
            if (validData)
            {
                tempByte[0] = meterBytes[6];

                Hour = BitConverter.ToInt32(tempByte, 0);
                //     Console.Write("Hour\t" + Hour + "\n");

                tempByte[0] = meterBytes[7];
                tempByte[1] = 0;
                tempByte[2] = 0;
                tempByte[3] = 0;
                Min = BitConverter.ToInt32(tempByte, 0);

                tempByte[0] = meterBytes[8];
                tempByte[1] = 0;
                tempByte[2] = 0;
                tempByte[3] = 0;
                Sec = BitConverter.ToInt32(tempByte, 0);
                      

                tempByte[0] = meterBytes[4];
                tempByte[1] = meterBytes[5];
                day = BitConverter.ToInt32(tempByte, 0);

                tempByte[0] = meterBytes[2];
                tempByte[1] = meterBytes[3];
                year = BitConverter.ToInt32(tempByte, 0);

                tempByte[0] = meterBytes[9];
                tempByte[1] = meterBytes[10];
                tempByte[2] = meterBytes[11];
                tempByte[3] = meterBytes[12];
                data1[3] = BitConverter.ToSingle(tempByte, 0);
                //data1[3] = ST;


                dataTime = dataTime.AddYears(year - 2015);
                dataTime = dataTime.AddDays(day - 1);
                dataTime = dataTime.AddHours(Hour);
                dataTime = dataTime.AddMinutes(Min);
                dataTime = dataTime.AddSeconds(Sec);

                //GET RAW BEAM  ------------------------------------------------------------
                tempByte[0] = meterBytes[13];
                tempByte[1] = meterBytes[14];
                tempByte[2] = meterBytes[15];
                tempByte[3] = meterBytes[16];
                data1[5] = BitConverter.ToSingle(tempByte, 0);
                //data1[5] = Beam;

                //GET VCC  ------------------------------------------------------------
                tempByte[0] = meterBytes[17];
                tempByte[1] = meterBytes[18];
                tempByte[2] = meterBytes[19];
                tempByte[3] = meterBytes[20];
                data1[6] = BitConverter.ToSingle(tempByte, 0);
                //data1[6] = VCC;

                //GET AL  ------------------------------------------------------------
                tempByte[0] = meterBytes[21];
                tempByte[1] = meterBytes[22];
                tempByte[2] = meterBytes[23];
                tempByte[3] = meterBytes[24];
                data1[7] = BitConverter.ToSingle(tempByte, 0);
                //data1[7] = AL;

                //GET AX  ------------------------------------------------------------
                tempByte[0] = meterBytes[25];
                tempByte[1] = meterBytes[26];
                tempByte[2] = meterBytes[27];
                tempByte[3] = meterBytes[28];
                data1[8] = BitConverter.ToSingle(tempByte, 0);
                //data1[8] = AX;
                //      Console.Write("Data 1 " + data1[8] + "\n");
                //GET VE  ------------------------------------------------------------
                tempByte[0] = meterBytes[29];
                tempByte[1] = meterBytes[30];
                tempByte[2] = meterBytes[31];
                tempByte[3] = meterBytes[32];
                data1[9] = BitConverter.ToSingle(tempByte, 0);
                //data1[9] = VE;

                //GET AX2  ------------------------------------------------------------
                tempByte[0] = meterBytes[33];
                tempByte[1] = meterBytes[34];
                tempByte[2] = meterBytes[35];
                tempByte[3] = meterBytes[36];
                data1[10] = BitConverter.ToSingle(tempByte, 0);
                //data1[10] = AX2;

                //GET XACC2  ------------------------------------------------------------
                tempByte[0] = meterBytes[37];
                tempByte[1] = meterBytes[38];
                tempByte[2] = meterBytes[39];
                tempByte[3] = meterBytes[40];
                data1[11] = BitConverter.ToSingle(tempByte, 0);
                //data1[11] = XACC2;

                //GET LACC2  ------------------------------------------------------------

                tempByte[0] = meterBytes[41];
                tempByte[1] = meterBytes[42];
                tempByte[2] = meterBytes[43];
                tempByte[3] = meterBytes[44];
                data1[12] = BitConverter.ToSingle(tempByte, 0);
                //data1[12] = LACC2;

                //GET XACC  ------------------------------------------------------------
                tempByte[0] = meterBytes[45];
                tempByte[1] = meterBytes[46];
                tempByte[2] = meterBytes[47];
                tempByte[3] = meterBytes[48];
                data1[13] = BitConverter.ToSingle(tempByte, 0);
                //data1[13] = XACC;

                //GET LACC  ------------------------------------------------------------
                tempByte[0] = meterBytes[49];
                tempByte[1] = meterBytes[50];
                tempByte[2] = meterBytes[51];
                tempByte[3] = meterBytes[52];
                data1[14] = BitConverter.ToSingle(tempByte, 0);
                //data1[14] = LACC;

                //GET AUX1  ------------------------------------------------------------
                tempByte[0] = meterBytes[53];
                tempByte[1] = meterBytes[54];
                data1[15] = BitConverter.ToSingle(tempByte, 0);
                //data1[15] = AUX1;

                //GET AUX2  ------------------------------------------------------------
                tempByte[0] = meterBytes[55];
                tempByte[1] = meterBytes[56];
                data1[16] = BitConverter.ToSingle(tempByte, 0);
                //data1[16] = AUX2;

                //GET AUX3  ------------------------------------------------------------
                tempByte[0] = meterBytes[57];
                tempByte[1] = meterBytes[58];
                data1[17] = BitConverter.ToSingle(tempByte, 0);
                //data1[17] = AUX3;

                //GET AUX4  ------------------------------------------------------------
                tempByte[0] = meterBytes[59];
                tempByte[1] = meterBytes[60];
                data1[18] = BitConverter.ToSingle(tempByte, 0);
                //data1[18] = AUX4;


                tempByte[0] = meterBytes[61];
                tempByte[1] = meterBytes[62];
                tempByte[2] = meterBytes[63];
                tempByte[3] = 0;
                i4Par = BitConverter.ToSingle(tempByte, 0);

                //GET +28V  ------------------------------------------------------------
                for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                tempByte[0] = meterBytes[64];
                tempByte[1] = meterBytes[65];
                //  Array.Copy(meterBytes, 64, tempByte, 0, 2);
                int p28Vi = BitConverter.ToInt32(tempByte, 0);
                SystemVoltages.p28V = Convert.ToDouble(p28Vi * 2 / 3276.7);
                //     IVOLTS[1] = p28V;

                //GET -28V  ------------------------------------------------------------
                for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                tempByte[0] = meterBytes[66];
                tempByte[1] = meterBytes[67];
                int n28Vi = BitConverter.ToInt32(tempByte, 0);
                SystemVoltages.n28V = Convert.ToDouble(n28Vi * -5 / 3276.7);   //  check this conversion
                //     IVOLTS[2] = n28V;

                //GET +24V  ------------------------------------------------------------
                for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                tempByte[0] = meterBytes[68];
                tempByte[1] = meterBytes[69];
                int p24Vi = BitConverter.ToInt32(tempByte, 0);
                SystemVoltages.p24V = Convert.ToDouble(p24Vi * 2 / 3276.7);
                //     IVOLTS[3] = p24V;

                //GET+15V  ------------------------------------------------------------
                for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                tempByte[0] = meterBytes[70];
                tempByte[1] = meterBytes[71];
                int p15Vi = BitConverter.ToInt32(tempByte, 0);
                SystemVoltages.p15V = Convert.ToDouble(p15Vi / 3276.7);   //  check this conversion
                //     IVOLTS[4] = p15V;

                //GET -15V  ------------------------------------------------------------
                for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                tempByte[0] = meterBytes[72];
                tempByte[1] = meterBytes[73];
                int n15Vi = BitConverter.ToInt32(tempByte, 0);
                SystemVoltages.n15V = Convert.ToDouble(n15Vi * -3 / 3276.7);   //  check this conversion
                //    IVOLTS[5] = n15V;

                //GET +5V  ------------------------------------------------------------
                for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                tempByte[0] = meterBytes[74];
                tempByte[1] = meterBytes[75];
                int p5Vi = BitConverter.ToInt32(tempByte, 0);
                SystemVoltages.p5V = Convert.ToDouble(p5Vi / 3 / 3276.7);   //  check this conversion
                //        IVOLTS[6] = p5V;



                //GET STATUS  ------------------------------------------------------------
                tempByte[0] = meterBytes[76];
                int ISTAT = tempByte[0];

                //GET PORT C INPUT  ------------------------------------------------------------
                for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                tempByte[0] = meterBytes[77];

                //GET PORT C INPUT  ------------------------------------------------------------
                for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                tempByte[0] = meterBytes[77];
                // IPORT[2] = tempByte[0];
                //               if (ISCSW == -99)
                //               {
                // exit gracefully???????
                //               }
                // CHECK FOR REMOTE EMBEDDED COMPUTER REBOOT  ------------------------------------------------------------
                //       else if(IBUF[1] == 1) // remote rebooted
                //       {
                //               IERR = -4;
                //       }
                // CHECK FOR TIME SET SUCCESSFULL/ FAIL  ------------------------------------------------------------
                //       else if(IBUF[1] == 2) // Time set successfull
                //       {
                //           IERR = -10;
                //       }
                //       else if(IBUF[1] == 3) // Time set fail
                //       {
                //           IERR = -11;
                //       }
                //CHECK G2000 BIAS  ------------------------------------------------------------
                //       else if(IBUF[1] == 4)//
                //       {
                // tempByte[0] = IBUF[2];
                //tempByte[1] = IBUF[3];
                //tempByte[2] = IBUF[4];
                //tempByte[3] = IBUF[5];
                //G2000 = BitConverter.ToSingle(tempByte, 0);
                //          GXTEMP = G2000
                //       }

                //                   return (InputData);







            }
        }




        public void CheckMeterDataSim()
        {




            //  NEED TO ADD ERROR CHECKING FOR END OF FILE
            //  NEED TO ADD OPEN FILE DIALOG ONLY IF FILE IS (MISSING OR MANUAL BOX IS CHECKED - ENGINEERING ONLY)
            ConfigData ConfigData = new ConfigData();
            FileStream myStream;

            byte[] tempByte = { 0, 0, 0, 0 };
            byte[] byte1 = new byte[1];
            byte[] byte2 = new byte[2];
            byte[] byte4 = new byte[4];
            byte[] byte10 = new byte[10];
            int dataLen, cmd;

            myStream = new FileStream("C:\\ZLS\\meter data no  space.bin", FileMode.Open);
            BinaryReader readBinary = new BinaryReader(myStream);



            dataLen = BitConverter.ToInt32(byte1, 0);

            //  dataLen = Convert.ToInt32(meterBytes[0]); //BitConverter.ToInt32(tempByte, 0);

            //   Console.Write("Length per data " + dataLen + "\t Calculated length " +( meterBytes.Length - 1) + "\n");



            bool validData = true;


            if (validData)
            {

                cmd = BitConverter.ToInt16(byte2, 0);



                Hour = BitConverter.ToInt16(byte2, 0);
                //     Console.Write("Hour\t" + Hour + "\n");

                Min = BitConverter.ToInt16(byte2, 0);
                Console.Write("Min\t" + Min + "\n");


                Sec = BitConverter.ToInt16(byte2, 0);
                Console.Write("Second\t" + Sec + "\n");


                day = BitConverter.ToInt16(byte2, 0);
                Console.Write("Day\t" + day + "\n");


                year = BitConverter.ToInt16(byte2, 0);
                Console.Write("Year\t" + year + "\n");


                data1[3] = BitConverter.ToSingle(byte4, 0);
                //data1[3] = ST;
                //    Console.Write("Spring Tension\t" + data1[3] + "\n");






                //GET RAW BEAM  ------------------------------------------------------------

                data1[5] = BitConverter.ToSingle(byte4, 0);
                //data1[5] = Beam;
                // Console.Write("Beam\t" + data1[5] + "\n");

                //GET VCC  ------------------------------------------------------------

                data1[6] = BitConverter.ToSingle(byte4, 0);
                //data1[6] = VCC;

                //GET AL  ------------------------------------------------------------

                data1[7] = BitConverter.ToSingle(byte4, 0);
                //data1[7] = AL;

                //GET AX  ------------------------------------------------------------

                data1[8] = BitConverter.ToSingle(byte4, 0);
                //data1[8] = AX;
                //      Console.Write("Data 1 " + data1[8] + "\n");
                //GET VE  ------------------------------------------------------------

                data1[9] = BitConverter.ToSingle(byte4, 0);
                //data1[9] = VE;

                //GET AX2  ------------------------------------------------------------

                data1[10] = BitConverter.ToSingle(byte4, 0);
                //data1[10] = AX2;

                //GET XACC2  ------------------------------------------------------------

                data1[11] = BitConverter.ToSingle(byte4, 0);
                //data1[11] = XACC2;

                //GET LACC2  ------------------------------------------------------------


                data1[12] = BitConverter.ToSingle(byte4, 0);
                //data1[12] = LACC2;

                //GET XACC  ------------------------------------------------------------

                data1[13] = BitConverter.ToSingle(byte4, 0);
                //data1[13] = XACC;

                //GET LACC  ------------------------------------------------------------

                data1[14] = BitConverter.ToSingle(byte4, 0);
                //data1[14] = LACC;

                //GET AUX1  ------------------------------------------------------------

                data1[15] = BitConverter.ToSingle(byte4, 0);
                //data1[15] = AUX1;

                //GET AUX2  ------------------------------------------------------------

                data1[16] = BitConverter.ToSingle(tempByte, 0);
                //data1[16] = AUX2;

                //GET AUX3  ------------------------------------------------------------

                data1[17] = BitConverter.ToSingle(tempByte, 0);
                //data1[17] = AUX3;

                //GET AUX4  ------------------------------------------------------------

                data1[18] = BitConverter.ToSingle(tempByte, 0);
                //data1[18] = AUX4;



                i4Par = BitConverter.ToSingle(tempByte, 0);

                //GET +28V  ------------------------------------------------------------



                int p28Vi = BitConverter.ToInt32(byte2, 0);
                SystemVoltages.p28V = Convert.ToDouble(p28Vi * 2 / 3276.7);
                //     IVOLTS[1] = p28V;

                //GET -28V  ------------------------------------------------------------

                int n28Vi = BitConverter.ToInt32(tempByte, 0);
                SystemVoltages.n28V = Convert.ToDouble(n28Vi * -5 / 3276.7);   //  check this conversion
                //     IVOLTS[2] = n28V;

                //GET +24V  ------------------------------------------------------------


                int p24Vi = BitConverter.ToInt32(tempByte, 0);
                SystemVoltages.p24V = Convert.ToDouble(p24Vi * 2 / 3276.7);
                //     IVOLTS[3] = p24V;

                //GET+15V  ------------------------------------------------------------

                int p15Vi = BitConverter.ToInt32(tempByte, 0);
                SystemVoltages.p15V = Convert.ToDouble(p15Vi / 3276.7);   //  check this conversion
                //     IVOLTS[4] = p15V;

                //GET -15V  ------------------------------------------------------------

                int n15Vi = BitConverter.ToInt32(tempByte, 0);
                SystemVoltages.n15V = Convert.ToDouble(n15Vi * -3 / 3276.7);   //  check this conversion
                //    IVOLTS[5] = n15V;

                //GET +5V  ------------------------------------------------------------

                int p5Vi = BitConverter.ToInt32(tempByte, 0);
                SystemVoltages.p5V = Convert.ToDouble(p5Vi / 3 / 3276.7);   //  check this conversion
                //        IVOLTS[6] = p5V;



                //GET STATUS  ------------------------------------------------------------
                //   tempByte[0] = meterBytes[76];
                int ISTAT = tempByte[0];

                //GET PORT C INPUT  ------------------------------------------------------------
                for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                //      tempByte[0] = meterBytes[77];

                //GET PORT C INPUT  ------------------------------------------------------------
                for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                //     tempByte[0] = meterBytes[77];
                // IPORT[2] = tempByte[0];
                //               if (ISCSW == -99)
                //               {
                // exit gracefully???????
                //               }
                // CHECK FOR REMOTE EMBEDDED COMPUTER REBOOT  ------------------------------------------------------------
                //       else if(IBUF[1] == 1) // remote rebooted
                //       {
                //               IERR = -4;
                //       }
                // CHECK FOR TIME SET SUCCESSFULL/ FAIL  ------------------------------------------------------------
                //       else if(IBUF[1] == 2) // Time set successfull
                //       {
                //           IERR = -10;
                //       }
                //       else if(IBUF[1] == 3) // Time set fail
                //       {
                //           IERR = -11;
                //       }
                //CHECK G2000 BIAS  ------------------------------------------------------------
                //       else if(IBUF[1] == 4)//
                //       {
                // tempByte[0] = IBUF[2];
                //tempByte[1] = IBUF[3];
                //tempByte[2] = IBUF[4];
                //tempByte[3] = IBUF[5];
                //G2000 = BitConverter.ToSingle(tempByte, 0);
                //          GXTEMP = G2000
                //       }

                //                   return (InputData);








            }

        }




        public double dFilt(double g)
        {
            //        IMPLICIT INTEGER*2 (I-N)  // 2 byte int
            double[] fw = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] gt = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int nPoint = 0;
            int k = 0;

            //        DATA GT,NPOINT/60*0.,0/
            // FILTER WEIGHTS

            double[,] fData = new double[10, 6]
            {
            { -.00034,-.00038,-.00041,-.00044,-.00046,-.00046},
            { -.00044,-.00039,-.00030,-.00015,.00007,.00037},
            {  .00079,.00133,.00202,.00289,.00396,.00526},
            {  .00679,.00859,.01066,.01299,.01558,.01841},
            {  .02143,.02460,.02785,.03110,.03426,.03723},
            {  .03992,.04223,.04408,.04539,.04613,.04626},
            {  .04579,.04474,.04315,.04109,.03864,.03589},
            {  .03292,.02984,.02671,.02362,.02063,.01780},
            {  .01516,.01274,.01056,.00863,.00694,.00548},
            {  .00424,.00321,.00235,.00166,.00111,.00068}
            };

            //            SAVE
            nPoint = nPoint + 1;
            if (nPoint > 60) nPoint = 1;
            gt[nPoint] = g;
            k = nPoint;
            //        DFILT = 0.0;
            //       DO 100 I=1,
            for (int i = 0; i < 160; i++)
            {
                k = k + 1;
                if (k > 60) k = 1;

                //   DFILT = DFILT + FW[I]*(GT[K]-GT[1]);// 100
            }

            //        DFILT = DFILT + GT[1];
            //           dFilt = dFilt + gt[1];
            return k;
        }
    }
}