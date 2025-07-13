using System;
using System.Collections.Generic;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Base
{
    public class ResourcesCounter : MonoBehaviour
    {
        private List<Resource> _resources;
        
        public int ResourcesCount => _resources.Count;
        public event Action<int> ResourcesValueChanged;

        private void Awake()
        {
            _resources = new List<Resource>();
        }

        public void SetResource(Resource resource)
        {
            _resources.Add(resource);
            ResourcesValueChanged?.Invoke(_resources.Count);
        }
    }
}