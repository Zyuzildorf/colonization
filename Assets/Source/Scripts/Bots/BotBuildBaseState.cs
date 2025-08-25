using Source.Scripts.Base;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Bots
{
    public class BotBuildBaseState : MovementState, IUpdatable
    {
        [SerializeField] private BotIdleState _botIdleState;
        
        private BotsBase _base;
        private Transform _flagPosition;
        private float _closeDistance = 0.1f;

        public void UpdateState()
        {
            Move(_flagPosition.position);
            Rotate(_flagPosition.position);

            if ((transform.position - _flagPosition.position).sqrMagnitude < _closeDistance * _closeDistance)
            {
                _base = Instantiate(BotCollector.BotsBase, _flagPosition.position, _flagPosition.rotation);
                _base.Initialize(BotCollector.BotsBase.StartBotsAmount, BotCollector.BotsBase.InputReader, 
                    BotCollector.BotsBase.Vault);
                
                StateMachine.SetState(_botIdleState);
            }
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _flagPosition = BotCollector.TargetPosition;
        }
    }
}