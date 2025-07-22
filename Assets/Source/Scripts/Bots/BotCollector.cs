using System;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Bots
{
    [RequireComponent(typeof(CollisionHandler), typeof(StateMachine))]
    public class BotCollector : MonoBehaviour
    {
        [SerializeField] private IdleState _idleState;
        
        private StateMachine _stateMachine;
        private CollisionHandler _collisionHandler;

        public Vector3 BasePosition { get; private set; }
        public Vector3 TargetPosition { get; private set; }
        public Tasks CurrentTask { get; private set; }

        private void Awake()
        {
            CompleteTask();
            BasePosition = transform.position;
            _stateMachine = GetComponent<StateMachine>();
            _collisionHandler = GetComponent<CollisionHandler>();
        }

        private void Start()
        {
            _stateMachine.SetState(_idleState);
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

        public void SetTask(Vector3 target, Tasks task)
        {
            CurrentTask = task;
            TargetPosition = target;
        }

        public void CompleteTask()
        {
            CurrentTask = Tasks.WaitForNewTask;
            TargetPosition = transform.position;
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