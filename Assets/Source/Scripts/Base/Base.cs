using Source.Scripts.Bots;
using Source.Scripts.Other;
using Source.Scripts.Spawners;
using UnityEngine;

namespace Source.Scripts.Base
{
    [RequireComponent(typeof(BotsSpawner), typeof(ResourcesSearcher), typeof(ResourcesCounter))]
    [RequireComponent(typeof(CreateNewBaseState), typeof(BaseBotsHandler), typeof(BaseResourcesHandler))]
    [RequireComponent(typeof(BaseStateMachine), typeof(CreateNewBotsState))]
    public class Base : MonoBehaviour
    {
        [SerializeField] private int _startBotsAmount = 3;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private ResourcesVault _vault;

        private BaseStateMachine _stateMachine;
        private CreateNewBotsState _botsState;
        private CreateNewBaseState _newBaseState;
        private BotsSpawner _botsSpawner;
        private ResourcesSearcher _searcher;
        private BaseBotsHandler _botsHandler;
        private BaseResourcesHandler _resourcesHandler;
        private ResourcesCounter _resourcesCounter;

        private void Awake()
        {
            _stateMachine = GetComponent<BaseStateMachine>();
            _botsState = GetComponent<CreateNewBotsState>();
            _newBaseState = GetComponent<CreateNewBaseState>();
            _botsSpawner = GetComponent<BotsSpawner>();
            _searcher = GetComponent<ResourcesSearcher>();
            _resourcesCounter = GetComponent<ResourcesCounter>();
            _botsHandler = GetComponent<BaseBotsHandler>();
            _resourcesHandler = GetComponent<BaseResourcesHandler>();
        }

        private void Start()
        {
            _botsHandler.Initialize(_botsSpawner, _resourcesCounter, _vault, _startBotsAmount);
            _resourcesHandler.Initialize(_searcher, _vault, _resourcesCounter);
            _botsState.Initialize(_botsHandler, _resourcesCounter);
            _newBaseState.Initialize(_botsHandler, _resourcesCounter);
            _stateMachine.SetState(_botsState);
        }

        public void SetResource(Resource resource)
        {
            _resourcesHandler.SetResource(resource);

            _stateMachine.CurrentState.ProcessResource();
        }

        public void GetFlagObject(Transform flagPosition)
        {
            _newBaseState.SetFlagPosition(flagPosition);
            _stateMachine.SetState(_newBaseState);
        }
    }
}