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
        
        private BotsBase _base;
        private List<BotCollector> _bots;
        private BotsSpawner _spawner;
        private ResourcesCounter _resourcesCounter;
        private WaitForSeconds _waitForBotsSearch;
        private ResourcesVault _vault;
        private Tasks _currentTask;
        private Transform _flagPosition;
        private int _baseCreationCost;
        
        public int BotCreationCost => _botCreationCost;
        public Tasks CurrentTask => _currentTask;
        public void SetTask(Tasks task) => _currentTask = task;
        public void SetFlagPosition(Transform flag) => _flagPosition = flag;
        public void SetNewBaseCreationCost(int cost) => _baseCreationCost = cost;
        
        public void Initialize(BotsSpawner spawner, ResourcesCounter counter, ResourcesVault vault, int startBotsAmount,
            BotsBase currentBase)
        {
            _base = currentBase;
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
                        GiveTask(bot);
                    }
                }
                yield return _waitForBotsSearch;
            }
        }

        private void GiveTask(BotCollector bot)
        {
            if (_currentTask == Tasks.CollectResources)
            {
                GiveCollectTask(bot);
            }
            else if (_currentTask == Tasks.BuildBase)
            {
                bot.SetTask(_flagPosition, Tasks.BuildBase);
                bot.SetBase(_base);
                _resourcesCounter.UseResources(_baseCreationCost);
                _currentTask = Tasks.CollectResources;
            }
        }
        
        private void GiveCollectTask(BotCollector bot)
        {
            if (_vault.TryGetFreeResource(out Resource resource))
            {
                bot.SetTask(resource.transform, _currentTask);
            }
        }
    }
}