using Source.Scripts.Interfaces;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Bots
{
    public class DeliveringResourceState : MovementState, IUpdatable, ITriggerable
    {
        [SerializeField] private IdleState _idleState;
    
        private Vector3 _targetPosition;

        public void ProcessTriggerCollider(Collider other)
        {
            if (other.TryGetComponent(out Base.Base botBase))
            {
                botBase.SetResource(DropResource());

                StateMachine.CompleteCollectTask();
                StateMachine.SetState(_idleState);
            }
        }
        
        public void UpdateState()
        {
            Move(_targetPosition);
            Rotate(_targetPosition);
        }

        public override void Enter()
        {
            base.Enter();
            
            _targetPosition = StateMachine.BasePosition;
        }

        private Resource DropResource()
        {
            Resource resource = transform.GetComponentInChildren<Resource>();
            resource.DeactivateKinematicBehavior();
            resource.transform.SetParent(null);
            resource.CallEvent();

            return resource;
        }
    }
}