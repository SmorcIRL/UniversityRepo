namespace Lab_1
{
    partial class СhartForm
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Chart_1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Chart_2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_2)).BeginInit();
            this.SuspendLayout();
            // 
            // Chart_1
            // 
            chartArea1.Name = "ChartArea1";
            this.Chart_1.ChartAreas.Add(chartArea1);
            this.Chart_1.Location = new System.Drawing.Point(12, 12);
            this.Chart_1.Name = "Chart_1";
            this.Chart_1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.Chart_1.Series.Add(series1);
            this.Chart_1.Size = new System.Drawing.Size(300, 300);
            this.Chart_1.TabIndex = 18;
            // 
            // Chart_2
            // 
            chartArea2.Name = "ChartArea1";
            this.Chart_2.ChartAreas.Add(chartArea2);
            this.Chart_2.Location = new System.Drawing.Point(337, 12);
            this.Chart_2.Name = "Chart_2";
            this.Chart_2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            this.Chart_2.Series.Add(series2);
            this.Chart_2.Size = new System.Drawing.Size(300, 300);
            this.Chart_2.TabIndex = 19;
            // 
            // СhartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 321);
            this.Controls.Add(this.Chart_2);
            this.Controls.Add(this.Chart_1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "СhartForm";
            this.Text = "СhartForm";
            ((System.ComponentModel.ISupportInitialize)(this.Chart_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_1;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_2;
    }
}