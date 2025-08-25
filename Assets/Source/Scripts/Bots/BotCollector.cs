using System;
using Source.Scripts.Base;
using Source.Scripts.Interfaces;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Bots
{
    [RequireComponent(typeof(CollisionHandler), typeof(BotsStateMachine))]
    public class BotCollector : MonoBehaviour
    {
        [SerializeField] private BotIdleState _botIdleState;
        [SerializeField] private BotBuildBaseState _botBuildBaseState;
        
        private BotsStateMachine _stateMachine;
        private CollisionHandler _collisionHandler;

        public BotsBase BotsBase { get; set; }
        public Transform BasePosition { get; private set; }
        public Transform TargetPosition { get; private set; }
        public Tasks CurrentTask { get; private set; }

        private void Awake()
        {
            CompleteTask();
            _stateMachine = GetComponent<BotsStateMachine>();
            _collisionHandler = GetComponent<CollisionHandler>();
        }

        private void Start()
        {
            _stateMachine.SetState(_botIdleState);
        }

        private void Update()
        {
            _stateMachine.UpdateCurrentState();
        }

        private void OnEnable()
        {
            _collisionHandler.TriggerEntered += ProcessCollision;
        }

        private void OnDisable()
        {
            _collisionHandler.TriggerEntered -= ProcessCollision;
        }

        public void SetBase(BotsBase botsBase)
        {
            BotsBase = botsBase;
        }
        
        public void SetTask(Transform target, Tasks task)
        {
            CurrentTask = task;
            TargetPosition = target;

            if (CurrentTask == Tasks.BuildBase)
            {
                _stateMachine.SetState(_botBuildBaseState);
            }
        }

        public void CompleteTask()
        {
            CurrentTask = Tasks.WaitForNewTask;
            TargetPosition = transform;
        }

        public void SetBasePosition(Transform basePosition)
        {
            BasePosition = basePosition;
        }
        
        private void ProcessCollision(Collider other)
        {
            if (_stateMachine.CurrentState is ITriggerable triggerable)
            {
                triggerable.ProcessTriggerCollider(other);
            }
        }
    }
}