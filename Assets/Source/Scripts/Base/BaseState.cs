using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Base
{
    [RequireComponent(typeof(BotsBase), typeof(BaseStateMachine))]
    public abstract class BaseState : State
    {
        protected BotsBase BotsBase;
        protected BaseStateMachine StateMachine;

        protected override void Start()
        {
            BotsBase = GetComponent<BotsBase>();
            StateMachine = GetComponent<BaseStateMachine>();
            base.Start();
        }

        public abstract void ProcessResource();
    }
}