using System.Collections;
using System.Collections.Generic;
using Source.Scripts.Bots;
using Source.Scripts.Other;
using Source.Scripts.Spawners;
using UnityEngine;

namespace Source.Scripts.Base
{
    public class BaseBotsHandler : MonoBehaviour
    {
        [SerializeField] private float _searchingBotsDelay = 1f;
        [SerializeField] private int _botCreationCost = 100;
        
        private List<BotCollector> _bots;
        private BotsSpawner _spawner;
        private ResourcesCounter _resourcesCounter;
        private WaitForSeconds _waitForBotsSearch;
        private ResourcesVault _vault;
        
        public int BotCreationCost => _botCreationCost;

        public void Initialize(BotsSpawner spawner, ResourcesCounter counter, ResourcesVault vault, int startBotsAmount)
        {
            _spawner = spawner;
            _vault = vault;
            _resourcesCounter = counter;
            _waitForBotsSearch = new WaitForSeconds(_searchingBotsDelay);
            
            _bots = _spawner.SpawnBots(startBotsAmount, transform);
            StartCoroutine(GiveTaskForFreeBots());
        }

        public void SpawnNewBots()
        {
            int spawnBotsAmount = _resourcesCounter.ResourcesCount / _botCreationCost;

            _bots.AddRange(_spawner.SpawnBots
                (spawnBotsAmount,transform));

            _resourcesCounter.UseResources(spawnBotsAmount * _botCreationCost);
        }

        private IEnumerator GiveTaskForFreeBots()
        {
            while (enabled)
            {
                foreach (BotCollector bot in _bots)
                {
                    if (bot.CurrentTask == Tasks.WaitForNewTask)
                    {
                        TryGiveTask(bot);
                    }
                }
                yield return _waitForBotsSearch;
            }
        }

        private void TryGiveTask(BotCollector bot)
        {
            if (_vault.TryGetFreeResource(out Resource resource))
            {
                bot.SetTask(resource.transform, Tasks.CollectResources);
            }
        }
    }
}