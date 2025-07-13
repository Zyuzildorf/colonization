using System.Collections;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Spawners
{
    public class ResourcesSpawner : Spawner<Resource>
    {
        [SerializeField] private int _amount;
        [SerializeField] private float _delay;
    
        private WaitForSeconds _waitForSeconds;
        
        private void Start()
        {
            _waitForSeconds = new WaitForSeconds(_delay);
            
            StartSpawning();
        }

        private void OnDisable()
        {
            foreach (Resource resource in ActiveObjects.ToArray())
            {
                resource.Collected -= OnCollected;
            }
        }

        protected override Resource CreatePooledObject()
        {
            Resource resource = base.CreatePooledObject();
            resource.Collected += OnCollected;
            
            return resource;
        }

        private void StartSpawning()
        {
            StartCoroutine(SpawnObjectsOverTime());
        }

        private IEnumerator SpawnObjectsOverTime()
        {
            int spawnedObjectsCount = 0;
        
            while (_amount > spawnedObjectsCount)
            {
                ReleaseObject();
                spawnedObjectsCount++;
                yield return _waitForSeconds;
            }
        }
        
        private void OnCollected(Resource resource)
        {
            GetObject(resource);
        }
    }
}