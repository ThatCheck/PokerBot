using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1
{
    public abstract class BornedValue<T> : BaseSmileDefinition
    {
        private decimal _value;
        private Boolean _canGoInfinite;

        public Boolean CanGoInfinite
        {
            get { return _canGoInfinite; }
        }
        public decimal Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public BornedValue(decimal value, bool canGoInfinite = false)
        {
            this._value = value;
            this._canGoInfinite = canGoInfinite;
        }

        public static int[] getBoundary()
        {
            throw new NotImplementedException();
        }

        public static String getValueFromInt(decimal valueToString)
        {
            int[] boundary = typeof(T).InvokeMember("getBoundary", BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod, null, null, null) as int[];
            for (int i = 0; i < boundary.Count() - 1; i++)
            {
                if (boundary[i] >= valueToString && (valueToString < boundary[i + 1] || (i == boundary.Count() - 2 && valueToString <= boundary[i + 1]) ))
                {
                    return "a" + boundary[i].ToString() + "_" + boundary[i + 1].ToString();
                }
            }
            //TO-DO : Found another hack
            if (boundary[boundary.Count() - 1] == 100)
            {
                return "a" + boundary[boundary.Count() - 2].ToString() + "_" + boundary[boundary.Count() - 1].ToString(); 
            }
            return "a_inf";
        }

        public override String ToString()
        {
            return getValueFromInt(this.Value);
        }

        public new static string getCaseName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        }

        public new static string[] getValueName()
        {
            List<String> data = new List<string>();
            foreach (int value in typeof(T).InvokeMember("getBoundary",BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod,null, null, null) as int[])
            {
                data.Add(getValueFromInt(value));
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            throw new NotImplementedException();
        }
    }
}
