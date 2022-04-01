using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utils
{
    [Serializable]
    public class ObjectsPool
    {
        private readonly Dictionary<GameObject, List<GameObject>> _objectsInPoll = new Dictionary<GameObject, List<GameObject>>();
        [SerializeField]private int pollSize = 20;

        public void ClearPool()
        {
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
                    var toPoolGameObject= GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity);
                    toPoolGameObject.SetActive(false);
                    newList.Add(toPoolGameObject);
                }
                _objectsInPoll.Add(obj, newList);
            }
        }
    }
}