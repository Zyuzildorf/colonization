using System.Collections;
using System.Collections.Generic;
using Source.Scripts.Bots;
using Source.Scripts.Other;
using Source.Scripts.Spawners;
using UnityEngine;

namespace Source.Scripts.Base
{
    [RequireComponent(typeof(BotsSpawner), typeof(ResourcesSearcher), typeof(ResourcesCounter))]
    public class Base : MonoBehaviour
    {
        [SerializeField] private int _startBotsAmount;
        [SerializeField] private float _searchingBotsDelay;
        [SerializeField] private float _searchingResourcesDelay;
        [SerializeField] private ResourcesVault _vault;
        [SerializeField] private int _botCreationCost;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _baseLayer;
        [SerializeField] private GameObject _flagPrefab;
        [SerializeField] private InputReader _inputReader;

        private List<BotCollector> _bots;
        private BotsSpawner _spawner;
        private ResourcesSearcher _searcher;
        private ResourcesCounter _resourcesCounter;
        private WaitForSeconds _waitForBotsSearch;
        private WaitForSeconds _waitForResourcesSearch;
        private GameObject _currentFlag;
        private bool _isBaseSelected;

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

        private void OnEnable()
        {
            _inputReader.LeftMouseButtonClicked += ProcessLeftButton;
            _inputReader.RightMouseButtonClicked += CancelSelection;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void ProcessLeftButton()
        {
            if (TryCastRaycast(_baseLayer, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    SelectBase();
                    Debug.Log("Base selected");
                    return;
                }

                CancelSelection();
            }
            else if (TryCastRaycast(_groundLayer, out hit))
            {
                if (_isBaseSelected)
                {
                    PlaceFlag(hit.point);
                }

                Debug.Log("Flag placed");
            }
        }

        private void PlaceFlag(Vector3 position)
        {
            if (_currentFlag != null)
            {
                _currentFlag.transform.position = position;
            }
            else
            {
                _currentFlag = Instantiate(_flagPrefab, position, Quaternion.identity);
            }
        }

        private void SelectBase()
        {
            _isBaseSelected = true;
        }

        private void CancelSelection()
        {
            _isBaseSelected = false;
            Debug.Log("Cancel base selection");
        }

        private bool TryCastRaycast(LayerMask mask, out RaycastHit hit)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            return Physics.Raycast(ray, out hit, Mathf.Infinity, mask);
        }

        public void SetResource(Resource resource)
        {
            _vault.RemoveBusyResource(resource);
            _resourcesCounter.SetResource(resource);

            if (_resourcesCounter.ResourcesCount >= _botCreationCost)
            {
                SpawnNewBots();
            }
        }

        private IEnumerator GiveTaskForFreeBots()
        {
            while (enabled)
            {
                foreach (BotCollector bot in _bots)
                {
                    if (bot.CurrentTask == Tasks.WaitForNewTask)
                    {
                        TryGiveCollectTask(bot);
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

        private void TryGiveCollectTask(BotCollector bot)
        {
            if (_vault.TryGetFreeResource(out Resource resource))
            {
                bot.SetTask(resource.transform.position, Tasks.CollectResources);
            }
        }

        private void SpawnNewBots()
        {
            int spawnBotsAmount = _resourcesCounter.ResourcesCount / _botCreationCost;

            _bots.AddRange(_spawner.SpawnBots
                (spawnBotsAmount));

            _resourcesCounter.UseResources(spawnBotsAmount * _botCreationCost);
        }
    }
}