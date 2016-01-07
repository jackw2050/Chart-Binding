using FileHelpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

//  Delimited file operations using FileHelpers  http://www.filehelpers.net
// iTextSharp  http://www.mikesdotnetting.com/article/89/itextsharp-page-layout-with-columns

namespace ChartBinding
{
    public partial class Form1 : Form
    {
        #region Declarations

        public static DataTable dataTable = new DataTable();
        public static Boolean engineerDebug = false;
        public static double cper = 18;
        public static string lineId;
        public static string surveyName = null;
        public static string fileType;
        public static int fileDateFormat = 1;
        public static string meterNumber;
        public static string gravityFileName;
        public static bool firstTime = true;
        public static DateTime fileStartTime;
        public static Boolean surveyNameSet = false;
        public static string customFileName;
        public static string fileName = "";
        public static string filePath = "c:\\Ultrasys\\data\\";

        public EngineeringForm EngineeringForm = new EngineeringForm();
        public FileClass FileClass = new FileClass();

        private delegate void SetChartCallback(object meterData);

        public static double[] table1 = {
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

        public static double[] table2 = {
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

        //                              0    1      2     3     4    5      6     7     8     9     10
        private byte[] freshData1 = { 0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2B, 0x2F, 0x24, 0x3E, 0x9D, 0x45, 0xA8, 0xBA, 0xB9, 0xC4, 0x7C, 0xBA, 0x83, 0xC1, 0xF6, 0x7C, 0x2F, 0xBE, 0x72, 0x68, 0x54, 0xBD, 0x7E, 0x29, 0x1C, 0x3D, 0xC2, 0x69, 0x5A, 0xB8, 0xBC, 0xF4, 0xB9, 0x3D, 0x7E, 0xA6, 0x9F, 0x3E, 0x9B, 0xD1, 0xCF, 0xBE, 0x54, 0xE1, 0xAB, 0x41, 0xF9, 0xFE, 0xC6, 0xFE, 0x2F, 0xFF, 0x37, 0xFF, 0xFF, 0xFF, 0xFF, 0xBF, 0xB7, 0xB1, 0x48, 0x3D, 0x96, 0x91, 0xBF, 0x69, 0x40, 0x58, 0xBE, 0x00, 0xFF, 0x2B };

        private byte[] freshData2 = { 0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2B, 0x30, 0x24, 0x3E, 0x9D, 0x45, 0x26, 0x5A, 0xBB, 0xC4, 0x31, 0x40, 0x90, 0xC1, 0x43, 0xD9, 0xA9, 0xBE, 0x0A, 0x1F, 0x4D, 0xBD, 0x8F, 0x2F, 0xF5, 0x3D, 0x41, 0x17, 0x32, 0xB9, 0xA1, 0x67, 0xAA, 0x3C, 0x0D, 0xFC, 0xBE, 0x3E, 0x98, 0x43, 0x8D, 0x40, 0x74, 0xE5, 0xBB, 0x41, 0xC2, 0xFE, 0xD8, 0xFE, 0x19, 0xFF, 0xC2, 0xFE, 0xFF, 0xFF, 0xFF, 0x98, 0xB7, 0x35, 0x48, 0x08, 0x96, 0x9B, 0xBF, 0x71, 0x40, 0x51, 0xBE, 0x00, 0xFF, 0x11 };
        private byte[] freshData3 = { 0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2B, 0x32, 0x24, 0x3E, 0x9D, 0x45, 0x71, 0xF4, 0xBE, 0xC4, 0x08, 0xB1, 0x8F, 0xC1, 0xF4, 0x56, 0x81, 0xBE, 0xBF, 0x71, 0xB0, 0xBD, 0x40, 0x75, 0x9B, 0x3D, 0x8F, 0xC0, 0x21, 0xBA, 0x3F, 0x4D, 0x7F, 0x3D, 0xEB, 0xD0, 0xB6, 0x3E, 0xC4, 0x85, 0xE4, 0x40, 0x89, 0x25, 0xB6, 0x41, 0xF1, 0xFE, 0xC2, 0xFE, 0x10, 0xFF, 0x3D, 0xFF, 0xFF, 0xFF, 0xFF, 0x50, 0xB8, 0xA0, 0x48, 0x75, 0x99, 0xB0, 0xBF, 0x89, 0x40, 0x72, 0xBE, 0x00, 0xF3, 0x9F };
        private byte[] freshData4 = { 0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2B, 0x34, 0x24, 0x3E, 0x9D, 0x45, 0x43, 0xAA, 0xC2, 0xC4, 0x85, 0x37, 0x91, 0xC1, 0xAC, 0x40, 0x65, 0xBE, 0xA2, 0x18, 0x99, 0xBD, 0xB1, 0xA9, 0x71, 0x3D, 0x9A, 0x1C, 0x00, 0xBA, 0x82, 0x75, 0x2E, 0x3D, 0x3B, 0x12, 0xA8, 0x3E, 0x1C, 0x29, 0xD0, 0x40, 0xDC, 0xBC, 0xAF, 0x41, 0xC4, 0xFE, 0x19, 0xFF, 0xC6, 0xFE, 0xE6, 0xFE, 0xFF, 0xFF, 0xFF, 0x1F, 0xB9, 0xB0, 0x47, 0x5D, 0x96, 0x99, 0xBF, 0xAD, 0x40, 0x59, 0xBE, 0x00, 0xF9, 0xB1 };
        private byte[] freshData5 = { 0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2B, 0x36, 0x24, 0x3E, 0x9D, 0x45, 0xD9, 0x25, 0xC6, 0xC4, 0x1C, 0x1C, 0x93, 0xC1, 0x3F, 0x61, 0x78, 0xBE, 0x33, 0xD5, 0xFE, 0xBC, 0x58, 0x74, 0x91, 0x3D, 0xCA, 0x67, 0xB8, 0xB8, 0x9B, 0x94, 0x20, 0x3C, 0x0B, 0x0E, 0xC3, 0x3E, 0x20, 0x38, 0x11, 0x40, 0x89, 0x25, 0xB6, 0x41, 0xDA, 0xFE, 0xF9, 0xFE, 0xC4, 0xFE, 0x31, 0xFF, 0xFF, 0xFF, 0xFF, 0xE9, 0xB8, 0x6C, 0x47, 0x49, 0x96, 0x92, 0xBF, 0xBF, 0x40, 0x2D, 0xBE, 0x00, 0xF9, 0x32 };
        private byte[] freshData6 = { 0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2B, 0x37, 0x24, 0x3E, 0x9D, 0x45, 0x23, 0x0A, 0xC8, 0xC4, 0xA9, 0x89, 0x3A, 0xC1, 0x23, 0x4B, 0x58, 0xBE, 0x06, 0x23, 0xDE, 0x3C, 0x72, 0x72, 0xF9, 0x3D, 0x8E, 0x30, 0x93, 0xB8, 0x9B, 0x38, 0xA8, 0x3C, 0xE0, 0x27, 0xE8, 0x3D, 0xB5, 0xCE, 0x5C, 0xC0, 0x2F, 0x3C, 0x39, 0x41, 0x04, 0xFF, 0x3F, 0xFF, 0x3B, 0xFF, 0x10, 0xFF, 0xFF, 0xFF, 0xFF, 0xC2, 0xB8, 0x04, 0x47, 0xF1, 0x95, 0x8A, 0xBF, 0x9C, 0x40, 0x74, 0xBE, 0x00, 0xF9, 0x65 };
        private byte[] freshData7 = { 0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2B, 0x39, 0x24, 0x3E, 0x9D, 0x45, 0x81, 0x59, 0xCB, 0xC4, 0xAC, 0x67, 0xF6, 0xC0, 0xEF, 0x72, 0xA7, 0xBD, 0xD0, 0xD0, 0x30, 0x3D, 0x9D, 0xF2, 0x46, 0x3D, 0xD7, 0xD0, 0x77, 0xB9, 0xE5, 0x33, 0x1E, 0x3D, 0x62, 0x62, 0x80, 0x3D, 0xEB, 0x5A, 0xB7, 0xC0, 0x26, 0x58, 0x14, 0x41, 0x08, 0xFF, 0x2E, 0xFF, 0x2D, 0xFF, 0x35, 0xFF, 0xFF, 0xFF, 0xFF, 0x24, 0xB8, 0x70, 0x47, 0x1C, 0x96, 0xA4, 0xBF, 0xB0, 0x40, 0x20, 0xBE, 0x00, 0xFD, 0xFC };
        private byte[] freshData8 = { 0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2B, 0x3A, 0x24, 0x3E, 0x9D, 0x45, 0x92, 0xF9, 0xCC, 0xC4, 0x27, 0x9C, 0x05, 0xC1, 0x8A, 0x72, 0x10, 0xBE, 0xCF, 0x4F, 0xB2, 0x3D, 0x18, 0x7C, 0xFE, 0x3D, 0xF8, 0x37, 0x0A, 0xBA, 0x68, 0x05, 0xBF, 0x3C, 0xEA, 0xDA, 0x93, 0x3D, 0xCB, 0x0B, 0xA7, 0xC0, 0xFA, 0x60, 0x1E, 0x41, 0x10, 0xFF, 0x3D, 0xFF, 0xC8, 0xFE, 0xF8, 0xFE, 0xFF, 0xFF, 0xFF, 0x12, 0xB8, 0x14, 0x47, 0xFE, 0x95, 0x92, 0xBF, 0x84, 0x40, 0x82, 0xBE, 0x00, 0xFD, 0x98 };
        private byte[] freshData9 = { 0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2B, 0x3B, 0x24, 0x3E, 0x9D, 0x45, 0x19, 0x9D, 0xCE, 0xC4, 0x69, 0xA3, 0xF2, 0xC0, 0x44, 0xA0, 0xAE, 0xBD, 0xDE, 0xA6, 0x59, 0x3D, 0x9B, 0xEA, 0x74, 0x3D, 0xC2, 0xC1, 0xAF, 0xB9, 0x37, 0x37, 0x3A, 0x3D, 0xC8, 0xD2, 0x5E, 0x3D, 0x8C, 0x01, 0xC8, 0xC0, 0x0F, 0x04, 0x09, 0x41, 0xFD, 0xFE, 0xC4, 0xFE, 0xE2, 0xFE, 0x37, 0xFF, 0xFF, 0xFF, 0xFF, 0xBF, 0xB7, 0xAF, 0x48, 0x10, 0x96, 0x9D, 0xBF, 0x7C, 0x40, 0x68, 0xBE, 0x00, 0xFF, 0x0A };
        private byte[] freshData10 = { 0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x00, 0x24, 0x3E, 0x9D, 0x45, 0xD8, 0x5F, 0xD0, 0xC4, 0x45, 0x6F, 0xF4, 0xC0, 0xC2, 0x32, 0xC6, 0xBD, 0x06, 0x80, 0x44, 0x3D, 0x16, 0x25, 0x91, 0x3D, 0x0D, 0xA5, 0x68, 0xB9, 0xDE, 0x02, 0x8C, 0x3C, 0x5A, 0x40, 0x6B, 0x3D, 0x95, 0xB3, 0x8B, 0xC0, 0xD3, 0xA6, 0x0A, 0x41, 0x35, 0xFF, 0xF1, 0xFE, 0x15, 0xFF, 0x35, 0xFF, 0xFF, 0xFF, 0xFF, 0x61, 0xB7, 0x04, 0x47, 0xC4, 0x95, 0x94, 0xBF, 0xBC, 0x40, 0x2F, 0xBE, 0x00, 0xFF, 0x79 };
        private byte[] freshData11 = { 0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x01, 0x24, 0x3E, 0x9D, 0x45, 0xDB, 0x0D, 0xD2, 0xC4, 0x15, 0xF5, 0xE3, 0xC0, 0x57, 0xC9, 0x94, 0xBD, 0x57, 0x3F, 0x23, 0x3D, 0x5E, 0x37, 0x37, 0x3D, 0x6F, 0x30, 0x23, 0xB9, 0x57, 0xFA, 0x74, 0x3C, 0x33, 0x91, 0x56, 0x3D, 0xD2, 0x8D, 0x6B, 0xC0, 0x48, 0x54, 0x05, 0x41, 0xF2, 0xFE, 0xD9, 0xFE, 0xC2, 0xFE, 0xE8, 0xFE, 0xFF, 0xFF, 0xFF, 0x54, 0xB7, 0x28, 0x47, 0xCC, 0x95, 0xA0, 0xBF, 0x89, 0x40, 0x1F, 0xBE, 0x00, 0xFF, 0x34 };
        private byte[] freshData12 = { 0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x02, 0x24, 0x3E, 0x9D, 0x45, 0xC0, 0xC8, 0xD3, 0xC4, 0xEE, 0xAB, 0xEB, 0xC0, 0x21, 0x85, 0xE8, 0xBD, 0x4B, 0x33, 0x58, 0x3D, 0x91, 0x32, 0xE5, 0x3D, 0xA4, 0x5A, 0x3E, 0xB9, 0x0C, 0xBA, 0x6F, 0x3C, 0x34, 0x18, 0x74, 0x3D, 0x94, 0x02, 0x4C, 0xC0, 0xD4, 0x55, 0x0B, 0x41, 0xC4, 0xFE, 0xBC, 0xFE, 0xD1, 0xFE, 0xC4, 0xFE, 0xFF, 0xFF, 0xFF, 0xB0, 0xB7, 0x95, 0x48, 0xFA, 0x95, 0x7E, 0xBF, 0x71, 0x40, 0x82, 0xBE, 0x00, 0xFF, 0xAA };
        public string[] traceName = { "Digital Gravity", "Spring Tension", "Cross Coupling", "Raw Beam", "Total Correction", "Raw Gravity", "AL", "AX", "VE", "AX2", "LACC", "XACC" };

        #endregion Declarations

        #region Fields

        // Thread Add Data delegate
        public delegate void AddDataDelegate();

        public AddDataDelegate addDataDel;

        #endregion Fields

        public Form1()
        {
            InitializeComponent();
            surveyName = surveyTextBox.Text;
            InitializedataGridView();
            DataRow myDataRow = dataTable.NewRow();
            ConfigData configData = new ConfigData();

            recordingTextBox.Visible = false;
            this.crossCouplingChart.Palette = ChartColorPalette.Pastel;
        }

        private void InitializedataGridView()
        {
            //  SETUP MAIN  DATA GRID
            this.dataGridView1.ColumnCount = 12;
            this.dataGridView1.Columns[0].Name = "Date/ Time";
            this.dataGridView1.Columns[1].Name = "Digital Gravity";
            this.dataGridView1.Columns[2].Name = "Spring Tension";
            this.dataGridView1.Columns[3].Name = "Cross Coupling";
            this.dataGridView1.Columns[4].Name = "Raw Beam";
            this.dataGridView1.Columns[5].Name = "Total Correction";
            // this.dataGridView1.Columns[6].Name = "Raw Gravity";
            this.dataGridView1.Columns[6].Name = "AL";
            this.dataGridView1.Columns[7].Name = "AX";
            this.dataGridView1.Columns[8].Name = "VE";
            this.dataGridView1.Columns[9].Name = "AX2";
            this.dataGridView1.Columns[10].Name = "LACC";
            this.dataGridView1.Columns[11].Name = "XACC";
        }

        public static class mySimData
        {
            public static byte[,] simData =  {
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x04, 0x24, 0x3E, 0x9D, 0x45, 0x13, 0x74, 0xD7, 0xC4, 0x16, 0x1E, 0x33, 0xC2, 0xDC, 0xFC, 0x72, 0x42, 0x8B, 0x65, 0xCB, 0xC1, 0x5B, 0x55, 0x78, 0x3C, 0x6B, 0x15, 0x9E, 0xBF, 0x4A, 0xF7, 0xBF, 0x42, 0xE0, 0x80, 0xC7, 0x43, 0x24, 0xEB, 0x4B, 0x42, 0x11, 0xD9, 0x72, 0x42, 0xE8, 0xFE, 0x39, 0xFF, 0xD2, 0xFE, 0xDA, 0xFE, 0xFF, 0xFF, 0xFF, 0x08, 0xB9, 0xAD, 0x48, 0xF1, 0x95, 0x99, 0xBF, 0x8D, 0x40, 0x78, 0xBE, 0x00, 0xF9, 0x43 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x05, 0x24, 0x3E, 0x9D, 0x45, 0x44, 0x24, 0xD9, 0xC4, 0xC1, 0x30, 0xC3, 0x40, 0x13, 0x7E, 0x2C, 0x3E, 0xC5, 0x56, 0x88, 0xBE, 0x4A, 0xB8, 0x9B, 0x3D, 0x2C, 0x3A, 0x9F, 0xBB, 0xA3, 0xC2, 0x47, 0x40, 0xA6, 0xE5, 0x25, 0x40, 0x39, 0x4F, 0xAE, 0x41, 0x83, 0x9E, 0x02, 0xC1, 0x1E, 0xFF, 0x18, 0xFF, 0x3B, 0xFF, 0xC4, 0xFE, 0xFF, 0xFF, 0xFF, 0xD9, 0xB8, 0x34, 0x47, 0xF0, 0x95, 0xB0, 0xBF, 0xB8, 0x40, 0x42, 0xBE, 0x00, 0xF9, 0x41 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x06, 0x24, 0x3E, 0x9D, 0x45, 0x75, 0x1E, 0xDB, 0xC4, 0xF5, 0x43, 0xE0, 0x40, 0xC2, 0x4C, 0xB0, 0x3D, 0x95, 0x57, 0x88, 0xBE, 0x5A, 0x0D, 0x15, 0x3E, 0xEA, 0xB2, 0x53, 0xBB, 0xEE, 0x46, 0xE1, 0x3E, 0xD4, 0xCA, 0x0F, 0x40, 0xDF, 0x67, 0x51, 0x41, 0x8F, 0x46, 0xC9, 0xC0, 0xDC, 0xFE, 0x20, 0xFF, 0x35, 0xFF, 0xC2, 0xFE, 0xFF, 0xFF, 0xFF, 0xF9, 0xB8, 0x46, 0x47, 0xE4, 0x98, 0xA2, 0xBF, 0xC2, 0x40, 0x43, 0xBE, 0x00, 0xF1, 0x38 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x07, 0x24, 0x3E, 0x9D, 0x45, 0xF0, 0xF5, 0xDC, 0xC4, 0x7C, 0xB4, 0xDA, 0x40, 0x76, 0x61, 0xF1, 0x3D, 0x6E, 0x34, 0x60, 0xBD, 0xEF, 0x61, 0x43, 0x3D, 0x56, 0x31, 0x06, 0xBA, 0x79, 0x4B, 0x57, 0x3E, 0xFA, 0x61, 0xD2, 0x3F, 0x5C, 0x31, 0x0F, 0x41, 0x57, 0x3F, 0xAD, 0xC0, 0x07, 0xFF, 0x30, 0xFF, 0x09, 0xFF, 0x20, 0xFF, 0xFF, 0xFF, 0xFF, 0xF0, 0xB8, 0xA6, 0x48, 0x23, 0x99, 0x9F, 0xBF, 0x82, 0x40, 0x6A, 0xBE, 0x00, 0xF1, 0x7C },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x08, 0x24, 0x3E, 0x9D, 0x45, 0xE3, 0x90, 0xDE, 0xC4, 0xDB, 0x79, 0xBF, 0x40, 0x4F, 0x56, 0x10, 0x3E, 0x18, 0x26, 0x13, 0xBD, 0x06, 0x02, 0xC8, 0x3D, 0x80, 0x8B, 0x57, 0xB9, 0x2B, 0xE8, 0x87, 0x3E, 0x7D, 0x8C, 0xF5, 0x3F, 0x15, 0xF2, 0x8B, 0x40, 0x6B, 0xCF, 0xF7, 0xC0, 0xCC, 0xFE, 0x37, 0xFF, 0x39, 0xFF, 0x2E, 0xFF, 0xFF, 0xFF, 0xFF, 0x04, 0xB9, 0xA7, 0x47, 0xE4, 0x98, 0xC4, 0xBF, 0x89, 0x40, 0x91, 0xBE, 0x00, 0xF1, 0x5C },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x09, 0x24, 0x3E, 0x9D, 0x45, 0x66, 0x20, 0xE0, 0xC4, 0x5C, 0x88, 0xF2, 0x40, 0x75, 0x0F, 0x7F, 0x3D, 0xB5, 0x1C, 0xE6, 0xBC, 0x5C, 0xEC, 0x9B, 0x3D, 0x8C, 0x90, 0xAE, 0xB8, 0x02, 0x8A, 0x9B, 0x3E, 0xF1, 0x10, 0x22, 0x40, 0x19, 0x18, 0x0E, 0x40, 0x1D, 0xC1, 0x0F, 0xC1, 0xE4, 0xFE, 0xD8, 0xFE, 0xF0, 0xFE, 0xC2, 0xFE, 0xFF, 0xFF, 0xFF, 0xB4, 0xB8, 0x10, 0x47, 0xCC, 0x95, 0xA8, 0xBF, 0xB7, 0x40, 0x2F, 0xBE, 0x00, 0xF9, 0x20 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x0A, 0x24, 0x3E, 0x9D, 0x45, 0x6E, 0xBF, 0xE1, 0xC4, 0xA5, 0xD9, 0x08, 0x41, 0x66, 0x88, 0x32, 0x3E, 0x9C, 0x66, 0x34, 0x3C, 0x83, 0x75, 0x2F, 0x3D, 0x02, 0x82, 0xE4, 0xB7, 0x13, 0x95, 0xA9, 0x3D, 0x76, 0xAE, 0x8C, 0x3F, 0x34, 0x60, 0x1B, 0xC0, 0x85, 0xFE, 0x23, 0xC1, 0xC2, 0xFE, 0xCC, 0xFE, 0xC8, 0xFE, 0x3D, 0xFF, 0xFF, 0xFF, 0xFF, 0x12, 0xB8, 0x0C, 0x47, 0x10, 0x96, 0xA0, 0xBF, 0x71, 0x40, 0x78, 0xBE, 0x00, 0xFD, 0x86 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x0B, 0x24, 0x3E, 0x9D, 0x45, 0x6D, 0x62, 0xE3, 0xC4, 0xA8, 0x39, 0x1D, 0x41, 0xF5, 0x00, 0x1D, 0x3E, 0xB0, 0xEB, 0xA1, 0x3D, 0x03, 0xC7, 0xC9, 0x3D, 0xED, 0x6F, 0x01, 0xBA, 0xC5, 0xF9, 0xD0, 0x3D, 0x1B, 0xF3, 0x20, 0x3F, 0xAD, 0xE6, 0xD8, 0xC0, 0xF7, 0x1B, 0xFE, 0xC0, 0xBC, 0xFE, 0xE1, 0xFE, 0xCA, 0xFE, 0x28, 0xFF, 0xFF, 0xFF, 0xFF, 0x99, 0xB7, 0x04, 0x47, 0x08, 0x96, 0x9D, 0xBF, 0x72, 0x40, 0x7A, 0xBE, 0x00, 0xFF, 0xBA },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x0C, 0x24, 0x3E, 0x9D, 0x45, 0xB5, 0x30, 0xE5, 0xC4, 0xCB, 0xF4, 0x28, 0x41, 0xA1, 0x7E, 0xFD, 0x3D, 0xA7, 0xE5, 0x96, 0x3D, 0x97, 0x23, 0x5A, 0x3D, 0xFB, 0x4D, 0x23, 0xBA, 0xB0, 0x5A, 0xD1, 0x3D, 0xF2, 0xC7, 0x40, 0x3F, 0x89, 0xF0, 0x05, 0xC1, 0xCE, 0x33, 0x49, 0xC1, 0x3B, 0xFF, 0x07, 0xFF, 0x39, 0xFF, 0x29, 0xFF, 0xFF, 0xFF, 0xFF, 0x84, 0xB7, 0x64, 0x47, 0x43, 0x96, 0x9F, 0xBF, 0xAF, 0x40, 0x1B, 0xBE, 0x00, 0xFF, 0x81 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x0D, 0x24, 0x3E, 0x9D, 0x45, 0x53, 0xEB, 0xE6, 0xC4, 0xFC, 0x0E, 0x20, 0x41, 0x17, 0x0D, 0x17, 0x3E, 0xDD, 0xA9, 0x15, 0x3E, 0x04, 0x40, 0xF0, 0x3D, 0xF8, 0x69, 0xC6, 0xBA, 0xC7, 0xFC, 0x04, 0x3E, 0x42, 0x37, 0x3A, 0x3F, 0x0A, 0xE0, 0x26, 0xC1, 0x5A, 0xCF, 0x2E, 0xC1, 0x04, 0xFF, 0x20, 0xFF, 0x17, 0xFF, 0x08, 0xFF, 0xFF, 0xFF, 0xFF, 0xCD, 0xB7, 0xBE, 0x47, 0xE1, 0x95, 0xA8, 0xBF, 0xB0, 0x40, 0x48, 0xBE, 0x00, 0xFF, 0xA5 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x0E, 0x24, 0x3E, 0x9D, 0x45, 0x4A, 0xA1, 0xE8, 0xC4, 0xA0, 0xA5, 0x29, 0x41, 0xB1, 0xD1, 0xD2, 0x3D, 0xFC, 0x03, 0x28, 0x3E, 0x42, 0x75, 0x8B, 0x3D, 0xF0, 0x62, 0x0C, 0xBB, 0xB9, 0xDE, 0x48, 0x3E, 0xB0, 0x84, 0x0C, 0x3F, 0x96, 0x2E, 0x4D, 0xC1, 0xAD, 0x35, 0x38, 0xC1, 0xE4, 0xFE, 0xC2, 0xFE, 0xD8, 0xFE, 0xC8, 0xFE, 0xFF, 0xFF, 0xFF, 0xB1, 0xB7, 0x5C, 0x47, 0xE1, 0x95, 0x94, 0xBF, 0x7A, 0x40, 0x70, 0xBE, 0x00, 0xFF, 0xD7 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x0F, 0x24, 0x3E, 0x9D, 0x45, 0x89, 0x4A, 0xEA, 0xC4, 0xA8, 0x77, 0x2B, 0x41, 0x49, 0xB3, 0x0C, 0x3E, 0xB4, 0x69, 0x1C, 0x3E, 0x18, 0x71, 0xC7, 0x3D, 0x61, 0x6F, 0x05, 0xBB, 0x9F, 0xB3, 0x25, 0x3E, 0xBA, 0xB7, 0x04, 0x3F, 0x5C, 0x48, 0x50, 0xC1, 0x66, 0x0F, 0x35, 0xC1, 0xE8, 0xFE, 0x3F, 0xFF, 0x19, 0xFF, 0xC4, 0xFE, 0xFF, 0xFF, 0xFF, 0x70, 0xB8, 0x37, 0x48, 0x3E, 0x96, 0x7D, 0xBF, 0x89, 0x40, 0x7A, 0xBE, 0x00, 0xFB, 0xF6 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x10, 0x24, 0x3E, 0x9D, 0x45, 0xA5, 0x4E, 0xEC, 0xC4, 0x9B, 0x17, 0x1B, 0x41, 0xF2, 0x74, 0x05, 0x3E, 0xA2, 0x56, 0x6B, 0x3E, 0x62, 0xB3, 0xFE, 0x3D, 0x7A, 0xCC, 0x68, 0xBB, 0x8A, 0x73, 0x9C, 0x3E, 0x42, 0x9D, 0xBB, 0x3F, 0x7F, 0xF5, 0x80, 0xC1, 0x00, 0x7F, 0x01, 0xC1, 0x29, 0xFF, 0xDA, 0xFE, 0x08, 0xFF, 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 0x1F, 0xB9, 0x3A, 0x47, 0xE1, 0x95, 0x7A, 0xBF, 0x9C, 0x40, 0x27, 0xBE, 0x00, 0xF9, 0x8B },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x11, 0x24, 0x3E, 0x9D, 0x45, 0x91, 0x4E, 0xEE, 0xC4, 0xFE, 0x50, 0x21, 0x41, 0x4D, 0xA0, 0xC8, 0x3D, 0x57, 0xC3, 0x62, 0x3E, 0xFB, 0x0A, 0x83, 0x3D, 0x2E, 0x7B, 0x9F, 0xBB, 0x91, 0x67, 0xD1, 0x3E, 0xDD, 0xA3, 0x91, 0x3F, 0x9E, 0xB0, 0xB0, 0xC1, 0x71, 0x21, 0x1A, 0xC1, 0xE9, 0xFE, 0xF9, 0xFE, 0xBC, 0xFE, 0x03, 0xFF, 0xFF, 0xFF, 0xFF, 0xF1, 0xB8, 0xB5, 0x48, 0x59, 0x96, 0x89, 0xBF, 0x91, 0x40, 0x69, 0xBE, 0x00, 0xF9, 0x48 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x12, 0x24, 0x3E, 0x9D, 0x45, 0xE4, 0x2B, 0xF0, 0xC4, 0x4D, 0x1F, 0x1C, 0x41, 0x83, 0xC6, 0x39, 0x3E, 0x86, 0x42, 0x9B, 0x3E, 0x64, 0x20, 0xD5, 0x3D, 0x09, 0xA5, 0xDF, 0xBB, 0x77, 0x77, 0xBD, 0x3E, 0xD7, 0x66, 0x44, 0x3F, 0xD9, 0x5E, 0xAE, 0xC1, 0x83, 0x1D, 0x23, 0xC1, 0xC4, 0xFE, 0x20, 0xFF, 0x30, 0xFF, 0x10, 0xFF, 0xFF, 0xFF, 0xFF, 0xF1, 0xB8, 0x3E, 0x47, 0xD2, 0x95, 0x94, 0xBF, 0x9E, 0x40, 0x58, 0xBE, 0x00, 0xF9, 0xA9 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x13, 0x24, 0x3E, 0x9D, 0x45, 0x18, 0xE4, 0xF1, 0xC4, 0x34, 0x0A, 0x19, 0x41, 0x0D, 0xCD, 0xC9, 0x3D, 0x7F, 0xF7, 0xAD, 0x3E, 0x5C, 0x22, 0xE2, 0x3D, 0x3E, 0x7C, 0x07, 0xBC, 0xAA, 0x86, 0xE5, 0x3E, 0x05, 0x75, 0x78, 0x3F, 0x23, 0x93, 0xC3, 0xC1, 0x32, 0xCA, 0x1A, 0xC1, 0x30, 0xFF, 0x3F, 0xFF, 0x1C, 0xFF, 0x29, 0xFF, 0xFF, 0xFF, 0xFF, 0xC2, 0xB8, 0x30, 0x47, 0xC4, 0x95, 0xAC, 0xBF, 0xB8, 0x40, 0x64, 0xBE, 0x00, 0xF9, 0xB9 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x14, 0x24, 0x3E, 0x9D, 0x45, 0xA0, 0x9F, 0xF3, 0xC4, 0xD1, 0xB1, 0x21, 0x41, 0x70, 0x52, 0x68, 0x3D, 0x9F, 0x32, 0x92, 0x3E, 0x01, 0xA1, 0x80, 0x3D, 0x37, 0x79, 0x01, 0xBC, 0x7F, 0xF7, 0x11, 0x3F, 0x18, 0x29, 0xB6, 0x3F, 0xB1, 0xC1, 0xDA, 0xC1, 0xC7, 0x17, 0x25, 0xC1, 0xF2, 0xFE, 0xD1, 0xFE, 0x21, 0xFF, 0xBA, 0xFE, 0xFF, 0xFF, 0xFF, 0x10, 0xB9, 0xB5, 0x48, 0x4F, 0x96, 0x91, 0xBF, 0x7D, 0x40, 0x3E, 0xBE, 0x00, 0xF9, 0x16 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x15, 0x24, 0x3E, 0x9D, 0x45, 0x9F, 0x51, 0xF5, 0xC4, 0x4E, 0xA5, 0x28, 0x41, 0xD4, 0x51, 0x38, 0x3E, 0x76, 0x16, 0x91, 0x3E, 0x9A, 0x9A, 0x61, 0x3D, 0xFC, 0xA1, 0x0F, 0xBC, 0x7F, 0xC3, 0x5B, 0x3F, 0x00, 0x9C, 0x45, 0x40, 0x27, 0x20, 0xF6, 0xC1, 0x6F, 0x29, 0x39, 0xC1, 0xD8, 0xFE, 0xF0, 0xFE, 0x1B, 0xFF, 0xF0, 0xFE, 0xFF, 0xFF, 0xFF, 0xC8, 0xB8, 0x08, 0x47, 0xCA, 0x95, 0xB0, 0xBF, 0x9D, 0x40, 0x7A, 0xBE, 0x00, 0xF9, 0x99 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x16, 0x24, 0x3E, 0x9D, 0x45, 0xBB, 0x10, 0xF7, 0xC4, 0x87, 0x99, 0x37, 0x41, 0x6D, 0x83, 0x22, 0x3E, 0xE8, 0x2E, 0xCC, 0x3E, 0x15, 0xCB, 0xB2, 0x3D, 0xDF, 0xB6, 0x5A, 0xBC, 0x38, 0x55, 0x5B, 0x3F, 0xB3, 0xD1, 0x8A, 0x3F, 0xEA, 0x76, 0x06, 0xC2, 0xE9, 0xFA, 0x15, 0xC1, 0xD0, 0xFE, 0x20, 0xFF, 0x19, 0xFF, 0xC0, 0xFE, 0xFF, 0xFF, 0xFF, 0x11, 0xB8, 0x2C, 0x47, 0x04, 0x99, 0xA2, 0xBF, 0xBD, 0x40, 0x47, 0xBE, 0x00, 0xF3, 0xAE },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x17, 0x24, 0x3E, 0x9D, 0x45, 0x95, 0xD3, 0xF8, 0xC4, 0x40, 0xAA, 0x32, 0x41, 0x0A, 0x30, 0xDE, 0x3D, 0xBA, 0x89, 0xD9, 0x3E, 0x3F, 0x58, 0xA2, 0x3D, 0x88, 0xC4, 0x81, 0xBC, 0xE1, 0x6E, 0x6F, 0x3F, 0x1F, 0xED, 0x3C, 0x3F, 0xDC, 0x56, 0x0F, 0xC2, 0xF1, 0x93, 0x3A, 0xC1, 0x11, 0xFF, 0x3F, 0xFF, 0xFD, 0xFE, 0xCC, 0xFE, 0xFF, 0xFF, 0xFF, 0xC9, 0xB7, 0x71, 0x47, 0xF0, 0x98, 0xAF, 0xBF, 0xB0, 0x40, 0x3D, 0xBE, 0x00, 0xF7, 0xA6 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x18, 0x24, 0x3E, 0x9D, 0x45, 0x3C, 0xA0, 0xFA, 0xC4, 0xD6, 0x58, 0x44, 0x41, 0x9A, 0x96, 0xFE, 0x3D, 0x98, 0x13, 0xFD, 0x3E, 0x09, 0xEA, 0xDE, 0x3D, 0x2A, 0x97, 0x93, 0xBC, 0xE6, 0x0B, 0x70, 0x3F, 0x3E, 0x93, 0x80, 0x3F, 0xCC, 0x2F, 0x0F, 0xC2, 0x9F, 0x14, 0x31, 0xC1, 0xB8, 0xFE, 0xF1, 0xFE, 0xC4, 0xFE, 0x2B, 0xFF, 0xFF, 0xFF, 0xFF, 0x78, 0xB7, 0x3C, 0x47, 0xEC, 0x98, 0xA4, 0xBF, 0x7C, 0x40, 0x82, 0xBE, 0x00, 0xF7, 0xC7 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x19, 0x24, 0x3E, 0x9D, 0x45, 0xC8, 0x89, 0xFC, 0xC4, 0x20, 0xA5, 0x4C, 0x41, 0x0F, 0xFE, 0x00, 0x3E, 0x8E, 0xE0, 0xDA, 0x3E, 0xBC, 0x3D, 0x9B, 0x3D, 0xBE, 0xEC, 0x87, 0xBC, 0x8E, 0x30, 0x89, 0x3F, 0xE0, 0x6A, 0x91, 0x3F, 0xDF, 0x25, 0x19, 0xC2, 0x50, 0xEF, 0x49, 0xC1, 0x1D, 0xFF, 0x09, 0xFF, 0x09, 0xFF, 0xE1, 0xFE, 0xFF, 0xFF, 0xFF, 0x62, 0xB7, 0x6E, 0x47, 0x7D, 0x99, 0x99, 0xBF, 0x71, 0x40, 0x3D, 0xBE, 0x00, 0xF7, 0xD2 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x1A, 0x24, 0x3E, 0x9D, 0x45, 0x99, 0x36, 0xFE, 0xC4, 0xEE, 0xD1, 0x3A, 0x41, 0x6A, 0xA8, 0x67, 0x3E, 0x73, 0xD4, 0x08, 0x3F, 0x57, 0x60, 0xDF, 0x3D, 0x5F, 0xD3, 0xA6, 0xBC, 0x14, 0xD2, 0x80, 0x3F, 0xFA, 0xA6, 0x04, 0x3F, 0xAA, 0x71, 0x16, 0xC2, 0xC3, 0x70, 0x43, 0xC1, 0x0D, 0xFF, 0x08, 0xFF, 0x20, 0xFF, 0x39, 0xFF, 0xFF, 0xFF, 0xFF, 0x7D, 0xB7, 0x1C, 0x47, 0xE4, 0x95, 0xA0, 0xBF, 0xB0, 0x40, 0x48, 0xBE, 0x00, 0xFF, 0x34 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x1B, 0x24, 0x3E, 0x9D, 0x45, 0x0D, 0xED, 0xFF, 0xC4, 0xBF, 0x7A, 0x41, 0x41, 0x4A, 0xDA, 0x97, 0x3D, 0xBE, 0x3D, 0xDA, 0x3E, 0x7F, 0x2D, 0x81, 0x3D, 0xC7, 0x7E, 0x91, 0xBC, 0x99, 0xA9, 0x97, 0x3F, 0xD2, 0x49, 0x74, 0x3F, 0x46, 0xC0, 0x24, 0xC2, 0x05, 0x0B, 0x24, 0xC1, 0xF1, 0xFE, 0xC2, 0xFE, 0xCC, 0xFE, 0xE6, 0xFE, 0xFF, 0xFF, 0xFF, 0x31, 0xB8, 0xAF, 0x48, 0x5E, 0x96, 0x7C, 0xBF, 0xA0, 0x40, 0x20, 0xBE, 0x00, 0xFD, 0x93 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x1C, 0x24, 0x3E, 0x9D, 0x45, 0x8B, 0xDD, 0x00, 0xC5, 0x88, 0xA3, 0x3C, 0x41, 0x6C, 0x44, 0x62, 0x3E, 0xE2, 0xED, 0x08, 0x3F, 0xBB, 0x46, 0xC1, 0x3D, 0x24, 0xB7, 0xB4, 0xBC, 0xC2, 0x29, 0x9B, 0x3F, 0x7D, 0xB4, 0xB2, 0x3F, 0x4C, 0xD3, 0x1F, 0xC2, 0x8E, 0x95, 0x28, 0xC1, 0xC4, 0xFE, 0xE8, 0xFE, 0x01, 0xFF, 0xE8, 0xFE, 0xFF, 0xFF, 0xFF, 0xC4, 0xB8, 0x04, 0x47, 0xD4, 0x95, 0xA1, 0xBF, 0xA4, 0x40, 0x48, 0xBE, 0x00, 0xF9, 0x76 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x1D, 0x24, 0x3E, 0x9D, 0x45, 0xEA, 0xD6, 0x01, 0xC5, 0x23, 0xFB, 0x3E, 0x41, 0xE8, 0x16, 0x07, 0x3E, 0xD1, 0x86, 0x15, 0x3F, 0x3C, 0x70, 0xCC, 0x3D, 0xB6, 0xF4, 0xD5, 0xBC, 0x2D, 0x9B, 0xB2, 0x3F, 0xBA, 0xE8, 0x3F, 0x3F, 0x80, 0xF2, 0x31, 0xC2, 0xE9, 0x15, 0x36, 0xC1, 0x1F, 0xFF, 0x29, 0xFF, 0xC2, 0xFE, 0xE8, 0xFE, 0xFF, 0xFF, 0xFF, 0x1A, 0xB9, 0x1D, 0x48, 0x4F, 0x96, 0x7E, 0xBF, 0x91, 0x40, 0x6C, 0xBE, 0x00, 0xF9, 0x08 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x1E, 0x24, 0x3E, 0x9D, 0x45, 0xCB, 0xCF, 0x02, 0xC5, 0xEA, 0x64, 0x3C, 0x41, 0xE6, 0x7A, 0x1C, 0x3E, 0x81, 0x72, 0x2D, 0x3F, 0x65, 0x88, 0x06, 0x3E, 0x6D, 0x2F, 0xFE, 0xBC, 0xBB, 0x56, 0xB1, 0x3F, 0x45, 0x88, 0x81, 0x3F, 0x2E, 0xCB, 0x30, 0xC2, 0xFC, 0xC0, 0x3F, 0xC1, 0xF9, 0xFE, 0x0D, 0xFF, 0xC4, 0xFE, 0xF0, 0xFE, 0xFF, 0xFF, 0xFF, 0xFE, 0xB8, 0xB5, 0x48, 0x5D, 0x96, 0x99, 0xBF, 0x9F, 0x40, 0x59, 0xBE, 0x00, 0xF9, 0xBA },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x1F, 0x24, 0x3E, 0x9D, 0x45, 0x6B, 0xB9, 0x03, 0xC5, 0x18, 0xBA, 0x26, 0x41, 0x3D, 0x2C, 0x17, 0x3E, 0xC7, 0x9C, 0x00, 0x3F, 0x2F, 0x1A, 0x82, 0x3D, 0x04, 0xDD, 0xC3, 0xBC, 0x0D, 0x32, 0xC4, 0x3F, 0xD4, 0xEE, 0xA0, 0x3F, 0xEF, 0x99, 0x39, 0xC2, 0x13, 0xC6, 0x0A, 0xC1, 0xD9, 0xFE, 0xE4, 0xFE, 0xB8, 0xFE, 0xBA, 0xFE, 0xFF, 0xFF, 0xFF, 0x20, 0xB9, 0x62, 0x48, 0xD2, 0x95, 0xA2, 0xBF, 0xB8, 0x40, 0x3F, 0xBE, 0x00, 0xF9, 0x97 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x20, 0x24, 0x3E, 0x9D, 0x45, 0x4D, 0x9A, 0x04, 0xC5, 0x34, 0x02, 0x3D, 0x41, 0xD5, 0x7E, 0x15, 0x3E, 0x73, 0x25, 0x17, 0x3F, 0x68, 0x85, 0xBC, 0x3D, 0xD4, 0xA5, 0xEB, 0xBC, 0x8E, 0x27, 0xD4, 0x3F, 0x00, 0x43, 0x14, 0x40, 0x4C, 0xBC, 0x3F, 0xC2, 0x39, 0xEA, 0x1D, 0xC1, 0x10, 0xFF, 0x2F, 0xFF, 0xE4, 0xFE, 0x39, 0xFF, 0xFF, 0xFF, 0xFF, 0xC2, 0xB8, 0x14, 0x47, 0xC8, 0x95, 0xB0, 0xBF, 0xC8, 0x40, 0x68, 0xBE, 0x00, 0xF9, 0x2D },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x21, 0x24, 0x3E, 0x9D, 0x45, 0xAF, 0x77, 0x05, 0xC5, 0x88, 0x2D, 0x50, 0x41, 0x35, 0x92, 0xAB, 0x3D, 0xED, 0x3A, 0x07, 0x3F, 0x8D, 0x59, 0x8D, 0x3D, 0x6A, 0xC4, 0xDA, 0xBC, 0xB5, 0x79, 0xDB, 0x3F, 0xD0, 0x37, 0xAC, 0x3F, 0x7A, 0xF9, 0x46, 0xC2, 0x02, 0x2C, 0x43, 0xC1, 0xC0, 0xFE, 0xBA, 0xFE, 0xCC, 0xFE, 0xBA, 0xFE, 0xFF, 0xFF, 0xFF, 0xB8, 0xB8, 0xF8, 0x46, 0xE8, 0x95, 0x92, 0xBF, 0x82, 0x40, 0x84, 0xBE, 0x00, 0xF9, 0x66 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x22, 0x24, 0x3E, 0x9D, 0x45, 0xCA, 0x57, 0x06, 0xC5, 0x1C, 0x87, 0x36, 0x41, 0x24, 0xE7, 0xF1, 0x3D, 0x04, 0x60, 0x00, 0x3F, 0xD2, 0xB4, 0x7F, 0x3D, 0x06, 0xD0, 0xD0, 0xBC, 0xDF, 0xE6, 0xDA, 0x3F, 0x07, 0x0C, 0xC2, 0x3E, 0x3B, 0x57, 0x47, 0xC2, 0xA0, 0xC3, 0x31, 0xC1, 0x11, 0xFF, 0xC2, 0xFE, 0xD4, 0xFE, 0xC2, 0xFE, 0xFF, 0xFF, 0xFF, 0xF4, 0xB7, 0x7C, 0x47, 0x08, 0x96, 0xA8, 0xBF, 0x9F, 0x40, 0x69, 0xBE, 0x00, 0xFB, 0x40 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x23, 0x24, 0x3E, 0x9D, 0x45, 0x78, 0x34, 0x07, 0xC5, 0xF3, 0xD1, 0x47, 0x41, 0x9E, 0x9B, 0x7D, 0x3D, 0x00, 0x04, 0x25, 0x3F, 0x2A, 0xDF, 0xC4, 0x3D, 0x7F, 0x71, 0x06, 0xBD, 0x1D, 0x6B, 0xD7, 0x3F, 0x12, 0xB1, 0x85, 0x3F, 0xC6, 0x19, 0x45, 0xC2, 0x26, 0xBE, 0x34, 0xC1, 0xC4, 0xFE, 0xD8, 0xFE, 0xBC, 0xFE, 0xF0, 0xFE, 0xFF, 0xFF, 0xFF, 0x48, 0xB8, 0xFF, 0x47, 0x10, 0x96, 0x9D, 0xBF, 0x6F, 0x40, 0x5D, 0xBE, 0x00, 0xFB, 0xDF },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x24, 0x24, 0x3E, 0x9D, 0x45, 0x4C, 0x1C, 0x08, 0xC5, 0x5E, 0xBC, 0x5C, 0x41, 0xCB, 0x6B, 0x45, 0x3E, 0x4D, 0xC1, 0x3F, 0x3F, 0xAB, 0xB9, 0xFC, 0x3D, 0xAF, 0xA3, 0x26, 0xBD, 0x81, 0x21, 0x02, 0x40, 0x2F, 0xA4, 0x9C, 0x3F, 0xEA, 0x11, 0x57, 0xC2, 0xB0, 0xF7, 0x39, 0xC1, 0xC2, 0xFE, 0xC2, 0xFE, 0xBC, 0xFE, 0xE8, 0xFE, 0xFF, 0xFF, 0xFF, 0x74, 0xB7, 0x93, 0x47, 0xCA, 0x95, 0xA8, 0xBF, 0xA0, 0x40, 0x49, 0xBE, 0x00, 0xFF, 0x20 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x25, 0x24, 0x3E, 0x9D, 0x45, 0x52, 0xF6, 0x08, 0xC5, 0x9B, 0x7D, 0x5A, 0x41, 0xEF, 0xFF, 0x06, 0x3E, 0x61, 0x7A, 0x1F, 0x3F, 0x8C, 0x5E, 0xA2, 0x3D, 0x25, 0x38, 0x09, 0xBD, 0x81, 0x9E, 0xF3, 0x3F, 0xFA, 0x6A, 0x7D, 0x3F, 0x4F, 0xB1, 0x51, 0xC2, 0x08, 0x33, 0x46, 0xC1, 0x19, 0xFF, 0x19, 0xFF, 0xF9, 0xFE, 0xFD, 0xFE, 0xFF, 0xFF, 0xFF, 0x6C, 0xB7, 0x3D, 0x48, 0x51, 0x96, 0x95, 0xBF, 0x7D, 0x40, 0x61, 0xBE, 0x00, 0xFF, 0x41 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x26, 0x24, 0x3E, 0x9D, 0x45, 0x8E, 0xD4, 0x09, 0xC5, 0xB5, 0xC6, 0x51, 0x41, 0x98, 0x79, 0xC1, 0x3D, 0xEB, 0xDB, 0xD0, 0x3E, 0x43, 0xA6, 0x14, 0x3D, 0xD8, 0x4E, 0xB3, 0xBC, 0x10, 0x15, 0xF4, 0x3F, 0xB1, 0x12, 0x48, 0x3F, 0x6F, 0x69, 0x51, 0xC2, 0xC0, 0x7A, 0x21, 0xC1, 0xE2, 0xFE, 0xF8, 0xFE, 0x20, 0xFF, 0xFD, 0xFE, 0xFF, 0xFF, 0xFF, 0xB0, 0xB7, 0x99, 0x48, 0x30, 0x96, 0x7D, 0xBF, 0x71, 0x40, 0x35, 0xBE, 0x00, 0xFF, 0x2C },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x27, 0x24, 0x3E, 0x9D, 0x45, 0x77, 0xCF, 0x0A, 0xC5, 0x45, 0x82, 0x58, 0x41, 0x3D, 0x31, 0x80, 0x3E, 0x56, 0x34, 0x73, 0x3F, 0x9D, 0xD4, 0x36, 0x3E, 0x72, 0x9A, 0x55, 0xBD, 0x36, 0xDF, 0x07, 0x40, 0xBB, 0xEC, 0xE6, 0x3F, 0x5C, 0x23, 0x58, 0xC2, 0xD6, 0xE9, 0x4C, 0xC1, 0xD2, 0xFE, 0xF0, 0xFE, 0x3D, 0xFF, 0xE9, 0xFE, 0xFF, 0xFF, 0xFF, 0x44, 0xB8, 0x58, 0x47, 0x60, 0x99, 0x9F, 0xBF, 0xB7, 0x40, 0x40, 0xBE, 0x00, 0xF5, 0x98 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x28, 0x24, 0x3E, 0x9D, 0x45, 0xFA, 0xD8, 0x0B, 0xC5, 0xC2, 0x96, 0x44, 0x41, 0x9B, 0xFF, 0xD1, 0x3D, 0x80, 0x81, 0x4D, 0x3F, 0xCF, 0x62, 0xF9, 0x3D, 0x86, 0x34, 0x39, 0xBD, 0xB4, 0xFB, 0x08, 0x40, 0x06, 0xDF, 0x76, 0x3F, 0x89, 0x7F, 0x5E, 0xC2, 0x94, 0xE7, 0x2B, 0xC1, 0xDB, 0xFE, 0xC4, 0xFE, 0xD0, 0xFE, 0xD1, 0xFE, 0xFF, 0xFF, 0xFF, 0x59, 0xB8, 0x51, 0x48, 0x11, 0x99, 0x89, 0xBF, 0x79, 0x40, 0x7A, 0xBE, 0x00, 0xF5, 0x88 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x29, 0x24, 0x3E, 0x9D, 0x45, 0xEB, 0xD4, 0x0C, 0xC5, 0x21, 0xC7, 0x46, 0x41, 0xE5, 0x23, 0xF2, 0x3D, 0x23, 0xB9, 0x34, 0x3F, 0xC4, 0x00, 0xD4, 0x3D, 0x29, 0xF0, 0x21, 0xBD, 0xAF, 0x5F, 0x02, 0x40, 0xD4, 0x45, 0xCD, 0x3F, 0x4D, 0x79, 0x58, 0xC2, 0x30, 0x1D, 0x3A, 0xC1, 0x31, 0xFF, 0xF9, 0xFE, 0x37, 0xFF, 0x39, 0xFF, 0xFF, 0xFF, 0xFF, 0xE4, 0xB8, 0x70, 0x48, 0x28, 0x99, 0xB0, 0xBF, 0xAD, 0x40, 0x20, 0xBE, 0x00, 0xF1, 0x75 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x2A, 0x24, 0x3E, 0x9D, 0x45, 0x5E, 0xCC, 0x0D, 0xC5, 0xA1, 0x15, 0x49, 0x41, 0x48, 0x97, 0xE9, 0x3D, 0x0A, 0x0A, 0x39, 0x3F, 0xA4, 0x7E, 0xCF, 0x3D, 0xFC, 0xCF, 0x25, 0xBD, 0x46, 0x79, 0x03, 0x40, 0x16, 0xAA, 0x5D, 0x3F, 0xED, 0xEC, 0x58, 0xC2, 0x22, 0x64, 0x12, 0xC1, 0x30, 0xFF, 0x28, 0xFF, 0x30, 0xFF, 0x29, 0xFF, 0xFF, 0xFF, 0xFF, 0xFC, 0xB8, 0x28, 0x47, 0x10, 0x96, 0xA8, 0xBF, 0xC5, 0x40, 0x1C, 0xBE, 0x00, 0xF9, 0x06 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x2B, 0x24, 0x3E, 0x9D, 0x45, 0xA2, 0xAC, 0x0E, 0xC5, 0x40, 0xE4, 0x47, 0x41, 0x6B, 0x15, 0xC8, 0x3D, 0xC8, 0x17, 0x17, 0x3F, 0x11, 0xDE, 0x74, 0x3D, 0xA9, 0xA4, 0x0C, 0xBD, 0x1B, 0xDE, 0x0E, 0x40, 0x29, 0x16, 0x39, 0x3F, 0x35, 0xCF, 0x64, 0xC2, 0xE6, 0x9E, 0x34, 0xC1, 0xF5, 0xFE, 0xF9, 0xFE, 0xE1, 0xFE, 0xFF, 0xFE, 0xFF, 0xFF, 0xFF, 0xE8, 0xB8, 0x70, 0x48, 0x40, 0x96, 0x91, 0xBF, 0x7A, 0x40, 0x82, 0xBE, 0x00, 0xF9, 0x99 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x2C, 0x24, 0x3E, 0x9D, 0x45, 0x7E, 0x89, 0x0F, 0xC5, 0x5E, 0x58, 0x51, 0x41, 0x23, 0xC6, 0x5F, 0x3D, 0xCD, 0xCC, 0x4C, 0x3F, 0x6A, 0x7C, 0xE5, 0x3D, 0xFF, 0x64, 0x40, 0xBD, 0xC1, 0xAD, 0x13, 0x40, 0x4D, 0x0D, 0x7E, 0x3F, 0xDA, 0x81, 0x67, 0xC2, 0x85, 0xFE, 0x23, 0xC1, 0xCA, 0xFE, 0xBC, 0xFE, 0xD1, 0xFE, 0xBA, 0xFE, 0xFF, 0xFF, 0xFF, 0x20, 0xB9, 0x89, 0x48, 0xC4, 0x95, 0xC2, 0xBF, 0x99, 0x40, 0x71, 0xBE, 0x00, 0xF9, 0x08 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x2D, 0x24, 0x3E, 0x9D, 0x45, 0x1D, 0x66, 0x10, 0xC5, 0x43, 0x2A, 0x3F, 0x41, 0x7D, 0x15, 0xB2, 0x3D, 0x31, 0xA5, 0x0A, 0x3F, 0x6C, 0x35, 0x62, 0x3D, 0xDF, 0x95, 0x02, 0xBD, 0x5A, 0xA5, 0x11, 0x40, 0x1C, 0x6D, 0xA2, 0x3F, 0x4A, 0x1C, 0x67, 0xC2, 0x65, 0xE1, 0x13, 0xC1, 0xB8, 0xFE, 0xE9, 0xFE, 0xB4, 0xFE, 0x1F, 0xFF, 0xFF, 0xFF, 0xFF, 0xBA, 0xB8, 0x42, 0x47, 0xD2, 0x95, 0x94, 0xBF, 0x7E, 0x40, 0x8A, 0xBE, 0x00, 0xF9, 0x28 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x2E, 0x24, 0x3E, 0x9D, 0x45, 0x6A, 0x3F, 0x11, 0xC5, 0x88, 0x17, 0x49, 0x41, 0x80, 0x1B, 0xFE, 0x3D, 0xB9, 0x7E, 0x1B, 0x3F, 0xFC, 0xAA, 0x81, 0x3D, 0x38, 0x92, 0x14, 0xBD, 0xDB, 0x2F, 0x11, 0x40, 0x47, 0x49, 0xD5, 0x3E, 0x49, 0xD1, 0x66, 0xC2, 0xD0, 0x95, 0x29, 0xC1, 0xF9, 0xFE, 0xE1, 0xFE, 0xC2, 0xFE, 0xF1, 0xFE, 0xFF, 0xFF, 0xFF, 0x60, 0xB8, 0xB1, 0x48, 0x19, 0x96, 0x7D, 0xBF, 0x71, 0x40, 0x7C, 0xBE, 0x00, 0xFB, 0x79 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x2F, 0x24, 0x3E, 0x9D, 0x45, 0x38, 0x24, 0x12, 0xC5, 0xED, 0x11, 0x48, 0x41, 0x9E, 0x47, 0xF2, 0x3D, 0xB0, 0xD2, 0x40, 0x3F, 0xD9, 0x0D, 0xDD, 0x3D, 0xA5, 0xE7, 0x31, 0xBD, 0xAB, 0x3F, 0x0B, 0x40, 0x11, 0x42, 0xD5, 0x3E, 0xA0, 0x43, 0x62, 0xC2, 0xDD, 0x52, 0x30, 0xC1, 0x1D, 0xFF, 0xF9, 0xFE, 0x2D, 0xFF, 0xFD, 0xFE, 0xFF, 0xFF, 0xFF, 0x18, 0xB8, 0x08, 0x48, 0x5D, 0x96, 0x8D, 0xBF, 0x79, 0x40, 0x3B, 0xBE, 0x00, 0xFB, 0x5D },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x30, 0x24, 0x3E, 0x9D, 0x45, 0xFE, 0x0A, 0x13, 0xC5, 0xFF, 0x08, 0x40, 0x41, 0x0E, 0x34, 0xC0, 0x3D, 0xCF, 0x00, 0x18, 0x3F, 0x58, 0x5A, 0x83, 0x3D, 0xDF, 0x6D, 0x0E, 0xBD, 0xDA, 0x93, 0x0D, 0x40, 0x0C, 0xA4, 0x28, 0x3F, 0x14, 0x81, 0x64, 0xC2, 0x20, 0xB5, 0x11, 0xC1, 0x17, 0xFF, 0x04, 0xFF, 0x20, 0xFF, 0x2F, 0xFF, 0xFF, 0xFF, 0xFF, 0x71, 0xB7, 0xFA, 0x47, 0xFA, 0x95, 0xB0, 0xBF, 0x84, 0x40, 0x48, 0xBE, 0x00, 0xFF, 0xFC },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x31, 0x24, 0x3E, 0x9D, 0x45, 0x5A, 0xEF, 0x13, 0xC5, 0x20, 0xE1, 0x62, 0x41, 0xB8, 0x85, 0x40, 0x3E, 0xAA, 0xCB, 0x6D, 0x3F, 0x3C, 0x74, 0x13, 0x3E, 0x97, 0x2C, 0x6A, 0xBD, 0xBD, 0x5B, 0x20, 0x40, 0xD9, 0x47, 0xEF, 0x3F, 0x4B, 0xF1, 0x6F, 0xC2, 0x88, 0x0D, 0x46, 0xC1, 0xF0, 0xFE, 0xD6, 0xFE, 0xE4, 0xFE, 0x3B, 0xFF, 0xFF, 0xFF, 0xFF, 0xB0, 0xB7, 0x6D, 0x48, 0xC2, 0x95, 0xB0, 0xBF, 0x82, 0x40, 0x6C, 0xBE, 0x00, 0xFF, 0xF4 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x32, 0x24, 0x3E, 0x9D, 0x45, 0x1F, 0xCB, 0x14, 0xC5, 0xBE, 0xEB, 0x5A, 0x41, 0xFB, 0x6A, 0xD7, 0x3D, 0x38, 0xCE, 0x09, 0x3F, 0x71, 0x98, 0x43, 0x3D, 0x7C, 0x65, 0x04, 0xBD, 0xEB, 0xF8, 0x19, 0x40, 0xCD, 0x80, 0x65, 0x3F, 0x06, 0x2F, 0x6D, 0xC2, 0x9F, 0x46, 0x31, 0xC1, 0xD0, 0xFE, 0xE4, 0xFE, 0xC8, 0xFE, 0x2F, 0xFF, 0xFF, 0xFF, 0xFF, 0x52, 0xB7, 0x87, 0x47, 0x55, 0x96, 0x72, 0xBF, 0x94, 0x40, 0x74, 0xBE, 0x00, 0xFF, 0x92 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x33, 0x24, 0x3E, 0x9D, 0x45, 0x92, 0xC7, 0x15, 0xC5, 0xC1, 0x0A, 0x47, 0x41, 0xF2, 0xD8, 0x6E, 0x3E, 0xB4, 0xE0, 0x26, 0x3F, 0x4E, 0xD6, 0x9B, 0x3D, 0xCE, 0x9E, 0x1E, 0xBD, 0x24, 0xDF, 0x12, 0x40, 0xC9, 0x10, 0x93, 0x3F, 0x19, 0x0B, 0x67, 0xC2, 0x78, 0xBE, 0xFE, 0xC0, 0xCC, 0xFE, 0xC8, 0xFE, 0x1C, 0xFF, 0x27, 0xFF, 0xFF, 0xFF, 0xFF, 0x12, 0xB8, 0x31, 0x48, 0xE2, 0x95, 0x8C, 0xBF, 0x82, 0x40, 0x74, 0xBE, 0x00, 0xFD, 0xC2 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x34, 0x24, 0x3E, 0x9D, 0x45, 0x85, 0xAF, 0x16, 0xC5, 0x1B, 0xCF, 0x50, 0x41, 0xE9, 0x25, 0x55, 0x3E, 0x47, 0x3B, 0x76, 0x3F, 0x91, 0xB2, 0x23, 0x3E, 0x3C, 0xC1, 0x70, 0xBD, 0xDF, 0x04, 0x21, 0x40, 0x82, 0xDD, 0x66, 0x3F, 0x79, 0xEF, 0x6E, 0xC2, 0xEA, 0xC4, 0x36, 0xC1, 0x2F, 0xFF, 0xF1, 0xFE, 0xD1, 0xFE, 0x29, 0xFF, 0xFF, 0xFF, 0xFF, 0x44, 0xB8, 0x2D, 0x48, 0xD2, 0x95, 0xA4, 0xBF, 0xB0, 0x40, 0x39, 0xBE, 0x00, 0xFD, 0x2F },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x35, 0x24, 0x3E, 0x9D, 0x45, 0x3D, 0xA4, 0x17, 0xC5, 0x46, 0x90, 0x3F, 0x41, 0x33, 0x7C, 0xC2, 0x3D, 0xD9, 0x0C, 0x40, 0x3F, 0x25, 0x6A, 0xB1, 0x3D, 0x10, 0x98, 0x3D, 0xBD, 0x27, 0x52, 0x1F, 0x40, 0xCB, 0xD6, 0x88, 0x3F, 0x5D, 0xE0, 0x70, 0xC2, 0x72, 0xB7, 0x1A, 0xC1, 0xC4, 0xFE, 0xD0, 0xFE, 0xC6, 0xFE, 0xF8, 0xFE, 0xFF, 0xFF, 0xFF, 0x22, 0xB8, 0xA3, 0x48, 0x5B, 0x96, 0x7E, 0xBF, 0x74, 0x40, 0x71, 0xBE, 0x00, 0xFD, 0x4A },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x36, 0x24, 0x3E, 0x9D, 0x45, 0x66, 0x99, 0x18, 0xC5, 0xAA, 0x11, 0x49, 0x41, 0x7C, 0x46, 0xF5, 0x3D, 0xD8, 0xCD, 0x32, 0x3F, 0x05, 0xA8, 0xAD, 0x3D, 0xA1, 0x31, 0x2E, 0xBD, 0xA5, 0xBC, 0x1C, 0x40, 0x21, 0x5D, 0x7F, 0x3F, 0x27, 0xC8, 0x6D, 0xC2, 0x5A, 0x01, 0x2F, 0xC1, 0x01, 0xFF, 0xE8, 0xFE, 0x3D, 0xFF, 0x3F, 0xFF, 0xFF, 0xFF, 0xFF, 0x0D, 0xB9, 0x0C, 0x47, 0x20, 0x96, 0x89, 0xBF, 0x73, 0x40, 0x84, 0xBE, 0x00, 0xF9, 0x17 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x37, 0x24, 0x3E, 0x9D, 0x45, 0xD7, 0x8F, 0x19, 0xC5, 0x7B, 0x60, 0x31, 0x41, 0x43, 0x93, 0xBD, 0x3D, 0x6E, 0x93, 0x2F, 0x3F, 0xA0, 0x14, 0xA7, 0x3D, 0xD0, 0x7B, 0x28, 0xBD, 0x5A, 0xC8, 0x15, 0x40, 0x92, 0xF4, 0x17, 0x3F, 0x7C, 0x72, 0x68, 0xC2, 0x6D, 0x14, 0xF9, 0xC0, 0x10, 0xFF, 0x1D, 0xFF, 0x11, 0xFF, 0x3D, 0xFF, 0xFF, 0xFF, 0xFF, 0xB4, 0xB8, 0x28, 0x48, 0x5D, 0x96, 0x84, 0xBF, 0xC4, 0x40, 0x10, 0xBE, 0x00, 0xF9, 0xFB },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x38, 0x24, 0x3E, 0x9D, 0x45, 0x7D, 0x75, 0x1A, 0xC5, 0x95, 0xAF, 0x1D, 0x41, 0x0C, 0xBD, 0xD8, 0x3D, 0x53, 0xA6, 0x5E, 0x3F, 0x93, 0xBD, 0x00, 0x3E, 0x0B, 0x3B, 0x55, 0xBD, 0x30, 0x00, 0x18, 0x40, 0x36, 0xF5, 0x7A, 0x3F, 0x01, 0xF0, 0x6A, 0xC2, 0xCE, 0x80, 0xE9, 0xC0, 0xD4, 0xFE, 0x30, 0xFF, 0xF3, 0xFE, 0xB2, 0xFE, 0xFF, 0xFF, 0xFF, 0xD0, 0xB8, 0x27, 0x48, 0xE2, 0x98, 0xA2, 0xBF, 0xB0, 0x40, 0x30, 0xBE, 0x00, 0xF1, 0x97 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x39, 0x24, 0x3E, 0x9D, 0x45, 0xEA, 0x70, 0x1B, 0xC5, 0x74, 0xE1, 0x0C, 0x41, 0xB4, 0x82, 0x94, 0x3D, 0x2A, 0x59, 0xFF, 0x3E, 0x6B, 0x86, 0x3F, 0x3D, 0x4D, 0xB8, 0xF3, 0xBC, 0x8D, 0xFA, 0x15, 0x40, 0x00, 0xA9, 0x0F, 0x3F, 0x3F, 0xCA, 0x69, 0xC2, 0x53, 0x32, 0xAB, 0xC0, 0xC2, 0xFE, 0x10, 0xFF, 0xC8, 0xFE, 0x08, 0xFF, 0xFF, 0xFF, 0xFF, 0x58, 0xB8, 0xB3, 0x48, 0x60, 0x99, 0x91, 0xBF, 0x7F, 0x40, 0x59, 0xBE, 0x00, 0xF3, 0x4A },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x3A, 0x24, 0x3E, 0x9D, 0x45, 0xDC, 0x4D, 0x1C, 0xC5, 0x9B, 0xF9, 0x29, 0x41, 0xA4, 0x56, 0x53, 0x3E, 0x5D, 0x71, 0x52, 0x3F, 0x27, 0x11, 0xFC, 0x3D, 0x3B, 0xFD, 0x46, 0xBD, 0xAF, 0x84, 0x10, 0x40, 0xF6, 0x82, 0xA6, 0x3F, 0xA6, 0x31, 0x65, 0xC2, 0x5B, 0x18, 0x0F, 0xC1, 0xB8, 0xFE, 0xDC, 0xFE, 0x2E, 0xFF, 0x20, 0xFF, 0xFF, 0xFF, 0xFF, 0x23, 0xB8, 0xCF, 0x47, 0x4B, 0x99, 0x99, 0xBF, 0x8A, 0x40, 0x82, 0xBE, 0x00, 0xF3, 0x42 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x3B, 0x24, 0x3E, 0x9D, 0x45, 0x2C, 0x34, 0x1D, 0xC5, 0x57, 0x1E, 0x33, 0x41, 0x1D, 0xE5, 0x22, 0x3E, 0x7F, 0x07, 0x17, 0x3F, 0x42, 0xD3, 0x88, 0x3D, 0x6C, 0x02, 0x0F, 0xBD, 0x7F, 0x9E, 0x15, 0x40, 0x56, 0xEF, 0x52, 0x3F, 0x7B, 0xF5, 0x67, 0xC2, 0xBA, 0xBE, 0x1E, 0xC1, 0x29, 0xFF, 0xC5, 0xFE, 0x10, 0xFF, 0x2F, 0xFF, 0xFF, 0xFF, 0xFF, 0x7D, 0xB8, 0xA1, 0x48, 0xE8, 0x95, 0xA4, 0xBF, 0xA4, 0x40, 0x20, 0xBE, 0x00, 0xFB, 0x88 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x00, 0x24, 0x3E, 0x9D, 0x45, 0xA7, 0x11, 0x1E, 0xC5, 0xA3, 0x7F, 0x2E, 0x41, 0xE1, 0xC6, 0xD3, 0x3D, 0x3B, 0x33, 0x3B, 0x3F, 0xFA, 0x08, 0xB8, 0x3D, 0xCA, 0x02, 0x34, 0xBD, 0xDF, 0xA6, 0x18, 0x40, 0x1C, 0xE3, 0xB6, 0x3F, 0xB4, 0x39, 0x6C, 0xC2, 0x58, 0x56, 0x0D, 0xC1, 0x2D, 0xFF, 0xE9, 0xFE, 0xBC, 0xFE, 0xD8, 0xFE, 0xFF, 0xFF, 0xFF, 0x31, 0xB8, 0x16, 0x47, 0xE2, 0x95, 0xB0, 0xBF, 0x8F, 0x40, 0x30, 0xBE, 0x00, 0xFB, 0xAC },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x01, 0x24, 0x3E, 0x9D, 0x45, 0x85, 0xF6, 0x1E, 0xC5, 0xA1, 0xB3, 0x1B, 0x41, 0x70, 0x28, 0xDE, 0x3D, 0x8A, 0xA6, 0x3C, 0x3F, 0xEC, 0x62, 0xC0, 0x3D, 0xF8, 0xA3, 0x36, 0xBD, 0x42, 0xD3, 0x18, 0x40, 0x96, 0xEF, 0x23, 0x3F, 0x85, 0x09, 0x6D, 0xC2, 0xEA, 0xAB, 0xB6, 0xC0, 0x3B, 0xFF, 0x37, 0xFF, 0x09, 0xFF, 0xF1, 0xFE, 0xFF, 0xFF, 0xFF, 0x64, 0xB7, 0x06, 0x47, 0x30, 0x96, 0x9F, 0xBF, 0xAB, 0x40, 0x20, 0xBE, 0x00, 0xFF, 0x89 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x02, 0x24, 0x3E, 0x9D, 0x45, 0xC7, 0xD6, 0x1F, 0xC5, 0xEB, 0xDC, 0x33, 0x41, 0xA8, 0x88, 0x10, 0x3E, 0xB2, 0x7F, 0x22, 0x3F, 0x56, 0xEC, 0x8C, 0x3D, 0xDA, 0x9C, 0x1F, 0xBD, 0x9E, 0x4D, 0x1C, 0x40, 0xC7, 0x96, 0x02, 0x3F, 0x17, 0xD3, 0x6D, 0xC2, 0xF2, 0x78, 0xFB, 0xC0, 0x09, 0xFF, 0xC2, 0xFE, 0x18, 0xFF, 0x2F, 0xFF, 0xFF, 0xFF, 0xFF, 0x31, 0xB8, 0x54, 0x47, 0x40, 0x96, 0xA8, 0xBF, 0x9F, 0x40, 0x35, 0xBE, 0x00, 0xFD, 0x07 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x03, 0x24, 0x3E, 0x9D, 0x45, 0x1E, 0xC6, 0x20, 0xC5, 0x62, 0x63, 0x41, 0x41, 0x27, 0xAF, 0xFE, 0x3D, 0xE5, 0x34, 0x54, 0x3F, 0x2A, 0x4A, 0xDF, 0x3D, 0xB8, 0x46, 0x4F, 0xBD, 0x11, 0x1F, 0x22, 0x40, 0x95, 0x5E, 0x8B, 0x3F, 0xEC, 0x96, 0x70, 0xC2, 0xB5, 0x34, 0x1C, 0xC1, 0xC4, 0xFE, 0x19, 0xFF, 0x19, 0xFF, 0xFD, 0xFE, 0xFF, 0xFF, 0xFF, 0x19, 0xB8, 0xBD, 0x48, 0xD2, 0x95, 0xB0, 0xBF, 0x7F, 0x40, 0x61, 0xBE, 0x00, 0xFD, 0x73 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x04, 0x24, 0x3E, 0x9D, 0x45, 0x83, 0xBC, 0x21, 0xC5, 0x16, 0xF6, 0x3F, 0x41, 0x83, 0x9B, 0x24, 0x3E, 0xBB, 0xF3, 0x4B, 0x3F, 0x30, 0x07, 0xE3, 0x3D, 0x73, 0xA7, 0x44, 0xBD, 0x42, 0x11, 0x18, 0x40, 0x1B, 0xF0, 0x71, 0x3F, 0xC1, 0x9E, 0x6A, 0xC2, 0x53, 0xCC, 0x0A, 0xC1, 0xCC, 0xFE, 0x10, 0xFF, 0x2D, 0xFF, 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 0x60, 0xB8, 0xAD, 0x48, 0x3D, 0x96, 0xB0, 0xBF, 0xA0, 0x40, 0x71, 0xBE, 0x00, 0xFD, 0xB1 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x05, 0x24, 0x3E, 0x9D, 0x45, 0x58, 0xA8, 0x22, 0xC5, 0xFD, 0x3B, 0x39, 0x41, 0x78, 0xC1, 0xB8, 0x3D, 0xAD, 0xD6, 0x2F, 0x3F, 0xD9, 0xCB, 0xA4, 0x3D, 0x1E, 0x6D, 0x2A, 0xBD, 0x86, 0xCE, 0x18, 0x40, 0x9B, 0xF8, 0x97, 0x3F, 0x35, 0xDC, 0x6C, 0xC2, 0x52, 0x36, 0x0A, 0xC1, 0x19, 0xFF, 0xBE, 0xFE, 0xC0, 0xFE, 0x08, 0xFF, 0xFF, 0xFF, 0xFF, 0x29, 0xB8, 0x38, 0x47, 0x02, 0x96, 0xB0, 0xBF, 0x81, 0x40, 0x74, 0xBE, 0x00, 0xFD, 0xC7 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x06, 0x24, 0x3E, 0x9D, 0x45, 0x85, 0x8A, 0x23, 0xC5, 0x02, 0xD0, 0x2A, 0x41, 0x38, 0x80, 0x78, 0x3D, 0x28, 0xCF, 0x37, 0x3F, 0x42, 0x49, 0xA4, 0x3D, 0xA8, 0x26, 0x34, 0xBD, 0xA9, 0x55, 0x1D, 0x40, 0x83, 0x5C, 0x5D, 0x3F, 0x88, 0x80, 0x6E, 0xC2, 0x57, 0xF2, 0xED, 0xC0, 0x1C, 0xFF, 0x1F, 0xFF, 0x09, 0xFF, 0xC2, 0xFE, 0xFF, 0xFF, 0xFF, 0x70, 0xB8, 0x60, 0x47, 0xC4, 0x95, 0xB0, 0xBF, 0xB0, 0x40, 0x64, 0xBE, 0x00, 0xFD, 0xC2 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x07, 0x24, 0x3E, 0x9D, 0x45, 0xD4, 0x72, 0x24, 0xC5, 0x28, 0x9B, 0x2D, 0x41, 0xD9, 0x61, 0xB9, 0x3D, 0xD3, 0x27, 0x22, 0x3F, 0xE9, 0x9C, 0x96, 0x3D, 0x2D, 0xFF, 0x16, 0xBD, 0x15, 0x47, 0x0B, 0x40, 0xCB, 0x7A, 0x80, 0x3F, 0xBE, 0x4C, 0x61, 0xC2, 0xDC, 0x88, 0xF0, 0xC0, 0xF5, 0xFE, 0x2F, 0xFF, 0x39, 0xFF, 0xFD, 0xFE, 0xFF, 0xFF, 0xFF, 0xBA, 0xB8, 0x0A, 0x47, 0xCC, 0x95, 0xA0, 0xBF, 0xB8, 0x40, 0x2D, 0xBE, 0x00, 0xF9, 0xC6 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x08, 0x24, 0x3E, 0x9D, 0x45, 0xAB, 0x65, 0x25, 0xC5, 0x62, 0xB0, 0x12, 0x41, 0x34, 0x8D, 0xFD, 0x3D, 0xC5, 0xCB, 0x7B, 0x3F, 0x70, 0x3B, 0x37, 0x3E, 0xA0, 0x3E, 0x6B, 0xBD, 0x32, 0xC8, 0x0F, 0x40, 0xA6, 0x6B, 0x25, 0x3F, 0x66, 0x12, 0x65, 0xC2, 0xB2, 0x3E, 0xDB, 0xC0, 0x2D, 0xFF, 0x19, 0xFF, 0xD1, 0xFE, 0xF1, 0xFE, 0xFF, 0xFF, 0xFF, 0xE8, 0xB8, 0xA9, 0x48, 0xF1, 0x95, 0xAF, 0xBF, 0x8D, 0x40, 0x3D, 0xBE, 0x00, 0xF9, 0x52 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x09, 0x24, 0x3E, 0x9D, 0x45, 0xB0, 0x5B, 0x26, 0xC5, 0x61, 0xAE, 0x1D, 0x41, 0xA8, 0x74, 0xD4, 0x3D, 0x40, 0xAC, 0x18, 0x3F, 0x7C, 0x55, 0x83, 0x3D, 0xF9, 0x2C, 0x0E, 0xBD, 0xDF, 0x87, 0x0C, 0x40, 0xE3, 0xA6, 0x29, 0x3F, 0x91, 0xCB, 0x62, 0xC2, 0x41, 0x02, 0xE3, 0xC0, 0xD4, 0xFE, 0x03, 0xFF, 0xD4, 0xFE, 0xDC, 0xFE, 0xFF, 0xFF, 0xFF, 0x02, 0xB8, 0xFC, 0x46, 0xC4, 0x95, 0x7E, 0xBF, 0xA8, 0x40, 0x82, 0xBE, 0x00, 0xFB, 0xAD },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x0A, 0x24, 0x4F, 0xA0, 0x45, 0x67, 0x3A, 0x27, 0xC5, 0xF4, 0x2C, 0x17, 0x41, 0x12, 0x84, 0x1F, 0x3E, 0x09, 0x59, 0x3C, 0x3F, 0xEC, 0x93, 0xE3, 0x3D, 0x21, 0x8D, 0x27, 0xBD, 0x3F, 0xC6, 0xFF, 0x3F, 0x31, 0xB9, 0x31, 0x3F, 0x4A, 0xE9, 0x56, 0xC2, 0x63, 0xCE, 0xF3, 0xC0, 0xC2, 0xFE, 0x04, 0xFF, 0x30, 0xFF, 0xE1, 0xFE, 0xFF, 0xFF, 0xFF, 0x02, 0xB8, 0xBC, 0x47, 0xD8, 0x95, 0x99, 0xBF, 0xAB, 0x40, 0x55, 0xBE, 0x00, 0xFB, 0x58 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x0B, 0x24, 0x4F, 0xA0, 0x45, 0x95, 0x8B, 0x28, 0xC5, 0xAE, 0xD9, 0xB2, 0x40, 0xFF, 0xF4, 0xD0, 0x3D, 0x20, 0x78, 0x53, 0x3F, 0x44, 0x1C, 0x13, 0x3E, 0x05, 0xF8, 0x37, 0xBD, 0x46, 0x70, 0xF1, 0x3F, 0x74, 0x40, 0x48, 0x3F, 0x90, 0x34, 0x52, 0xC2, 0x68, 0xA5, 0x35, 0xC0, 0xF1, 0xFE, 0xD9, 0xFE, 0xD8, 0xFE, 0x10, 0xFF, 0xFF, 0xFF, 0xFF, 0x48, 0xB8, 0x8E, 0x48, 0xA1, 0x95, 0x80, 0xBF, 0x84, 0x40, 0x82, 0xBE, 0x00, 0xFB, 0x05 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x0C, 0x24, 0x4F, 0xA0, 0x45, 0xEC, 0x2F, 0x29, 0xC5, 0x14, 0xFA, 0xBD, 0x40, 0x35, 0x50, 0x8C, 0x3D, 0xD0, 0xBB, 0xAB, 0x3E, 0x54, 0xFC, 0xC6, 0x3C, 0x5F, 0x26, 0x92, 0xBC, 0x68, 0xCD, 0xF8, 0x3F, 0x44, 0xCA, 0x6B, 0x3F, 0x9F, 0x7A, 0x51, 0xC2, 0x8C, 0xE8, 0xC7, 0xC0, 0x2D, 0xFF, 0x2F, 0xFF, 0x37, 0xFF, 0x2D, 0xFF, 0xFF, 0xFF, 0xFF, 0x49, 0xB8, 0x01, 0x47, 0xE1, 0x95, 0x82, 0xBF, 0xA9, 0x40, 0x20, 0xBE, 0x00, 0xFB, 0x18 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x0D, 0x24, 0x4F, 0xA0, 0x45, 0xAA, 0x14, 0x2A, 0xC5, 0x37, 0x0F, 0xA7, 0x40, 0x27, 0xE9, 0x9D, 0x3D, 0xBD, 0xCE, 0x47, 0x3F, 0x0C, 0xAA, 0x06, 0x3E, 0x83, 0xD1, 0x27, 0xBD, 0x5D, 0x94, 0xE5, 0x3F, 0x31, 0xBF, 0x4E, 0x3E, 0x04, 0xE8, 0x4B, 0xC2, 0xC2, 0x27, 0xE3, 0xBF, 0xF5, 0xFE, 0xB2, 0xFE, 0x08, 0xFF, 0x35, 0xFF, 0xFF, 0xFF, 0xFF, 0x58, 0xB8, 0xAB, 0x48, 0x51, 0x99, 0x92, 0xBF, 0xB0, 0x40, 0x30, 0xBE, 0x00, 0xF3, 0x50 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x0E, 0x24, 0x4F, 0xA0, 0x45, 0x93, 0xF4, 0x2A, 0xC5, 0x98, 0xFC, 0xD2, 0x40, 0x6B, 0x36, 0x24, 0xBD, 0x9F, 0x5F, 0x06, 0x3F, 0xD8, 0x06, 0x82, 0x3D, 0x1A, 0x97, 0xE0, 0xBC, 0x1F, 0xD3, 0xE8, 0x3F, 0x4B, 0x5D, 0x10, 0x3F, 0x59, 0x86, 0x4E, 0xC2, 0x73, 0x81, 0xBB, 0xC0, 0x30, 0xFF, 0xF9, 0xFE, 0xCA, 0xFE, 0xD9, 0xFE, 0xFF, 0xFF, 0xFF, 0x48, 0xB8, 0x7B, 0x48, 0x31, 0x99, 0x80, 0xBF, 0x9D, 0x40, 0x51, 0xBE, 0x00, 0xF5, 0x8D },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x0F, 0x24, 0x4F, 0xA0, 0x45, 0xE3, 0xDB, 0x2B, 0xC5, 0x69, 0xBB, 0x0B, 0x41, 0xB9, 0x99, 0xD2, 0x3D, 0x8A, 0x09, 0x34, 0x3F, 0x0C, 0xEE, 0xD8, 0x3D, 0xC8, 0x87, 0x19, 0xBD, 0x6C, 0x63, 0xEA, 0x3F, 0xB8, 0x0C, 0x2D, 0x3F, 0x59, 0x6D, 0x4E, 0xC2, 0x48, 0xA1, 0xA5, 0xC0, 0x2D, 0xFF, 0x19, 0xFF, 0xFA, 0xFE, 0xBA, 0xFE, 0xFF, 0xFF, 0xFF, 0x3A, 0xB8, 0x08, 0x47, 0x18, 0x99, 0x9E, 0xBF, 0x94, 0x40, 0x48, 0xBE, 0x00, 0xF5, 0xAC },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x10, 0x24, 0x4F, 0xA0, 0x45, 0x7B, 0x8A, 0x2C, 0xC5, 0x86, 0xAE, 0x10, 0x41, 0x71, 0x8E, 0x48, 0xBD, 0x03, 0x30, 0xA4, 0x3E, 0x29, 0x18, 0xA5, 0x3C, 0xFD, 0x32, 0x8A, 0xBC, 0x0B, 0xE0, 0xEA, 0x3F, 0x05, 0xE1, 0x75, 0x3F, 0xD6, 0x4D, 0x4D, 0xC2, 0xF7, 0x81, 0xBD, 0xC0, 0x31, 0xFF, 0x3F, 0xFF, 0x19, 0xFF, 0xCC, 0xFE, 0xFF, 0xFF, 0xFF, 0x02, 0xB8, 0xAA, 0x47, 0xE8, 0x95, 0xB0, 0xBF, 0xBD, 0x40, 0x1D, 0xBE, 0x00, 0xFD, 0x90 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x11, 0x24, 0x4F, 0xA0, 0x45, 0xC0, 0x55, 0x2D, 0xC5, 0x13, 0xD6, 0x16, 0x41, 0x15, 0xB9, 0xDD, 0x3D, 0x2C, 0x2F, 0x2E, 0x3F, 0x72, 0xC6, 0xCE, 0x3D, 0x9F, 0xC5, 0x14, 0xBD, 0xCE, 0x01, 0xEC, 0x3F, 0xF7, 0x7C, 0x73, 0x3F, 0xFC, 0x0C, 0x50, 0xC2, 0x58, 0x3D, 0xEE, 0xC0, 0x45, 0xFF, 0x3D, 0xFF, 0xE9, 0xFE, 0x19, 0xFF, 0xFF, 0xFF, 0xFF, 0x58, 0xB8, 0x28, 0x47, 0x10, 0x96, 0x91, 0xBF, 0x7E, 0x40, 0x58, 0xBE, 0x00, 0xFD, 0xAD },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x12, 0x24, 0x4F, 0xA0, 0x45, 0xA9, 0x1D, 0x2E, 0xC5, 0xA3, 0x56, 0x0D, 0x41, 0x9A, 0x93, 0x98, 0x3D, 0x71, 0xFD, 0x17, 0x3F, 0xCB, 0x07, 0xA5, 0x3D, 0x2C, 0x99, 0xFE, 0xBC, 0xA2, 0x93, 0xEC, 0x3F, 0xBC, 0xE5, 0x6A, 0x3F, 0x18, 0x03, 0x4E, 0xC2, 0x83, 0x51, 0xC3, 0xC0, 0xE2, 0xFE, 0xCC, 0xFE, 0xC2, 0xFE, 0xE4, 0xFE, 0xFF, 0xFF, 0xFF, 0x4D, 0xB8, 0x32, 0x47, 0xD8, 0x95, 0xB0, 0xBF, 0x71, 0x40, 0x51, 0xBE, 0x00, 0xFD, 0xDE },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x13, 0x24, 0x4F, 0xA0, 0x45, 0xA2, 0xE0, 0x2E, 0xC5, 0x6E, 0x9A, 0x10, 0x41, 0xC4, 0xEE, 0xB4, 0xBB, 0x75, 0x5C, 0xCD, 0x3E, 0xA4, 0xA0, 0x21, 0x3D, 0xC5, 0xA7, 0xA9, 0xBC, 0x23, 0x4F, 0xDC, 0x3F, 0x53, 0x8A, 0x29, 0x3F, 0x09, 0x65, 0x46, 0xC2, 0xF8, 0xCC, 0xBD, 0xC0, 0xD4, 0xFE, 0x11, 0xFF, 0x19, 0xFF, 0xCA, 0xFE, 0xFF, 0xFF, 0xFF, 0x60, 0xB8, 0xD8, 0x47, 0x4F, 0x96, 0x74, 0xBF, 0x7B, 0x40, 0x40, 0xBE, 0x00, 0xFD, 0xD5 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x14, 0x79, 0x54, 0xA3, 0x45, 0x28, 0x9D, 0x2F, 0xC5, 0x7A, 0x29, 0x07, 0x41, 0xAE, 0x93, 0x83, 0x3D, 0x86, 0x0D, 0x15, 0x3F, 0x25, 0x93, 0xA9, 0x3D, 0x59, 0x6D, 0xEE, 0xBC, 0x40, 0x05, 0xD5, 0x3F, 0x11, 0x7B, 0x4A, 0x3F, 0xC7, 0x4B, 0x45, 0xC2, 0x70, 0xF1, 0xB9, 0xC0, 0x49, 0xFF, 0xFA, 0xFE, 0xC8, 0xFE, 0xDC, 0xFE, 0xFF, 0xFF, 0xFF, 0x12, 0xB8, 0xC4, 0x47, 0x5F, 0x96, 0xA4, 0xBF, 0xBB, 0x40, 0x69, 0xBE, 0x00, 0xFB, 0x74 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x15, 0x79, 0x54, 0xA3, 0x45, 0x4C, 0x91, 0x30, 0xC5, 0xCE, 0x14, 0xF6, 0x40, 0x3B, 0x42, 0x39, 0x3D, 0x89, 0xD1, 0x23, 0x3F, 0x41, 0x3F, 0xDD, 0x3D, 0x17, 0xFF, 0xFE, 0xBC, 0x67, 0xF9, 0xC5, 0x3F, 0x50, 0x4B, 0x90, 0x3F, 0x67, 0x67, 0x3D, 0xC2, 0xD3, 0xD8, 0xEB, 0xC0, 0x31, 0xFF, 0x19, 0xFF, 0x3F, 0xFF, 0x1D, 0xFF, 0xFF, 0xFF, 0xFF, 0x12, 0xB8, 0x60, 0x47, 0xFF, 0x95, 0x92, 0xBF, 0xA0, 0x40, 0x1B, 0xBE, 0x00, 0xFB, 0xA8 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x16, 0x79, 0x54, 0xA3, 0x45, 0x91, 0x5C, 0x31, 0xC5, 0x6F, 0x60, 0xF6, 0x40, 0x69, 0x9E, 0x08, 0xBD, 0x97, 0xB2, 0x88, 0x3E, 0xBF, 0xD7, 0x8D, 0x3C, 0x37, 0x32, 0x5C, 0xBC, 0xD9, 0x36, 0xDB, 0x3F, 0x4D, 0x6F, 0xCF, 0x3F, 0xC5, 0xB5, 0x44, 0xC2, 0x3D, 0x29, 0xA0, 0xC0, 0xF4, 0xFE, 0xE4, 0xFE, 0x04, 0xFF, 0x20, 0xFF, 0xFF, 0xFF, 0xFF, 0x50, 0xB8, 0x9D, 0x48, 0xDA, 0x95, 0xB0, 0xBF, 0xAF, 0x40, 0x7A, 0xBE, 0x00, 0xFB, 0x3C },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x17, 0x79, 0x54, 0xA3, 0x45, 0x3B, 0xE5, 0x31, 0xC5, 0x9D, 0xE9, 0x54, 0x41, 0x05, 0x10, 0x96, 0x3D, 0xFD, 0x18, 0x11, 0x3F, 0x93, 0x5B, 0xCB, 0x3D, 0x27, 0x1B, 0xD4, 0xBC, 0xC4, 0x1F, 0xB5, 0x3F, 0x70, 0x78, 0x90, 0x3F, 0x4C, 0xED, 0x2F, 0xC2, 0x66, 0x5E, 0xF5, 0xC0, 0xF0, 0xFE, 0x2D, 0xFF, 0x3B, 0xFF, 0x03, 0xFF, 0xFF, 0xFF, 0xFF, 0x75, 0xB8, 0x7F, 0x48, 0xE2, 0x95, 0xA8, 0xBF, 0xB5, 0x40, 0x44, 0xBE, 0x00, 0xFB, 0xFB },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x19, 0x79, 0x54, 0xA3, 0x45, 0xA8, 0x48, 0x33, 0xC5, 0x0F, 0x18, 0xD1, 0x40, 0x6A, 0x8D, 0x3B, 0x3D, 0x52, 0x49, 0x03, 0x3F, 0xDF, 0x93, 0x9A, 0x3D, 0xB7, 0xF5, 0xC0, 0xBC, 0x6D, 0xEA, 0xAD, 0x3F, 0xF2, 0x80, 0x0C, 0x3F, 0x5E, 0xF5, 0x30, 0xC2, 0xF8, 0x98, 0x7E, 0xC0, 0xE4, 0xFE, 0x3F, 0xFF, 0xFA, 0xFE, 0xBA, 0xFE, 0xFF, 0xFF, 0xFF, 0xD2, 0xB8, 0x23, 0x47, 0x45, 0x96, 0x92, 0xBF, 0xAF, 0x40, 0x64, 0xBE, 0x00, 0xF9, 0x6A },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x1A, 0x79, 0x54, 0xA3, 0x45, 0x05, 0x00, 0x34, 0xC5, 0x57, 0x4C, 0xF3, 0x40, 0x1E, 0x7F, 0x67, 0x3D, 0xA9, 0x81, 0xDF, 0x3E, 0x64, 0x68, 0x4B, 0x3D, 0x12, 0x92, 0xAD, 0xBC, 0x23, 0x2F, 0xCB, 0x3F, 0x2B, 0x49, 0x85, 0x3E, 0x6E, 0xB9, 0x40, 0xC2, 0x48, 0xD3, 0xA5, 0xC0, 0x30, 0xFF, 0x08, 0xFF, 0x3B, 0xFF, 0x47, 0xFF, 0xFF, 0xFF, 0xFF, 0x04, 0xB9, 0xF8, 0x46, 0x20, 0x96, 0xB0, 0xBF, 0x7C, 0x40, 0x68, 0xBE, 0x00, 0xF9, 0xBD },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x1B, 0x79, 0x54, 0xA3, 0x45, 0x07, 0xA9, 0x34, 0xC5, 0xF0, 0x99, 0xCC, 0x40, 0x0F, 0x95, 0xF0, 0x3C, 0x4E, 0x96, 0xCA, 0x3E, 0xF6, 0xF9, 0x29, 0x3D, 0x82, 0x1A, 0xA1, 0xBC, 0xD6, 0x72, 0xCE, 0x3F, 0x6A, 0x1C, 0x0E, 0x3F, 0x1F, 0x86, 0x41, 0xC2, 0x80, 0x0E, 0x81, 0xC0, 0xE1, 0xFE, 0xD4, 0xFE, 0x20, 0xFF, 0x3B, 0xFF, 0xFF, 0xFF, 0xFF, 0x31, 0xB8, 0x04, 0x47, 0x2F, 0x96, 0x9D, 0xBF, 0xB0, 0x40, 0x69, 0xBE, 0x00, 0xFD, 0xA2 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x1C, 0x79, 0x54, 0xA3, 0x45, 0x49, 0x63, 0x35, 0xC5, 0xF6, 0x5C, 0xF8, 0x40, 0x0A, 0x9D, 0x6A, 0x3C, 0x49, 0x45, 0x9F, 0x3E, 0xE5, 0x60, 0xE4, 0x3C, 0x1D, 0x17, 0x77, 0xBC, 0x33, 0xDB, 0xC6, 0x3F, 0x48, 0xE4, 0x91, 0x3F, 0x76, 0xAD, 0x3C, 0xC2, 0xAB, 0x88, 0xD7, 0xC0, 0xCC, 0xFE, 0xE1, 0xFE, 0xCA, 0xFE, 0xD4, 0xFE, 0xFF, 0xFF, 0xFF, 0x6D, 0xB8, 0x9B, 0x48, 0x5D, 0x96, 0x7D, 0xBF, 0xB0, 0x40, 0x20, 0xBE, 0x00, 0xFD, 0x0C },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x1D, 0x79, 0x54, 0xA3, 0x45, 0xD0, 0x0A, 0x36, 0xC5, 0x3A, 0x01, 0xB5, 0x40, 0x0B, 0xBC, 0x94, 0x3D, 0x23, 0x6B, 0x0F, 0x3F, 0x81, 0x54, 0xB1, 0x3D, 0x9E, 0x5A, 0xDA, 0xBC, 0x20, 0xA2, 0xBB, 0x3F, 0xC9, 0xD3, 0x29, 0x3F, 0xE0, 0xBD, 0x39, 0xC2, 0x8A, 0x07, 0x47, 0xC0, 0x2F, 0xFF, 0x19, 0xFF, 0xFB, 0xFE, 0x39, 0xFF, 0xFF, 0xFF, 0xFF, 0x21, 0xB8, 0x35, 0x48, 0x75, 0x99, 0x92, 0xBF, 0xC4, 0x40, 0x0F, 0xBE, 0x00, 0xF5, 0x54 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x1E, 0x79, 0x51, 0xA6, 0x45, 0xD9, 0xCB, 0x36, 0xC5, 0x77, 0x6A, 0xAD, 0x40, 0x68, 0xA0, 0x0F, 0x3D, 0x64, 0x18, 0xC5, 0x3E, 0xE5, 0xB4, 0x33, 0x3D, 0x21, 0xCE, 0x8A, 0xBC, 0xCB, 0x78, 0xA3, 0x3F, 0xBD, 0x6B, 0x5B, 0x3F, 0xDF, 0x0D, 0x29, 0xC2, 0xB1, 0x76, 0x5A, 0xC0, 0xE4, 0xFE, 0xF8, 0xFE, 0xCC, 0xFE, 0x10, 0xFF, 0xFF, 0xFF, 0xFF, 0x6D, 0xB8, 0xD6, 0x47, 0x3B, 0x99, 0xA2, 0xBF, 0xC1, 0x40, 0x68, 0xBE, 0x00, 0xF5, 0x10 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x1F, 0x79, 0x51, 0xA6, 0x45, 0x78, 0x88, 0x37, 0xC5, 0x19, 0x52, 0x22, 0x41, 0x8A, 0xE6, 0x8B, 0xBB, 0xF1, 0xE2, 0xB0, 0x3E, 0x0F, 0x17, 0x24, 0x3D, 0xDB, 0x19, 0x78, 0xBC, 0xEC, 0x43, 0xAB, 0x3F, 0x3D, 0x09, 0x02, 0x3F, 0x65, 0x3A, 0x2C, 0xC2, 0x72, 0x9E, 0xFB, 0xC0, 0xF8, 0xFE, 0xD4, 0xFE, 0x10, 0xFF, 0x49, 0xFF, 0xFF, 0xFF, 0xFF, 0x04, 0xB8, 0x21, 0x47, 0xC4, 0x98, 0xC1, 0xBF, 0x7A, 0x40, 0x48, 0xBE, 0x00, 0xF5, 0x96 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x20, 0x79, 0x51, 0xA6, 0x45, 0x57, 0xF4, 0x37, 0xC5, 0x6E, 0x75, 0x12, 0x41, 0x46, 0x78, 0x53, 0x3D, 0x0C, 0x08, 0xD9, 0x3E, 0x6E, 0xB0, 0x7E, 0x3D, 0x14, 0xE6, 0x96, 0xBC, 0x08, 0x5A, 0xA2, 0x3F, 0x1A, 0xB4, 0x24, 0x3F, 0xEB, 0x40, 0x27, 0xC2, 0x13, 0xC6, 0x8A, 0xC0, 0x37, 0xFF, 0x3D, 0xFF, 0xE2, 0xFE, 0x08, 0xFF, 0xFF, 0xFF, 0xFF, 0x31, 0xB8, 0x75, 0x48, 0x11, 0x99, 0xA2, 0xBF, 0x90, 0x40, 0x1E, 0xBE, 0x00, 0xF3, 0xA2 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x21, 0x79, 0x51, 0xA6, 0x45, 0x6F, 0xA0, 0x38, 0xC5, 0x7F, 0xB8, 0x0B, 0x41, 0xF7, 0xE9, 0xBF, 0xBB, 0xB2, 0x0D, 0xE4, 0x3D, 0x80, 0x54, 0x8D, 0x3B, 0x83, 0x4B, 0x99, 0xBB, 0xCE, 0xD3, 0x9C, 0x3F, 0xD5, 0x91, 0x91, 0x3F, 0x08, 0xE6, 0x25, 0xC2, 0x11, 0xFE, 0x89, 0xC0, 0x39, 0xFF, 0x19, 0xFF, 0xD8, 0xFE, 0x04, 0xFF, 0xFF, 0xFF, 0xFF, 0x79, 0xB8, 0x69, 0x48, 0x39, 0x96, 0x9D, 0xBF, 0x6A, 0x40, 0x44, 0xBE, 0x00, 0xFB, 0xAF },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x22, 0x79, 0x51, 0xA6, 0x45, 0x36, 0xFF, 0x38, 0xC5, 0x09, 0x53, 0xD4, 0x40, 0x3A, 0x78, 0x62, 0xBC, 0x85, 0xFB, 0x87, 0x3E, 0xB9, 0x87, 0xB8, 0x3C, 0x38, 0xEA, 0x3C, 0xBC, 0xDA, 0xC6, 0xA0, 0x3F, 0x36, 0x43, 0x12, 0x3F, 0x01, 0x6F, 0x2A, 0xC2, 0xC1, 0x29, 0xA2, 0xC0, 0x04, 0xFF, 0x2F, 0xFF, 0x39, 0xFF, 0xD2, 0xFE, 0xFF, 0xFF, 0xFF, 0x3A, 0xB8, 0x9D, 0x47, 0x59, 0x96, 0x84, 0xBF, 0xB7, 0x40, 0x71, 0xBE, 0x00, 0xFB, 0x97 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x23, 0x79, 0x51, 0xA6, 0x45, 0x6E, 0x9F, 0x39, 0xC5, 0x88, 0x32, 0x0B, 0x41, 0xBF, 0xE4, 0x0E, 0x3D, 0x84, 0x37, 0xE9, 0x3E, 0x27, 0xCC, 0x9A, 0x3D, 0xCC, 0xAC, 0x9D, 0xBC, 0x1A, 0xF4, 0x99, 0x3F, 0x3C, 0x77, 0xBC, 0x3F, 0x45, 0x2A, 0x24, 0xC2, 0xB1, 0xA8, 0xDA, 0xC0, 0xC2, 0xFE, 0xE3, 0xFE, 0x39, 0xFF, 0x11, 0xFF, 0xFF, 0xFF, 0xFF, 0x39, 0xB8, 0xAD, 0x48, 0x5D, 0x96, 0x8D, 0xBF, 0xA5, 0x40, 0x7A, 0xBE, 0x00, 0xFB, 0x1E },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x24, 0x79, 0x51, 0xA6, 0x45, 0x93, 0x20, 0x3A, 0xC5, 0x3B, 0x6F, 0xCD, 0x40, 0x88, 0xF4, 0xAC, 0x3C, 0x1A, 0x1A, 0x60, 0x3E, 0x93, 0x4D, 0x9A, 0x3C, 0x65, 0xBA, 0x10, 0xBC, 0x3D, 0xC9, 0x89, 0x3F, 0x79, 0x40, 0x2F, 0x3F, 0xF6, 0x6C, 0x1C, 0xC2, 0x8B, 0xD1, 0x86, 0xC0, 0x19, 0xFF, 0x20, 0xFF, 0x2D, 0xFF, 0x2F, 0xFF, 0xFF, 0xFF, 0xFF, 0x70, 0xB8, 0xA1, 0x48, 0x31, 0x96, 0x89, 0xBF, 0x71, 0x40, 0x79, 0xBE, 0x00, 0xFB, 0x38 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x25, 0x79, 0x51, 0xA6, 0x45, 0x5E, 0xA9, 0x3A, 0xC5, 0x6F, 0x2B, 0xA0, 0x40, 0x2B, 0x6D, 0x74, 0x3C, 0x34, 0xCF, 0x98, 0x3E, 0xA5, 0x5D, 0x10, 0x3D, 0x4E, 0xA1, 0x3E, 0xBC, 0xAB, 0xD7, 0x84, 0x3F, 0x92, 0x1E, 0x18, 0x3F, 0xE2, 0x51, 0x1A, 0xC2, 0x5F, 0x72, 0x31, 0xC0, 0x1E, 0xFF, 0xF0, 0xFE, 0xB8, 0xFE, 0xF8, 0xFE, 0xFF, 0xFF, 0xFF, 0xD9, 0xB8, 0x41, 0x47, 0x21, 0x96, 0x82, 0xBF, 0x92, 0x40, 0x58, 0xBE, 0x00, 0xF9, 0xB3 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x26, 0x79, 0x51, 0xA6, 0x45, 0x55, 0x38, 0x3B, 0xC5, 0xFC, 0x69, 0xD4, 0x40, 0xBF, 0x9D, 0x95, 0x3C, 0xA7, 0x83, 0xA7, 0x3E, 0xAF, 0xCE, 0x10, 0x3D, 0xD3, 0xE4, 0x5E, 0xBC, 0x92, 0xF5, 0x8F, 0x3F, 0xCD, 0x33, 0x3B, 0x3F, 0x3D, 0x5B, 0x20, 0xC2, 0xD3, 0x57, 0xAB, 0xC0, 0x00, 0xFF, 0xE9, 0xFE, 0xD0, 0xFE, 0x27, 0xFF, 0xFF, 0xFF, 0xFF, 0x11, 0xB9, 0xB7, 0x48, 0x5D, 0x96, 0x99, 0xBF, 0x85, 0x40, 0x20, 0xBE, 0x00, 0xF9, 0xDE },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x27, 0x79, 0x51, 0xA6, 0x45, 0xD6, 0xCA, 0x3B, 0xC5, 0xE9, 0xAB, 0xB0, 0x40, 0xF5, 0x78, 0xDE, 0x3C, 0xCB, 0x03, 0xA4, 0x3E, 0x09, 0x48, 0x18, 0x3D, 0x9E, 0x9E, 0x58, 0xBC, 0xE8, 0xDE, 0x92, 0x3F, 0xC1, 0xF3, 0x60, 0x3F, 0x13, 0x06, 0x23, 0xC2, 0x01, 0xB1, 0x01, 0xC0, 0xC2, 0xFE, 0x04, 0xFF, 0x20, 0xFF, 0x08, 0xFF, 0xFF, 0xFF, 0xFF, 0x59, 0xB8, 0x4D, 0x48, 0x11, 0x96, 0x99, 0xBF, 0x7C, 0x40, 0x64, 0xBE, 0x00, 0xFD, 0x8D },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x28, 0xCE, 0x42, 0xA9, 0x45, 0xF5, 0x57, 0x3C, 0xC5, 0x72, 0xB6, 0xAD, 0x40, 0x6E, 0x7D, 0xC0, 0x3C, 0x2B, 0x06, 0xB7, 0x3E, 0x94, 0x8E, 0x3D, 0x3D, 0xF5, 0xD4, 0x75, 0xBC, 0x13, 0x6D, 0x94, 0x3F, 0x92, 0x58, 0xD0, 0x3E, 0x6E, 0x6C, 0x20, 0xC2, 0xD9, 0xC6, 0x6E, 0xC0, 0xBC, 0xFE, 0x20, 0xFF, 0x08, 0xFF, 0x2D, 0xFF, 0xFF, 0xFF, 0xFF, 0x32, 0xB8, 0x61, 0x47, 0x3D, 0x96, 0x7A, 0xBF, 0x8C, 0x40, 0x7E, 0xBE, 0x00, 0xFD, 0xE6 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x29, 0xCE, 0x42, 0xA9, 0x45, 0x07, 0x15, 0x3D, 0xC5, 0xE7, 0xF9, 0xF8, 0x40, 0x8F, 0x94, 0x37, 0x3D, 0xE9, 0x77, 0xEE, 0x3E, 0xF1, 0xAE, 0xB1, 0x3D, 0x5A, 0x4D, 0x93, 0xBC, 0xED, 0x68, 0x85, 0x3F, 0x9B, 0x5E, 0x40, 0x3F, 0xD0, 0x62, 0x19, 0xC2, 0x80, 0x59, 0x81, 0xC0, 0xC6, 0xFE, 0x37, 0xFF, 0x31, 0xFF, 0xF1, 0xFE, 0xFF, 0xFF, 0xFF, 0x30, 0xB8, 0x06, 0x47, 0xA4, 0x95, 0xAC, 0xBF, 0xAF, 0x40, 0x20, 0xBE, 0x00, 0xFD, 0xDD },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x2A, 0xCE, 0x42, 0xA9, 0x45, 0x22, 0x8B, 0x3D, 0xC5, 0xC1, 0x06, 0xD3, 0x40, 0x43, 0x44, 0x07, 0xBE, 0xEC, 0xF1, 0xBB, 0xBD, 0x47, 0xE9, 0xAD, 0x3B, 0x27, 0xC5, 0x71, 0x3B, 0x8C, 0x47, 0x91, 0x3F, 0x6A, 0x82, 0x85, 0x3F, 0x75, 0x2E, 0x1C, 0xC2, 0xBC, 0xD1, 0x9F, 0xC0, 0xE2, 0xFE, 0x20, 0xFF, 0x30, 0xFF, 0xE4, 0xFE, 0xFF, 0xFF, 0xFF, 0x04, 0xB8, 0x84, 0x47, 0xF0, 0x95, 0xB0, 0xBF, 0x99, 0x40, 0x59, 0xBE, 0x00, 0xFD, 0x74 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x2B, 0xCE, 0x42, 0xA9, 0x45, 0x17, 0x09, 0x3E, 0xC5, 0xD0, 0x5B, 0x5E, 0x3F, 0x30, 0x60, 0x6F, 0x3C, 0xEE, 0xEE, 0xFD, 0x3E, 0xDA, 0x62, 0xC6, 0x3D, 0xBA, 0xA6, 0xA2, 0xBC, 0x07, 0x66, 0x86, 0x3F, 0x5D, 0x10, 0xD6, 0x3E, 0x43, 0x3C, 0x1B, 0xC2, 0x35, 0x41, 0x9C, 0x3D, 0xE1, 0xFE, 0xC8, 0xFE, 0x03, 0xFF, 0x39, 0xFF, 0xFF, 0xFF, 0xFF, 0x50, 0xB8, 0x08, 0x48, 0x5F, 0x96, 0x7A, 0xBF, 0xAC, 0x40, 0x28, 0xBE, 0x00, 0xFD, 0xB5 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x2C, 0xCE, 0x42, 0xA9, 0x45, 0x03, 0x76, 0x3E, 0xC5, 0x90, 0x3B, 0xCF, 0x40, 0x2D, 0xA6, 0xF8, 0x3C, 0xE0, 0x5E, 0x1E, 0x3E, 0xAE, 0x36, 0x0F, 0x3C, 0xBB, 0x98, 0xBD, 0xBB, 0x4E, 0x80, 0x70, 0x3F, 0x91, 0x47, 0x34, 0x3F, 0x4F, 0x17, 0x11, 0xC2, 0xB1, 0x27, 0x9A, 0xC0, 0xC4, 0xFE, 0x04, 0xFF, 0x29, 0xFF, 0x09, 0xFF, 0xFF, 0xFF, 0xFF, 0x70, 0xB7, 0x02, 0x47, 0x30, 0x96, 0xA0, 0xBF, 0xB0, 0x40, 0x2D, 0xBE, 0x00, 0xFF, 0xAA },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x2D, 0xCE, 0x42, 0xA9, 0x45, 0x7C, 0xF0, 0x3E, 0xC5, 0xC7, 0x0D, 0xBF, 0x40, 0x15, 0x62, 0xD6, 0x3D, 0x7F, 0x46, 0x86, 0x3E, 0x39, 0xA6, 0x35, 0x3D, 0x45, 0x17, 0x18, 0xBC, 0x02, 0x51, 0x63, 0x3F, 0xA2, 0xE3, 0xD6, 0x3F, 0x40, 0x47, 0x09, 0xC2, 0xCD, 0x03, 0x69, 0xC0, 0x03, 0xFF, 0x02, 0xFF, 0x39, 0xFF, 0x10, 0xFF, 0xFF, 0xFF, 0xFF, 0x60, 0xB8, 0xAF, 0x48, 0xFA, 0x95, 0xA4, 0xBF, 0xB0, 0x40, 0x20, 0xBE, 0x00, 0xFB, 0xC7 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x2E, 0xCE, 0x42, 0xA9, 0x45, 0x92, 0x6E, 0x3F, 0xC5, 0xE5, 0x79, 0x9C, 0x40, 0xB5, 0xCD, 0x0C, 0x3D, 0x82, 0x21, 0x6C, 0x3E, 0xD6, 0xD6, 0xEF, 0x3C, 0xB6, 0xD5, 0x02, 0xBC, 0x55, 0x41, 0x6C, 0x3F, 0xEF, 0xAD, 0x5A, 0x3F, 0xC1, 0x1B, 0x0A, 0xC2, 0xA6, 0xFE, 0x54, 0xC0, 0x19, 0xFF, 0xC2, 0xFE, 0xE8, 0xFE, 0x2F, 0xFF, 0xFF, 0xFF, 0xFF, 0x6F, 0xB8, 0x1B, 0x48, 0x59, 0x99, 0x94, 0xBF, 0xA3, 0x40, 0x91, 0xBE, 0x00, 0xF3, 0x02 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x2F, 0xCE, 0x42, 0xA9, 0x45, 0xE4, 0xFA, 0x3F, 0xC5, 0x1A, 0x87, 0xA7, 0x40, 0xB5, 0x1A, 0x09, 0xBC, 0x9B, 0x55, 0x88, 0x3E, 0xC1, 0x81, 0x1A, 0x3D, 0xF8, 0x24, 0x14, 0xBC, 0xF8, 0x4A, 0x49, 0x3F, 0xBE, 0x0C, 0x07, 0x3F, 0x31, 0x0D, 0x02, 0xC2, 0xBC, 0x07, 0xE0, 0xBF, 0xD8, 0xFE, 0xB8, 0xFE, 0xFD, 0xFE, 0xC2, 0xFE, 0xFF, 0xFF, 0xFF, 0x39, 0xB8, 0x35, 0x48, 0xE4, 0x98, 0xAC, 0xBF, 0x82, 0x40, 0x5C, 0xBE, 0x00, 0xF3, 0xA7 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x30, 0xCE, 0x42, 0xA9, 0x45, 0xE4, 0x6E, 0x40, 0xC5, 0x58, 0x2B, 0x1A, 0x40, 0xA4, 0x28, 0x58, 0xBD, 0x7A, 0x8A, 0x34, 0x3D, 0xE3, 0xE2, 0x25, 0x3B, 0xA8, 0x69, 0xC1, 0xBA, 0xC0, 0xD4, 0x4A, 0x3F, 0x36, 0x92, 0x55, 0x3F, 0x95, 0xBF, 0x03, 0xC2, 0xE3, 0xF3, 0xF3, 0xBF, 0xC2, 0xFE, 0xB8, 0xFE, 0xCA, 0xFE, 0xC4, 0xFE, 0xFF, 0xFF, 0xFF, 0x21, 0xB8, 0x1C, 0x47, 0xEC, 0x98, 0xA1, 0xBF, 0x94, 0x40, 0x54, 0xBE, 0x00, 0xF3, 0x04 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x31, 0xCE, 0x42, 0xA9, 0x45, 0x7C, 0xE9, 0x40, 0xC5, 0xE5, 0x78, 0x3A, 0x40, 0xAF, 0x5A, 0xE3, 0xBC, 0x1B, 0x99, 0x70, 0x3E, 0x92, 0xED, 0x18, 0x3D, 0xB6, 0x88, 0xF4, 0xBB, 0xEA, 0x3E, 0x39, 0x3F, 0xA4, 0x22, 0xA5, 0x3F, 0x67, 0x0D, 0xF6, 0xC1, 0xEF, 0x01, 0x7A, 0xC0, 0x15, 0xFF, 0xD0, 0xFE, 0x29, 0xFF, 0x11, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0xB9, 0x49, 0x48, 0x5D, 0x96, 0x7E, 0xBF, 0xB7, 0x40, 0x30, 0xBE, 0x00, 0xF9, 0xAC },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x32, 0x23, 0x20, 0xAC, 0x45, 0x05, 0x40, 0x41, 0xC5, 0xA6, 0xF5, 0x42, 0x40, 0x5C, 0xED, 0x94, 0x3D, 0x02, 0x74, 0xAC, 0x3D, 0xC5, 0xD5, 0xEA, 0x3B, 0xD4, 0x40, 0x3F, 0xBB, 0x2F, 0x86, 0x61, 0x3F, 0x3F, 0x58, 0x89, 0x3F, 0x2C, 0x2C, 0x07, 0xC2, 0x95, 0xB1, 0x4C, 0x3F, 0xEC, 0xFE, 0xEB, 0xFE, 0xD2, 0xFE, 0xCC, 0xFE, 0xFF, 0xFF, 0xFF, 0xF9, 0xB8, 0x9B, 0x47, 0x3F, 0x96, 0x89, 0xBF, 0x74, 0x40, 0x92, 0xBE, 0x00, 0xF9, 0x66 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x33, 0x23, 0x20, 0xAC, 0x45, 0xCA, 0x99, 0x41, 0xC5, 0xA7, 0x1E, 0x11, 0x41, 0x86, 0xCA, 0x32, 0x3C, 0x4A, 0x8E, 0xEB, 0x3C, 0xD0, 0x03, 0xB2, 0x39, 0x38, 0xFE, 0x7A, 0xBA, 0xB7, 0x47, 0x47, 0x3F, 0x6E, 0x8F, 0x70, 0x3F, 0x3F, 0xEF, 0x00, 0xC2, 0xB7, 0xC8, 0xDD, 0xC0, 0xC2, 0xFE, 0xE8, 0xFE, 0x39, 0xFF, 0xD8, 0xFE, 0xFF, 0xFF, 0xFF, 0x20, 0xB9, 0x22, 0x48, 0x2B, 0x96, 0x91, 0xBF, 0xB4, 0x40, 0x59, 0xBE, 0x00, 0xF9, 0xB0 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x34, 0x23, 0x20, 0xAC, 0x45, 0x94, 0xFE, 0x41, 0xC5, 0x98, 0x75, 0x6F, 0x40, 0x50, 0x11, 0x9E, 0xBB, 0x00, 0x09, 0xC7, 0x3E, 0xFD, 0xB5, 0xAB, 0x3D, 0x29, 0x21, 0x56, 0xBC, 0xA7, 0xAA, 0x4C, 0x3F, 0x41, 0xB8, 0xEB, 0x3F, 0xD4, 0x48, 0x03, 0xC2, 0x2B, 0x14, 0x17, 0xC0, 0x30, 0xFF, 0xE9, 0xFE, 0x35, 0xFF, 0xE1, 0xFE, 0xFF, 0xFF, 0xFF, 0x08, 0xB8, 0xAC, 0x47, 0x39, 0x96, 0x99, 0xBF, 0xA4, 0x40, 0x1F, 0xBE, 0x00, 0xFD, 0x95 },
{0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2D, 0x35, 0x23, 0x20, 0xAC, 0x45, 0x19, 0x2C, 0x42, 0xC5, 0x21, 0xEC, 0xCD, 0x3F, 0x90, 0xE6, 0xAA, 0xBD, 0x15, 0x67, 0x5F, 0xBD, 0x31, 0x13, 0x2E, 0x3B, 0x61, 0x14, 0xF0, 0x3A, 0x66, 0x47, 0x42, 0x3F, 0x28, 0x70, 0x4B, 0x3F, 0xA4, 0x82, 0x03, 0xC2, 0x3A, 0x67, 0x9E, 0xBF, 0xE8, 0xFE, 0xD0, 0xFE, 0xE8, 0xFE, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0x60, 0xB8, 0x1C, 0x48, 0x02, 0x96, 0x8C, 0xBF, 0x7D, 0x40, 0x71, 0xBE, 0x00, 0xFD, 0x7B },
};
        }

        public class startupData
        {
            public string Status;
        }

        private void TimeWorker()
        {
            while (true)
            {
                this.Invoke(new UpdateTimeTextCallback(this.UpdateTimeText), new object[] { });
                Thread.Sleep(1000);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread TimeThread = new Thread(new ThreadStart(TimeWorker));
            TimeThread.IsBackground = true;
            TimeThread.Start();
            traceVisibilityComboBox.Visible = false;
            SetupChart();
            SetupDataTable();
            ReadConfigFile();
            readCalibrationFile();// Read calibration table file  Meter#.tab
            DateTime nowDateTime = DateTime.Now;
            this.timeNowLabel.Text = Convert.ToString(nowDateTime);
            this.meterNumberTextBox.Text = ConfigData.meterNumber;
            //   traceTrackBar.Dock = DockStyle.Bottom;
            traceTrackBar.Width = 100;
            traceTrackBar.Height = 15;
            traceTrackBar.Minimum = 1;
            traceTrackBar.Maximum = 10;
            traceTrackBar.Value = 1;
            groupBox1.Visible = false;
            auxGroupBox.Visible = false;
            manualStartupGroupBox.Visible = false;
            SwitchesTorqueMotorsCheckBox.Enabled = false;
            SwitchesSpringTensionCheckBox.Enabled = false;

            fileType = Properties.Settings.Default.fileFormat;
            fileDateFormat = Properties.Settings.Default.fileDateFormat;
            traceTrackBar.Value = Properties.Settings.Default.traceWidth;


        }

        public void SetupDataTable()
        {
            DataRow myDataRow = dataTable.NewRow();
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.DateTime");
            dtColumn.ColumnName = "DateTime";
            dtColumn.Caption = "Date/Time";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = true;
            // Add id column to the DataColumnCollection.
            dataTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Double");
            dtColumn.ColumnName = "DigitalGravity";
            dtColumn.Caption = "Digital Gravity";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            // Add id column to the DataColumnCollection.
            dataTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Double");
            dtColumn.ColumnName = "springTension";
            dtColumn.Caption = "Spring Tension";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            // Add id column to the DataColumnCollection.
            dataTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Double");
            dtColumn.ColumnName = "CrossCoupling";
            dtColumn.Caption = "Cross Coupling";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            // Add id column to the DataColumnCollection.
            dataTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Double");
            dtColumn.ColumnName = "RawBeam";
            dtColumn.Caption = "Raw Beam";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            // Add id column to the DataColumnCollection.
            dataTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Double");
            dtColumn.ColumnName = "TotalCorrection";
            dtColumn.Caption = "Total Correction";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            // Add id column to the DataColumnCollection.
            dataTable.Columns.Add(dtColumn);

            /*     dtColumn = new DataColumn();
                 dtColumn.DataType = System.Type.GetType("System.Double");
                 dtColumn.ColumnName = "RawGravity";
                 dtColumn.Caption = "Raw Gravity";
                 dtColumn.ReadOnly = false;
                 dtColumn.Unique = false;
                 // Add id column to the DataColumnCollection.
                 dataTable.Columns.Add(dtColumn);
     */
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Double");
            dtColumn.ColumnName = "AL";
            dtColumn.Caption = "AL";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            // Add id column to the DataColumnCollection.
            dataTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Double");
            dtColumn.ColumnName = "AX";
            dtColumn.Caption = "AX";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            // Add id column to the DataColumnCollection.
            dataTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Double");
            dtColumn.ColumnName = "VE";
            dtColumn.Caption = "VE";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            // Add id column to the DataColumnCollection.
            dataTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Double");
            dtColumn.ColumnName = "AX2";
            dtColumn.Caption = "AX2";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            // Add id column to the DataColumnCollection.
            dataTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Double");
            dtColumn.ColumnName = "LACC";
            dtColumn.Caption = "LACC";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            // Add id column to the DataColumnCollection.
            dataTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Double");
            dtColumn.ColumnName = "XACC";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            // Add id column to the DataColumnCollection.
            dataTable.Columns.Add(dtColumn);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = dataTable.Columns["DateTime"];
            dataTable.PrimaryKey = PrimaryKeyColumns;
            // Instantiate the  DataSet variable.

            DataSet dtSet = new DataSet("meterData");

            // Add the custTable to the DataSet.
            dtSet.Tables.Add(dataTable);
        }

        private void oneSecStuff()// change to array    double[] data1, double[] data2, double[] data3, double ccFact
        {
            ConfigData ConfigData = new ConfigData();
            MeterData.data1[4] = 0.0;       //crossCoupling
            //sprintTension200Sum = 0;
            for (int i = 6; i < 12; i++)//Calculate Cross coupling
            {
                MeterData.data1[4] = MeterData.data1[4] + MeterData.data1[i] * ConfigData.crossCouplingFactors[i];
                // Console.Write("Data1[" + i + "]\t" + Class1.data1[i] + "\n");
            }
            // Console.Write("Cross coupling: " + Class1.data1[4] + "\n");
            Filter320();
            TenSecondStuff();
            AnalogFilter();
        }

        private void TenSecondStuff()
        {
            ConfigData ConfigData = new ConfigData();
            MeterData Class1 = new MeterData();
            MeterData.beam = ConfigData.beamScale * MeterData.data4[5];           // Beam scale determined by K-check
            MeterData.beamFirstDifference = MeterData.beam - MeterData.oldBeam;      // Get beam velocities first difference
            MeterData.oldBeam = MeterData.beam;
            MeterData.totalCorrection = MeterData.beamFirstDifference * 3 + MeterData.data4[4];   // Scale velocity to mGal & add cross coupling
            MeterData.rAwg = MeterData.data4[3] + MeterData.totalCorrection;                      // Add spring tension
            MeterData.data4[2] = DigitalFilter(MeterData.rAwg);               // Filter with LaCoste 60 point table

            MeterData.gravity = UpLook(MeterData.data4[2]) + .05;                 // Apply calibration table
        }

        private double UpLook(double gVal)
        {
            // CONVERTS GRAVITY VALUES ACCORDING TO THE CALIBRATION TABLE
            // FOR METERS WITH A CONSTANT CALIBRATION FACTOR, A DUMMY
            // CALIBRATION TABLE MUST BE CREATED USING MKTABLE.
            int ind;
            double upLook;
            ind = Convert.ToInt32(Math.Abs(gVal / 100)); ;

            if (ind > 60)
            {
                ind = 60;
            }
            upLook = table1[ind] + (gVal - (ind * 100)) * table2[ind];

            return gVal;
        }

        private double DigitalFilter(double data)// PERFORMS 60 POINT DIGITAL FILTER ON GRAVITY
        {
            double[] GT = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] DATA = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int K;
            double dfilt;
            int nPoint = 0;

            // FILTER WEIGHTS
            double[] filterWeights = { -.00034, -.00038, -.00041, -.00044, -.00046, -.00046, -.00044, -.00039, -.00030, -.00015, .00007, .00037, .00079, .00133, .00202, .00289, .00396, .00526, .00679, .00859, .01066, .01299, .01558, .01841, .02143, .02460, .02785, .03110, .03426, .03723, .03992, .04223, .04408, .04539, .04613, .04626, .04579, .04474, .04315, .04109, .03864, .03589, .03292, .02984, .02671, .02362, .02063, .01780, .01516, .01274, .01056, .00863, .00694, .00548, .00424, .00321, .00235, .00166, .00111, .00068 };
            nPoint++;
            if (nPoint >= 60)
            {
                nPoint = 0;
            }

            GT[nPoint] = data;
            K = nPoint;
            dfilt = 0;

            for (int i = 0; i < 60; i++)
            {
                K++;
                if (K >= 60)
                {
                    K = 0;
                }

                dfilt = dfilt + filterWeights[i] * GT[K] - GT[1];
            }
            return dfilt;
        }

        private void Filter320()        //(double[] data1, double[] data2, double[] data3, double[] data4);
        {
            for (int ii = 3; ii < 18; ii++)//    VERIFY IF IT SHOULD INCLUDE 18
            {
                MeterData.data2[ii] = MeterData.data2[ii] + (MeterData.data1[ii] - MeterData.data2[ii]) * .05;//  (.05 = 1/20)
                MeterData.data3[ii] = MeterData.data3[ii] + (MeterData.data2[ii] - MeterData.data3[ii]) * .05;//  (.05 = 1/20)
                MeterData.data4[ii] = MeterData.data4[ii] + (MeterData.data3[ii] - MeterData.data4[ii]) * .05;//  (.05 = 1/20)
            }
        }

        private void AnalogFilter()
        {
            MeterData.data4[1] = MeterData.data4[1] + (MeterData.rAwg - MeterData.data4[1]) / cper;                                             // Gravity
            MeterData.data4[21] = MeterData.data4[21] + (MeterData.data4[5] - MeterData.data4[21]) / cper;  // AV B
            MeterData.data4[22] = MeterData.data4[22] + (MeterData.data4[4] - MeterData.data4[22]) / cper;  // Cross Coupling
            MeterData.data4[23] = MeterData.data4[23] + (MeterData.totalCorrection - MeterData.data4[23]) / cper;          // TC
        }

        private void AddDataToGrid()
        {
            DateTime firstDateTime = new DateTime(2015, 1, 1, MeterData.Hour, MeterData.Min, MeterData.Sec);

            DateTime myDateTime = firstDateTime.AddDays(MeterData.day - 1);
            bool normalGravity = true;
            string digitalGravity = MeterData.data4[2].ToString("F4");
            string springTension = MeterData.data1[3].ToString("F4");
            string crossCoupling = MeterData.data4[4].ToString("F4");
            string rawBeam = MeterData.data1[5].ToString("F4");
            string totalCorrection = MeterData.totalCorrection.ToString("F4");
            //     string rawGravity = MeterData.data4[2].ToString("F4");
            string AL = MeterData.data4[7].ToString("F4");
            string AX = MeterData.data4[8].ToString("F4");
            string VE = MeterData.data4[9].ToString("F4");
            string AX2 = MeterData.data4[10].ToString("F4");
            string LACC = MeterData.data4[13].ToString("F4");
            string XACC = MeterData.data4[14].ToString("F4");

            if (normalGravity)
            {
                //                    line id,                 date/time,                  digital gravity,                            st,                               cross coupling                raw beam,                    tc(total correction
                // string[] row1 = new string[] { Convert.ToString(myDateTime), digitalGravity, springTension, crossCoupling, rawBeam, totalCorrection, rawGravity, AL, AX, VE, AX2, LACC, XACC };
                string[] row1 = new string[] { Convert.ToString(myDateTime), digitalGravity, springTension, crossCoupling, totalCorrection, AL, AX, VE, AX2, LACC, XACC };

                this.dataGridView1.Rows.Add(row1);
            }
            this.dataGridView1.Update();
        }

        [DelimitedRecord("0  ")]
        public class calData
        {
            [FieldConverter(ConverterKind.Double)]
            //  [FieldConverter(ConverterKind.Decimal, ".")] // The decimal separator is .
            public double value1;

            [FieldConverter(ConverterKind.Double)]
            //  [FieldConverter(ConverterKind.Decimal, ".")] // The decimal separator is .
            public double value2;
        }

        public void readCalibrationFile()
        {
            string docPath = "C:\\LCT stuff\\S99.tab";
            var engine = new FileHelperEngine<calData>();

            var records = engine.ReadFile(docPath);
            int i = 0;
            foreach (var record in records)
            {
                table1[i] = record.value1;
                table2[i] = record.value2;

                if (engineerDebug) Console.WriteLine(record.value1 + "\t" + record.value2 + "\n");
                // if(engineerDebug) Console.WriteLine(record.value2 + "\n");
            }
        }

        private void GetMeterData()
        {
            MeterData MeterData = new MeterData();
            //    MeterData.CheckMeterDataSim();

            MeterData Class1 = new MeterData();

            DataGridView dataGridView1 = new DataGridView();

            int arrayLength = mySimData.simData.GetLength(1);
            int arrayWidth = mySimData.simData.GetLength(0);
            byte[] byteArray = new byte[arrayWidth];

            for (int ii = 0; ii < arrayWidth; ii++)
            {
                var task = Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < arrayLength; i++)
                    {
                        byteArray[i] = mySimData.simData[ii, i];
                    }

                    Class1.CheckMeterData(byteArray);
                    oneSecStuff();
                    AddDataToGrid();
                    UpdateChart();
                    Thread.Sleep(1000);
                });
            }
        }

        [DelimitedRecord(",")]
        public class Orders
        {
            public string LineID;

            public int Year;
            public int Days;
            public int Hour;
            public int Minute;
            public int Second;

            [FieldConverter(ConverterKind.Double)]
            public double Gravity;

            public double SpringTension;
            public double CrossCoupling;
            public double AvgBeam;
            public double VCC;
            public double AL;
            public double AX;
            public double VE;
            public double AX2;
            public double XACC2;
            public double LACC2;
            public double XACC;
            public double LACC;
            public double Period;
            public double ParallelData;

            //  [FieldConverter(ConverterKind.Double)]
            public double AUX1;

            public double AUX2;
            public double AUX3;
            //  public double AUX4;
        }

        /*
                private void ArrayData()
                {
                    MeterData MeterData = new MeterData();
                    MeterData Class1 = new MeterData();

                    DataGridView dataGridView1 = new DataGridView();

                    int arrayLength = mySimData.simData.GetLength(1);
                    int arrayWidth = mySimData.simData.GetLength(0);
                    byte[] byteArray = new byte[arrayWidth];

                    if (surveyName == "dude")
                    {
                        // MessageBox.Show("Survey name cannot be empty.");
                    }
                    //////////////////////////////////////////////////////////
                    else
                    {
                        //  recordingTextBox.Visible = true;

                        // start new thread here GetMeterData();

                        for (int ii = 0; ii < arrayWidth; ii++)
                        {
                            for (int i = 0; i < arrayLength; i++)
                            {
                                byteArray[i] = mySimData.simData[ii, i];
                            }

                            Class1.CheckMeterData(byteArray);
                            oneSecStuff();
                            AddDataToGrid();
                            UpdateChart();
                            RecordDataToFile("Append", d);

                            // Thread.Sleep(1000);
                        }
                        //      FileClass.RecordDataToFile("Append");
                    }
                }
        */

        public class myData//  set this up so all chart data in in one class.  Maybe not needed use MeterData
        {
            public DateTime Date;
            public string LineID;
            public int Year;
            public int Days;
            public int Hour;
            public int Minute;
            public int Second;
            public double Gravity;
            public double SpringTension;
            public double CrossCoupling;
            public double RawBeam;
            public double TotalCorrection;
            public double RawGravity;
            public double VCC;
            public double AL;
            public double AX;
            public double VE;
            public double AX2;
            public double XACC2;
            public double LACC2;
            public double XACC;
            public double LACC;
            public double Period;
            public double ParallelData;
            public double AUX1;
            public double AUX2;
            public double AUX3;
            public double AUX4;
        }

        public void UpdateChartThreadSafe(myData d)
        {
            //      UPDATE MAIN GRAVITY CHART
            GravityChart.Series["Digital Gravity"].Points.AddXY(d.Date, d.Gravity);
            GravityChart.Series["Spring Tension"].Points.AddXY(d.Date, d.SpringTension);
            GravityChart.Series["Cross Coupling"].Points.AddXY(d.Date, d.CrossCoupling);
            GravityChart.Series["Raw Beam"].Points.AddXY(d.Date, d.RawBeam);
            GravityChart.Series["Total Correction"].Points.AddXY(d.Date, d.TotalCorrection);
            GravityChart.Update();

            // crossCouplingChart.Series["Raw Gravity"].Points.AddXY(d.Date, d.RawGravity);
            crossCouplingChart.Series["AL"].Points.AddXY(d.Date, d.AL);
            crossCouplingChart.Series["AX"].Points.AddXY(d.Date, d.AX);
            crossCouplingChart.Series["VE"].Points.AddXY(d.Date, d.VE);
            crossCouplingChart.Series["AX2"].Points.AddXY(d.Date, d.AX2);
            crossCouplingChart.Series["LACC"].Points.AddXY(d.Date, d.LACC);
            crossCouplingChart.Series["XACC"].Points.AddXY(d.Date, d.XACC);
            crossCouplingChart.Update();

            var runningTime = TimeSpan.FromTicks(DateTime.Now.Ticks - fileStartTime.Ticks);
            // new DateTime(runningTime.Ticks).ToString("HH:mm");

            recordingDurationLabel.Text = "Duration: " + new DateTime(runningTime.Ticks).ToString("HH:mm:ss");
        }

        private void AddDataPoint(myData d)// this will be the new update chart etc.
        {
            //    DateTime firstDateTime = new DateTime(2015, 1, 1, d.Hour, d.Minute, d.Second);
            //    DateTime myDateTime = firstDateTime.AddDays(d.Days - 1);
            //  firstDateTime.AddDays(d.Days - 1);

            if (this.InvokeRequired)
            {
                this.Invoke(new Action<myData>(this.AddDataPoint), new object[] { d });
            }
            else
            {
                UpdateChartThreadSafe(d);
                AddDataToGridThreadSafe(d);
                // addDataToFileThreadSafe(d);

                RecordDataToFile("Append", d);  // need threadsafe version
            }
        }

        public delegate void UpdateRecordBoxCallback(Boolean i);

        public delegate void UpdateFileNameLabelCallback(string fileName);

        public delegate void UpdateTimeTextCallback();

        private void UpdateRecordBox(Boolean i)
        {
            recordingTextBox.Visible = i;
            DateTime nowDateTime = DateTime.Now;
            fileStartTime = nowDateTime;
            fileStartTimeLabel.Text = "File start time:  " + Convert.ToString(nowDateTime);
        }

        private void UpdateNameLabel(string fileName)
        {
            fileLabel.Text = fileName;
        }

        private void UpdateTimeText()
        {
            DateTime nowDateTime = DateTime.Now;
            timeNowLabel.Text = Convert.ToString(nowDateTime);
        }

        private void ArrayDataWorker(object obj)
        {
            MeterData MeterData = new MeterData();
            MeterData Class1 = new MeterData();
            Orders myOrders = new Orders();
            var _delegate = (Action<myData>)obj;
            DataGridView dataGridView1 = new DataGridView();

            int arrayLength = mySimData.simData.GetLength(1);
            int arrayWidth = mySimData.simData.GetLength(0);
            byte[] byteArray = new byte[arrayWidth];

            DateTime now = DateTime.Now;

            if (Form1.fileDateFormat == 1)
            {
                fileName = filePath + ConfigData.meterNumber + "-" + surveyName + "-" + now.ToString("yyyy-MMM-dd-HH-mm-ss") + "." + fileType;
            }
            else if (fileDateFormat == 2)
            {
                fileName = filePath + ConfigData.meterNumber + "-" + surveyName + "-" + now.ToString("yyyy-mm-dd-HH-mm-ss") + "." + fileType;
            }
            else if (Form1.fileDateFormat == 3)
            {
                fileName = filePath + ConfigData.meterNumber + "-" + surveyName + "-" + now.ToString("yyyy-dd-HH-mm-ss") + "." + fileType;
            }
            else if (fileDateFormat == 4)
            {
                fileName = filePath + customFileName + "." + fileType;
            }
            Properties.Settings.Default.fileDateFormat = fileDateFormat;

            this.Invoke(new UpdateFileNameLabelCallback(this.UpdateNameLabel), new object[] { fileName });
            FileClass FileClass = new FileClass();
            //  Form1.gravityFileName = sampleFileNamelabel.Text;
            RecordDataToFile("Open");

            this.Invoke(new UpdateRecordBoxCallback(this.UpdateRecordBox), new object[] { true });

            for (int ii = 0; ii < arrayWidth; ii++)
            {
                for (int i = 0; i < arrayLength; i++)
                {
                    byteArray[i] = mySimData.simData[ii, i];
                }
                /*        if (ii == arrayWidth - 1)
                        {
                            foreach (var series in GravityChart.Series)
                            {
                                series.Points.Clear();
                            }
                            foreach (var series in crossCouplingChart.Series)
                            {
                                series.Points.Clear();
                            }
                            ii = 0;
                        }*/
                Class1.CheckMeterData(byteArray);
                DateTime firstDateTime = new DateTime(2015, 1, 1, MeterData.Hour, MeterData.Min, MeterData.Sec);
                DateTime myDateTime = firstDateTime.AddDays(MeterData.day - 1);
                oneSecStuff();
                _delegate(new myData
                {
                    Date = myDateTime,
                    Year = MeterData.year,
                    Days = MeterData.day,
                    Hour = MeterData.Hour,
                    Minute = MeterData.Min,
                    Second = MeterData.Sec,
                    Gravity = MeterData.data4[2],
                    CrossCoupling = MeterData.data4[4],
                    SpringTension = MeterData.data1[3],
                    RawBeam = MeterData.data1[5],
                    TotalCorrection = MeterData.totalCorrection,
                    RawGravity = MeterData.data4[2],
                    AL = MeterData.data4[7],
                    AX = MeterData.data4[8],
                    AX2 = MeterData.data4[10],
                    VE = MeterData.data4[9],
                    XACC = MeterData.data4[14],
                    LACC = MeterData.data4[13]
                });

                Thread.Sleep(1000);

                //      FileClass.RecordDataToFile("Append");
            }
        }

        private void worker(object obj)// this method will get data from Marine Data File
        {
            Orders myOrders = new Orders();

            var _delegate = (Action<myData>)obj;

            var engine = new FileHelperEngine<Orders>();
            var records = engine.ReadFile("C:\\Users\\User\\Desktop\\ChartBinding\\Marine data.csv");
            foreach (var record in records)
            {
                DateTime firstDateTime = new DateTime(2015, 1, 1, record.Hour, record.Minute, record.Second);
                DateTime myDateTime = firstDateTime.AddDays(record.Days - 1);

                _delegate(new myData { Date = myDateTime, Year = record.Year, Days = record.Days, Hour = record.Hour, Minute = record.Minute, Second = record.Second, Gravity = record.Gravity, CrossCoupling = record.CrossCoupling, SpringTension = record.SpringTension, RawBeam = record.AvgBeam, AL = record.AL, AX = record.AX, VE = record.VE, XACC = record.XACC, LACC = record.LACC });
                //   Console.WriteLine(record.Days);
                //   Console.WriteLine(record.Gravity);
                //   Console.WriteLine(record.CrossCoupling);
                Thread.Sleep(1000);
            }
        }

        private void addDataToFileThreadSafe(myData d)
        {
        }

        private void AddDataToGridThreadSafe(myData d)
        {
            bool normalGravity = true;
            string date = Convert.ToString(d.Date);
            string digitalGravity = d.Gravity.ToString("F4");
            string springTension = d.SpringTension.ToString("F4");
            string crossCoupling = d.CrossCoupling.ToString("F4");
            string rawBeam = d.RawBeam.ToString("F4");
            string totalCorrection = MeterData.totalCorrection.ToString("F4");
            // string rawGravity = MeterData.data4[2].ToString("F4");
            string AL = d.AL.ToString("F4");
            string AX = d.AX.ToString("F4");
            string VE = MeterData.data4[9].ToString("F4");
            string AX2 = MeterData.data4[10].ToString("F4");
            string LACC = MeterData.data4[13].ToString("F4");
            string XACC = MeterData.data4[14].ToString("F4");

            if (normalGravity)
            {
                //                    line id,                 date/time,                  digital gravity,                            st,                               cross coupling                raw beam,                    tc(total correction
                //  string[] row1 = new string[] { date, digitalGravity, springTension, crossCoupling, rawBeam, totalCorrection, rawGravity, AL, AX, VE, AX2, LACC, XACC };
                string[] row1 = new string[] { date, digitalGravity, springTension, crossCoupling, rawBeam, totalCorrection, AL, AX, VE, AX2, LACC, XACC };

                //  this.dataGridView1.Rows.Add(row1);

                dataGridView1.Rows.Insert(0, row1);
            }
            this.dataGridView1.Update();
        }

        private void ReadMarineDataFile()
        {
            var engine = new FileHelperEngine<Orders>();
            var records = engine.ReadFile("C:\\Users\\User\\Desktop\\ChartBinding\\Marine data.csv");
            foreach (var record in records)
            {
                //   Console.WriteLine(record.Year);
                //   Console.WriteLine(record.Gravity);
                //   Console.WriteLine(record.SpringTension);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //    ArrayData();
            //      GetFileData();
            Boolean okToRun = false;

            if (surveyNameSet == false)
            {
                MessageBox.Show("Warning! \nNo survey information was entered.");
                surveyNameSet = true;
            }
            else
            {
                okToRun = true;
            }

            if (okToRun)
            {
                Thread WorkerThread = new Thread(new ParameterizedThreadStart(ArrayDataWorker));
                WorkerThread.IsBackground = true;
                WorkerThread.Start(new Action<myData>(this.AddDataPoint));
            }
            okToRun = true;
        }

        private void exitProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void engineeringPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordForm myPassword = new PasswordForm();
            myPassword.Show();
        }

        private void setDateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTimeForm myDateForm = new DateTimeForm();
            myDateForm.Show();
        }

        private void systemAuxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (auxGroupBox.Visible == true)
            {
                auxGroupBox.Visible = false;
            }
            else
            {
                auxGroupBox.Visible = true;
            }
        }

        private void printPreviewCrossCouplingChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crossCouplingChart.Printing.PageSetup();
            crossCouplingChart.Printing.PrintPreview();
        }

        private void printGravityChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GravityChart.Printing.PageSetup();

            GravityChart.Printing.PrintPreview();
        }

        private void chartColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chartColorComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SetChartAreaColors(chartColorComboBox.SelectedIndex);
        }

        private void traceTrackBar_Scroll(object sender, EventArgs e)
        {
            SetChartBorderWidth(traceTrackBar.Value);
            Properties.Settings.Default.traceWidth = traceTrackBar.Value;
        }

        private void traceVisibilityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string traceSelect = traceName[traceVisibilityComboBox.SelectedIndex];

            switch (traceSelect)
            {
                case "Digital Gravity":
                    if (ChartVisibility.digitalGravity == true)
                    {
                        ChartVisibility.digitalGravity = false;
                    }
                    else
                    {
                        ChartVisibility.digitalGravity = true;
                    }
                    break;

                case "Spring Tension":
                    if (ChartVisibility.springTension == true)
                    {
                        ChartVisibility.springTension = false;
                    }
                    else
                    {
                        ChartVisibility.springTension = true;
                    }
                    break;

                case "Cross Coupling":
                    if (ChartVisibility.crossCoupling == true)
                    {
                        ChartVisibility.crossCoupling = false;
                    }
                    else
                    {
                        ChartVisibility.crossCoupling = true;
                    }
                    break;

                case "Raw Beam":
                    if (ChartVisibility.rawBeam == true)
                    {
                        ChartVisibility.rawBeam = false;
                    }
                    else
                    {
                        ChartVisibility.rawBeam = true;
                    }
                    break;

                case "Total Correction":
                    if (ChartVisibility.totalCorrection == true)
                    {
                        ChartVisibility.totalCorrection = false;
                    }
                    else
                    {
                        ChartVisibility.totalCorrection = true;
                    }
                    break;

                case "AL":
                    if (ChartVisibility.AL == true)
                    {
                        ChartVisibility.AL = false;
                    }
                    else
                    {
                        ChartVisibility.AL = true;
                    }
                    break;

                case "AX":
                    if (ChartVisibility.AX == true)
                    {
                        ChartVisibility.AX = false;
                    }
                    else
                    {
                        ChartVisibility.AX = true;
                    }
                    break;

                case "VE":
                    if (ChartVisibility.VE == true)
                    {
                        ChartVisibility.VE = false;
                    }
                    else
                    {
                        ChartVisibility.VE = true;
                    }
                    break;

                case "AX2":
                    if (ChartVisibility.AX2 == true)
                    {
                        ChartVisibility.AX2 = false;
                    }
                    else
                    {
                        ChartVisibility.AX2 = true;
                    }
                    break;

                case "XACC":
                    if (ChartVisibility.XACC == true)
                    {
                        ChartVisibility.XACC = false;
                    }
                    else
                    {
                        ChartVisibility.XACC = true;
                    }
                    break;

                case "LACC":
                    if (ChartVisibility.LACC == true)
                    {
                        ChartVisibility.LACC = false;
                    }
                    else
                    {
                        ChartVisibility.LACC = true;
                    }
                    break;

                default:
                    break;
            }
            SetChartVisibility();
        }

        private void chartOptionsButton_Click(object sender, EventArgs e)
        {
            if (chartOptionsButton.Text == "Chart Options")
            {
                chartOptionsButton.Text = "Done";
                groupBox1.Visible = true;
            }
            else
            {
                chartOptionsButton.Text = "Chart Options";
                groupBox1.Visible = false;
            }
        }

        private void traceColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string paletteSelect = Convert.ToString(traceColorComboBox.SelectedItem);
            SetTraceColor(paletteSelect);
        }

        private void printConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogConfigDataToFileTable();
        }

        private void meterNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            meterNumber = meterNumberTextBox.Text;
            Properties.Settings.Default.meterNumber = meterNumberTextBox.Text;
        }

        private void saveConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
        }

        private void printGravityChartToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ChartMarkers(true);
            iTextSharp.text.Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

            iTextSharp.text.Font font16Normal = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            PdfWriter wri = PdfWriter.GetInstance(pdfDoc, new FileStream("C:\\LCT stuff\\GravityChart.pdf", FileMode.Create));
            pdfDoc.Open();//Open Document to write
            pdfDoc.Add(new Paragraph("         Meter # " + Form1.meterNumber + "                            Survey: " + surveyName, font16Normal));

            using (MemoryStream stream = new MemoryStream())
            {
                GravityChart.SaveImage(stream, ChartImageFormat.Png);
                Image chartImage = Image.GetInstance(stream.GetBuffer());
                chartImage.ScalePercent(55f);// Scale to 70%
                //doc.PageSize.Width gives the width in points of the document,
                //remove the margin (36 points) and the width of the image (?? points)
                //for the X-axis co-ordinate, and the margin and height of the image from the
                //total height of the document for the Y-axis co-ordinate.
                chartImage.SetAbsolutePosition(pdfDoc.PageSize.Width - 72f - 750f, pdfDoc.PageSize.Height - 36f - 200f);

                crossCouplingChart.SaveImage(stream, ChartImageFormat.Png);
                Image chartImage2 = Image.GetInstance(stream.GetBuffer());
                chartImage2.ScalePercent(55f);// Scale to 55%
                chartImage.SetAbsolutePosition(pdfDoc.PageSize.Width - 72f - 750f, pdfDoc.PageSize.Height - 36f - 400f);

                pdfDoc.Add(chartImage);
                pdfDoc.Add(chartImage2);
                pdfDoc.Close();

                Process.Start("C:\\LCT stuff\\GravityChart.pdf");
            }
            ChartMarkers(false);
        }

        private void surveyTextBox_TextChanged(object sender, EventArgs e)
        {
            surveyName = surveyTextBox.Text;
            surveyNameSet = true;
        }

        private void markersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ChartMarkers(markersCheckBox.Checked);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            // Close file

            recordingTextBox.Visible = false;
        }

        private void StartupWorker(object obj)
        {
            var _delegate = (Action<startupData>)obj;

            _delegate(new startupData { Status = "Starting gyros..." });
            Thread.Sleep(5000);
            _delegate(new startupData { Status = "Gyro status OK" });
            Thread.Sleep(1500);
            _delegate(new startupData { Status = "Startuing torque motors" });
            Thread.Sleep(1500);
            _delegate(new startupData { Status = "This will take a while...please be patient..." });
            Thread.Sleep(10000);
            _delegate(new startupData { Status = "Startup complete" });
            Thread.Sleep(3000);
            _delegate(new startupData { Status = "" });
        }

        private void autoStartupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autoStartupCheckBox.Checked)
            {
                // start thread to sumulate startup
                Thread StartupThread = new Thread(new ParameterizedThreadStart(StartupWorker));
                StartupThread.IsBackground = true;
                StartupThread.Start(new Action<startupData>(this.UpdateStartupStatus));
            }
        }

        private void UpdateStartupStatus(startupData d)// this will be the new update chart etc.
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<startupData>(this.UpdateStartupStatus), new object[] { d });
            }
            else
            {
                if (d.Status == "")
                {
                    startupLabel.Visible = false;
                    autoStartupCheckBox.Visible = false;
                }
                startupLabel.Text = d.Status;
            }
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

        #region Chart Methods

        //*****************************************************************************
        //                       Chart Methods
        //*****************************************************************************
        public static class ChartColors
        {
            public static System.Drawing.Color digitalGravity = System.Drawing.Color.SteelBlue;
            public static System.Drawing.Color springTension = System.Drawing.Color.BlueViolet;
            public static System.Drawing.Color crossCoupling = System.Drawing.Color.DarkOrange;
            public static System.Drawing.Color rawBeam = System.Drawing.Color.DeepSkyBlue;
            public static System.Drawing.Color totalCorrection = System.Drawing.Color.ForestGreen;

            //   public static System.Drawing.Color rawGravity = System.Drawing.Color.RoyalBlue;
            public static System.Drawing.Color AL = System.Drawing.Color.LightSeaGreen;

            public static System.Drawing.Color AX = System.Drawing.Color.OrangeRed;
            public static System.Drawing.Color VE = System.Drawing.Color.PaleVioletRed;
            public static System.Drawing.Color AX2 = System.Drawing.Color.Red;
            public static System.Drawing.Color LACC = System.Drawing.Color.Wheat;
            public static System.Drawing.Color XACC = System.Drawing.Color.SteelBlue;
        }

        public static class ChartVisibility
        {
            public static Boolean digitalGravity = true;
            public static Boolean springTension = true;
            public static Boolean crossCoupling = true;
            public static Boolean rawBeam = true;
            public static Boolean totalCorrection = true;

            //   public static Boolean rawGravity = true;
            public static Boolean AL = true;

            public static Boolean AX = true;
            public static Boolean VE = true;
            public static Boolean AX2 = true;
            public static Boolean LACC = true;
            public static Boolean XACC = true;
        }

        public void SetChartBorderWidth(int width)
        {
            GravityChart.Series["Digital Gravity"].BorderWidth = width;
            GravityChart.Series["Spring Tension"].BorderWidth = width;
            GravityChart.Series["Raw Beam"].BorderWidth = width;
            GravityChart.Series["Cross Coupling"].BorderWidth = width;
            GravityChart.Series["Total Correction"].BorderWidth = width;
            //  crossCouplingChart.Series["Raw Gravity"].BorderWidth = width;
            crossCouplingChart.Series["LACC"].BorderWidth = width;
            crossCouplingChart.Series["XACC"].BorderWidth = width;
            crossCouplingChart.Series["AX2"].BorderWidth = width;
            crossCouplingChart.Series["VE"].BorderWidth = width;
            crossCouplingChart.Series["AX"].BorderWidth = width;
            crossCouplingChart.Series["AL"].BorderWidth = width;
        }

        public void SetChartVisibility()
        {
            if (ChartVisibility.digitalGravity == false)
            {
                GravityChart.Series["Digital Gravity"].Color = System.Drawing.Color.Transparent;
                // this.GravityChart.Series["Digital Gravity"].Enabled = true;
            }
            else
            {
                GravityChart.Series["Digital Gravity"].Color = ChartColors.digitalGravity;
                //  this.GravityChart.Series["Digital Gravity"].Enabled = false;
            }

            if (ChartVisibility.springTension == false)
            {
                GravityChart.Series["Spring Tension"].Color = System.Drawing.Color.Transparent;
            }
            else
            {
                GravityChart.Series["Spring Tension"].Color = ChartColors.springTension;
            }

            if (ChartVisibility.crossCoupling == false)
            {
                GravityChart.Series["Cross Coupling"].Color = System.Drawing.Color.Transparent;
            }
            else
            {
                GravityChart.Series["Cross Coupling"].Color = ChartColors.crossCoupling;
            }

            if (ChartVisibility.rawBeam == false)
            {
                GravityChart.Series["Raw Beam"].Color = System.Drawing.Color.Transparent;
            }
            else
            {
                GravityChart.Series["Raw Beam"].Color = ChartColors.rawBeam;
            }

            if (ChartVisibility.totalCorrection == false)
            {
                GravityChart.Series["Total Correction"].Color = System.Drawing.Color.Transparent;
            }
            else
            {
                GravityChart.Series["Total Correction"].Color = ChartColors.totalCorrection;
            }

            if (ChartVisibility.AL == false)
            {
                crossCouplingChart.Series["AL"].Color = System.Drawing.Color.Transparent;
            }
            else
            {
                crossCouplingChart.Series["AL"].Color = ChartColors.AL;
            }

            if (ChartVisibility.AX == false)
            {
                crossCouplingChart.Series["AX"].Color = System.Drawing.Color.Transparent;
            }
            else
            {
                crossCouplingChart.Series["AX"].Color = ChartColors.AX;
            }

            if (ChartVisibility.VE == false)
            {
                crossCouplingChart.Series["VE"].Color = System.Drawing.Color.Transparent;
            }
            else
            {
                crossCouplingChart.Series["VE"].Color = ChartColors.VE;
            }

            if (ChartVisibility.AX2 == false)
            {
                crossCouplingChart.Series["AX2"].Color = System.Drawing.Color.Transparent;
            }
            else
            {
                crossCouplingChart.Series["AX2"].Color = ChartColors.AX2;
            }

            if (ChartVisibility.LACC == false)
            {
                crossCouplingChart.Series["LACC"].Color = System.Drawing.Color.Transparent;
            }
            else
            {
                crossCouplingChart.Series["LACC"].Color = ChartColors.LACC;
            }

            if (ChartVisibility.XACC == false)
            {
                crossCouplingChart.Series["XACC"].Color = System.Drawing.Color.Transparent;
            }
            else
            {
                crossCouplingChart.Series["XACC"].Color = ChartColors.XACC;
            }

            //    this.GravityChart.Update();
            //   this.crossCouplingChart.Update();
        }

        private void SetupChart()
        {
            BindingSource SBind = new BindingSource();

            // this.GravityChart.Series["Digital Gravity"].Label = "Y = #VALY\nX = #VALX"; sample

            //      SETUP MAIN PAGE GRAVITY CHART
            traceTrackBar.Value = 2;
            GravityChart.Series["Digital Gravity"].XValueMember = "dateTime";
            GravityChart.Series["Digital Gravity"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            GravityChart.Series["Digital Gravity"].YValueMembers = "gravity";
            GravityChart.Series["Digital Gravity"].ToolTip = "Time = #VALX\n#VALY";
            GravityChart.DataSource = dataTable;
            GravityChart.DataBind();

            GravityChart.Series["Digital Gravity"].XValueType = ChartValueType.DateTime;

            GravityChart.Series["Spring Tension"].XValueMember = "dateTime";
            GravityChart.Series["Spring Tension"].YValueMembers = "springTension";
            GravityChart.Series["Spring Tension"].ToolTip = "Time = #VALX\n#VALY";
            GravityChart.DataSource = dataTable;
            GravityChart.DataBind();

            GravityChart.Series["Cross Coupling"].XValueMember = "dateTime";
            GravityChart.Series["Cross Coupling"].YValueMembers = "crossCoupling";
            GravityChart.Series["Cross Coupling"].ToolTip = "#VALY";
            GravityChart.DataSource = dataTable;
            GravityChart.DataBind();

            GravityChart.Series["Raw Beam"].XValueMember = "dateTime";
            GravityChart.Series["Raw Beam"].YValueMembers = "RawBeam";
            GravityChart.Series["Raw Beam"].ToolTip = "#VALY";
            GravityChart.DataSource = dataTable;
            GravityChart.DataBind();

            GravityChart.Series["Total Correction"].XValueMember = "dateTime";
            GravityChart.Series["Total Correction"].YValueMembers = "TotalCorrection";
            GravityChart.Series["Total Correction"].ToolTip = "#VALY";
            GravityChart.DataSource = dataTable;
            GravityChart.DataBind();

            // this.GravityChart.Titles.Add("Gravity");

            //      X AXIS

            // this.GravityChart.ChartAreas["ChartArea1"].AxisX.Interval = 10;

            // Set cursor interval properties
            GravityChart.ChartAreas["ChartArea1"].CursorX.Interval = .001D;
            GravityChart.ChartAreas["ChartArea1"].CursorX.IntervalType = DateTimeIntervalType.Seconds;
            GravityChart.ChartAreas["ChartArea1"].CursorY.Interval = 1;
            GravityChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "hh:mm:ss";

            // Set automatic zooming
            GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            GravityChart.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;

            // Set automatic scrolling
            GravityChart.ChartAreas["ChartArea1"].CursorX.AutoScroll = true;
            GravityChart.ChartAreas["ChartArea1"].CursorY.AutoScroll = true;

            if (false)
            {
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(2, 3);
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset(1);
                GravityChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;// can vary from -90 to + 90

                GravityChart.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
                GravityChart.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
                GravityChart.ChartAreas["ChartArea1"].CursorX.IntervalType = DateTimeIntervalType.Minutes;
                GravityChart.ChartAreas["ChartArea1"].CursorX.Interval = .01;// .1D;

                GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.SmallScrollSizeType = DateTimeIntervalType.Minutes;
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.SmallScrollSize = .1D;
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;

                GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Minutes;
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.MinSize = .1D;

                GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Minutes;
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.SmallScrollMinSize = .1D;
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = false;

                // Change scrollbar colors
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.BackColor = System.Drawing.Color.LightGray;
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
            }

            if (true)
            {
                // Set scrollbar size
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.Size = 12;
                // show either just the center scroll button..
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
                // .. or include the left and right buttons:
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.ButtonStyle =
                     ScrollBarButtonStyles.All ^ ScrollBarButtonStyles.ResetZoom;

                // Scrollbars position
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
                GravityChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = true;
                // this.GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = 100;  // number (!) of data points visible
            }
            //  Y AXIS
            //  this.GravityChart.ChartAreas["ChartArea1"].AxisY.ScaleView.ZoomReset(1);
            GravityChart.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            GravityChart.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            GravityChart.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;

            crossCouplingChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss";
            crossCouplingChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "hh:mm:ss";
            crossCouplingChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
            crossCouplingChart.Series["AL"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;

            // Setup chart series

            crossCouplingChart.Series["AL"].XValueMember = "dateTime";
            crossCouplingChart.Series["AL"].YValueMembers = "AL";
            crossCouplingChart.DataSource = dataTable;
            crossCouplingChart.DataBind();

            crossCouplingChart.Series["AX"].XValueMember = "dateTime";
            crossCouplingChart.Series["AX"].YValueMembers = "AX";
            crossCouplingChart.DataSource = dataTable;
            crossCouplingChart.DataBind();

            crossCouplingChart.Series["VE"].XValueMember = "dateTime";
            crossCouplingChart.Series["VE"].YValueMembers = "VE";
            crossCouplingChart.DataSource = dataTable;
            crossCouplingChart.DataBind();

            crossCouplingChart.Series["AX2"].XValueMember = "dateTime";
            crossCouplingChart.Series["AX2"].YValueMembers = "AX2";
            crossCouplingChart.DataSource = dataTable;
            crossCouplingChart.DataBind();

            crossCouplingChart.Series["XACC"].XValueMember = "dateTime";
            crossCouplingChart.Series["XACC"].YValueMembers = "XACC2";
            crossCouplingChart.DataSource = dataTable;
            crossCouplingChart.DataBind();

            crossCouplingChart.Series["LACC"].XValueMember = "dateTime";
            crossCouplingChart.Series["LACC"].YValueMembers = "LACC";
            crossCouplingChart.DataSource = dataTable;
            crossCouplingChart.DataBind();

            //this.crossCouplingChart.Titles.Add("Cross Coupling");

            crossCouplingChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(2, 3);
            crossCouplingChart.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset(1);
            crossCouplingChart.ChartAreas["ChartArea1"].AxisY.ScaleView.ZoomReset(1);
            crossCouplingChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;// can vary from -90 to + 90
            //Enable range selection and zooming end user interface

            crossCouplingChart.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            crossCouplingChart.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            crossCouplingChart.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            crossCouplingChart.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            crossCouplingChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            crossCouplingChart.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            crossCouplingChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            crossCouplingChart.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            // Get default values and apply

            SetTraceColor(Properties.Settings.Default.tracePalette);//     Set trace color palette
            SetChartAreaColors(Properties.Settings.Default.chartColor);//  Set chart background color
            SetChartBorderWidth(Properties.Settings.Default.traceWidth);// Set trace width
            ChartMarkers(Properties.Settings.Default.traceMarkers);//      Enable/ disable trace markers
           
        }

        public void SetChartColors()
        {
            GravityChart.Series["Digital Gravity"].Color = ChartColors.digitalGravity;
            GravityChart.Series["Spring Tension"].Color = ChartColors.springTension;
            GravityChart.Series["Cross Coupling"].Color = ChartColors.crossCoupling;
            GravityChart.Series["Raw Beam"].Color = ChartColors.rawBeam;
            GravityChart.Series["Total Correction"].Color = ChartColors.totalCorrection;
            //  crossCouplingChart.Series["Raw Gravity"].Color = ChartColors.rawGravity;
            crossCouplingChart.Series["AL"].Color = ChartColors.AL;
            crossCouplingChart.Series["AX"].Color = ChartColors.AX;
            crossCouplingChart.Series["VE"].Color = ChartColors.VE;
            crossCouplingChart.Series["AX2"].Color = ChartColors.AX2;
            crossCouplingChart.Series["XACC"].Color = ChartColors.XACC;
            crossCouplingChart.Series["LACC"].Color = ChartColors.LACC;
            GravityChart.Update();
            crossCouplingChart.Update();
        }

        public void ChartMarkers(bool enable)
        {
            if (enable == true)
            {
                GravityChart.Series["Digital Gravity"].MarkerStyle = MarkerStyle.Circle;
                GravityChart.Series["Spring Tension"].MarkerStyle = MarkerStyle.Cross;
                GravityChart.Series["Cross Coupling"].MarkerStyle = MarkerStyle.Diamond;
                GravityChart.Series["Raw Beam"].MarkerStyle = MarkerStyle.Square;
                GravityChart.Series["Total Correction"].MarkerStyle = MarkerStyle.Triangle;

                //  crossCouplingChart.Series["Raw Gravity"].MarkerStyle = MarkerStyle.Circle;
                crossCouplingChart.Series["AL"].MarkerStyle = MarkerStyle.Triangle;
                crossCouplingChart.Series["AX"].MarkerStyle = MarkerStyle.Star5;
                crossCouplingChart.Series["VE"].MarkerStyle = MarkerStyle.Square;
                crossCouplingChart.Series["AX2"].MarkerStyle = MarkerStyle.Diamond;
                crossCouplingChart.Series["LACC"].MarkerStyle = MarkerStyle.Cross;
                crossCouplingChart.Series["XACC"].MarkerStyle = MarkerStyle.Star10;
            }
            else
            {
                GravityChart.Series["Digital Gravity"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["Spring Tension"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["Cross Coupling"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["Raw Beam"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["Total Correction"].MarkerStyle = MarkerStyle.None;

                // crossCouplingChart.Series["Raw Gravity"].MarkerStyle = MarkerStyle.None;
                crossCouplingChart.Series["AL"].MarkerStyle = MarkerStyle.None;
                crossCouplingChart.Series["AX"].MarkerStyle = MarkerStyle.None;
                crossCouplingChart.Series["VE"].MarkerStyle = MarkerStyle.None;
                crossCouplingChart.Series["AX2"].MarkerStyle = MarkerStyle.None;
                crossCouplingChart.Series["LACC"].MarkerStyle = MarkerStyle.None;
                crossCouplingChart.Series["XACC"].MarkerStyle = MarkerStyle.None;
            }
        }

        public void UpdateChart()
        {
            //   DataTable dataTable = new DataTable();
            DataRow myDataRow = dataTable.NewRow();
            DateTime firstDateTime = new DateTime(2015, 1, 1, MeterData.Hour, MeterData.Min, MeterData.Sec);
            DateTime myDateTime = firstDateTime.AddDays(MeterData.day - 1);

            //      UPDATE MAIN GRAVITY CHART
            this.GravityChart.Series["Digital Gravity"].Points.AddXY(myDateTime, MeterData.data4[2]);
            this.GravityChart.Series["Spring Tension"].Points.AddXY(myDateTime, MeterData.data1[3]);
            this.GravityChart.Series["Cross Coupling"].Points.AddXY(myDateTime, MeterData.data4[4]);
            this.GravityChart.Series["Raw Beam"].Points.AddXY(myDateTime, MeterData.data1[5]);
            this.GravityChart.Series["Total Correction"].Points.AddXY(myDateTime, MeterData.totalCorrection);

            if (firstTime)
            {
                long ticks = myDateTime.Ticks;

                GravityChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
                GravityChart.ChartAreas[0].AxisX.Interval = 10;
                GravityChart.ChartAreas[0].AxisX.Minimum = myDateTime.ToOADate();//42291.863;
                firstTime = false;
            }

            GravityChart.Update();

            //      UPDATE MAIN CROSS COUPLING CHART

            //  crossCouplingChart.Series["Raw Gravity"].Points.AddXY(myDateTime, MeterData.data4[2]);
            crossCouplingChart.Series["AL"].Points.AddXY(myDateTime, MeterData.data4[7]);
            crossCouplingChart.Series["AX"].Points.AddXY(myDateTime, MeterData.data4[8]);
            crossCouplingChart.Series["VE"].Points.AddXY(myDateTime, MeterData.data4[9]);
            crossCouplingChart.Series["AX2"].Points.AddXY(myDateTime, MeterData.data4[10]);
            crossCouplingChart.Series["LACC"].Points.AddXY(myDateTime, MeterData.data4[13]);
            crossCouplingChart.Series["XACC"].Points.AddXY(myDateTime, MeterData.data4[14]);
            crossCouplingChart.Update();

            if (false)
            {
                if (GravityChart.ChartAreas[0].AxisX.ScaleView.IsZoomed)
                {
                    double offset = this.GravityChart.ChartAreas[0].AxisX.Minimum - this.GravityChart.ChartAreas[0].AxisX.ScaleView.Position;

                    GravityChart.ChartAreas[0].AxisX.LabelStyle.IntervalOffset = offset;
                    GravityChart.ChartAreas[0].AxisX.MajorGrid.IntervalOffset = offset;
                    GravityChart.ChartAreas[0].AxisX.MajorTickMark.IntervalOffset = offset;
                    GravityChart.ChartAreas[0].AxisX.MinorGrid.IntervalOffset = offset;
                    GravityChart.ChartAreas[0].AxisX.MinorTickMark.IntervalOffset = offset;
                }
                else
                {
                    GravityChart.ChartAreas[0].AxisX.LabelStyle.IntervalOffset = 0;
                    GravityChart.ChartAreas[0].AxisX.MajorGrid.IntervalOffset = 0;
                    GravityChart.ChartAreas[0].AxisX.MajorTickMark.IntervalOffset = 0;
                    GravityChart.ChartAreas[0].AxisX.MinorGrid.IntervalOffset = 0;
                    GravityChart.ChartAreas[0].AxisX.MinorTickMark.IntervalOffset = 0;
                }
            }
        }

        public void SetChartAreaColors(int scheme)
        {
            if (scheme == 0)// Light background
            {
                //  GRAVITY
                GravityChart.ChartAreas["ChartArea1"].BackColor = System.Drawing.Color.White;
                GravityChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["ChartArea1"].AxisX2.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["ChartArea1"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.Black;

                // CROSS COUPLING
                crossCouplingChart.ChartAreas["ChartArea1"].BackColor = System.Drawing.Color.White;
                crossCouplingChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Black;
                crossCouplingChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
                crossCouplingChart.ChartAreas["ChartArea1"].AxisX2.MajorGrid.LineColor = System.Drawing.Color.Black;
                crossCouplingChart.ChartAreas["ChartArea1"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.Black;
                Properties.Settings.Default.chartColor = 0;
            }
            if (scheme == 1)// gray background
            {
                //  GRAVITY
                GravityChart.ChartAreas["ChartArea1"].BackColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["ChartArea1"].AxisX2.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["ChartArea1"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.Black;

                // CROSS COUPLING
                crossCouplingChart.ChartAreas["ChartArea1"].BackColor = System.Drawing.Color.Gray;
                crossCouplingChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Black;
                crossCouplingChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
                crossCouplingChart.ChartAreas["ChartArea1"].AxisX2.MajorGrid.LineColor = System.Drawing.Color.Black;
                crossCouplingChart.ChartAreas["ChartArea1"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.Black;
                Properties.Settings.Default.chartColor = 1;
            }
            if (scheme == 2)// black background
            {
                //  GRAVITY
                GravityChart.ChartAreas["ChartArea1"].BackColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["ChartArea1"].AxisX2.MajorGrid.LineColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["ChartArea1"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.Gray;

                // CROSS COUPLING
                crossCouplingChart.ChartAreas["ChartArea1"].BackColor = System.Drawing.Color.Black;
                crossCouplingChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
                crossCouplingChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
                crossCouplingChart.ChartAreas["ChartArea1"].AxisX2.MajorGrid.LineColor = System.Drawing.Color.Gray;
                crossCouplingChart.ChartAreas["ChartArea1"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.Gray;
                Properties.Settings.Default.chartColor = 2;
            }
        }

        private void chart1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            HitTestResult result = this.GravityChart.HitTest(e.X, e.Y);
            if (result != null && result.Object != null)
            {
                // When user hits the LegendItem
                if (result.Object is LegendItem)
                {
                    // Legend item result
                    LegendItem legendItem = (LegendItem)result.Object;

                    // series item selected
                    Series selectedSeries = (Series)legendItem.Tag;

                    if (selectedSeries != null)
                    {
                        if (selectedSeries.Enabled)
                            selectedSeries.Enabled = false;
                        else
                            selectedSeries.Enabled = true;
                    }
                }
            }
        }

        private void SetTraceColor(string colorPalette)
        {
            switch (colorPalette)
            {
                case "None":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.None;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.None;
                    Properties.Settings.Default.tracePalette = "None";
                    break;

                case "Bright":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Bright;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Bright;
                    Properties.Settings.Default.tracePalette = "Bright";
                    break;

                case "Grayscale":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Grayscale;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Grayscale;
                    Properties.Settings.Default.tracePalette = "Grayscale";
                    break;

                case "Excel":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Excel;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Excel;
                    Properties.Settings.Default.tracePalette = "Excel";
                    break;

                case "Light":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Light;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Light;
                    Properties.Settings.Default.tracePalette = "Light";
                    break;

                case "Pastel":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Pastel;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Pastel;
                    Properties.Settings.Default.tracePalette = "Pastel";
                    break;

                case "EarthTones":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.EarthTones;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.EarthTones;
                    Properties.Settings.Default.tracePalette = "EarthTones";
                    break;

                case "SemiTransparant":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.SemiTransparent;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.SemiTransparent;
                    Properties.Settings.Default.tracePalette = "SemiTransparant";
                    break;

                case "Berry":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Berry;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Berry;
                    Properties.Settings.Default.tracePalette = "Berry";
                    break;

                case "Chocolate":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Chocolate;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Chocolate;
                    Properties.Settings.Default.tracePalette = "Chocolate";
                    break;

                case "Fire":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Fire;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.Fire;
                    Properties.Settings.Default.tracePalette = "Fire";
                    break;

                case "SeaGreen":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.SeaGreen;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.SeaGreen;
                    Properties.Settings.Default.tracePalette = "SeaGreen";
                    break;

                case "BrightPastel":
                    this.GravityChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.BrightPastel;
                    this.crossCouplingChart.Palette = this.crossCouplingChart.Palette = ChartColorPalette.BrightPastel;
                    Properties.Settings.Default.tracePalette = "BrightPastel";
                    break;

                default:
                    break;
            }
            this.crossCouplingChart.ApplyPaletteColors();
            this.GravityChart.ApplyPaletteColors();
        }

        #endregion Chart Methods

        #region Meter Startup

        private void manualStartupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            manualStartupGroupBox.Visible = true;
        }

        private void switchesGyroCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Thread StartupThread = new Thread(new ParameterizedThreadStart(GyroWorker));
            StartupThread.IsBackground = true;
            StartupThread.Start(new Action<startupData>(this.UpdateManualStartupStatus));
        }

        private void GyroWorker(object obj)
        {
            var _delegate = (Action<startupData>)obj;
            _delegate(new startupData { Status = "Gyro delay" });
            Thread.Sleep(5000);
            _delegate(new startupData { Status = "Gyro (200Hz) ON" });
            Thread.Sleep(1500);
        }

        private void TorqueWorker(object obj)
        {
            var _delegate = (Action<startupData>)obj;
            _delegate(new startupData { Status = "Torque delay" });
            Thread.Sleep(2000);
            _delegate(new startupData { Status = "Torque Motors ON" });
            Thread.Sleep(1500);
        }

        private void SpringTensionWorker(object obj)
        {
            var _delegate = (Action<startupData>)obj;
            _delegate(new startupData { Status = "Spring tension delay" });
            Thread.Sleep(2000);
            _delegate(new startupData { Status = "Spring Tension ON" });
            Thread.Sleep(1500);
            _delegate(new startupData { Status = "" });
        }

        private void UpdateManualStartupStatus(startupData d)// this will be the new update chart etc.
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<startupData>(this.UpdateManualStartupStatus), new object[] { d });
            }
            else
            {
                if (d.Status == "")
                {
                    startupLabel.Visible = false;
                    autoStartupCheckBox.Visible = false;
                    manualStartupGroupBox.Visible = false;
                    manualStartupCheckBox.Visible = false;
                }
                else if (d.Status == "Gyro (200Hz) ON")
                {
                    switchesGyroCheckBox.Text = "Gyro (200Hz) ON";
                    SwitchesTorqueMotorsCheckBox.Enabled = true;
                    startupLabel.Text = "Gyro status OK";
                }
                else if (d.Status == "Gyro delay")
                {
                    startupLabel.Text = "Gyros spinning up.  Please wait...";
                }
                else if (d.Status == "Torque Motors ON")
                {
                    SwitchesTorqueMotorsCheckBox.Text = d.Status;
                    SwitchesSpringTensionCheckBox.Enabled = true;
                    startupLabel.Text = "Torque motors engaged";
                }
                else if (d.Status == "Torque delay")
                {
                    startupLabel.Text = "Engaging torque motors";
                }
                else if (d.Status == "Spring tension delay")
                {
                    startupLabel.Text = "Enabling spring tension";
                }
                else if (d.Status == "Spring Tension ON")
                {
                    startupLabel.Text = "Spring tension enabled";
                }
                else
                {
                    startupLabel.Text = d.Status;
                }
            }
        }

        private void SwitchesTorqueMotorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Thread TorqueMotorStartupThread = new Thread(new ParameterizedThreadStart(TorqueWorker));
            TorqueMotorStartupThread.IsBackground = true;
            TorqueMotorStartupThread.Start(new Action<startupData>(this.UpdateManualStartupStatus));
        }

        private void SwitchesSpringTensionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Thread STStartupThread = new Thread(new ParameterizedThreadStart(SpringTensionWorker));
            STStartupThread.IsBackground = true;
            STStartupThread.Start(new Action<startupData>(this.UpdateManualStartupStatus));
        }

        #endregion Meter Startup

        #region File Class

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
                ConfigData.meterNumber = System.Text.Encoding.Default.GetString(byte10).Trim();
                // ConfigData.meterNumber.Trim();
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
            string delimitor = ",";

            switch (Form1.fileType)
            {
                case "csv":
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

            if (fileOperation == "Open")
            {
                using (StreamWriter writer = File.CreateText(Form1.fileName))
                {
                    string header = "Date" + delimitor + "Gravity" + delimitor + "Spring Tension" + delimitor
                        + "Cross coupling" + delimitor + "Raw Beam" + delimitor + "VCC or CML" + delimitor + "AL"
                        + delimitor + "AX" + delimitor + "VE" + delimitor + "AX2 or CMX" + delimitor + "XACC2" + delimitor
                        + "LACC2" + delimitor + "XACC" + delimitor + "LACC" + delimitor + "Parallel Port" + delimitor
                        + "Platform Period" + delimitor + "AUX1" + delimitor + "AUX2" + delimitor + "AUX3" + delimitor + "AUX4";
                    writer.WriteLine(header);
                }
            }
        }

        public void RecordDataToFile(string fileOperation, myData d)
        {
            // fileOperation  0 - open,  1 - append, 2 - close
            string delimitor = ",";

            switch (Form1.fileType)
            {
                case "csv":
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
                //  fileName = "C:\\Ultrasys\\Data\\" + "GravityData" + now.ToString("yyyyMMddHHmmsstt") + ".csv";
                //  fileName = Form1.filePath;
            }
            else
            {
                //  fileName = "C:\\Ultrasys\\Data\\" + Form1.gravityFileName;
            }

            if (fileOperation == "Open")
            {
                using (StreamWriter writer = File.CreateText(Form1.fileName))
                {
                    string header = "Date" + delimitor + "Gravity" + delimitor + "Spring Tension" + delimitor
                        + "Cross coupling" + delimitor + "Raw Beam" + delimitor + "VCC or CML" + delimitor + "AL"
                        + delimitor + "AX" + delimitor + "VE" + delimitor + "AX2 or CMX" + delimitor + "XACC2" + delimitor
                        + "LACC2" + delimitor + "XACC" + delimitor + "LACC" + delimitor + "Parallel Port" + delimitor
                        + "Platform Period" + delimitor + "AUX1" + delimitor + "AUX2" + delimitor + "AUX3" + delimitor + "AUX4";
                    writer.WriteLine(header);
                }
            }
            else if (fileOperation == "Append")
            {
                string myString = Convert.ToString(d.Date) + delimitor + Convert.ToString(d.Gravity) + delimitor + Convert.ToString(d.SpringTension)
                     + delimitor + Convert.ToString(d.CrossCoupling) + delimitor + Convert.ToString(d.RawBeam) + delimitor + Convert.ToString(d.VCC)
                     + delimitor + Convert.ToString(d.AL) + delimitor + Convert.ToString(d.AX) + delimitor + Convert.ToString(d.VE)
                     + delimitor + Convert.ToString(d.AX2) + delimitor + Convert.ToString(d.XACC2) + delimitor + Convert.ToString(d.LACC2)
                     + delimitor + Convert.ToString(d.XACC) + delimitor + Convert.ToString(d.LACC) + delimitor + Convert.ToString(d.ParallelData)
                     + delimitor + Convert.ToString(d.AUX1) + delimitor + Convert.ToString(d.AUX2) + delimitor + Convert.ToString(d.AUX3)
                     + delimitor + Convert.ToString(d.AUX4);

                /*    string myString = Convert.ToString(MeterData.dataTime) + delimitor + Convert.ToString(MeterData.data4[2] * ConfigData.beamScale)
                   +delimitor + Convert.ToString(MeterData.data4[3]) + delimitor + Convert.ToString(MeterData.data4[4])
                   + delimitor + Convert.ToString(MeterData.data1[5]) + delimitor + Convert.ToString(MeterData.data1[6])
                   + delimitor + Convert.ToString(MeterData.data1[7]) + delimitor + Convert.ToString(MeterData.data1[8])
                   + delimitor + Convert.ToString(MeterData.data1[9]) + delimitor + Convert.ToString(MeterData.data1[10])
                   + delimitor + Convert.ToString(MeterData.data1[11]) + delimitor + Convert.ToString(MeterData.data1[12])
                   + delimitor + Convert.ToString(MeterData.data1[13]) + delimitor + Convert.ToString(MeterData.data1[14])
                   + delimitor + "I4PAR" + delimitor + "APERX * 1.0E6"

                    + delimitor + Convert.ToString(MeterData.data1[15]) + delimitor + Convert.ToString(MeterData.data1[16])
                    + delimitor + Convert.ToString(MeterData.data1[17]) + delimitor + Convert.ToString(MeterData.data1[18]);
                   */
                using (StreamWriter writer = File.AppendText(Form1.fileName))
                {
                    // writer.WriteLine("{0},{1},{2},{3},{4}, {5},{6}", MeterData.year, MeterData.day, MeterData.Hour, MeterData.Min, MeterData.Sec, MeterData.data4[2]);
                    //  writer.WriteLine(Convert.ToString(MeterData.dataTime), ",",Convert.ToString(MeterData.data4[2]),",");
                    writer.WriteLine(myString);
                }
            }
        }

        #endregion File Class



        private void exitProgramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveSettings();
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About myAboutForm = new About();
            myAboutForm.Show();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
                  string helpfile = "C:\\Ultrasys\\Dynamic Meter Help.chm";
                 Help.ShowHelp(this, helpfile);

              //   Help.ShowHelp(TextBox1, "file://c:\\charmap.chm");


        }

        private void fileFormatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FileFormatForm myFileForm = new FileFormatForm();
            myFileForm.Show();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////
    }
}