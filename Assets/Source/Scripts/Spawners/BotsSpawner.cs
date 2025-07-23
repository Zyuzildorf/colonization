using System.Collections.Generic;
using Source.Scripts.Bots;
using UnityEngine;

namespace Source.Scripts.Spawners
{
    public class BotsSpawner : Spawner<BotCollector>
    {
        public List<BotCollector> SpawnBots(int amount, Transform basePosition)
        {
            List<BotCollector> bots = new List<BotCollector>();
        
            for (int i = 0; i < amount; i++)
            {
                bots.Add(ReleaseObject());
            }

            foreach (BotCollector bot in bots)
            {
                bot.SetBasePosition(basePosition);
            }
            
            return bots;
        }
    }
}