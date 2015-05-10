namespace MultiOppOdds
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
            this.graph = new ZedGraph.ZedGraphControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pocket = new System.Windows.Forms.TextBox();
            this.board = new System.Windows.Forms.TextBox();
            this.Calculate = new System.Windows.Forms.Button();
            this.Speed = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PotentialCheckBox = new System.Windows.Forms.CheckBox();
            this.HandStrengthCheck = new System.Windows.Forms.CheckBox();
            this.WinCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Speed)).BeginInit();
            this.SuspendLayout();
            // 
            // graph
            // 
            this.graph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.graph.IsAutoScrollRange = false;
            this.graph.IsEnableHPan = true;
            this.graph.IsEnableHZoom = true;
            this.graph.IsEnableVPan = true;
            this.graph.IsEnableVZoom = true;
            this.graph.IsPrintFillPage = true;
            this.graph.IsPrintKeepAspectRatio = true;
            this.graph.IsScrollY2 = false;
            this.graph.IsShowContextMenu = true;
            this.graph.IsShowCopyMessage = true;
            this.graph.IsShowCursorValues = false;
            this.graph.IsShowHScrollBar = false;
            this.graph.IsShowPointValues = false;
            this.graph.IsShowVScrollBar = false;
            this.graph.IsZoomOnMouseCenter = false;
            this.graph.Location = new System.Drawing.Point(0, 89);
            this.graph.Name = "graph";
            this.graph.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.graph.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.graph.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.graph.PointDateFormat = "g";
            this.graph.PointValueFormat = "G";
            this.graph.ScrollMaxX = 0;
            this.graph.ScrollMaxY = 0;
            this.graph.ScrollMaxY2 = 0;
            this.graph.ScrollMinX = 0;
            this.graph.ScrollMinY = 0;
            this.graph.ScrollMinY2 = 0;
            this.graph.Size = new System.Drawing.Size(660, 247);
            this.graph.TabIndex = 0;
            this.graph.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.graph.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.graph.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.graph.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.graph.ZoomStepFraction = 0.1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Player Pocket:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Board:";
            // 
            // pocket
            // 
            this.pocket.Location = new System.Drawing.Point(105, 12);
            this.pocket.Name = "pocket";
            this.pocket.Size = new System.Drawing.Size(114, 20);
            this.pocket.TabIndex = 3;
            this.pocket.Text = "As Ks";
            this.pocket.TextChanged += new System.EventHandler(this.pocket_TextChanged);
            // 
            // board
            // 
            this.board.Location = new System.Drawing.Point(105, 48);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(114, 20);
            this.board.TabIndex = 4;
            this.board.TextChanged += new System.EventHandler(this.board_TextChanged);
            // 
            // Calculate
            // 
            this.Calculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Calculate.Location = new System.Drawing.Point(510, 8);
            this.Calculate.Name = "Calculate";
            this.Calculate.Size = new System.Drawing.Size(88, 27);
            this.Calculate.TabIndex = 5;
            this.Calculate.Text = "Calculate";
            this.Calculate.UseVisualStyleBackColor = true;
            this.Calculate.Click += new System.EventHandler(this.Calculate_Click);
            // 
            // Speed
            // 
            this.Speed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Speed.Location = new System.Drawing.Point(493, 38);
            this.Speed.Name = "Speed";
            this.Speed.Size = new System.Drawing.Size(119, 45);
            this.Speed.TabIndex = 6;
            this.Speed.Value = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(460, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fast";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(610, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Accurate";
            // 
            // PotentialCheckBox
            // 
            this.PotentialCheckBox.AutoSize = true;
            this.PotentialCheckBox.Location = new System.Drawing.Point(251, 58);
            this.PotentialCheckBox.Name = "PotentialCheckBox";
            this.PotentialCheckBox.Size = new System.Drawing.Size(113, 17);
            this.PotentialCheckBox.TabIndex = 9;
            this.PotentialCheckBox.Text = "Pos/Neg Potential";
            this.PotentialCheckBox.UseVisualStyleBackColor = true;

            // 
            // HandStrengthCheck
            // 
            this.HandStrengthCheck.AutoSize = true;
            this.HandStrengthCheck.Location = new System.Drawing.Point(251, 35);
            this.HandStrengthCheck.Name = "HandStrengthCheck";
            this.HandStrengthCheck.Size = new System.Drawing.Size(95, 17);
            this.HandStrengthCheck.TabIndex = 10;
            this.HandStrengthCheck.Text = "Hand Strength";
            this.HandStrengthCheck.UseVisualStyleBackColor = true;
            // 
            // WinCheckBox
            // 
            this.WinCheckBox.AutoSize = true;
            this.WinCheckBox.Checked = true;
            this.WinCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WinCheckBox.Location = new System.Drawing.Point(251, 12);
            this.WinCheckBox.Name = "WinCheckBox";
            this.WinCheckBox.Size = new System.Drawing.Size(45, 17);
            this.WinCheckBox.TabIndex = 11;
            this.WinCheckBox.Text = "Win";
            this.WinCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 337);
            this.Controls.Add(this.WinCheckBox);
            this.Controls.Add(this.HandStrengthCheck);
            this.Controls.Add(this.PotentialCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Speed);
            this.Controls.Add(this.Calculate);
            this.Controls.Add(this.board);
            this.Controls.Add(this.pocket);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.graph);
            this.Name = "Form1";
            this.Text = "Multi-Opponent Odds";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Speed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl graph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pocket;
        private System.Windows.Forms.TextBox board;
        private System.Windows.Forms.Button Calculate;
        private System.Windows.Forms.TrackBar Speed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox PotentialCheckBox;
        private System.Windows.Forms.CheckBox HandStrengthCheck;
        private System.Windows.Forms.CheckBox WinCheckBox;
    }
}

