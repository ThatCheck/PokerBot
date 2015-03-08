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
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trainerToolStripMenuItem});
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
            this.trainToolStripMenuItem});
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

    }
}

