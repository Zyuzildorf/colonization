using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<Collider> TriggerEntered;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEntered?.Invoke(other);
    }
}