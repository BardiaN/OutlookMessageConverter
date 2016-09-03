﻿namespace OutlookMessageConverter
{
    partial class FormMain
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
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.UpButton = new System.Windows.Forms.Button();
            this.DownButton = new System.Windows.Forms.Button();
            this.MessagesTreeView = new System.Windows.Forms.TreeView();
            this.MainTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 3;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainTableLayoutPanel.Controls.Add(this.UpButton, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.DownButton, 0, 1);
            this.MainTableLayoutPanel.Controls.Add(this.MessagesTreeView, 1, 0);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 2;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.58491F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.41509F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(700, 424);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // UpButton
            // 
            this.UpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpButton.Location = new System.Drawing.Point(3, 3);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(14, 200);
            this.UpButton.TabIndex = 1;
            this.UpButton.UseVisualStyleBackColor = true;
            // 
            // DownButton
            // 
            this.DownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DownButton.Location = new System.Drawing.Point(3, 209);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(14, 212);
            this.DownButton.TabIndex = 2;
            this.DownButton.UseVisualStyleBackColor = true;
            // 
            // MessagesTreeView
            // 
            this.MessagesTreeView.AllowDrop = true;
            this.MessagesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessagesTreeView.Location = new System.Drawing.Point(23, 3);
            this.MessagesTreeView.Name = "MessagesTreeView";
            this.MainTableLayoutPanel.SetRowSpan(this.MessagesTreeView, 2);
            this.MessagesTreeView.Size = new System.Drawing.Size(334, 418);
            this.MessagesTreeView.TabIndex = 3;
            this.MessagesTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.MessagesTreeView_DragDrop);
            this.MessagesTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.MessagesTreeView_DragEnter);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 424);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Outlook MSG converter";
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.TreeView MessagesTreeView;
    }
}
