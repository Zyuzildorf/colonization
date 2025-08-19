using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Bots
{
    [RequireComponent(typeof(BotCollector), typeof(BotsStateMachine))]
    public abstract class BotState : State
    {
        protected BotsStateMachine StateMachine;
        protected BotCollector BotCollector;

        protected override void Start()
        {
            BotCollector = GetComponent<BotCollector>();
            StateMachine = GetComponent<BotsStateMachine>();
            base.Start();
        }
    }
}