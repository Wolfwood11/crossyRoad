using System;
using System.Collections.Generic;
using System.Linq;
using Base;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Utils
{
    [Serializable]
    public class ObjectsPool
    {
        private readonly Dictionary<GameObject, List<GameObject>> _objectsInPoll = new Dictionary<GameObject, List<GameObject>>();
        [SerializeField]private int pollSize = 20;

        public Action PollUpdated;

        public void DeactivateAllPooled()
        {
            foreach (var pooledList in _objectsInPoll)
            {
                foreach (var obj in pooledList.Value)
                {
                    obj.SetActive(false);
                }
            }
        }
        
        public List<T> GetObjectsOfType<T>() where T:class
        {
            var listOf = new List<T>();
            foreach (var pooledList in _objectsInPoll)
            {
                foreach (var obj in pooledList.Value)
                {
                    var child = obj.GetComponent<T>();
                    if (child != null)
                    {
                        listOf.Add(child);
                    }
                }
            }

            return listOf;
        }
        
        public void ClearPool()
        {
            PollUpdated = null;
            _objectsInPoll.Clear();
        }
        public GameObject GetPolledObject(GameObject obj)
        {
            if (!_objectsInPoll.ContainsKey(obj)) return null;
            var objectsList = _objectsInPoll[obj];
            foreach (var polled in objectsList)
            {
                if (!polled.gameObject.activeInHierarchy)
                {
                    return polled;
                }
            }

            return null;
        }

        public void PollObject(GameObject obj)
        {
            if (!_objectsInPoll.ContainsKey(obj))
            {
                var newList = new List<GameObject>();
                for (int i = 0; i < pollSize; i++)
                {
                    var toPoolGameObject = SpawnPooled(obj);
                    newList.Add(toPoolGameObject);
                }
                _objectsInPoll.Add(obj, newList);
            }
        }

        public GameObject AddToPoll(GameObject obj)
        {
            if (!_objectsInPoll.ContainsKey(obj))
            {
                PollObject(obj);
                PollUpdated?.Invoke();
                return GetPolledObject(obj);
            }
            
            var toPoolGameObject = SpawnPooled(obj);
            _objectsInPoll[obj].Add(toPoolGameObject);
            PollUpdated?.Invoke();
            return toPoolGameObject;
        }

        private static GameObject SpawnPooled(GameObject obj)
        {
            var toPoolGameObject = Object.Instantiate(obj, Vector3.zero, Quaternion.identity);
            toPoolGameObject.SetActive(false);
            return toPoolGameObject;
        }
    }
}