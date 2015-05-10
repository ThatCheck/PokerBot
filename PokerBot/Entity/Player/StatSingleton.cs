using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
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

        public void initFromFile(String filepath)
        {
            using (StreamReader streamReader = new StreamReader(filepath))
            {
                String[] line = streamReader.ReadToEnd().Split(new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                Parallel.ForEach(line, player =>
                {
                    Stats currentStat = Stats.loadFromString(player);
                    this._statsCacherList.GetOrAdd(currentStat.Name, currentStat);
                });
            }
        }

        public Stats getStatForPlayerByName(String name, Boolean refresh = false)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Unable to set null for player name");
            }
            string lowerName = name.ToLower();
            if (!this._statsCacherList.ContainsKey(lowerName))
            {
                Stats currentStat = new Stats(lowerName);
                this._statsCacherList.GetOrAdd(lowerName, currentStat);
            }
            if(refresh)
            {
                this._statsCacherList[lowerName].refresh();
            }
            return this._statsCacherList[lowerName];
        }
    }
}
