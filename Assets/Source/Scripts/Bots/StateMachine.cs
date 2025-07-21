using Source.Scripts.Interfaces;
using UnityEngine;
using System;

namespace Source.Scripts.Bots
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private IdleState _idleState;

        private CollectorState _currentState;
        
        public Vector3 BasePosition { get; private set; }
        public Vector3 TargetPosition { get; private set; }
        public Transform NewBasePosition { get; private set; }
        public bool IsTaskToCollectReceived { get; private set; }
        public bool IsTaskToBuildNewBaseReceived { get; private set; }
        
        public event Action TaskCompleted;

        private void Awake()
        {
            BasePosition = transform.position;

            SetState(_idleState);
        }

        public void UpdateCurrentState()
        {
            if (_currentState is IUpdatable updatable)
            {
                updatable.UpdateState();
            }
        }

        public void SetState(CollectorState state)
        {
            if (_currentState == state)
            {
                return;
            }

            _currentState = state;

            if (_currentState is IEnterable enterable)
            {
                enterable.Enter();
            }
        }

        public void ProcessCollision(Collider other)
        {
            if (_currentState is ITriggerable triggerable)
            {
                triggerable.ProcessTriggerCollider(other);
            }
        }

        public void CompleteCollectTask()
        {
            IsTaskToCollectReceived = false;
            TargetPosition = transform.position;
            
            TaskCompleted?.Invoke();
        }

        public void CompleteBuildTask()
        {
            IsTaskToBuildNewBaseReceived = false;
        }

        public void SetCollectTask(Vector3 target)
        {
            IsTaskToCollectReceived = true;
            TargetPosition = target;
        }

        public void SetBuildTask(Transform target)
        {
            IsTaskToBuildNewBaseReceived = true;
            NewBasePosition = target;
        }
    }
}