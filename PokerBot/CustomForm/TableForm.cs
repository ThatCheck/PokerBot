using PokerBot.Entity.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerBot.CustomForm
{
    public partial class TableForm : Form
    {
        private Table _table;
        public TableForm(Table table)
        {
            InitializeComponent();
            this._table = table;
        }

        private void TableForm_Shown(object sender, EventArgs e)
        {
            this._table.EndTableAnalyzer += _table_EndTableAnalyzer;
        }

        void _table_EndTableAnalyzer(object sender)
        {
            MethodInvoker action = () => this.Text = this._table.Name;
            this.BeginInvoke(action);
            MethodInvoker action2 = () => this.label1.Text = "Hand ID : " + this._table.HandId.ToString();
            this.BeginInvoke(action2);
            MethodInvoker action3 = () => this.label2.Text = "Total Player : " + this._table.NumberPlayer.ToString();
            this.BeginInvoke(action3);
            MethodInvoker action4 = () => this.label3.Text = "Player Sit-in : " + this._table.NumberPlayerPlaying.ToString();
            this.BeginInvoke(action4);
            MethodInvoker action5 = () => this.label4.Text = "Hashset ID : " + this._table.Hash.ToString();
            this.BeginInvoke(action5); 
        }
    }
}
