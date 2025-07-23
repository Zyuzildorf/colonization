using Source.Scripts.Interfaces;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Bots
{
    [RequireComponent(typeof(BotCollector), typeof(StateMachine))]
    public abstract class BotState : MonoBehaviour, IExitable, IEnterable
    {
        protected BotCollector BotCollector;
        protected StateMachine StateMachine;

        private void Start()
        {
            StateMachine = GetComponent<StateMachine>();
            BotCollector = GetComponent<BotCollector>();
            enabled = false;
        }

        public virtual void Enter()
        {
            enabled = true;
        }
        
        public virtual void Exit()
        {
            enabled = false;
        }
    }
}