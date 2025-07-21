using Source.Scripts.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Source.Scripts.Bots
{
    public class IdleState : CollectorState, IUpdatable
    {
        [SerializeField] private SearchingResourceState _searchingResourceState;

        public void UpdateState()
        {
            if (StateMachine.IsTaskToCollectReceived)
            {
                StateMachine.SetState(_searchingResourceState);
            }
            else if (StateMachine.IsTaskToBuildNewBaseReceived)
            {
                //StateMachine.SetState();
            }
        }
    }
}