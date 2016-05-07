namespace MainForm
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
            this.editorGroupBox = new System.Windows.Forms.GroupBox();
            this.editorHost = new System.Windows.Forms.Integration.ElementHost();
            this.editorSaveButton = new System.Windows.Forms.Button();
            this.editorOpenButton = new System.Windows.Forms.Button();
            this.editorNewButton = new System.Windows.Forms.Button();
            this.algorithmPlaybackGroupBox = new System.Windows.Forms.GroupBox();
            this.algorithmFinishedLabel = new System.Windows.Forms.Label();
            this.playbackPanel = new System.Windows.Forms.Panel();
            this.nextStepButton = new System.Windows.Forms.Button();
            this.previousStepButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.algorithmPicker = new System.Windows.Forms.ComboBox();
            this.algorithmPickerGroupBox = new System.Windows.Forms.GroupBox();
            this.algorithmInfoPanel = new System.Windows.Forms.Panel();
            this.algorithmInfoLabel = new System.Windows.Forms.Label();
            this.algorithmOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.noOptionsLabel = new System.Windows.Forms.Label();
            this.editorGroupBox.SuspendLayout();
            this.algorithmPlaybackGroupBox.SuspendLayout();
            this.algorithmPickerGroupBox.SuspendLayout();
            this.algorithmInfoPanel.SuspendLayout();
            this.algorithmOptionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // editorGroupBox
            // 
            this.editorGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.editorGroupBox.Controls.Add(this.editorHost);
            this.editorGroupBox.Controls.Add(this.editorSaveButton);
            this.editorGroupBox.Controls.Add(this.editorOpenButton);
            this.editorGroupBox.Controls.Add(this.editorNewButton);
            this.editorGroupBox.Location = new System.Drawing.Point(12, 12);
            this.editorGroupBox.Name = "editorGroupBox";
            this.editorGroupBox.Size = new System.Drawing.Size(282, 537);
            this.editorGroupBox.TabIndex = 9;
            this.editorGroupBox.TabStop = false;
            this.editorGroupBox.Text = "Graph source";
            // 
            // editorHost
            // 
            this.editorHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.editorHost.Location = new System.Drawing.Point(6, 48);
            this.editorHost.Name = "editorHost";
            this.editorHost.Size = new System.Drawing.Size(270, 483);
            this.editorHost.TabIndex = 3;
            this.editorHost.Text = "elementHost1";
            this.editorHost.Child = null;
            // 
            // editorSaveButton
            // 
            this.editorSaveButton.Enabled = false;
            this.editorSaveButton.Location = new System.Drawing.Point(169, 19);
            this.editorSaveButton.Name = "editorSaveButton";
            this.editorSaveButton.Size = new System.Drawing.Size(75, 23);
            this.editorSaveButton.TabIndex = 2;
            this.editorSaveButton.Text = "Save";
            this.editorSaveButton.UseVisualStyleBackColor = true;
            // 
            // editorOpenButton
            // 
            this.editorOpenButton.Location = new System.Drawing.Point(88, 19);
            this.editorOpenButton.Name = "editorOpenButton";
            this.editorOpenButton.Size = new System.Drawing.Size(75, 23);
            this.editorOpenButton.TabIndex = 1;
            this.editorOpenButton.Text = "Open";
            this.editorOpenButton.UseVisualStyleBackColor = true;
            this.editorOpenButton.Click += new System.EventHandler(this.editorOpenButton_Click);
            // 
            // editorNewButton
            // 
            this.editorNewButton.Location = new System.Drawing.Point(6, 19);
            this.editorNewButton.Name = "editorNewButton";
            this.editorNewButton.Size = new System.Drawing.Size(75, 23);
            this.editorNewButton.TabIndex = 0;
            this.editorNewButton.Text = "New";
            this.editorNewButton.UseVisualStyleBackColor = true;
            this.editorNewButton.Click += new System.EventHandler(this.editorNewButton_Click);
            // 
            // algorithmPlaybackGroupBox
            // 
            this.algorithmPlaybackGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.algorithmPlaybackGroupBox.Controls.Add(this.algorithmFinishedLabel);
            this.algorithmPlaybackGroupBox.Controls.Add(this.playbackPanel);
            this.algorithmPlaybackGroupBox.Controls.Add(this.nextStepButton);
            this.algorithmPlaybackGroupBox.Controls.Add(this.previousStepButton);
            this.algorithmPlaybackGroupBox.Controls.Add(this.startButton);
            this.algorithmPlaybackGroupBox.Location = new System.Drawing.Point(300, 132);
            this.algorithmPlaybackGroupBox.Name = "algorithmPlaybackGroupBox";
            this.algorithmPlaybackGroupBox.Size = new System.Drawing.Size(696, 417);
            this.algorithmPlaybackGroupBox.TabIndex = 10;
            this.algorithmPlaybackGroupBox.TabStop = false;
            this.algorithmPlaybackGroupBox.Text = "Algorithm playback";
            // 
            // algorithmFinishedLabel
            // 
            this.algorithmFinishedLabel.AutoSize = true;
            this.algorithmFinishedLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.algorithmFinishedLabel.Location = new System.Drawing.Point(87, 24);
            this.algorithmFinishedLabel.Name = "algorithmFinishedLabel";
            this.algorithmFinishedLabel.Size = new System.Drawing.Size(139, 13);
            this.algorithmFinishedLabel.TabIndex = 9;
            this.algorithmFinishedLabel.Text = "Algorithm has been finished.";
            this.algorithmFinishedLabel.Visible = false;
            // 
            // playbackPanel
            // 
            this.playbackPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playbackPanel.Location = new System.Drawing.Point(6, 48);
            this.playbackPanel.Name = "playbackPanel";
            this.playbackPanel.Size = new System.Drawing.Size(684, 363);
            this.playbackPanel.TabIndex = 8;
            // 
            // nextStepButton
            // 
            this.nextStepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nextStepButton.Enabled = false;
            this.nextStepButton.Location = new System.Drawing.Point(615, 19);
            this.nextStepButton.Name = "nextStepButton";
            this.nextStepButton.Size = new System.Drawing.Size(75, 23);
            this.nextStepButton.TabIndex = 7;
            this.nextStepButton.Text = "Next";
            this.nextStepButton.UseVisualStyleBackColor = true;
            this.nextStepButton.Click += new System.EventHandler(this.nextStepButton_Click);
            // 
            // previousStepButton
            // 
            this.previousStepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.previousStepButton.Enabled = false;
            this.previousStepButton.Location = new System.Drawing.Point(534, 19);
            this.previousStepButton.Name = "previousStepButton";
            this.previousStepButton.Size = new System.Drawing.Size(75, 23);
            this.previousStepButton.TabIndex = 6;
            this.previousStepButton.Text = "Previous";
            this.previousStepButton.UseVisualStyleBackColor = true;
            this.previousStepButton.Click += new System.EventHandler(this.previousStepButton_Click);
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(6, 19);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // algorithmPicker
            // 
            this.algorithmPicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmPicker.FormattingEnabled = true;
            this.algorithmPicker.Location = new System.Drawing.Point(6, 20);
            this.algorithmPicker.Name = "algorithmPicker";
            this.algorithmPicker.Size = new System.Drawing.Size(184, 21);
            this.algorithmPicker.TabIndex = 4;
            this.algorithmPicker.SelectedIndexChanged += new System.EventHandler(this.algorithmPicker_SelectedIndexChanged);
            // 
            // algorithmPickerGroupBox
            // 
            this.algorithmPickerGroupBox.Controls.Add(this.algorithmInfoPanel);
            this.algorithmPickerGroupBox.Controls.Add(this.algorithmPicker);
            this.algorithmPickerGroupBox.Location = new System.Drawing.Point(301, 13);
            this.algorithmPickerGroupBox.Name = "algorithmPickerGroupBox";
            this.algorithmPickerGroupBox.Size = new System.Drawing.Size(323, 113);
            this.algorithmPickerGroupBox.TabIndex = 11;
            this.algorithmPickerGroupBox.TabStop = false;
            this.algorithmPickerGroupBox.Text = "Algorithm picker";
            // 
            // algorithmInfoPanel
            // 
            this.algorithmInfoPanel.Controls.Add(this.algorithmInfoLabel);
            this.algorithmInfoPanel.Location = new System.Drawing.Point(6, 47);
            this.algorithmInfoPanel.Name = "algorithmInfoPanel";
            this.algorithmInfoPanel.Size = new System.Drawing.Size(311, 63);
            this.algorithmInfoPanel.TabIndex = 11;
            // 
            // algorithmInfoLabel
            // 
            this.algorithmInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.algorithmInfoLabel.Location = new System.Drawing.Point(0, 0);
            this.algorithmInfoLabel.Name = "algorithmInfoLabel";
            this.algorithmInfoLabel.Size = new System.Drawing.Size(311, 63);
            this.algorithmInfoLabel.TabIndex = 11;
            // 
            // algorithmOptionsGroupBox
            // 
            this.algorithmOptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.algorithmOptionsGroupBox.Controls.Add(this.noOptionsLabel);
            this.algorithmOptionsGroupBox.Location = new System.Drawing.Point(630, 13);
            this.algorithmOptionsGroupBox.Name = "algorithmOptionsGroupBox";
            this.algorithmOptionsGroupBox.Size = new System.Drawing.Size(366, 113);
            this.algorithmOptionsGroupBox.TabIndex = 12;
            this.algorithmOptionsGroupBox.TabStop = false;
            this.algorithmOptionsGroupBox.Text = "Algorithm options";
            // 
            // noOptionsLabel
            // 
            this.noOptionsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noOptionsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noOptionsLabel.Location = new System.Drawing.Point(3, 16);
            this.noOptionsLabel.Name = "noOptionsLabel";
            this.noOptionsLabel.Size = new System.Drawing.Size(360, 94);
            this.noOptionsLabel.TabIndex = 1;
            this.noOptionsLabel.Text = "No algorithm selected.";
            this.noOptionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.algorithmOptionsGroupBox);
            this.Controls.Add(this.algorithmPickerGroupBox);
            this.Controls.Add(this.algorithmPlaybackGroupBox);
            this.Controls.Add(this.editorGroupBox);
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "MainForm";
            this.Text = "Graph Algorithms";
            this.editorGroupBox.ResumeLayout(false);
            this.algorithmPlaybackGroupBox.ResumeLayout(false);
            this.algorithmPlaybackGroupBox.PerformLayout();
            this.algorithmPickerGroupBox.ResumeLayout(false);
            this.algorithmInfoPanel.ResumeLayout(false);
            this.algorithmOptionsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox editorGroupBox;
        private System.Windows.Forms.Button editorSaveButton;
        private System.Windows.Forms.Button editorOpenButton;
        private System.Windows.Forms.Button editorNewButton;
        private System.Windows.Forms.GroupBox algorithmPlaybackGroupBox;
        internal System.Windows.Forms.ComboBox algorithmPicker;
        private System.Windows.Forms.GroupBox algorithmPickerGroupBox;
        private System.Windows.Forms.GroupBox algorithmOptionsGroupBox;
        private System.Windows.Forms.Button nextStepButton;
        private System.Windows.Forms.Button previousStepButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Panel algorithmInfoPanel;
        private System.Windows.Forms.Label algorithmInfoLabel;
        private System.Windows.Forms.Panel playbackPanel;
        private System.Windows.Forms.Label algorithmFinishedLabel;
        private System.Windows.Forms.Label noOptionsLabel;
        private System.Windows.Forms.Integration.ElementHost editorHost;
    }
}

