using PokerBot.Entity.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Action
{
    public abstract class BaseAction : BaseSmileDefinition
    {
        public new static string getCaseName()
        {
            throw new NotImplementedException();
        }

        public new  static string[] getValueName()
        {
            throw new NotImplementedException();
        }

        public new static string[] getArcForValue()
        {
            throw new NotImplementedException();
        }

        private List<HandHistories.Objects.Actions.HandAction> _listPlayer;
        private String _player;
        protected HandHistories.Objects.Cards.Street _currentStreet;

        public String Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public List<HandHistories.Objects.Actions.HandAction> ListPlayer
        {
            get { return _listPlayer; }
            set { _listPlayer = value; }
        }

        public BaseAction(List<HandHistories.Objects.Actions.HandAction> listPlayer,String player)
        {
            this._listPlayer = listPlayer;
            this._player = player;
        }

        public override String ToString()
        {
            var dictionnaryHand = this.getActionEnumDictionnary();
            ActionEnumType actionFromPlayer = dictionnaryHand[this.Player];
            return actionFromPlayer.ToBayesianNetwork();
        }

        public abstract Dictionary<String, ActionEnumType> getActionEnumDictionnary();
    }
}
