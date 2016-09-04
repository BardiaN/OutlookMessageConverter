namespace OutlookMessageConverter
{
    partial class FormHelp
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
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxHint = new System.Windows.Forms.PictureBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.textBoxHint = new System.Windows.Forms.TextBox();
            this.checkBoxShowInLoading = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHint)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.54945F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.45055F));
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxHint, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.lblHint, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxHint, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.checkBoxShowInLoading, 1, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.09434F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.90566F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(455, 265);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // pictureBoxHint
            // 
            this.pictureBoxHint.BackgroundImage = global::OutlookMessageConverter.Properties.Resources.hint;
            this.pictureBoxHint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxHint.InitialImage = null;
            this.pictureBoxHint.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxHint.Name = "pictureBoxHint";
            this.pictureBoxHint.Size = new System.Drawing.Size(41, 29);
            this.pictureBoxHint.TabIndex = 0;
            this.pictureBoxHint.TabStop = false;
            // 
            // lblHint
            // 
            this.lblHint.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHint.Location = new System.Drawing.Point(50, 7);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(150, 20);
            this.lblHint.TabIndex = 1;
            this.lblHint.Text = "Just a few hints...";
            // 
            // textBoxHint
            // 
            this.textBoxHint.BackColor = System.Drawing.SystemColors.Info;
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxHint, 2);
            this.textBoxHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxHint.Location = new System.Drawing.Point(3, 38);
            this.textBoxHint.Multiline = true;
            this.textBoxHint.Name = "textBoxHint";
            this.textBoxHint.ReadOnly = true;
            this.textBoxHint.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHint.Size = new System.Drawing.Size(449, 193);
            this.textBoxHint.TabIndex = 2;
            // 
            // checkBoxShowInLoading
            // 
            this.checkBoxShowInLoading.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxShowInLoading.AutoSize = true;
            this.checkBoxShowInLoading.Checked = true;
            this.checkBoxShowInLoading.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowInLoading.Location = new System.Drawing.Point(50, 241);
            this.checkBoxShowInLoading.Name = "checkBoxShowInLoading";
            this.checkBoxShowInLoading.Size = new System.Drawing.Size(198, 17);
            this.checkBoxShowInLoading.TabIndex = 3;
            this.checkBoxShowInLoading.Text = "Always show when application starts";
            this.checkBoxShowInLoading.UseVisualStyleBackColor = true;
            // 
            // FormHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 265);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "FormHelp";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Help";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHelp_FormClosing);
            this.Load += new System.EventHandler(this.FormHelp_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormHelp_KeyDown);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.PictureBox pictureBoxHint;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.TextBox textBoxHint;
        private System.Windows.Forms.CheckBox checkBoxShowInLoading;
    }
}