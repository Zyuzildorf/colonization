using Source.Scripts.Interfaces;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Bots
{
    public class BotSearchingResourceState : MovementState, IUpdatable, ITriggerable
    {
        [SerializeField]  private BotDeliveringResourceState _botDeliveringResourceState;
        [SerializeField] private float _closeDistance = 0.1f;
        [SerializeField] private Vector3 _pickUpOffset;
    
        private Transform _targetPosition;
        private bool _isResourceTaken;

        public void ProcessTriggerCollider(Collider other)
        {
            if (other.TryGetComponent(out Resource resource) && IsTargetResource(resource)) 
            {
                PickUpResource(resource);
            }
        }
        
        public void UpdateState()
        {
            Move(_targetPosition.position);
            Rotate(_targetPosition.position);

            if (_isResourceTaken)
            {
                StateMachine.SetState(_botDeliveringResourceState);
            }
        }
    
        public override void Enter()
        {
            base.Enter();
            
            _isResourceTaken = false;
            _targetPosition = BotCollector.TargetPosition;
        }

        private void PickUpResource(Resource resource)
        {
            resource.ActivateKinematicBehavior();
        
            resource.transform.SetParent(transform);
            resource.transform.localPosition = new Vector3(_pickUpOffset.x, _pickUpOffset.y,
                _pickUpOffset.z);
        
            _isResourceTaken = true;
        }

        private bool IsTargetResource(Resource resource)
        {
            Vector2 targetPosition = new Vector2(_targetPosition.position.x, _targetPosition.position.z);
            Vector2 resourcePosition = new Vector2(resource.transform.position.x, resource.transform.position.z);
        
            if ((targetPosition - resourcePosition).sqrMagnitude < _closeDistance * _closeDistance)
            {
                return true;
            }
            return false;
        }
    }
}