namespace TrackFilter
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
            this.FilterData = new System.Windows.Forms.Button();
            this.ProgressLbl = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectedPathLbl = new System.Windows.Forms.Label();
            this.SelectFolder = new System.Windows.Forms.Button();
            this.CancelFilter = new System.Windows.Forms.Button();
            this.TotalFrames = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MaxDistance = new System.Windows.Forms.NumericUpDown();
            this.MinFrames = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.TotalFrames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinFrames)).BeginInit();
            this.SuspendLayout();
            // 
            // FilterData
            // 
            this.FilterData.Location = new System.Drawing.Point(170, 199);
            this.FilterData.Name = "FilterData";
            this.FilterData.Size = new System.Drawing.Size(126, 50);
            this.FilterData.TabIndex = 0;
            this.FilterData.Text = "Filter Data";
            this.FilterData.UseVisualStyleBackColor = true;
            this.FilterData.Click += new System.EventHandler(this.FilterData_Click);
            // 
            // ProgressLbl
            // 
            this.ProgressLbl.AutoSize = true;
            this.ProgressLbl.Location = new System.Drawing.Point(178, 91);
            this.ProgressLbl.Name = "ProgressLbl";
            this.ProgressLbl.Size = new System.Drawing.Size(88, 13);
            this.ProgressLbl.TabIndex = 1;
            this.ProgressLbl.Text = "Processing ... 0%";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 107);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(436, 22);
            this.progressBar.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Selected Folder:";
            // 
            // SelectedPathLbl
            // 
            this.SelectedPathLbl.AutoSize = true;
            this.SelectedPathLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SelectedPathLbl.Location = new System.Drawing.Point(15, 168);
            this.SelectedPathLbl.Name = "SelectedPathLbl";
            this.SelectedPathLbl.Size = new System.Drawing.Size(35, 15);
            this.SelectedPathLbl.TabIndex = 4;
            this.SelectedPathLbl.Text = "None";
            // 
            // SelectFolder
            // 
            this.SelectFolder.Location = new System.Drawing.Point(15, 199);
            this.SelectFolder.Name = "SelectFolder";
            this.SelectFolder.Size = new System.Drawing.Size(126, 50);
            this.SelectFolder.TabIndex = 5;
            this.SelectFolder.Text = "Select Folder";
            this.SelectFolder.UseVisualStyleBackColor = true;
            this.SelectFolder.Click += new System.EventHandler(this.SelectFolder_Click);
            // 
            // CancelFilter
            // 
            this.CancelFilter.Location = new System.Drawing.Point(321, 199);
            this.CancelFilter.Name = "CancelFilter";
            this.CancelFilter.Size = new System.Drawing.Size(126, 50);
            this.CancelFilter.TabIndex = 6;
            this.CancelFilter.Text = "Cancel";
            this.CancelFilter.UseVisualStyleBackColor = true;
            this.CancelFilter.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // TotalFrames
            // 
            this.TotalFrames.Location = new System.Drawing.Point(15, 12);
            this.TotalFrames.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.TotalFrames.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TotalFrames.Name = "TotalFrames";
            this.TotalFrames.Size = new System.Drawing.Size(49, 20);
            this.TotalFrames.TabIndex = 7;
            this.TotalFrames.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Total Number of Frames";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Minimum Number of Frames";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(70, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Maximum Distance";
            // 
            // MaxDistance
            // 
            this.MaxDistance.DecimalPlaces = 2;
            this.MaxDistance.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.MaxDistance.Location = new System.Drawing.Point(15, 64);
            this.MaxDistance.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxDistance.Name = "MaxDistance";
            this.MaxDistance.Size = new System.Drawing.Size(49, 20);
            this.MaxDistance.TabIndex = 11;
            this.MaxDistance.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            // 
            // MinFrames
            // 
            this.MinFrames.Location = new System.Drawing.Point(15, 38);
            this.MinFrames.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MinFrames.Name = "MinFrames";
            this.MinFrames.Size = new System.Drawing.Size(49, 20);
            this.MinFrames.TabIndex = 13;
            this.MinFrames.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 261);
            this.Controls.Add(this.MinFrames);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.MaxDistance);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TotalFrames);
            this.Controls.Add(this.CancelFilter);
            this.Controls.Add(this.SelectFolder);
            this.Controls.Add(this.SelectedPathLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.ProgressLbl);
            this.Controls.Add(this.FilterData);
            this.MaximumSize = new System.Drawing.Size(475, 300);
            this.MinimumSize = new System.Drawing.Size(475, 300);
            this.Name = "Form1";
            this.Text = "Track Filter";
            ((System.ComponentModel.ISupportInitialize)(this.TotalFrames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinFrames)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FilterData;
        private System.Windows.Forms.Label ProgressLbl;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label SelectedPathLbl;
        private System.Windows.Forms.Button SelectFolder;
        private System.Windows.Forms.Button CancelFilter;
        private System.Windows.Forms.NumericUpDown TotalFrames;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown MaxDistance;
        private System.Windows.Forms.NumericUpDown MinFrames;
    }
}

