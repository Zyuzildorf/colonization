using System.Collections.Generic;
using System.Linq;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Base
{
    public class ResourcesVault : MonoBehaviour
    {
        private List<Resource> _freeResources = new List<Resource>();
        private List<Resource> _busyResources = new List<Resource>();

        public int FreeResourcesCount => _freeResources.Count;

        public bool TryGetFreeResource(out Resource freeResource)
        {
            if (_freeResources.Count > 0)
            {
                freeResource = GiveLastFreeResource();
                return true;
            }

            freeResource = null;
            return false;
        }

        public void RemoveBusyResource(Resource resource)
        {
            _busyResources.Remove(resource);
        }

        public void SetResources(List<Resource> resources)
        {
            foreach (Resource resource in resources)
            {
                if (_busyResources.Contains(resource) == false && _freeResources.Contains(resource) == false)
                {
                    _freeResources.Add(resource);
                }
            }
        }

        private Resource GiveLastFreeResource()
        {
            Resource resource = _freeResources.Last();
            _freeResources.Remove(resource);
            _busyResources.Add(resource);
            return resource;
        }
    }
}