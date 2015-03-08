using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.NumberPlayerSitIn
{
    public abstract class BaseNumberPlayerSitIn : BaseSmileDefinition
    {
        private int _numberPlayerSitIn;
        private NumberPlayerSitInEnumType _enum;
        public int NumberPlayerSitIn
        {
            get { return _numberPlayerSitIn; }
            set { _numberPlayerSitIn = value; }
        }

        public BaseNumberPlayerSitIn(int number)
        {
            this._numberPlayerSitIn = number;
            switch (this.NumberPlayerSitIn)
            {
                case 2:
                    this._enum = NumberPlayerSitInEnumType.Two;
                    break;
                case 3:
                    this._enum = NumberPlayerSitInEnumType.Three;
                    break;
                case 4:
                    this._enum = NumberPlayerSitInEnumType.Four;
                    break;
                case 5:
                    this._enum = NumberPlayerSitInEnumType.Five;
                    break;
                case 6:
                    this._enum = NumberPlayerSitInEnumType.Six;
                    break;
                default:
                    throw new ArgumentException("Unable to map number player with Enum");
            }
        }

        public override String ToString()
        {
            return this._enum.ToBayesianNetwork();
        }
    }
}
