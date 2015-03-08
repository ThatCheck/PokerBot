using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Player
{
    public class Stats
    {
        private decimal _vpip;
        private decimal _pfr;
        private decimal _threeBetPF;
        private decimal _foldToThreeBet;
        private decimal _numberHand;
        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public decimal NumberHand
        {
            get { return _numberHand; }
            set { _numberHand = value; }
        }
        public decimal ThreeBetPF
        {
            get { return _threeBetPF; }
            set { _threeBetPF = value; }
        }

        public decimal FoldToThreeBet
        {
            get { return _foldToThreeBet; }
            set { _foldToThreeBet = value; }
        }

        public decimal Pfr
        {
            get { return _pfr; }
            set { _pfr = value; }
        }

        public decimal Vpip
        {
            get { return _vpip; }
            set { _vpip = value; }
        }

        public Stats(String name)
        {
            this._name = name;
            this.loadFromBD();
        }

        private void loadFromBD()
        {
            Dictionary<String,String> data = PokerBot.Postgres.PostgresConnector.getDefaultStatForPlayer(this._name);
            if (data != null)
            {
                this._numberHand = decimal.Parse(data["cnt_hands"].ToString());
                if (this._numberHand < 100)
                {
                    this.setDefaultValue();
                }
                else
                {
                    this._vpip = (decimal.Parse(data["cnt_vpip"].ToString()) / (this._numberHand - decimal.Parse(data["cnt_walks"].ToString()))) * 100;
                    this._pfr = (decimal.Parse(data["cnt_pfr"].ToString()) / (decimal.Parse(data["cnt_pfr_opp"].ToString()))) * 100;
                    this._threeBetPF = (decimal.Parse(data["cnt_p_3bet"].ToString()) / (decimal.Parse(data["cnt_p_3bet_opp"].ToString()))) * 100;
                    if (decimal.Parse(data["cnt_p_3bet_def_opp"].ToString()) != 0)
                    {
                        this._foldToThreeBet = (decimal.Parse(data["cnt_p_3bet_def_action_fold"].ToString()) / (decimal.Parse(data["cnt_p_3bet_def_opp"].ToString()))) * 100;
                    }
                    
                }
            }
            else
            {
                this.setDefaultValue();
            }
        }

        private void setDefaultValue()
        {
            this.Vpip = 20;
            this.Pfr = 17;
            this.ThreeBetPF = 7;
            this.FoldToThreeBet = 75;
        }

        public void refresh()
        {
            this.loadFromBD();
        }
    }
}
