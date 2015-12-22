using FileHelpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Windows.Forms;

namespace ChartBinding
{
    public class FileClass
    {
        public void ReadConfigFile()
        {
            //  NEED TO ADD ERROR CHECKING FOR END OF FILE
            //  NEED TO ADD OPEN FILE DIALOG ONLY IF FILE IS (MISSING OR MANUAL BOX IS CHECKED - ENGINEERING ONLY)
            ConfigData ConfigData = new ConfigData();
            FileStream myStream;
        //    double[] CCFACT = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //    double[] AFILT = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] tempByte = { 0, 0, 0, 0 };
            byte[] byte2 = new byte[2];
            byte[] byte4 = new byte[4];
            byte[] byte10 = new byte[10];

            try
            {
                myStream = new FileStream("C:\\LCT stuff\\CONFIG20.ref", FileMode.Open);
                BinaryReader readBinary = new BinaryReader(myStream);

                readBinary.Read(byte2, 0, 2);
                readBinary.Read(byte4, 0, 4);

                ConfigData.beamScale = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("BEAM SCALE FACTOR-------------------- \t{0:n6}.", ConfigData.beamScale);

                readBinary.Read(byte2, 0, 2);
                ConfigData.numAuxChan = BitConverter.ToInt16(byte2, 0);
                if (Form1.engineerDebug) Console.WriteLine("NUMBER OF AUXILIARY ANALOG CHANNELS-- \t" + Convert.ToString(ConfigData.numAuxChan));

                readBinary.Read(byte10, 0, 10);
                ConfigData.meterNumber = System.Text.Encoding.Default.GetString(byte10);
                if (Form1.engineerDebug) Console.WriteLine("Meter number is---------------------- \t" + ConfigData.meterNumber);

                readBinary.Read(byte2, 0, 2);
                ConfigData.linePrinterSwitch = BitConverter.ToInt16(byte2, 0);
                if (Form1.engineerDebug) Console.WriteLine("LINE PRINTER SWITCH------------------ \t" + Convert.ToString(ConfigData.linePrinterSwitch));

                readBinary.Read(byte2, 0, 2);
                ConfigData.fileNameSwitch = BitConverter.ToInt16(byte2, 0);
                if (Form1.engineerDebug) Console.WriteLine("FILE NAME SWITCH--------------------- \t" + Convert.ToString(ConfigData.fileNameSwitch));

                readBinary.Read(byte2, 0, 2);
                ConfigData.hardDiskSwitch = BitConverter.ToInt16(byte2, 0); ;
                if (Form1.engineerDebug) Console.WriteLine("HARD DISK SWITCH--------------------- \t" + Convert.ToString(ConfigData.hardDiskSwitch));

                readBinary.Read(byte10, 0, 10);
                ConfigData.engPassword = System.Text.Encoding.Default.GetString(byte10);
                if (Form1.engineerDebug) Console.WriteLine("Magic value is ---------------------- \t" + ConfigData.engPassword);

                readBinary.Read(byte2, 0, 2);
                ConfigData.monitorDisplaySwitch = BitConverter.ToInt16(byte2, 0);
                if (Form1.engineerDebug) Console.WriteLine("MONITOR DISPLAY SWITCH--------------- \t" + Convert.ToString(ConfigData.monitorDisplaySwitch));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossPeriod = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS PERIOD-------------------- \t{0:e4}", ConfigData.crossPeriod);

                readBinary.Read(byte4, 0, 4);
                ConfigData.longPeriod = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS PERIOD-------------------- \t{0:e4}", ConfigData.longPeriod);

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossDampFactor = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS DAMPING------------------- \t{0:e4}", ConfigData.crossDampFactor);

                readBinary.Read(byte4, 0, 4);
                ConfigData.longDampFactor = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS DAMPING------------------- \t{0:e4}", ConfigData.longDampFactor);

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossGain = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS GAIN---------------------- \t" + Convert.ToString(ConfigData.crossGain));

                readBinary.Read(byte4, 0, 4);
                ConfigData.longGain = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS GAIN---------------------- \t" + Convert.ToString(ConfigData.longGain));

                readBinary.Read(byte2, 0, 2);
                ConfigData.serialPortSwitch = BitConverter.ToInt16(byte2, 0);
                if (Form1.engineerDebug) Console.WriteLine("SERIAL PORT FORMAT SWITCH------------ \t" + Convert.ToString(ConfigData.serialPortSwitch));

                readBinary.Read(byte2, 0, 2);
                ConfigData.digitalInputSwitch = BitConverter.ToInt16(byte2, 0);
                if (Form1.engineerDebug) Console.WriteLine("DIGITAL INPUT SWITCH----------------- \t" + Convert.ToString(ConfigData.digitalInputSwitch));

                readBinary.Read(byte2, 0, 2);
                ConfigData.printerEmulationSwitch = BitConverter.ToInt16(byte2, 0);
                if (ConfigData.printerEmulationSwitch == 2)
                {
                    if (Form1.engineerDebug) Console.WriteLine("PRINTER EMULATION-------------------- \t" + "ESC_P");
                }
                if (ConfigData.printerEmulationSwitch == 3)
                {
                    if (Form1.engineerDebug) Console.WriteLine("PRINTER EMULATION-------------------- \t" + "ESC_P2");
                }
                else
                {
                    if (Form1.engineerDebug) Console.WriteLine("PRINTER EMULATION-------------------- \t" + "DPL24C");
                }

                readBinary.Read(byte2, 0, 2);
                ConfigData.serialPortOutputSwitch = BitConverter.ToInt16(byte2, 0);
                if (Form1.engineerDebug) Console.WriteLine("SERIAL PORT OUTPUT SWITCH------------ \t" + Convert.ToString(ConfigData.serialPortOutputSwitch));

                readBinary.Read(byte2, 0, 2);
                ConfigData.alarmSwitch = BitConverter.ToInt16(byte2, 0);
                if (Form1.engineerDebug) Console.WriteLine("ALARM SWITCH------------------------- \t" + Convert.ToString(ConfigData.alarmSwitch));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossLead = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.crossLead));

                readBinary.Read(byte4, 0, 4);
                ConfigData.longLead = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.longLead));

                readBinary.Read(byte4, 0, 4);
                ConfigData.springTensionMax = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("MAXIMUM SPRING TENSION VALUE--------- \t" + Convert.ToString(ConfigData.springTensionMax));

                readBinary.Read(byte4, 0, 4);
                ConfigData.modeSwitch = BitConverter.ToInt16(byte4, 0);
                if (ConfigData.modeSwitch == 0)
                {
                    if (Form1.engineerDebug) Console.WriteLine("MODE SWITCH-------------------------- \t" + "Marine");
                }
                else
                {
                    if (Form1.engineerDebug) Console.WriteLine("MODE SWITCH-------------------------- \t" + "Hires");
                }

                readBinary.Read(byte4, 0, 4);
                ConfigData.iAuxGain = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("I aux gain value is --------------- \t" + Convert.ToString(ConfigData.iAuxGain));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossBias = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS BIAS---------------------- \t" + Convert.ToString(ConfigData.crossBias));

                readBinary.Read(byte4, 0, 4);
                ConfigData.longBias = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS BIAS---------------------- \t" + Convert.ToString(ConfigData.longBias));

                readBinary.Read(byte2, 0, 2);// extra read for alignment.  need to find out why
                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[6] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("VCC---------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[6]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[7] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("AL----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[7]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[8] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("AX----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[8]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[9] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("VE----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[9]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[10] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("AX2---------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[10]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[11] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("XACC**2------------------------------ \t" + Convert.ToString(ConfigData.crossCouplingFactors[11]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[12] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("LACC**2------------------------------ \t" + Convert.ToString(ConfigData.crossCouplingFactors[12]));

                readBinary.Read(byte2, 0, 2);
                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[13] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (4)---------- \t{0:e4}.", ConfigData.crossCouplingFactors[13]);

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[14] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (4)----------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[14]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[15] = BitConverter.ToSingle(byte4, 0);
                if (ConfigData.crossCouplingFactors[15] == 1)
                {
                    if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (16)--------- \t" + "N/A");
                }
                else
                {
                    if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (16)--------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[15]));
                }

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[16] = BitConverter.ToSingle(byte4, 0);
                if (ConfigData.crossCouplingFactors[15] == 1)
                {
                    if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (16)---------- \t" + "N/A");
                }
                else
                {
                    if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (16)---------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[16]));
                }

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[1] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("AX PHASE----------------------------- \t" + Convert.ToString(ConfigData.analogFilter[1]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[2] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("AL PHASE----------------------------- \t" + Convert.ToString(ConfigData.analogFilter[2]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[3] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("AFILT[3]---------------------------- \t" + Convert.ToString(ConfigData.analogFilter[3]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[4] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("VCC PHASE---------------------------- \t" + Convert.ToString(ConfigData.analogFilter[4]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[5] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (4)---- \t" + Convert.ToString(ConfigData.analogFilter[5]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[6] = BitConverter.ToSingle(byte4, 0);
                if (Form1.engineerDebug) Console.WriteLine("LONG AXIS COMPENSATION PHASE (4)----- \t" + Convert.ToString(ConfigData.analogFilter[6]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[7] = BitConverter.ToSingle(byte4, 0);
                if (ConfigData.crossCouplingFactors[15] == 1)
                {
                    if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (16)--- \t" + "N/A");
                }
                else
                {
                    if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (16)--- \t" + Convert.ToString(ConfigData.analogFilter[7]));
                }

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[8] = BitConverter.ToSingle(byte4, 0);
                if (ConfigData.crossCouplingFactors[15] == 1)
                {
                    if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION PHASE (16)--- \t" + "N/A");
                }
                else
                {
                    if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION PHASE (16)--- \t" + Convert.ToString(ConfigData.analogFilter[7]));
                }

                Console.ReadLine();
                readBinary.Close();

                //LogConfigData(ConfigData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /*
                public void LogConfigData(ConfigData ConfigData)
                {
                    // ConfigData ConfigData = new ConfigData();
                    if (Form1.engineerDebug) Console.WriteLine("\n\nConfiguration data for meter number   \t" + ConfigData.meterNumber);
                    if (Form1.engineerDebug) Console.WriteLine("\n\t User defined parameters\n");
                    if (Form1.engineerDebug) Console.WriteLine("Number of auxillary analog channels-- \t" + Convert.ToString(ConfigData.numAuxChan));
                    if (Form1.engineerDebug) Console.WriteLine("DIGITAL INPUT SWITCH----------------- \t" + Convert.ToString(ConfigData.digitalInputSwitch));
                    if (Form1.engineerDebug) Console.WriteLine("MONITOR DISPLAY SWITCH--------------- \t" + Convert.ToString(ConfigData.monitorDisplaySwitch));
                    if (Form1.engineerDebug) Console.WriteLine("LINE PRINTER SWITCH------------------ \t" + Convert.ToString(ConfigData.linePrinterSwitch));
                    if (Form1.engineerDebug) Console.WriteLine("FILE NAME SWITCH--------------------- \t" + Convert.ToString(ConfigData.fileNameSwitch));
                    if (Form1.engineerDebug) Console.WriteLine("HARD DISK SWITCH--------------------- \t" + Convert.ToString(ConfigData.hardDiskSwitch));
                    if (Form1.engineerDebug) Console.WriteLine("SERIAL PORT FORMAT SWITCH------------ \t" + Convert.ToString(ConfigData.serialPortSwitch));
                    if (Form1.engineerDebug) Console.WriteLine("SERIAL PORT OUTPUT SWITCH------------ \t" + Convert.ToString(ConfigData.serialPortOutputSwitch));
                    if (Form1.engineerDebug) Console.WriteLine("ALARM SWITCH------------------------- \t" + Convert.ToString(ConfigData.alarmSwitch));
                    if (ConfigData.printerEmulationSwitch == 2)
                    {
                        if (Form1.engineerDebug) Console.WriteLine("PRINTER EMULATION-------------------- \t" + "ESC_P");
                    }
                    if (ConfigData.printerEmulationSwitch == 3)
                    {
                        if (Form1.engineerDebug) Console.WriteLine("PRINTER EMULATION-------------------- \t" + "ESC_P2");
                    }
                    else
                    {
                        if (Form1.engineerDebug) Console.WriteLine("PRINTER EMULATION-------------------- \t" + "DPL24C");
                    }
                    if (ConfigData.modeSwitch == 0)
                    {
                        if (Form1.engineerDebug) Console.WriteLine("MODE SWITCH-------------------------- \t" + "Marine");
                    }
                    else
                    {
                        if (Form1.engineerDebug) Console.WriteLine("MODE SWITCH-------------------------- \t" + "Hires");
                    }
                    if (Form1.engineerDebug) Console.WriteLine("\n\tParameters defined by ZLS.\n");
                    if (Form1.engineerDebug) Console.WriteLine("BEAM SCALE FACTOR-------------------- \t{0:n6}.", ConfigData.beamScale);
                    if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS PERIOD-------------------- \t{0:e4}", ConfigData.crossPeriod);
                    if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS DAMPING------------------- \t{0:e4}", ConfigData.crossDampFactor);
                    if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS GAIN---------------------- \t" + Convert.ToString(ConfigData.crossGain));
                    if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.crossLead));
                    if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (4)---------- \t{0:e4}.", ConfigData.crossCouplingFactors[13]);
                    if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (4)---- \t" + Convert.ToString(ConfigData.analogFilter[5]));
                    if (ConfigData.crossCouplingFactors[15] == 1)
                    {
                        if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (16)--------- \t" + "N/A");
                    }
                    else
                    {
                        if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (16)--------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[15]));
                    }

                    if (ConfigData.crossCouplingFactors[15] == 1)
                    {
                        if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (16)--- \t" + "N/A");
                    }
                    else
                    {
                        if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (16)--- \t" + Convert.ToString(ConfigData.analogFilter[7]));
                    }
                    if (Form1.engineerDebug) Console.WriteLine("CROSS-AXIS BIAS---------------------- \t" + Convert.ToString(ConfigData.crossBias));
                    if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS PERIOD-------------------- \t{0:e4}", ConfigData.longPeriod);
                    if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS DAMPING------------------- \t{0:e4}", ConfigData.longDampFactor);
                    if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS GAIN---------------------- \t{0:n4}", ConfigData.longGain);
                    if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.longLead));
                    if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (4)----------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[14]));
                    if (Form1.engineerDebug) Console.WriteLine("LONG AXIS COMPENSATION PHASE (4)----- \t" + Convert.ToString(ConfigData.analogFilter[6]));
                    if (ConfigData.crossCouplingFactors[15] == 1)
                    {
                        if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (16)---------- \t" + "N/A");
                    }
                    else
                    {
                        if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (16)---------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[16]));
                    }
                    if (ConfigData.crossCouplingFactors[15] == 1)
                    {
                        if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION PHASE (16)---- \t" + "N/A");
                    }
                    else
                    {
                        if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION PHASE (16)--- \t" + Convert.ToString(ConfigData.analogFilter[7]));
                    }
                    if (Form1.engineerDebug) Console.WriteLine("LONG-AXIS BIAS---------------------- \t" + Convert.ToString(ConfigData.longBias));
                    if (Form1.engineerDebug) Console.WriteLine("VCC---------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[6]));
                    if (Form1.engineerDebug) Console.WriteLine("AL----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[7]));
                    if (Form1.engineerDebug) Console.WriteLine("AX----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[8]));
                    if (Form1.engineerDebug) Console.WriteLine("VE----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[9]));
                    if (Form1.engineerDebug) Console.WriteLine("AX2---------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[10]));
                    if (Form1.engineerDebug) Console.WriteLine("XACC**2------------------------------ \t" + Convert.ToString(ConfigData.crossCouplingFactors[11]));
                    if (Form1.engineerDebug) Console.WriteLine("LACC**2------------------------------ \t" + Convert.ToString(ConfigData.crossCouplingFactors[12]));
                    if (Form1.engineerDebug) Console.WriteLine("AX PHASE----------------------------- \t" + Convert.ToString(ConfigData.analogFilter[1]));
                    if (Form1.engineerDebug) Console.WriteLine("AL PHASE----------------------------- \t" + Convert.ToString(ConfigData.analogFilter[2]));
                    if (Form1.engineerDebug) Console.WriteLine("VCC PHASE---------------------------- \t" + Convert.ToString(ConfigData.analogFilter[4]));
                    if (Form1.engineerDebug) Console.WriteLine("MAXIMUM SPRING TENSION VALUE--------- \t" + Convert.ToString(ConfigData.springTensionMax));
                }

                //http://www.codeproject.com/Articles/686994/Create-Read-Advance-PDF-Report-using-iTextSharp-in#1
        */

        public void LogConfigDataToFile()
        {
            PdfPTable table = new PdfPTable(2);
            PdfPCell cell = new PdfPCell(new Phrase("Header spanning 2 columns"));

            cell.Colspan = 3;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            //  Setup margins
            //  Left Margin: 36pt => 0.5 inch
            //  Right Margin: 72pt => 1 inch
            //  Top Margin: 108pt => 1.5 inch
            //  Bottom Margini: 180pt => 2.5 inch

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            Font times = new Font(bfTimes, 10);

            //Create a System.IO.FileStream object:
            FileStream fs = new FileStream("C:\\LCT stuff\\Meter Configuration.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            //Step 2: Create a iTextSharp.text.Document object: with page size and margins
            Document doc = new Document(PageSize.LETTER, 36, 72, 36, 36);

            // Setting Document properties e.g.
            // 1. Title
            // 2. Subject
            // 3. Keywords
            // 4. Creator
            // 5. Author
            // 6. Header
            doc.AddTitle("Configuration for meter #: " + Form1.meterNumber);
            doc.AddSubject("Configuration data");
            //  doc.AddKeywords("Metadata, iTextSharp 5.4.4, Chapter 1, Tutorial");
            doc.AddCreator("ZLS");
            doc.AddAuthor("Jack Walker");
            doc.AddHeader("Nothing", "No Header");// Add creation date

            //Step 3: Create a iTextSharp.text.pdf.PdfWriter object. It helps to write the Document to the Specified FileStream:
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            //Step 4: Openning the Document:
            doc.Open();

            //Step 5: Adding a Paragraph by creating a iTextSharp.text.Paragraph object:

            doc.Add(new Paragraph("\n\t User defined parameters\n", times));
            doc.Add(new Paragraph("Number of auxillary analog channels \t\t" + Convert.ToString(ConfigData.numAuxChan)));
            doc.Add(new Paragraph("DIGITAL INPUT SWITCH \t\t" + Convert.ToString(ConfigData.digitalInputSwitch)));
            doc.Add(new Paragraph("MONITOR DISPLAY SWITCH \t\t" + Convert.ToString(ConfigData.monitorDisplaySwitch)));
            doc.Add(new Paragraph("LINE PRINTER SWITCH \t\t" + Convert.ToString(ConfigData.linePrinterSwitch)));
            doc.Add(new Paragraph("FILE NAME SWITCH \t\t" + Convert.ToString(ConfigData.fileNameSwitch)));
            doc.Add(new Paragraph("HARD DISK SWITCH \t\t" + Convert.ToString(ConfigData.hardDiskSwitch)));
            doc.Add(new Paragraph("SERIAL PORT FORMAT SWITCH \t\t" + Convert.ToString(ConfigData.serialPortSwitch)));
            doc.Add(new Paragraph("SERIAL PORT OUTPUT SWITCH \t\t" + Convert.ToString(ConfigData.serialPortOutputSwitch)));
            doc.Add(new Paragraph("ALARM SWITCH \t\t" + Convert.ToString(ConfigData.alarmSwitch)));
            if (ConfigData.printerEmulationSwitch == 2)
            {
                doc.Add(new Paragraph("PRINTER EMULATION-------------------- \t" + "ESC_P"));
            }
            if (ConfigData.printerEmulationSwitch == 3)
            {
                doc.Add(new Paragraph("PRINTER EMULATION-------------------- \t" + "ESC_P2"));
            }
            else
            {
                doc.Add(new Paragraph("PRINTER EMULATION-------------------- \t" + "DPL24C"));
            }
            if (ConfigData.modeSwitch == 0)
            {
                doc.Add(new Paragraph("MODE SWITCH-------------------------- \t" + "Marine"));
            }
            else
            {
                doc.Add(new Paragraph("MODE SWITCH-------------------------- \t" + "Hires"));
            }
            doc.Add(new Paragraph("\n\tParameters defined by ZLS.\n"));
            doc.Add(new Paragraph("BEAM SCALE FACTOR-------------------- \t" + Math.Round(ConfigData.beamScale, 6)));
            doc.Add(new Paragraph("CROSS-AXIS PERIOD-------------------- \t" + Math.Round(ConfigData.crossPeriod, 4)));

            doc.Add(new Paragraph("CROSS-AXIS DAMPING------------------- \t" + Math.Round(ConfigData.crossDampFactor, 4)));
            doc.Add(new Paragraph("CROSS-AXIS GAIN---------------------- \t" + Convert.ToString(ConfigData.crossGain)));
            doc.Add(new Paragraph("CROSS-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.crossLead)));
            doc.Add(new Paragraph("CROSS-AXIS COMPENSATION (4)---------- \t" + Math.Round(ConfigData.crossCouplingFactors[13], 4)));
            doc.Add(new Paragraph("CROSS-AXIS COMPENSATION PHASE (4)---- \t" + Convert.ToString(ConfigData.analogFilter[5])));
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                doc.Add(new Paragraph("CROSS-AXIS COMPENSATION (16)--------- \t" + "N/A"));
            }
            else
            {
                doc.Add(new Paragraph("CROSS-AXIS COMPENSATION (16)--------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[15])));
            }

            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                doc.Add(new Paragraph("CROSS-AXIS COMPENSATION PHASE (16)--- \t" + "N/A"));
            }
            else
            {
                doc.Add(new Paragraph("CROSS-AXIS COMPENSATION PHASE (16)--- \t" + Convert.ToString(ConfigData.analogFilter[7])));
            }
            doc.Add(new Paragraph("CROSS-AXIS BIAS---------------------- \t" + Math.Round(ConfigData.crossBias, 4)));
            doc.Add(new Paragraph("LONG-AXIS PERIOD-------------------- \t" + Math.Round(ConfigData.longPeriod)));
            doc.Add(new Paragraph("LONG-AXIS DAMPING------------------- \t" + Math.Round(ConfigData.longDampFactor)));
            doc.Add(new Paragraph("LONG-AXIS GAIN---------------------- \t" + Math.Round(ConfigData.longGain)));
            doc.Add(new Paragraph("LONG-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.longLead)));
            doc.Add(new Paragraph("LONG-AXIS COMPENSATION (4)----------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[14])));
            doc.Add(new Paragraph("LONG AXIS COMPENSATION PHASE (4)----- \t" + Convert.ToString(ConfigData.analogFilter[6])));
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                doc.Add(new Paragraph("LONG-AXIS COMPENSATION (16)---------- \t" + "N/A"));
            }
            else
            {
                doc.Add(new Paragraph("LONG-AXIS COMPENSATION (16)---------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[16])));
            }
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                doc.Add(new Paragraph("LONG-AXIS COMPENSATION PHASE (16)---- \t" + "N/A"));
            }
            else
            {
                doc.Add(new Paragraph("LONG-AXIS COMPENSATION PHASE (16)--- \t" + Convert.ToString(ConfigData.analogFilter[7])));
            }
            doc.Add(new Paragraph("LONG-AXIS BIAS---------------------- \t" + Convert.ToString(ConfigData.longBias)));
            doc.Add(new Paragraph("VCC---------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[6])));
            doc.Add(new Paragraph("AL----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[7])));
            doc.Add(new Paragraph("AX----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[8])));
            doc.Add(new Paragraph("VE----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[9])));
            doc.Add(new Paragraph("AX2---------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[10])));
            doc.Add(new Paragraph("XACC**2------------------------------ \t" + Convert.ToString(ConfigData.crossCouplingFactors[11])));
            doc.Add(new Paragraph("LACC**2------------------------------ \t" + Convert.ToString(ConfigData.crossCouplingFactors[12])));
            doc.Add(new Paragraph("AX PHASE----------------------------- \t" + Convert.ToString(ConfigData.analogFilter[1])));
            doc.Add(new Paragraph("AL PHASE----------------------------- \t" + Convert.ToString(ConfigData.analogFilter[2])));
            doc.Add(new Paragraph("VCC PHASE---------------------------- \t" + Convert.ToString(ConfigData.analogFilter[4])));
            doc.Add(new Paragraph("MAXIMUM SPRING TENSION VALUE--------- \t" + Convert.ToString(ConfigData.springTensionMax)));

            //Step 6: Closing the Document:
            doc.Close();
        }

        public void LogConfigDataToFileTable()
        {
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times = new Font(bfTimes, 10);

            iTextSharp.text.Font fontTinyItalic = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font font16Normal = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 500f;

            //fix the absolute width of the table
            table.LockedWidth = true;

            //relative col widths in proportions - 1/3 and 2/3
            float[] widths = new float[] { 4f, 1f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;

            PdfPCell cell = new PdfPCell(new Phrase("User defined parameters"));
            PdfPCell cell2 = new PdfPCell(new Phrase("ZLS defined parameters"));

            //  PdfPCell theCell = new PdfPCell(new Paragraph("Configuration Data for " + meterNumber, font16Normal));
            //   theCell.Colspan = 2;
            //  theCell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            //  table.AddCell(theCell);

            //leave a gap before and after the table

            table.SpacingBefore = 50f;
            table.SpacingAfter = 50f;

            //Create a System.IO.FileStream object:
            FileStream fs = new FileStream("C:\\LCT stuff\\Meter Configuration.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            //Step 2: Create a iTextSharp.text.Document object: with page size and margins
            Document doc = new Document(PageSize.LETTER, 36, 36, 36, 36);

            doc.AddTitle("Configuration for meter #: " + Form1.meterNumber);
            doc.AddSubject("Configuration data");
            //  doc.AddKeywords("Metadata, iTextSharp 5.4.4, Chapter 1, Tutorial");
            doc.AddCreator("ZLS");
            doc.AddAuthor("Jack Walker");
            doc.AddHeader("Nothing", "No Header");// Add creation date

            //Step 3: Create a iTextSharp.text.pdf.PdfWriter object. It helps to write the Document to the Specified FileStream:
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            //Step 4: Openning the Document:
            doc.Open();

            doc.Add(new Paragraph("", fontTinyItalic));
            doc.Add(new Paragraph("Configuration Data for " + Form1.meterNumber, font16Normal));
            doc.Add(new Paragraph("", fontTinyItalic));

            cell.Colspan = 2;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            //Step 5: Adding a Paragraph by creating a iTextSharp.text.Paragraph object:

            //  table.AddCell(new PdfPCell(new Paragraph(new PdfPCell(new Paragraph(Label1.Text, fontTinyItalic)));

            //  table.AddCell(new PdfPCell(new Paragraph("User defined parameters", fontTinyItalic));
            table.AddCell(new PdfPCell(new Paragraph("Number of auxillary analog channels", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.numAuxChan), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("DIGITAL INPUT SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.digitalInputSwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("MONITOR DISPLAY SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.monitorDisplaySwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("LINE PRINTER SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.linePrinterSwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("FILE NAME SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.fileNameSwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("HARD DISK SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.hardDiskSwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("SERIAL PORT FORMAT SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.serialPortSwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("SERIAL PORT OUTPUT SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.serialPortOutputSwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("ALARM SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.alarmSwitch), fontTinyItalic)));

            if (ConfigData.printerEmulationSwitch == 2)
            {
                table.AddCell(new PdfPCell(new Paragraph("PRINTER EMULATION", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("ESC_P", fontTinyItalic)));
            }
            if (ConfigData.printerEmulationSwitch == 3)
            {
                table.AddCell(new PdfPCell(new Paragraph("PRINTER EMULATION", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("ESC_P2", fontTinyItalic)));
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("PRINTER EMULATION", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("DPL24C", fontTinyItalic)));
            }
            if (ConfigData.modeSwitch == 0)
            {
                table.AddCell(new PdfPCell(new Paragraph("MODE SWITCH", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("Marine", fontTinyItalic)));
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("MODE SWITCH", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("Hires", fontTinyItalic)));
            }

            // Items specified by ZLS
            cell2.Colspan = 2;
            cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell2);

            table.AddCell(new PdfPCell(new Paragraph("BEAM SCALE FACTOR", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.beamScale, 6)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS PERIOD", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossPeriod, 4)), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS DAMPING", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossDampFactor, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS GAIN", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossGain, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS LEAD", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossLead, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS COMPENSATION (4 inch)", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[13], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS COMPENSATION PHASE (4 inch)", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[5], 4)), fontTinyItalic)));
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS COMPENSATION (16 inch)", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("N/A", fontTinyItalic)));
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS COMPENSATION (16 inch)", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[15], 4)), fontTinyItalic)));
            }

            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS COMPENSATION PHASE 16 inch", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("N/A", fontTinyItalic)));
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS COMPENSATION PHASE 16 inch", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[7], 4)), fontTinyItalic)));
            }
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS BIAS ", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossBias, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS PERIOD ", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.longPeriod, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS DAMPING ", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.longDampFactor, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS GAIN ", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.longGain, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS LEAD ", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.longLead, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS COMPENSATION (4 inch )", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[14], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LONG AXIS COMPENSATION PHASE (4 inch)", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[6])), fontTinyItalic)));

            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS COMPENSATION (16 inch)", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("N/A", fontTinyItalic)));
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS COMPENSATION (16 inch) ", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[16], 4)), fontTinyItalic)));
            }
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS COMPENSATION PHASE (16 inch)", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("N/A", fontTinyItalic)));
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS COMPENSATION PHASE (16 inch)", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[7], 4)), fontTinyItalic)));
            }
            table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS BIAS", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.longBias, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("VCC", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[6], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("AL", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[7], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("AX", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[8], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("VE", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[9], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("AX2", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[10], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("XACC**2", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[11], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LACC**2", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[12], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("AX PHASE", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[1], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("AL PHASE", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[2], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("VCC PHASE", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[4], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("MAXIMUM SPRING TENSION VALUE", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.springTensionMax, 4)), fontTinyItalic)));
            doc.Add(table);

            //Step 6: Closing the Document:
            doc.Close();
        }

    
        public void RecordDataToFile(string fileOperation)
        {
            // fileOperation  0 - open,  1 - append, 2 - close
            string delimitor = ",";

            switch (Form1.fileType)
            {
                case  "csv":
                    delimitor = ",";
                    break;
                case "tsv":
                    delimitor = "\t";
                    break;
                case "txt":
                    delimitor = " ";
                    break;
                default:
                    break;
            }

            string fileName; 

            if (Form1.gravityFileName == null)
            {
                DateTime now = DateTime.Now;
               // String.Format("{0:dds}", now);
                fileName = "C:\\Ultrasys\\Data\\" + "GravityData" + now.ToString("yyyyMMddHHmmsstt") + ".csv";
            }
            else
            {
                fileName = "C:\\Ultrasys\\Data\\" + Form1.gravityFileName;
            }


            if (fileOperation == "Open")
            {
                // create a writer and open the file
                // TextWriter tw = new StreamWriter("C:\\LCT stuff\\Meter data.csv");
                // string header = "Date, Gravity, Spring tension, cross coupling, raw beam, VCC or CML, AL, AX, VE, AX2 or CMX, XACC2, LACC2, XACC, LACC, Parallel port, Platform period,AUX1, AUX2,  AUX3, AUX4";
                // tw.WriteLine(header);

                using (StreamWriter writer = File.CreateText(fileName))
                {
                    string header = "Date" + delimitor +  "Gravity" + delimitor +  "Spring Tension" + delimitor +  "Cross coupling" + delimitor +  "Raw Beam" + delimitor +  "VCC or CML" + delimitor + "AL" + delimitor +  "AX" + delimitor +  "VE" + delimitor +  "AX2 or CMX" + delimitor +  "XACC2" + delimitor +  "LACC2" + delimitor +  "XACC" + delimitor +  "LACC" + delimitor +  "Parallel Port" + delimitor +  "Platform Period" + delimitor + "AUX1" + delimitor +  "AUX2" + delimitor +   "AUX3" + delimitor +  "AUX4";
                    writer.WriteLine(header);
                }
            }
            else if (fileOperation == "Append")
            {
                string myString = Convert.ToString(MeterData.dataTime) + delimitor + Convert.ToString(MeterData.data4[2] * ConfigData.beamScale)
                    + delimitor + Convert.ToString(MeterData.data4[3]) + delimitor + Convert.ToString(MeterData.data4[4])
                    + delimitor + Convert.ToString(MeterData.data1[5]) + delimitor + Convert.ToString(MeterData.data1[6])
                    + delimitor + Convert.ToString(MeterData.data1[7]) + delimitor + Convert.ToString(MeterData.data1[8])
                    + delimitor + Convert.ToString(MeterData.data1[9]) + delimitor + Convert.ToString(MeterData.data1[10])
                    + delimitor + Convert.ToString(MeterData.data1[11]) + delimitor + Convert.ToString(MeterData.data1[12])
                    + delimitor + Convert.ToString(MeterData.data1[13]) + delimitor + Convert.ToString(MeterData.data1[14])
                    + delimitor + "I4PAR" + delimitor + "APERX * 1.0E6"
                   
                     + delimitor + Convert.ToString(MeterData.data1[15]) + delimitor + Convert.ToString(MeterData.data1[16])
                     + delimitor + Convert.ToString(MeterData.data1[17]) + delimitor + Convert.ToString(MeterData.data1[18]);

                using (StreamWriter writer = File.AppendText(fileName))
                {
                    // writer.WriteLine("{0},{1},{2},{3},{4}, {5},{6}", MeterData.year, MeterData.day, MeterData.Hour, MeterData.Min, MeterData.Sec, MeterData.data4[2]);
                    //  writer.WriteLine(Convert.ToString(MeterData.dataTime), ",",Convert.ToString(MeterData.data4[2]),",");
                    writer.WriteLine(myString);
                }
            }
        }
    }
}