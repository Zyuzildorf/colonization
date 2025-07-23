using Source.Scripts.Other;
using Source.Scripts.Spawners;
using UnityEngine;

namespace Source.Scripts.Base
{
    [RequireComponent(typeof(BotsSpawner), typeof(ResourcesSearcher), typeof(ResourcesCounter))]
    [RequireComponent(typeof(BaseSelectionHandler), typeof(BaseBotsHandler), typeof(BaseResourcesHandler))]
    [RequireComponent(typeof(StateMachine))]
    public class Base : MonoBehaviour
    {
        [SerializeField] private int _startBotsAmount = 3;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private ResourcesVault _vault;

        private StateMachine _stateMachine;
        private BotsSpawner _botsSpawner;
        private ResourcesSearcher _searcher;
        private BaseSelectionHandler _selectionHandler;
        private BaseBotsHandler _botsHandler;
        private BaseResourcesHandler _resourcesHandler;
        private ResourcesCounter _resourcesCounter;

        private void Awake()
        {
            _stateMachine = GetComponent<StateMachine>();
            _botsSpawner = GetComponent<BotsSpawner>();
            _searcher = GetComponent<ResourcesSearcher>();
            _resourcesCounter = GetComponent<ResourcesCounter>();
            _selectionHandler = GetComponent<BaseSelectionHandler>();
            _botsHandler = GetComponent<BaseBotsHandler>();
            _resourcesHandler = GetComponent<BaseResourcesHandler>();
        }

        private void Start()
        {
            _botsHandler.Initialize(_botsSpawner, _resourcesCounter, _vault, _startBotsAmount);
            _resourcesHandler.Initialize(_searcher, _vault, _resourcesCounter);
        }

        public void SetResource(Resource resource)
        {
           _resourcesHandler.SetResource(resource);

            if (_resourcesCounter.ResourcesCount >= _botsHandler.BotCreationCost)
            {
                _botsHandler.SpawnNewBots();
            }
        }
    }
}