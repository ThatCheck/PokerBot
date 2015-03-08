using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Player
{
    public sealed class StatSingleton
    {
        private static readonly Lazy<StatSingleton> lazy = new Lazy<StatSingleton>(() => new StatSingleton());
        private readonly object _lockerRequest = new object();
        public static StatSingleton Instance { get { return lazy.Value; } }
        private ConcurrentDictionary<String, Stats> _statsCacherList;
        private StatSingleton()
        {
            this._statsCacherList = new ConcurrentDictionary<string, Stats>();
        }

        public Stats getStatForPlayerByName(String name, Boolean refresh = false)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Unable to set null for player name");
            }

            if (!this._statsCacherList.ContainsKey(name))
            {
                Stats currentStat = new Stats(name);
                this._statsCacherList.GetOrAdd(name, currentStat);
            }
            if(refresh)
            {
                this._statsCacherList[name].refresh();
            }
            return this._statsCacherList[name];
        }
    }
}
