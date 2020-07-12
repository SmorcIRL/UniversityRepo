namespace Events
{
    partial class MainForm
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
            this.dataGridView_Results = new System.Windows.Forms.DataGridView();
            this.A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_StopCalculating = new System.Windows.Forms.Button();
            this.panel_StartPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label_EnterDelta = new System.Windows.Forms.Label();
            this.maskedTextBox_DeltaDInput = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox_DeltaCInput = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox_RadiusInput = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Start = new System.Windows.Forms.Button();
            this.Field = new System.Windows.Forms.PictureBox();
            this.button_StopShooting = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Results)).BeginInit();
            this.panel_StartPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Field)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_Results
            // 
            this.dataGridView_Results.AllowUserToAddRows = false;
            this.dataGridView_Results.AllowUserToDeleteRows = false;
            this.dataGridView_Results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Results.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.A,
            this.B,
            this.Count});
            this.dataGridView_Results.Location = new System.Drawing.Point(10, 10);
            this.dataGridView_Results.Name = "dataGridView_Results";
            this.dataGridView_Results.ReadOnly = true;
            this.dataGridView_Results.Size = new System.Drawing.Size(360, 380);
            this.dataGridView_Results.TabIndex = 0;
            // 
            // A
            // 
            this.A.HeaderText = "A";
            this.A.Name = "A";
            this.A.ReadOnly = true;
            this.A.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // B
            // 
            this.B.HeaderText = "B";
            this.B.Name = "B";
            this.B.ReadOnly = true;
            this.B.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Count
            // 
            this.Count.HeaderText = "Count";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            this.Count.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // button_StopCalculating
            // 
            this.button_StopCalculating.Location = new System.Drawing.Point(119, 402);
            this.button_StopCalculating.Name = "button_StopCalculating";
            this.button_StopCalculating.Size = new System.Drawing.Size(119, 51);
            this.button_StopCalculating.TabIndex = 1;
            this.button_StopCalculating.Text = "Stop calculating";
            this.button_StopCalculating.UseVisualStyleBackColor = true;
            this.button_StopCalculating.Click += new System.EventHandler(this.button_StopCalculating_Click);
            // 
            // panel_StartPanel
            // 
            this.panel_StartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_StartPanel.Controls.Add(this.panel1);
            this.panel_StartPanel.Controls.Add(this.button_Start);
            this.panel_StartPanel.Location = new System.Drawing.Point(211, 123);
            this.panel_StartPanel.Name = "panel_StartPanel";
            this.panel_StartPanel.Size = new System.Drawing.Size(352, 185);
            this.panel_StartPanel.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label_EnterDelta);
            this.panel1.Controls.Add(this.maskedTextBox_DeltaDInput);
            this.panel1.Controls.Add(this.maskedTextBox_DeltaCInput);
            this.panel1.Controls.Add(this.maskedTextBox_RadiusInput);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(16, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 114);
            this.panel1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(11, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "The shooting delta (ms):";
            // 
            // label_EnterDelta
            // 
            this.label_EnterDelta.AutoSize = true;
            this.label_EnterDelta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_EnterDelta.Location = new System.Drawing.Point(11, 19);
            this.label_EnterDelta.Name = "label_EnterDelta";
            this.label_EnterDelta.Size = new System.Drawing.Size(156, 20);
            this.label_EnterDelta.TabIndex = 0;
            this.label_EnterDelta.Text = "The sleep delta (ms):";
            // 
            // maskedTextBox_DeltaDInput
            // 
            this.maskedTextBox_DeltaDInput.Location = new System.Drawing.Point(212, 76);
            this.maskedTextBox_DeltaDInput.Mask = "00000";
            this.maskedTextBox_DeltaDInput.Name = "maskedTextBox_DeltaDInput";
            this.maskedTextBox_DeltaDInput.Size = new System.Drawing.Size(100, 20);
            this.maskedTextBox_DeltaDInput.TabIndex = 7;
            this.maskedTextBox_DeltaDInput.Text = "2000";
            this.maskedTextBox_DeltaDInput.ValidatingType = typeof(int);
            // 
            // maskedTextBox_DeltaCInput
            // 
            this.maskedTextBox_DeltaCInput.Location = new System.Drawing.Point(212, 21);
            this.maskedTextBox_DeltaCInput.Mask = "00000";
            this.maskedTextBox_DeltaCInput.Name = "maskedTextBox_DeltaCInput";
            this.maskedTextBox_DeltaCInput.Size = new System.Drawing.Size(100, 20);
            this.maskedTextBox_DeltaCInput.TabIndex = 3;
            this.maskedTextBox_DeltaCInput.Text = "3000";
            this.maskedTextBox_DeltaCInput.ValidatingType = typeof(int);
            // 
            // maskedTextBox_RadiusInput
            // 
            this.maskedTextBox_RadiusInput.Location = new System.Drawing.Point(212, 50);
            this.maskedTextBox_RadiusInput.Mask = "000";
            this.maskedTextBox_RadiusInput.Name = "maskedTextBox_RadiusInput";
            this.maskedTextBox_RadiusInput.Size = new System.Drawing.Size(100, 20);
            this.maskedTextBox_RadiusInput.TabIndex = 5;
            this.maskedTextBox_RadiusInput.Text = "100";
            this.maskedTextBox_RadiusInput.ValidatingType = typeof(int);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(11, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "The radius (px):";
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(131, 135);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(108, 41);
            this.button_Start.TabIndex = 2;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // Field
            // 
            this.Field.BackColor = System.Drawing.SystemColors.Window;
            this.Field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Field.Location = new System.Drawing.Point(380, 10);
            this.Field.Name = "Field";
            this.Field.Size = new System.Drawing.Size(450, 380);
            this.Field.TabIndex = 3;
            this.Field.TabStop = false;
            // 
            // button_StopShooting
            // 
            this.button_StopShooting.Location = new System.Drawing.Point(539, 402);
            this.button_StopShooting.Name = "button_StopShooting";
            this.button_StopShooting.Size = new System.Drawing.Size(119, 51);
            this.button_StopShooting.TabIndex = 4;
            this.button_StopShooting.Text = "Stop shooting";
            this.button_StopShooting.UseVisualStyleBackColor = true;
            this.button_StopShooting.Click += new System.EventHandler(this.button_StopShooting_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 462);
            this.Controls.Add(this.panel_StartPanel);
            this.Controls.Add(this.dataGridView_Results);
            this.Controls.Add(this.Field);
            this.Controls.Add(this.button_StopCalculating);
            this.Controls.Add(this.button_StopShooting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Results)).EndInit();
            this.panel_StartPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Field)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_Results;
        private System.Windows.Forms.DataGridViewTextBoxColumn A;
        private System.Windows.Forms.DataGridViewTextBoxColumn B;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.Button button_StopCalculating;
        private System.Windows.Forms.Panel panel_StartPanel;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_DeltaCInput;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Label label_EnterDelta;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_RadiusInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Field;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_DeltaDInput;
        private System.Windows.Forms.Button button_StopShooting;
    }
}