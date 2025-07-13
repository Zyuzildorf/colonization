using System.Collections.Generic;
using Source.Scripts.Bots;

namespace Source.Scripts.Spawners
{
    public class BotsSpawner : Spawner<BotCollector>
    {
        public List<BotCollector> SpawnBots(int amount)
        {
            List<BotCollector> bots = new List<BotCollector>();
        
            for (int i = 0; i < amount; i++)
            {
                bots.Add(ReleaseObject());
            }
        
            return bots;
        }
    }
}