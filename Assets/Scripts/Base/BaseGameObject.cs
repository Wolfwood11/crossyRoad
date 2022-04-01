using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class BaseGameObject : MonoBehaviour
    {
        private readonly List<BaseComponent> _components = new List<BaseComponent>();
        
        private static bool _isPaused = false;
        protected static bool IsPaused
        {
            get => _isPaused;
            set
            {
                _isPaused = value;
                Time.timeScale = _isPaused ? 0 : 1;
            }
        }

        protected void Deactivate()
        {
            gameObject.SetActive(false);
            transform.position = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        }

        protected void RegisterComponent(BaseComponent component)
        {
            if (!_components.Contains(component))
            {
                component.Owner = this;
                _components.Add(component);
            }
        }
        protected void UnRegisterComponent(BaseComponent component)
        {
            if (_components.Contains(component))
            {
                _components.Remove(component);
            }
        }
        
        protected virtual void Awake()
        {
            _components.Clear();
            IsPaused = false;
        }
        
        // Start is called before the first frame update
        protected virtual void Start()
        {
            foreach (var component in _components)
            {
                component.Start();
            }
        }

        protected void OnDestroy()
        {
            foreach (var component in _components)
            {
                component.OnDestroy();
            }
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (_isPaused) return;
            foreach (var component in _components)
            {
                component.UpdateComponent();
            }
        }

        protected void FixedUpdate()
        {
            if (_isPaused) return;
            foreach (var component in _components)
            {
                component.FixedUpdateComponent();
            }
        }
    }
}
