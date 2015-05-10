namespace OddsGridApp
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
            this.PocketCards = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Board = new System.Windows.Forms.TextBox();
            this.oddsGrid1 = new OddsGrid.OddsGrid();
            this.OpponentCards = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Calculate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Player:";
            // 
            // PocketCards
            // 
            this.PocketCards.Location = new System.Drawing.Point(76, 9);
            this.PocketCards.Name = "PocketCards";
            this.PocketCards.Size = new System.Drawing.Size(200, 20);
            this.PocketCards.TabIndex = 2;
            this.PocketCards.Text = "AKo";
            this.PocketCards.TextChanged += new System.EventHandler(this.PocketCards_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Board:";
            // 
            // Board
            // 
            this.Board.Location = new System.Drawing.Point(76, 57);
            this.Board.Name = "Board";
            this.Board.Size = new System.Drawing.Size(100, 20);
            this.Board.TabIndex = 4;
            this.Board.Text = "Ts Qs 2d";
            this.Board.TextChanged += new System.EventHandler(this.Board_TextChanged);
            // 
            // oddsGrid1
            // 
            this.oddsGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oddsGrid1.Board = "";
            this.oddsGrid1.Location = new System.Drawing.Point(0, 83);
            this.oddsGrid1.Name = "oddsGrid1";
            this.oddsGrid1.Opponent = "?";
            this.oddsGrid1.Pocket = "";
            this.oddsGrid1.Size = new System.Drawing.Size(405, 307);
            this.oddsGrid1.TabIndex = 0;
            // 
            // OpponentCards
            // 
            this.OpponentCards.Location = new System.Drawing.Point(76, 31);
            this.OpponentCards.Name = "OpponentCards";
            this.OpponentCards.Size = new System.Drawing.Size(200, 20);
            this.OpponentCards.TabIndex = 6;
            this.OpponentCards.Text = "99";
            this.OpponentCards.TextChanged += new System.EventHandler(this.OpponentCards_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Opponent:";
            // 
            // Calculate
            // 
            this.Calculate.Location = new System.Drawing.Point(299, 12);
            this.Calculate.Name = "Calculate";
            this.Calculate.Size = new System.Drawing.Size(81, 35);
            this.Calculate.TabIndex = 7;
            this.Calculate.Text = "Calculate";
            this.Calculate.UseVisualStyleBackColor = true;
            this.Calculate.Click += new System.EventHandler(this.Calculate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 395);
            this.Controls.Add(this.Calculate);
            this.Controls.Add(this.OpponentCards);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Board);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PocketCards);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.oddsGrid1);
            this.Name = "Form1";
            this.Text = "Hand Odds App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OddsGrid.OddsGrid oddsGrid1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PocketCards;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Board;
        private System.Windows.Forms.TextBox OpponentCards;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Calculate;

    }
}

