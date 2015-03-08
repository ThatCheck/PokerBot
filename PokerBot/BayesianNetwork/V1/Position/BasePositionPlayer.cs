using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Position
{
    public abstract class BasePositionPlayer : BaseSmileDefinition
    {
        private PositionEnumType _positionPlayer;

        public PositionEnumType PositionPlayer
        {
            get { return _positionPlayer; }
            set { _positionPlayer = value; }
        }

        public BasePositionPlayer()
        {

        }

        public override string ToString()
        {
            return this.PositionPlayer.ToBayesianNetwork();
        }
    }
}
