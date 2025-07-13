using System.Collections.Generic;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Base
{
    public class ResourcesSearcher : MonoBehaviour
    {
        [SerializeField] private float _searchRadius;
        [SerializeField] private int _maxColliders = 10;

        private Collider[] _hitColliders;

        public bool TryFindResources(out List<Resource> resources)
        {
            resources = new List<Resource>();

            if (_hitColliders == null)
            {
                _hitColliders = new Collider[_maxColliders];
            }

            int collidersAmount = Physics.OverlapSphereNonAlloc(transform.position, _searchRadius, _hitColliders);

            for (int i = 0; i < collidersAmount; i++)
            {
                if (_hitColliders[i].TryGetComponent(out Resource resource))
                {
                    resources.Add(resource);
                }
            }
            
            return resources.Count > 0;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _searchRadius);
        }
    }
}