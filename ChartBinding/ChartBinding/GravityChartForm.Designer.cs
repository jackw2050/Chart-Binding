namespace ChartBinding
{
    partial class GravityChartForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.GravityChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hideFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visabilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.digitalGravityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.springTensionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crossCouplingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawBeamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totalCorrectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.GravityChart)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GravityChart
            // 
            this.GravityChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GravityChart.BackColor = System.Drawing.Color.Ivory;
            this.GravityChart.BackImageTransparentColor = System.Drawing.Color.Black;
            this.GravityChart.BackSecondaryColor = System.Drawing.Color.Black;
            this.GravityChart.BorderSkin.PageColor = System.Drawing.Color.Black;
            chartArea1.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.Name = "ChartArea1";
            this.GravityChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.GravityChart.Legends.Add(legend1);
            this.GravityChart.Location = new System.Drawing.Point(0, 27);
            this.GravityChart.Name = "GravityChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.LabelAngle = 45;
            series1.Legend = "Legend1";
            series1.Name = "Digital Gravity";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Spring Tension";
            series2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "Cross Coupling";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.Name = "Raw Beam";
            series4.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Legend = "Legend1";
            series5.Name = "Total Correction";
            this.GravityChart.Series.Add(series1);
            this.GravityChart.Series.Add(series2);
            this.GravityChart.Series.Add(series3);
            this.GravityChart.Series.Add(series4);
            this.GravityChart.Series.Add(series5);
            this.GravityChart.Size = new System.Drawing.Size(887, 271);
            this.GravityChart.TabIndex = 18;
            this.GravityChart.Text = "Gravity";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideFormToolStripMenuItem,
            this.visabilityToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(887, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hideFormToolStripMenuItem
            // 
            this.hideFormToolStripMenuItem.Name = "hideFormToolStripMenuItem";
            this.hideFormToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.hideFormToolStripMenuItem.Text = "Hide Form";
            this.hideFormToolStripMenuItem.Click += new System.EventHandler(this.hideFormToolStripMenuItem_Click_1);
            // 
            // visabilityToolStripMenuItem
            // 
            this.visabilityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.noneToolStripMenuItem,
            this.digitalGravityToolStripMenuItem,
            this.springTensionToolStripMenuItem,
            this.crossCouplingToolStripMenuItem,
            this.rawBeamToolStripMenuItem,
            this.totalCorrectionToolStripMenuItem});
            this.visabilityToolStripMenuItem.Name = "visabilityToolStripMenuItem";
            this.visabilityToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.visabilityToolStripMenuItem.Text = "Visibility";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.allToolStripMenuItem.Text = "All";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.noneToolStripMenuItem.Text = "None";
            // 
            // digitalGravityToolStripMenuItem
            // 
            this.digitalGravityToolStripMenuItem.Name = "digitalGravityToolStripMenuItem";
            this.digitalGravityToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.digitalGravityToolStripMenuItem.Text = "Digital Gravity";
            this.digitalGravityToolStripMenuItem.Click += new System.EventHandler(this.digitalGravityToolStripMenuItem_Click);
            // 
            // springTensionToolStripMenuItem
            // 
            this.springTensionToolStripMenuItem.Name = "springTensionToolStripMenuItem";
            this.springTensionToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.springTensionToolStripMenuItem.Text = "Spring Tension";
            this.springTensionToolStripMenuItem.Click += new System.EventHandler(this.springTensionToolStripMenuItem_Click);
            // 
            // crossCouplingToolStripMenuItem
            // 
            this.crossCouplingToolStripMenuItem.Name = "crossCouplingToolStripMenuItem";
            this.crossCouplingToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.crossCouplingToolStripMenuItem.Text = "Cross Coupling";
            this.crossCouplingToolStripMenuItem.Click += new System.EventHandler(this.crossCouplingToolStripMenuItem_Click);
            // 
            // rawBeamToolStripMenuItem
            // 
            this.rawBeamToolStripMenuItem.Name = "rawBeamToolStripMenuItem";
            this.rawBeamToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.rawBeamToolStripMenuItem.Text = "Raw Beam";
            this.rawBeamToolStripMenuItem.Click += new System.EventHandler(this.rawBeamToolStripMenuItem_Click);
            // 
            // totalCorrectionToolStripMenuItem
            // 
            this.totalCorrectionToolStripMenuItem.Name = "totalCorrectionToolStripMenuItem";
            this.totalCorrectionToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.totalCorrectionToolStripMenuItem.Text = "Total Correction";
            this.totalCorrectionToolStripMenuItem.Click += new System.EventHandler(this.totalCorrectionToolStripMenuItem_Click);
            // 
            // GravityChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(887, 301);
            this.ControlBox = false;
            this.Controls.Add(this.GravityChart);
            this.Controls.Add(this.menuStrip1);
            this.Location = new System.Drawing.Point(50, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GravityChartForm";
            this.Text = "Gravity";
            this.Load += new System.EventHandler(this.GravityChartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GravityChart)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart GravityChart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hideFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visabilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem digitalGravityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem springTensionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crossCouplingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawBeamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totalCorrectionToolStripMenuItem;

    }
}