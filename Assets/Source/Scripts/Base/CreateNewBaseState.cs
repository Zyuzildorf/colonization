using System.Collections;
using Source.Scripts.Bots;
using UnityEngine;

namespace Source.Scripts.Base
{
    public class CreateNewBaseState : BaseState
    {
        [SerializeField] private int _costOfNewBase = 5;
        [SerializeField] private CreateNewBotsState _createNewBotsState;

        private BaseBotsHandler _botsHandler;
        private ResourcesCounter _counter;
        private Transform _currentFlag;

        public void Initialize(BaseBotsHandler botsHandler, ResourcesCounter counter)
        {
            _botsHandler = botsHandler;
            _counter = counter;
        }

        public void SetFlagPosition(Transform flagPosition)
        {
            _currentFlag = flagPosition;
        }

        public override void ProcessResource()
        {
            if (_counter.ResourcesCount >= _costOfNewBase)
            {
                _botsHandler.SetFlagPosition(_currentFlag);
                _botsHandler.SetNewBaseCreationCost(_costOfNewBase);
                _botsHandler.SetTask(Tasks.BuildBase);
                StartCoroutine(CheckForTaskChanges());
            }
        }

        private IEnumerator CheckForTaskChanges()
        {
            while (enabled)
            {
                if (_botsHandler.CurrentTask != Tasks.BuildBase)
                {
                    StateMachine.SetState(_createNewBotsState);
                    yield break;
                }
                
                yield return null;
            }
        }
    }
}