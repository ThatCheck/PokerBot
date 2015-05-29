using PokerBot.BayesianNetwork;
using PokerBot.CaseBased.Base;
using PokerBot.CustomForm;
using PokerBot.CustomForm.Training;
using PokerBot.Entity.Table;
using PokerBot.Entity.Window;
using PokerBot.Hand;
using PokerBot.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerBot
{
    public partial class Form1 : Form
    {
        WindowSupervisor _windowSupervisor;
        List<Tuple<Table,TableForm>> _tableList;
        private static System.Timers.Timer aTimer;
        private MemoryReader.MemoryReader _memoryReader;
        LoggerForm _loggerForm;
        Trainer.Trainer _trainer;
        QBRBase _qbr;
        public Form1()
        {
            InitializeComponent();
            PokerBot.Entity.Player.StatSingleton.Instance.initFromFile(@"./Trainer/PlayerData/extractValuePlayer.txt");
            TwoPlusTwoHandEvaluator.Instance.init();
            Debug.WriteLine(SmileSingleton.Instance.ToString());
            //SmileSingleton.Instance.init(8);
        }

        void TableList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Table table = ((KeyValuePair<IntPtr, Table>)e.NewItems[0]).Value;
                CustomForm.TableForm tableForm = new CustomForm.TableForm(table);
                this.Invoke((MethodInvoker)delegate
                {
                    tableForm.MdiParent = this;
                    tableForm.Show();
                });
                this._tableList.Add(Tuple.Create<Table,TableForm>(table,tableForm));
                if (this._memoryReader == null)
                {
                    this._memoryReader = new MemoryReader.MemoryReader(((KeyValuePair<IntPtr, Table>)e.NewItems[0]).Key);
                    this._memoryReader.DecodeErrorEndMemory += _memoryReader_DecodeErrorEndMemory;
                    Thread t = new Thread(() =>
                    {
                        while (true)
                        {
                            this._memoryReader.analyze();
                        }
                    });

                    t.Start();
                }
                else if(this._memoryReader.Hwnd != ((KeyValuePair<IntPtr, Table>)e.NewItems[0]).Key)
                {
                    this._memoryReader.Hwnd = ((KeyValuePair<IntPtr, Table>)e.NewItems[0]).Key;
                }
                this._memoryReader.DecodeEndMemory += this._tableList.Last().Item1.NewHandEventHandler;
            }
        }

        void _memoryReader_DecodeErrorEndMemory(object sender, Entity.Event.DecodedErrorHandEventArgs data)
        {
            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            this._loggerForm = new LoggerForm();
            this._loggerForm.MdiParent = this;
            this._loggerForm.Show();


            _trainer = new Trainer.Trainer();

            aTimer = new System.Timers.Timer(10000);

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += aTimer_Elapsed;

            // Set the Interval to 2 seconds (2000 milliseconds).
            aTimer.Interval = 2000;
            aTimer.Enabled = true;

            this._tableList = new List<Tuple<Table,TableForm>>();
            this._windowSupervisor = new WindowSupervisor();
            this._windowSupervisor.TableList.CollectionChanged += TableList_CollectionChanged;
        }

        void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this._windowSupervisor.discoverTable();
        }

        private void get100WinningPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._trainer.saveMostWinningPlayer(100);
        }
        private async void créerLeNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Task.Factory.StartNew( () => this._trainer.createBayesianNetwork("./network.xdsl"));
            MessageBox.Show("Fin de la création du Network ", "FIN ! ", MessageBoxButtons.OK);
        }

        private async void generateDataTrainingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractDataForm dataForm = new ExtractDataForm(this._trainer);
            dataForm.Show();
            await Task.Factory.StartNew(() => this._trainer.generateDataTraining(Directory.GetFiles(@"./Trainer/Data"), "./training.txt", HandHistories.Objects.GameDescription.SiteName.PartyPokerFr));
            dataForm.Hide();
            dataForm.Dispose();
            MessageBox.Show("Fin de l'extraction des données", "FIN ! ", MessageBoxButtons.OK);
        }

        private async void trainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() => this._trainer.train("./network.xdsl", "./training.txt"));
            MessageBox.Show("Fiin de l'entrainement s", "FIN ! ", MessageBoxButtons.OK);
        }

        private async void testNetworkAccuracyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractDataForm dataForm = new ExtractDataForm(this._trainer);
            dataForm.Show();
            await Task.Factory.StartNew(() => this._trainer.testTrain(@"./network.xdsl", Directory.GetFiles(@"./Trainer/DataTester")[0], HandHistories.Objects.GameDescription.SiteName.PartyPokerFr));
            dataForm.Hide();
            dataForm.Dispose();
            MessageBox.Show("Fiin du test !", "FIN ! ", MessageBoxButtons.OK);
        }

        private async void generateDataPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractDataForm dataForm = new ExtractDataForm(this._trainer);
            dataForm.Show();
            await Task.Factory.StartNew(() => this._trainer.extractDataPlayer(@"./Trainer/PlayerData/ListPlayer.txt", @"./Trainer/PlayerData/extractValuePlayer.txt"));
            dataForm.Hide();
            dataForm.Dispose();
            MessageBox.Show("Fin de l'extraction des données", "FIN ! ", MessageBoxButtons.OK);
        }

        private async void extractToPreFlopcaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractDataForm dataForm = new ExtractDataForm(this._trainer);
            dataForm.Show();
            await Task.Factory.StartNew(() => this._trainer.generateCBRPreFlopRange(Directory.GetFiles(@"./Trainer/Data"), "./preflopCase.dat", HandHistories.Objects.GameDescription.SiteName.PartyPokerFr));
            dataForm.Hide();
            dataForm.Dispose();
            MessageBox.Show("Fin de l'extraction des données.", "FIN ! ", MessageBoxButtons.OK);
        }

        private void loadCBRPreFlopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._qbr.unserialize("./preflopCase.dat");
            MessageBox.Show("Fin du chargement des données.", "FIN ! ", MessageBoxButtons.OK);
        }

        private async void generateDecisionCBRPreFlopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractDataForm dataForm = new ExtractDataForm(this._trainer);
            dataForm.Show();
            await Task.Factory.StartNew(() => this._trainer.generateCBRPreFlopDecision(Directory.GetFiles(@"./Trainer/Data"), "./preflopCaseDecision.dat", HandHistories.Objects.GameDescription.SiteName.PartyPokerFr));
            dataForm.Hide();
            dataForm.Dispose();
            MessageBox.Show("Fin de l'extraction des données.", "FIN ! ", MessageBoxButtons.OK);
        }

        private async void generatePstFlopDecisionCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractDataForm dataForm = new ExtractDataForm(this._trainer);
            dataForm.Show();
            await Task.Factory.StartNew(() => this._trainer.generateCBRPostFlopDecision(Directory.GetFiles(@"./Trainer/Data"), "./postFlopCaseDecision.dat", "./network.xdsl", HandHistories.Objects.GameDescription.SiteName.PartyPokerFr));
            dataForm.Hide();
            dataForm.Dispose();
            MessageBox.Show("Fin de l'extraction des données.", "FIN ! ", MessageBoxButtons.OK);
        }
    }
}
