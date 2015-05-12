namespace PokerBot
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.trainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.get100WinningPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.créerLeNetworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateDataTrainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateDataPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testNetworkAccuracyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractToPreFlopcaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCBRPreFlopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateDecisionCBRPreFlopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postFlopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generatePstFlopDecisionCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trainerToolStripMenuItem,
            this.testToolStripMenuItem,
            this.preToolStripMenuItem,
            this.postFlopToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // trainerToolStripMenuItem
            // 
            this.trainerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.get100WinningPersonToolStripMenuItem,
            this.créerLeNetworkToolStripMenuItem,
            this.generateDataTrainingToolStripMenuItem,
            this.trainToolStripMenuItem,
            this.generateDataPlayerToolStripMenuItem});
            this.trainerToolStripMenuItem.Name = "trainerToolStripMenuItem";
            this.trainerToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.trainerToolStripMenuItem.Text = "Trainer";
            // 
            // get100WinningPersonToolStripMenuItem
            // 
            this.get100WinningPersonToolStripMenuItem.Name = "get100WinningPersonToolStripMenuItem";
            this.get100WinningPersonToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.get100WinningPersonToolStripMenuItem.Text = "Get 100 winnings persons";
            this.get100WinningPersonToolStripMenuItem.Click += new System.EventHandler(this.get100WinningPersonToolStripMenuItem_Click);
            // 
            // créerLeNetworkToolStripMenuItem
            // 
            this.créerLeNetworkToolStripMenuItem.Name = "créerLeNetworkToolStripMenuItem";
            this.créerLeNetworkToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.créerLeNetworkToolStripMenuItem.Text = "Créer le network";
            this.créerLeNetworkToolStripMenuItem.Click += new System.EventHandler(this.créerLeNetworkToolStripMenuItem_Click);
            // 
            // generateDataTrainingToolStripMenuItem
            // 
            this.generateDataTrainingToolStripMenuItem.Name = "generateDataTrainingToolStripMenuItem";
            this.generateDataTrainingToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.generateDataTrainingToolStripMenuItem.Text = "Generate Data Training";
            this.generateDataTrainingToolStripMenuItem.Click += new System.EventHandler(this.generateDataTrainingToolStripMenuItem_Click);
            // 
            // trainToolStripMenuItem
            // 
            this.trainToolStripMenuItem.Name = "trainToolStripMenuItem";
            this.trainToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.trainToolStripMenuItem.Text = "Train";
            this.trainToolStripMenuItem.Click += new System.EventHandler(this.trainToolStripMenuItem_Click);
            // 
            // generateDataPlayerToolStripMenuItem
            // 
            this.generateDataPlayerToolStripMenuItem.Name = "generateDataPlayerToolStripMenuItem";
            this.generateDataPlayerToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.generateDataPlayerToolStripMenuItem.Text = "Generate data player";
            this.generateDataPlayerToolStripMenuItem.Click += new System.EventHandler(this.generateDataPlayerToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testNetworkAccuracyToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // testNetworkAccuracyToolStripMenuItem
            // 
            this.testNetworkAccuracyToolStripMenuItem.Name = "testNetworkAccuracyToolStripMenuItem";
            this.testNetworkAccuracyToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.testNetworkAccuracyToolStripMenuItem.Text = "Test Network Accuracy";
            this.testNetworkAccuracyToolStripMenuItem.Click += new System.EventHandler(this.testNetworkAccuracyToolStripMenuItem_Click);
            // 
            // preToolStripMenuItem
            // 
            this.preToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractToPreFlopcaseToolStripMenuItem,
            this.loadCBRPreFlopToolStripMenuItem,
            this.generateDecisionCBRPreFlopToolStripMenuItem});
            this.preToolStripMenuItem.Name = "preToolStripMenuItem";
            this.preToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.preToolStripMenuItem.Text = "PreFlop";
            // 
            // extractToPreFlopcaseToolStripMenuItem
            // 
            this.extractToPreFlopcaseToolStripMenuItem.Name = "extractToPreFlopcaseToolStripMenuItem";
            this.extractToPreFlopcaseToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.extractToPreFlopcaseToolStripMenuItem.Text = "Extract to PreFlopcase";
            this.extractToPreFlopcaseToolStripMenuItem.Click += new System.EventHandler(this.extractToPreFlopcaseToolStripMenuItem_Click);
            // 
            // loadCBRPreFlopToolStripMenuItem
            // 
            this.loadCBRPreFlopToolStripMenuItem.Name = "loadCBRPreFlopToolStripMenuItem";
            this.loadCBRPreFlopToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.loadCBRPreFlopToolStripMenuItem.Text = "Load CBRPreFlop";
            this.loadCBRPreFlopToolStripMenuItem.Click += new System.EventHandler(this.loadCBRPreFlopToolStripMenuItem_Click);
            // 
            // generateDecisionCBRPreFlopToolStripMenuItem
            // 
            this.generateDecisionCBRPreFlopToolStripMenuItem.Name = "generateDecisionCBRPreFlopToolStripMenuItem";
            this.generateDecisionCBRPreFlopToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.generateDecisionCBRPreFlopToolStripMenuItem.Text = "Generate decision CBRPreFlop";
            this.generateDecisionCBRPreFlopToolStripMenuItem.Click += new System.EventHandler(this.generateDecisionCBRPreFlopToolStripMenuItem_Click);
            // 
            // postFlopToolStripMenuItem
            // 
            this.postFlopToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generatePstFlopDecisionCaseToolStripMenuItem});
            this.postFlopToolStripMenuItem.Name = "postFlopToolStripMenuItem";
            this.postFlopToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.postFlopToolStripMenuItem.Text = "PostFlop";
            // 
            // generatePstFlopDecisionCaseToolStripMenuItem
            // 
            this.generatePstFlopDecisionCaseToolStripMenuItem.Name = "generatePstFlopDecisionCaseToolStripMenuItem";
            this.generatePstFlopDecisionCaseToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.generatePstFlopDecisionCaseToolStripMenuItem.Text = "Generate pst flop decision case";
            this.generatePstFlopDecisionCaseToolStripMenuItem.Click += new System.EventHandler(this.generatePstFlopDecisionCaseToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Bonjour ! ";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem trainerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem get100WinningPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem créerLeNetworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateDataTrainingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testNetworkAccuracyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateDataPlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractToPreFlopcaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCBRPreFlopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateDecisionCBRPreFlopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem postFlopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generatePstFlopDecisionCaseToolStripMenuItem;

    }
}

