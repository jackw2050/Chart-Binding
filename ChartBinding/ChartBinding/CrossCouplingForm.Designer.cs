namespace ChartBinding
{
    partial class CrossCouplingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.crossCouplingChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hideFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visabilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawGravityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aX2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xACCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lACCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.crossCouplingChart)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // crossCouplingChart
            // 
            this.crossCouplingChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.crossCouplingChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.crossCouplingChart.Legends.Add(legend2);
            this.crossCouplingChart.Location = new System.Drawing.Point(12, 27);
            this.crossCouplingChart.Name = "crossCouplingChart";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series8.Legend = "Legend1";
            series8.Name = "Raw Gravity";
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series9.Legend = "Legend1";
            series9.Name = "AL";
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series10.Legend = "Legend1";
            series10.Name = "AX";
            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series11.Legend = "Legend1";
            series11.Name = "VE";
            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series12.Legend = "Legend1";
            series12.Name = "AX2";
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series13.Legend = "Legend1";
            series13.Name = "LACC";
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series14.Legend = "Legend1";
            series14.Name = "XACC";
            this.crossCouplingChart.Series.Add(series8);
            this.crossCouplingChart.Series.Add(series9);
            this.crossCouplingChart.Series.Add(series10);
            this.crossCouplingChart.Series.Add(series11);
            this.crossCouplingChart.Series.Add(series12);
            this.crossCouplingChart.Series.Add(series13);
            this.crossCouplingChart.Series.Add(series14);
            this.crossCouplingChart.Size = new System.Drawing.Size(798, 321);
            this.crossCouplingChart.TabIndex = 0;
            this.crossCouplingChart.Text = "chart1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideFormToolStripMenuItem,
            this.visabilityToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(822, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hideFormToolStripMenuItem
            // 
            this.hideFormToolStripMenuItem.Name = "hideFormToolStripMenuItem";
            this.hideFormToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.hideFormToolStripMenuItem.Text = "Hide Form";
            this.hideFormToolStripMenuItem.Click += new System.EventHandler(this.hideFormToolStripMenuItem_Click);
            // 
            // visabilityToolStripMenuItem
            // 
            this.visabilityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.noneToolStripMenuItem,
            this.rawGravityToolStripMenuItem,
            this.aLToolStripMenuItem,
            this.aXToolStripMenuItem,
            this.vEToolStripMenuItem,
            this.aX2ToolStripMenuItem,
            this.xACCToolStripMenuItem,
            this.lACCToolStripMenuItem});
            this.visabilityToolStripMenuItem.Name = "visabilityToolStripMenuItem";
            this.visabilityToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.visabilityToolStripMenuItem.Text = "Visability";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.allToolStripMenuItem.Text = "All";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.noneToolStripMenuItem.Text = "None";
            // 
            // rawGravityToolStripMenuItem
            // 
            this.rawGravityToolStripMenuItem.Name = "rawGravityToolStripMenuItem";
            this.rawGravityToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rawGravityToolStripMenuItem.Text = "Raw Gravity";
            // 
            // aLToolStripMenuItem
            // 
            this.aLToolStripMenuItem.Name = "aLToolStripMenuItem";
            this.aLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aLToolStripMenuItem.Text = "AL";
            // 
            // aXToolStripMenuItem
            // 
            this.aXToolStripMenuItem.Name = "aXToolStripMenuItem";
            this.aXToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aXToolStripMenuItem.Text = "AX";
            // 
            // vEToolStripMenuItem
            // 
            this.vEToolStripMenuItem.Name = "vEToolStripMenuItem";
            this.vEToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.vEToolStripMenuItem.Text = "VE";
            // 
            // aX2ToolStripMenuItem
            // 
            this.aX2ToolStripMenuItem.Name = "aX2ToolStripMenuItem";
            this.aX2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aX2ToolStripMenuItem.Text = "AX2";
            // 
            // xACCToolStripMenuItem
            // 
            this.xACCToolStripMenuItem.Name = "xACCToolStripMenuItem";
            this.xACCToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.xACCToolStripMenuItem.Text = "XACC";
            // 
            // lACCToolStripMenuItem
            // 
            this.lACCToolStripMenuItem.Name = "lACCToolStripMenuItem";
            this.lACCToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lACCToolStripMenuItem.Text = "LACC";
            // 
            // CrossCouplingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 400);
            this.ControlBox = false;
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.crossCouplingChart);
            this.Name = "CrossCouplingForm";
            this.Text = "CrossCouplingForm";
            ((System.ComponentModel.ISupportInitialize)(this.crossCouplingChart)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart crossCouplingChart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hideFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visabilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawGravityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aX2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xACCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lACCToolStripMenuItem;


    }
}