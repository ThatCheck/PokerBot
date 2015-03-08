using PokerBot.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.CaseBased.Base
{
    public class CaseBase
    {
        private ActionEnum _action;
        private Double _quality;
        private int _numberOfPlayer;
        private double _relativePosition;
        private int _playerInCurrentHand;
        private int _playerYetToAct;
        private double _betCommitted;
        private double _betToCall;
        private double _potOdds;
        private double _handStrengh;

        public double HandStrengh
        {
            get { return _handStrengh; }
            set { _handStrengh = value; }
        }

        public double PotOdds
        {
            get { return _potOdds; }
            set { _potOdds = value; }
        }
        public double BetToCall
        {
            get { return _betToCall; }
            set { _betToCall = value; }
        }

        public double BetCommitted
        {
            get { return _betCommitted; }
            set { _betCommitted = value; }
        }

        public int PlayerYetToAct
        {
            get { return _playerYetToAct; }
            set { _playerYetToAct = value; }
        }

        public int PlayerInCurrentHand
        {
            get { return _playerInCurrentHand; }
            set { _playerInCurrentHand = value; }
        }

        public double RelativePosition
        {
            get { return _relativePosition; }
            set { _relativePosition = value; }
        }

        public int NumberOfPlayer
        {
            get { return _numberOfPlayer; }
            set { _numberOfPlayer = value; }
        }

        public Double Quality
        {
            get { return _quality; }
        }

        public ActionEnum Action
        {
            get { return _action; }
        }

        public CaseBase(ActionEnum action, Double quality)
        {
            this._action = action;
            this._quality = quality;
        }
    }
}
