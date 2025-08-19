using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Base
{
    [RequireComponent(typeof(Base), typeof(BaseStateMachine))]
    public abstract class BaseState : State
    {
        protected Base Base;
        protected BaseStateMachine StateMachine;

        protected override void Start()
        {
            Base = GetComponent<Base>();
            StateMachine = GetComponent<BaseStateMachine>();
            base.Start();
        }

        public abstract void ProcessResource();
    }
}