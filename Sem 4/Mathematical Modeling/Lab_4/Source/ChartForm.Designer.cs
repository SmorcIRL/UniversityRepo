namespace Lab_4
{
    partial class ChartForm
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
            this.Label_1 = new System.Windows.Forms.Label();
            this.Label_2 = new System.Windows.Forms.Label();
            this.Chart_1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Chart_2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_2)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_1
            // 
            this.Label_1.AutoSize = true;
            this.Label_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label_1.Location = new System.Drawing.Point(287, 9);
            this.Label_1.Name = "Label_1";
            this.Label_1.Size = new System.Drawing.Size(103, 24);
            this.Label_1.TabIndex = 24;
            this.Label_1.Text = "Задание 1";
            // 
            // Label_2
            // 
            this.Label_2.AutoSize = true;
            this.Label_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label_2.Location = new System.Drawing.Point(287, 338);
            this.Label_2.Name = "Label_2";
            this.Label_2.Size = new System.Drawing.Size(103, 24);
            this.Label_2.TabIndex = 25;
            this.Label_2.Text = "Задание 2";
            // 
            // Chart_1
            // 
            chartArea1.Name = "ChartArea1";
            this.Chart_1.ChartAreas.Add(chartArea1);
            this.Chart_1.Location = new System.Drawing.Point(12, 36);
            this.Chart_1.Name = "Chart_1";
            this.Chart_1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.Chart_1.Series.Add(series1);
            this.Chart_1.Size = new System.Drawing.Size(633, 280);
            this.Chart_1.TabIndex = 27;
            this.Chart_1.Text = "7";
            // 
            // Chart_2
            // 
            chartArea2.Name = "ChartArea1";
            this.Chart_2.ChartAreas.Add(chartArea2);
            this.Chart_2.Location = new System.Drawing.Point(12, 365);
            this.Chart_2.Name = "Chart_2";
            this.Chart_2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            this.Chart_2.Series.Add(series2);
            this.Chart_2.Size = new System.Drawing.Size(633, 280);
            this.Chart_2.TabIndex = 28;
            this.Chart_2.Text = "7";
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 656);
            this.Controls.Add(this.Chart_2);
            this.Controls.Add(this.Chart_1);
            this.Controls.Add(this.Label_2);
            this.Controls.Add(this.Label_1);
            this.Name = "ChartForm";
            this.Text = "ChartForm";
            ((System.ComponentModel.ISupportInitialize)(this.Chart_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_1;
        private System.Windows.Forms.Label Label_2;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_1;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_2;
    }
}