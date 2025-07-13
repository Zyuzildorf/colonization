using System;
using UnityEngine;

namespace Source.Scripts.Other
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class Resource : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private BoxCollider _collider;
    
        public event Action<Resource> Collected;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<BoxCollider>();
        }

        public void ActivateKinematicBehavior()
        {
            _rigidbody.isKinematic = true;
            _collider.isTrigger = true;
        }

        public void DeactivateKinematicBehavior()
        {
            _rigidbody.isKinematic = false;
            _collider.isTrigger = false;
        }

        public void CallEvent()
        {
            Collected.Invoke(this);
        }
    }
}