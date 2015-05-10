namespace Benchmark
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threadPriorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.belowNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboveNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evaluateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evaluateTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inlineIterationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cIterationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evaluateIterateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threadPoolEvaluateIterateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(702, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.threadPriorityToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // threadPriorityToolStripMenuItem
            // 
            this.threadPriorityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lowestToolStripMenuItem,
            this.belowNormalToolStripMenuItem,
            this.normalToolStripMenuItem,
            this.aboveNormalToolStripMenuItem,
            this.highestToolStripMenuItem});
            this.threadPriorityToolStripMenuItem.Name = "threadPriorityToolStripMenuItem";
            this.threadPriorityToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.threadPriorityToolStripMenuItem.Text = "Thread Priority";
            // 
            // lowestToolStripMenuItem
            // 
            this.lowestToolStripMenuItem.Name = "lowestToolStripMenuItem";
            this.lowestToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lowestToolStripMenuItem.Text = "Lowest";
            this.lowestToolStripMenuItem.Click += new System.EventHandler(this.lowestToolStripMenuItem_Click);
            // 
            // belowNormalToolStripMenuItem
            // 
            this.belowNormalToolStripMenuItem.Name = "belowNormalToolStripMenuItem";
            this.belowNormalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.belowNormalToolStripMenuItem.Text = "Below Normal";
            this.belowNormalToolStripMenuItem.Click += new System.EventHandler(this.belowNormalToolStripMenuItem_Click);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // aboveNormalToolStripMenuItem
            // 
            this.aboveNormalToolStripMenuItem.Name = "aboveNormalToolStripMenuItem";
            this.aboveNormalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboveNormalToolStripMenuItem.Text = "Above Normal";
            this.aboveNormalToolStripMenuItem.Click += new System.EventHandler(this.aboveNormalToolStripMenuItem_Click);
            // 
            // highestToolStripMenuItem
            // 
            this.highestToolStripMenuItem.Name = "highestToolStripMenuItem";
            this.highestToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.highestToolStripMenuItem.Text = "Highest";
            this.highestToolStripMenuItem.Click += new System.EventHandler(this.highestToolStripMenuItem_Click);
            // 
            // zg1
            // 
            this.zg1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zg1.Location = new System.Drawing.Point(0, 27);
            this.zg1.Name = "zg1";
            this.zg1.ScrollMaxX = 0;
            this.zg1.ScrollMaxY = 0;
            this.zg1.ScrollMaxY2 = 0;
            this.zg1.ScrollMinX = 0;
            this.zg1.ScrollMinY = 0;
            this.zg1.ScrollMinY2 = 0;
            this.zg1.Size = new System.Drawing.Size(701, 462);
            this.zg1.TabIndex = 1;
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.evaluateToolStripMenuItem,
            this.evaluateTypeToolStripMenuItem,
            this.inlineIterationToolStripMenuItem,
            this.cIterationToolStripMenuItem,
            this.evaluateIterateToolStripMenuItem,
            this.threadPoolEvaluateIterateToolStripMenuItem,
            this.toolStripMenuItem1,
            this.selectAllToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // evaluateToolStripMenuItem
            // 
            this.evaluateToolStripMenuItem.Name = "evaluateToolStripMenuItem";
            this.evaluateToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.evaluateToolStripMenuItem.Text = "Evaluate";
            this.evaluateToolStripMenuItem.Click += new System.EventHandler(this.evaluateToolStripMenuItem_Click);
            // 
            // evaluateTypeToolStripMenuItem
            // 
            this.evaluateTypeToolStripMenuItem.Name = "evaluateTypeToolStripMenuItem";
            this.evaluateTypeToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.evaluateTypeToolStripMenuItem.Text = "EvaluateType";
            this.evaluateTypeToolStripMenuItem.Click += new System.EventHandler(this.evaluateTypeToolStripMenuItem_Click);
            // 
            // inlineIterationToolStripMenuItem
            // 
            this.inlineIterationToolStripMenuItem.Name = "inlineIterationToolStripMenuItem";
            this.inlineIterationToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.inlineIterationToolStripMenuItem.Text = "Inline Iteration";
            this.inlineIterationToolStripMenuItem.Click += new System.EventHandler(this.inlineIterationToolStripMenuItem_Click);
            // 
            // cIterationToolStripMenuItem
            // 
            this.cIterationToolStripMenuItem.Name = "cIterationToolStripMenuItem";
            this.cIterationToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.cIterationToolStripMenuItem.Text = "C# Iteration";
            this.cIterationToolStripMenuItem.Click += new System.EventHandler(this.cIterationToolStripMenuItem_Click);
            // 
            // evaluateIterateToolStripMenuItem
            // 
            this.evaluateIterateToolStripMenuItem.Name = "evaluateIterateToolStripMenuItem";
            this.evaluateIterateToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.evaluateIterateToolStripMenuItem.Text = "Evaluate/Iterate";
            this.evaluateIterateToolStripMenuItem.Click += new System.EventHandler(this.evaluateIterateToolStripMenuItem_Click);
            // 
            // threadPoolEvaluateIterateToolStripMenuItem
            // 
            this.threadPoolEvaluateIterateToolStripMenuItem.Name = "threadPoolEvaluateIterateToolStripMenuItem";
            this.threadPoolEvaluateIterateToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.threadPoolEvaluateIterateToolStripMenuItem.Text = "Thread Pool Evaluate/Iterate";
            this.threadPoolEvaluateIterateToolStripMenuItem.Click += new System.EventHandler(this.threadPoolEvaluateIterateToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(222, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 490);
            this.Controls.Add(this.zg1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Benchmark";
            this.Load += new System.EventHandler(this.OnLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private ZedGraph.ZedGraphControl zg1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threadPriorityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem belowNormalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboveNormalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem evaluateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem evaluateTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inlineIterationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cIterationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem evaluateIterateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threadPoolEvaluateIterateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    }
}

