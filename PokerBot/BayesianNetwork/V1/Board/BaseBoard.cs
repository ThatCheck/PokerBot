using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Board
{
    public class BaseBoard : BaseSmileDefinition
    {
        protected BoardEnumType _boardEnumType;
        protected Entity.Table.Board _board;

        public Entity.Table.Board Board
        {
            get { return _board; }
        }

        public BaseBoard(Entity.Table.Board board)
        {
            this._board = board;
        }

        public BoardEnumType BoardEnumType
        {
            get { return _boardEnumType; }
            set { _boardEnumType = value; }
        }

        public override String ToString()
        {
            return this._boardEnumType.ToBayesianNetwork();
        }
    }
}
