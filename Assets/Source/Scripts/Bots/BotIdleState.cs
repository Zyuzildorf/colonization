using System;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Bots
{
    public class BotIdleState : BotState, IUpdatable
    {
        [SerializeField] private BotSearchingResourceState _botSearchingResourceState;

        public void UpdateState()
        {
            if (BotCollector.CurrentTask == Tasks.CollectResources)
            {
                StateMachine.SetState(_botSearchingResourceState);
            }
            else if (BotCollector.CurrentTask == Tasks.BuildBase)
            {
                
            }
        }
    }
}