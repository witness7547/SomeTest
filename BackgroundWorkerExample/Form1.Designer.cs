﻿namespace BackgroundWorkerExample
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.startAsync = new System.Windows.Forms.Button();
            this.cancelAsync = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "0%";
            // 
            // startAsync
            // 
            this.startAsync.Location = new System.Drawing.Point(34, 167);
            this.startAsync.Name = "startAsync";
            this.startAsync.Size = new System.Drawing.Size(75, 23);
            this.startAsync.TabIndex = 1;
            this.startAsync.Text = "start";
            this.startAsync.UseVisualStyleBackColor = true;
            this.startAsync.Click += new System.EventHandler(this.startAsync_Click);
            // 
            // cancelAsync
            // 
            this.cancelAsync.Location = new System.Drawing.Point(173, 166);
            this.cancelAsync.Name = "cancelAsync";
            this.cancelAsync.Size = new System.Drawing.Size(75, 23);
            this.cancelAsync.TabIndex = 2;
            this.cancelAsync.Text = "cancel";
            this.cancelAsync.UseVisualStyleBackColor = true;
            this.cancelAsync.Click += new System.EventHandler(this.cancelAsync_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.cancelAsync);
            this.Controls.Add(this.startAsync);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startAsync;
        private System.Windows.Forms.Button cancelAsync;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

