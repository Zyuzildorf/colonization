using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Other
{
    public abstract class State: MonoBehaviour, IExitable, IEnterable
    {
        protected virtual void Start()
        {
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