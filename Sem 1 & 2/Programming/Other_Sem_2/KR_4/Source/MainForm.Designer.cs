namespace KR_4
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
            this.Field = new System.Windows.Forms.PictureBox();
            this.label_scale = new System.Windows.Forms.Label();
            this.panel_info = new System.Windows.Forms.Panel();
            this.label_axis_y = new System.Windows.Forms.Label();
            this.label_axis_x = new System.Windows.Forms.Label();
            this.label_fourth_4 = new System.Windows.Forms.Label();
            this.label_fourth_3 = new System.Windows.Forms.Label();
            this.label_fourth_2 = new System.Windows.Forms.Label();
            this.label_fourth_1 = new System.Windows.Forms.Label();
            this.label_info_additional = new System.Windows.Forms.Label();
            this.label_info_points = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.label_info_status = new System.Windows.Forms.Label();
            this.label_info_logo = new System.Windows.Forms.Label();
            this.pointTable = new System.Windows.Forms.DataGridView();
            this.Module = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Field)).BeginInit();
            this.panel_info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pointTable)).BeginInit();
            this.SuspendLayout();
            // 
            // Field
            // 
            this.Field.BackColor = System.Drawing.Color.White;
            this.Field.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Field.Location = new System.Drawing.Point(10, 10);
            this.Field.Name = "Field";
            this.Field.Size = new System.Drawing.Size(500, 500);
            this.Field.TabIndex = 0;
            this.Field.TabStop = false;
            // 
            // label_scale
            // 
            this.label_scale.AutoSize = true;
            this.label_scale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label_scale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_scale.Location = new System.Drawing.Point(447, 487);
            this.label_scale.Name = "label_scale";
            this.label_scale.Size = new System.Drawing.Size(54, 15);
            this.label_scale.TabIndex = 1;
            this.label_scale.Text = "Scale 1:4";
            // 
            // panel_info
            // 
            this.panel_info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_info.Controls.Add(this.label_axis_y);
            this.panel_info.Controls.Add(this.label_axis_x);
            this.panel_info.Controls.Add(this.label_fourth_4);
            this.panel_info.Controls.Add(this.label_fourth_3);
            this.panel_info.Controls.Add(this.label_fourth_2);
            this.panel_info.Controls.Add(this.label_fourth_1);
            this.panel_info.Controls.Add(this.label_info_additional);
            this.panel_info.Controls.Add(this.label_info_points);
            this.panel_info.Controls.Add(this.label_status);
            this.panel_info.Controls.Add(this.label_info_status);
            this.panel_info.Controls.Add(this.label_info_logo);
            this.panel_info.Controls.Add(this.pointTable);
            this.panel_info.Location = new System.Drawing.Point(528, 10);
            this.panel_info.Name = "panel_info";
            this.panel_info.Size = new System.Drawing.Size(430, 500);
            this.panel_info.TabIndex = 2;
            // 
            // label_axis_y
            // 
            this.label_axis_y.AutoSize = true;
            this.label_axis_y.Location = new System.Drawing.Point(250, 471);
            this.label_axis_y.Name = "label_axis_y";
            this.label_axis_y.Size = new System.Drawing.Size(45, 13);
            this.label_axis_y.TabIndex = 11;
            this.label_axis_y.Text = "Axis Y:  ";
            // 
            // label_axis_x
            // 
            this.label_axis_x.AutoSize = true;
            this.label_axis_x.Location = new System.Drawing.Point(84, 471);
            this.label_axis_x.Name = "label_axis_x";
            this.label_axis_x.Size = new System.Drawing.Size(42, 13);
            this.label_axis_x.TabIndex = 10;
            this.label_axis_x.Text = "Axis X: ";
            // 
            // label_fourth_4
            // 
            this.label_fourth_4.AutoSize = true;
            this.label_fourth_4.Location = new System.Drawing.Point(250, 439);
            this.label_fourth_4.Name = "label_fourth_4";
            this.label_fourth_4.Size = new System.Drawing.Size(73, 13);
            this.label_fourth_4.TabIndex = 9;
            this.label_fourth_4.Text = "Fourth fourth: ";
            // 
            // label_fourth_3
            // 
            this.label_fourth_3.AutoSize = true;
            this.label_fourth_3.Location = new System.Drawing.Point(250, 416);
            this.label_fourth_3.Name = "label_fourth_3";
            this.label_fourth_3.Size = new System.Drawing.Size(67, 13);
            this.label_fourth_3.TabIndex = 8;
            this.label_fourth_3.Text = "Third fourth: ";
            // 
            // label_fourth_2
            // 
            this.label_fourth_2.AutoSize = true;
            this.label_fourth_2.Location = new System.Drawing.Point(84, 439);
            this.label_fourth_2.Name = "label_fourth_2";
            this.label_fourth_2.Size = new System.Drawing.Size(80, 13);
            this.label_fourth_2.TabIndex = 7;
            this.label_fourth_2.Text = "Second fourth: ";
            // 
            // label_fourth_1
            // 
            this.label_fourth_1.AutoSize = true;
            this.label_fourth_1.Location = new System.Drawing.Point(84, 416);
            this.label_fourth_1.Name = "label_fourth_1";
            this.label_fourth_1.Size = new System.Drawing.Size(62, 13);
            this.label_fourth_1.TabIndex = 6;
            this.label_fourth_1.Text = "First fourth: ";
            // 
            // label_info_additional
            // 
            this.label_info_additional.AutoSize = true;
            this.label_info_additional.Location = new System.Drawing.Point(3, 416);
            this.label_info_additional.Name = "label_info_additional";
            this.label_info_additional.Size = new System.Drawing.Size(56, 13);
            this.label_info_additional.TabIndex = 5;
            this.label_info_additional.Text = "Additional:";
            // 
            // label_info_points
            // 
            this.label_info_points.AutoSize = true;
            this.label_info_points.Location = new System.Drawing.Point(3, 78);
            this.label_info_points.Name = "label_info_points";
            this.label_info_points.Size = new System.Drawing.Size(75, 13);
            this.label_info_points.TabIndex = 4;
            this.label_info_points.Text = "Sorted points: ";
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.ForeColor = System.Drawing.Color.Red;
            this.label_status.Location = new System.Drawing.Point(81, 32);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(98, 13);
            this.label_status.TabIndex = 2;
            this.label_status.Text = "Input file not found.";
            // 
            // label_info_status
            // 
            this.label_info_status.AutoSize = true;
            this.label_info_status.Location = new System.Drawing.Point(3, 32);
            this.label_info_status.Name = "label_info_status";
            this.label_info_status.Size = new System.Drawing.Size(43, 13);
            this.label_info_status.TabIndex = 1;
            this.label_info_status.Text = "Status: ";
            // 
            // label_info_logo
            // 
            this.label_info_logo.AutoSize = true;
            this.label_info_logo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_info_logo.Location = new System.Drawing.Point(195, 4);
            this.label_info_logo.Name = "label_info_logo";
            this.label_info_logo.Size = new System.Drawing.Size(35, 17);
            this.label_info_logo.TabIndex = 0;
            this.label_info_logo.Text = "Info";
            // 
            // pointTable
            // 
            this.pointTable.AllowUserToAddRows = false;
            this.pointTable.AllowUserToDeleteRows = false;
            this.pointTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pointTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Module,
            this.X,
            this.Y});
            this.pointTable.Location = new System.Drawing.Point(84, 78);
            this.pointTable.Name = "pointTable";
            this.pointTable.ReadOnly = true;
            this.pointTable.Size = new System.Drawing.Size(307, 311);
            this.pointTable.TabIndex = 3;
            // 
            // Module
            // 
            this.Module.HeaderText = "Module";
            this.Module.Name = "Module";
            this.Module.ReadOnly = true;
            this.Module.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Module.Width = 80;
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.ReadOnly = true;
            this.X.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.X.Width = 80;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.ReadOnly = true;
            this.Y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Y.Width = 80;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 524);
            this.Controls.Add(this.panel_info);
            this.Controls.Add(this.label_scale);
            this.Controls.Add(this.Field);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.Field)).EndInit();
            this.panel_info.ResumeLayout(false);
            this.panel_info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pointTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Field;
        private System.Windows.Forms.Label label_scale;
        private System.Windows.Forms.Panel panel_info;
        private System.Windows.Forms.Label label_axis_y;
        private System.Windows.Forms.Label label_axis_x;
        private System.Windows.Forms.Label label_fourth_4;
        private System.Windows.Forms.Label label_fourth_3;
        private System.Windows.Forms.Label label_fourth_2;
        private System.Windows.Forms.Label label_fourth_1;
        private System.Windows.Forms.Label label_info_additional;
        private System.Windows.Forms.Label label_info_points;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Label label_info_status;
        private System.Windows.Forms.Label label_info_logo;
        private System.Windows.Forms.DataGridView pointTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Module;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
    }
}

