using System;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Bots
{
    public class StateMachine : MonoBehaviour
    {
        public CollectorState CurrentState { get; private set; }


        public void UpdateCurrentState()
        {
            if (CurrentState is IUpdatable updatable)
            {
                updatable.UpdateState();
            }
        }

        public void SetState(CollectorState state)
        {
            if (CurrentState == state)
            {
                return;
            }

            CurrentState = state;

            if (CurrentState is IEnterable enterable)
            {
                enterable.Enter();
            }
        }
    }
}