using System.Collections;
using System.Collections.Generic;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Base
{
    public class BaseResourcesHandler : MonoBehaviour
    {
        [SerializeField] private float _searchingResourcesDelay = 1f;
        
        private ResourcesSearcher _searcher;
        private ResourcesCounter _counter;
        private ResourcesVault _vault;
        private WaitForSeconds _waitForResourcesSearch;

        public void Initialize(ResourcesSearcher searcher, ResourcesVault vault, ResourcesCounter counter)
        {
            _searcher = searcher;
            _vault = vault;
            _counter = counter;
            _waitForResourcesSearch = new WaitForSeconds(_searchingResourcesDelay);
            
            StartCoroutine(FindFreeAvailableResources());
        }

        public void SetResource(Resource resource)
        {
            _vault.RemoveBusyResource(resource);
            _counter.SetResource(resource);
        }
        
        private IEnumerator FindFreeAvailableResources()
        {
            while (enabled)
            {
                if (_vault.FreeResourcesCount <= 0)
                {
                    if (_searcher.TryFindResources(out List<Resource> resources))
                    {
                        _vault.SetResources(resources);
                    }
                }

                yield return _waitForResourcesSearch;
            }
        }
    }
}