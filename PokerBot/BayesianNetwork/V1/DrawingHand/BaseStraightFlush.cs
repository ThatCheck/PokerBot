using HandHistories.Objects.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.DrawingHand
{
    public abstract class BaseStraightFlush : BaseSmileDefinition
    {
        private StateFlushStraightEnumType _enum;

        public StateFlushStraightEnumType Value
        {
            get { return _enum; }
            set { _enum = value; }
        }
        protected Entity.Table.Board _board;

        public BaseStraightFlush(Entity.Table.Board board)
        {
            this._board = board;
        }

        public override String ToString()
        {
            return this._enum.ToBayesianNetwork();
        }
    }
}
