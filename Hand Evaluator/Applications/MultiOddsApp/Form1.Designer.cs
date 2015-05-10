namespace MultiOddsApp
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
            this.userControl11 = new MultiOddsGrid.UserControl1();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Pocket = new System.Windows.Forms.TextBox();
            this.Board = new System.Windows.Forms.TextBox();
            this.Opponents = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // userControl11
            // 
            this.userControl11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.userControl11.Board = "";
            this.userControl11.Location = new System.Drawing.Point(-2, 102);
            this.userControl11.Name = "userControl11";
            this.userControl11.Opponents = 1;
            this.userControl11.Pocket = "";
            this.userControl11.Size = new System.Drawing.Size(433, 283);
            this.userControl11.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pocket:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Board:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Opponents:";
            // 
            // Pocket
            // 
            this.Pocket.Location = new System.Drawing.Point(87, 18);
            this.Pocket.Name = "Pocket";
            this.Pocket.Size = new System.Drawing.Size(100, 20);
            this.Pocket.TabIndex = 4;
            this.Pocket.TextChanged += new System.EventHandler(this.Pocket_TextChanged);
            // 
            // Board
            // 
            this.Board.Location = new System.Drawing.Point(87, 48);
            this.Board.Name = "Board";
            this.Board.Size = new System.Drawing.Size(100, 20);
            this.Board.TabIndex = 5;
            this.Board.TextChanged += new System.EventHandler(this.Board_TextChanged);
            // 
            // Opponents
            // 
            this.Opponents.FormattingEnabled = true;
            this.Opponents.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.Opponents.Location = new System.Drawing.Point(87, 76);
            this.Opponents.Name = "Opponents";
            this.Opponents.Size = new System.Drawing.Size(100, 21);
            this.Opponents.TabIndex = 6;
            this.Opponents.Text = "1";
            this.Opponents.SelectedIndexChanged += new System.EventHandler(this.Opponents_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 384);
            this.Controls.Add(this.Opponents);
            this.Controls.Add(this.Board);
            this.Controls.Add(this.Pocket);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userControl11);
            this.Name = "Form1";
            this.Text = "MultiOdds";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MultiOddsGrid.UserControl1 userControl11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Pocket;
        private System.Windows.Forms.TextBox Board;
        private System.Windows.Forms.ComboBox Opponents;
    }
}

