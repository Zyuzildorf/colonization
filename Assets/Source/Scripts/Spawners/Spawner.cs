using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private float _radius;
        [SerializeField] private int _initialPoolSize = 10;
        [SerializeField] private bool _canExpand = true;

        private Queue<T> _pool = new Queue<T>();
        protected List<T> ActiveObjects = new List<T>();

        protected virtual void Awake()
        {
            PrewarmPool();
        }
        
        protected virtual T ReleaseObject()
        {
            if (_pool.Count > 0)
            {
                T obj = _pool.Dequeue();
                return ActionOnRelease(obj);
            }
            
            if (_canExpand)
            {
                T obj = CreatePooledObject();
                return ActionOnRelease(obj);
            }
            
            return null;
        }

        protected virtual void GetObject(T obj)
        {
            ActionOnGet(obj);
        }

        protected virtual void ActionOnGet(T obj)
        {
            obj.gameObject.SetActive(false);
            ActiveObjects.Remove(obj);
            _pool.Enqueue(obj);
        }
        
        protected virtual T ActionOnRelease(T obj)
        {
            obj.transform.position = GetRandomSpawnPosition();
            obj.gameObject.SetActive(true);
            ActiveObjects.Add(obj);
            
            return obj;
        }
        
        protected virtual T CreatePooledObject()
        {
            T newObj = Instantiate(_prefab, transform);
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(null);
            _pool.Enqueue(newObj);
            return newObj;
        }

        private void PrewarmPool()
        {
            for (int i = 0; i < _initialPoolSize; i++)
            {
                CreatePooledObject();
            }
        }
        
        private Vector3 GetRandomSpawnPosition()
        {
            return new Vector3(
                Random.Range(transform.position.x - _radius, transform.position.x + _radius),
                transform.position.y,
                Random.Range(transform.position.z - _radius, transform.position.z + _radius)
            );
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}