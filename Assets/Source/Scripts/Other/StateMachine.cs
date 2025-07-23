using Source.Scripts.Bots;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Other
{
    public class StateMachine : MonoBehaviour
    {
        public BotState CurrentState { get; private set; }


        public void UpdateCurrentState()
        {
            if (CurrentState is IUpdatable updatable)
            {
                updatable.UpdateState();
            }
        }

        public void SetState(BotState state)
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