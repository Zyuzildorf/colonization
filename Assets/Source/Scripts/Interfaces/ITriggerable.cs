using UnityEngine;

namespace Source.Scripts.Interfaces
{
    public interface ITriggerable
    {
        void ProcessTriggerCollider(Collider other);
    }
}