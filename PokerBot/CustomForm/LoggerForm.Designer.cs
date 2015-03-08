namespace PokerBot.CustomForm
{
    partial class LoggerForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._memoryReaderLogger = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._posteGreLogger = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._richTextBoxTrainingLogger = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 561);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._memoryReaderLogger);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "MemoryReader";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _memoryReaderLogger
            // 
            this._memoryReaderLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this._memoryReaderLogger.Location = new System.Drawing.Point(3, 3);
            this._memoryReaderLogger.Name = "_memoryReaderLogger";
            this._memoryReaderLogger.Size = new System.Drawing.Size(770, 529);
            this._memoryReaderLogger.TabIndex = 0;
            this._memoryReaderLogger.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._posteGreLogger);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 535);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Postgres";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _posteGreLogger
            // 
            this._posteGreLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this._posteGreLogger.Location = new System.Drawing.Point(3, 3);
            this._posteGreLogger.Name = "_posteGreLogger";
            this._posteGreLogger.Size = new System.Drawing.Size(770, 529);
            this._posteGreLogger.TabIndex = 0;
            this._posteGreLogger.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._richTextBoxTrainingLogger);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(776, 535);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "TrainingLog";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // _richTextBoxTrainingLogger
            // 
            this._richTextBoxTrainingLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this._richTextBoxTrainingLogger.Location = new System.Drawing.Point(3, 3);
            this._richTextBoxTrainingLogger.Name = "_richTextBoxTrainingLogger";
            this._richTextBoxTrainingLogger.Size = new System.Drawing.Size(770, 529);
            this._richTextBoxTrainingLogger.TabIndex = 0;
            this._richTextBoxTrainingLogger.Text = "";
            // 
            // LoggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl1);
            this.Name = "LoggerForm";
            this.Text = "Logging Application";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox _memoryReaderLogger;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox _posteGreLogger;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox _richTextBoxTrainingLogger;
    }
}