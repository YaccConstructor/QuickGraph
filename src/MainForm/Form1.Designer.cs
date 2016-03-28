namespace MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.algorithmsList = new System.Windows.Forms.ListBox();
            this.codeEditorPanel = new System.Windows.Forms.Panel();
            this.playerPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.algorithmsList);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 529);
            this.panel1.TabIndex = 0;
            // 
            // algorithmsList
            // 
            this.algorithmsList.FormattingEnabled = true;
            this.algorithmsList.Location = new System.Drawing.Point(0, 0);
            this.algorithmsList.Name = "algorithmsList";
            this.algorithmsList.Size = new System.Drawing.Size(138, 537);
            this.algorithmsList.TabIndex = 0;
            this.algorithmsList.SelectedIndexChanged += new System.EventHandler(this.algorithmsList_SelectedValueChanged);
            this.algorithmsList.SelectedValueChanged += new System.EventHandler(this.algorithmsList_SelectedValueChanged);
            // 
            // codeEditorPanel
            // 
            this.codeEditorPanel.Location = new System.Drawing.Point(145, 0);
            this.codeEditorPanel.Name = "codeEditorPanel";
            this.codeEditorPanel.Size = new System.Drawing.Size(273, 270);
            this.codeEditorPanel.TabIndex = 1;
            // 
            // playerPanel
            // 
            this.playerPanel.Location = new System.Drawing.Point(424, 0);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(696, 568);
            this.playerPanel.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(145, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "New";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(226, 276);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(307, 276);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Open";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(424, 574);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(132, 30);
            this.button4.TabIndex = 5;
            this.button4.Text = "Previous";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(988, 574);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(132, 30);
            this.button5.TabIndex = 6;
            this.button5.Text = "Next";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(7, 574);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(132, 30);
            this.button6.TabIndex = 7;
            this.button6.Text = "Run";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 643);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.playerPanel);
            this.Controls.Add(this.codeEditorPanel);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Graph Algorithms";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel codeEditorPanel;
        public System.Windows.Forms.Panel playerPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        public System.Windows.Forms.ListBox algorithmsList;
    }
}

