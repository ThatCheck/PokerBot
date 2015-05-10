using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerBot.CustomForm.Training
{
    public partial class ExtractDataForm : Form
    {
        Trainer.Trainer _trainer;
        public ExtractDataForm(Trainer.Trainer trainer)
        {
            InitializeComponent();
            this._trainer = trainer;
            this._trainer.ProgressEvent += _trainer_ProgressEvent;
        }

        void _trainer_ProgressEvent(object sender, Event.ProgressEventArgs data)
        {
            int value = Math.Min((int)Math.Round(((double)data.Progress / (double)data.Max) * 100), 100);
            MethodInvoker action = () => this.progressBar1.Value = value;
            this.BeginInvoke(action);
            MethodInvoker action2 = () => this.label1.Text = data.Progress + "/" + data.Max;
            this.BeginInvoke(action2);
        }
    }
}
