using System.Collections;
using System.Collections.Generic;
using Source.Scripts.Bots;
using Source.Scripts.Other;
using Source.Scripts.Spawners;
using UnityEngine;

namespace Source.Scripts.Base
{
    [RequireComponent(typeof(BotsSpawner), typeof(ResourcesSearcher), 
        typeof(ResourcesCounter))]
    public class Base : MonoBehaviour
    {
        [SerializeField] private int _startBotsAmount;
        [SerializeField] private float _searchingBotsDelay;
        [SerializeField] private float _searchingResourcesDelay;
        [SerializeField] private ResourcesVault _vault;

        private List<BotCollector> _bots;
        private BotsSpawner _spawner;
        private ResourcesSearcher _searcher;
        private ResourcesCounter _resourcesCounter;
        private List<Resource> _resources;
        private WaitForSeconds _waitForBotsSearch;
        private WaitForSeconds _waitForResourcesSearch;

        private void Awake()
        {
            _spawner = GetComponent<BotsSpawner>();
            _searcher = GetComponent<ResourcesSearcher>();
            _resourcesCounter = GetComponent<ResourcesCounter>();
            
            _waitForBotsSearch = new WaitForSeconds(_searchingBotsDelay);
            _waitForResourcesSearch = new WaitForSeconds(_searchingResourcesDelay);
        }

        private void Start()
        {
            _bots = _spawner.SpawnBots(_startBotsAmount);

            StartCoroutine(GiveTaskForFreeBots());
            StartCoroutine(FindFreeAvailableResources());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void SetResource(Resource resource)
        {
            _vault.RemoveBusyResource(resource);
            _resourcesCounter.SetResource(resource);
        }

        private IEnumerator GiveTaskForFreeBots()
        {
            while (enabled)
            {
                foreach (BotCollector bot in _bots)
                {
                    if (bot.IsBotFree)
                    {
                        TryGiveTask(bot);
                    }
                }

                yield return _waitForBotsSearch;
            }
        }

        private IEnumerator FindFreeAvailableResources()
        {
            while (enabled)
            {
                if (_vault.FreeResourcesCount <= 0)
                {
                    if (_searcher.TryFindResources(out List<Resource> resources))
                    {
                        _vault.SetResources(resources);
                    }
                }

                yield return _waitForResourcesSearch;
            }
        }

        private void TryGiveTask(BotCollector bot)
        {
            if (_vault.TryGetFreeResource(out Resource resource))
            {
                bot.SetTask(resource.transform.position);
            }
        }
    }
}