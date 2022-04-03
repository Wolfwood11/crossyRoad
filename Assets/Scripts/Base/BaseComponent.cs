using System;
using UnityEngine;

namespace Base
{
    [Serializable]
    public abstract class BaseComponent
    {
        public BaseGameObject Owner { get; set; }

        public bool Enabled { get; set; } = true;

        public virtual void Start()
        {
            
        }
        
        public virtual void OnDestroy()
        {
            
        }

        protected virtual void TickComponent()
        {
            
        }
        
        protected virtual void FixedTickComponent()
        {
            
        }
        
        public void UpdateComponent()
        {
            if (!Enabled || !Owner.gameObject.activeInHierarchy) return;
            TickComponent();
        }
        
        public void FixedUpdateComponent()
        {
            if (!Enabled || !Owner.gameObject.activeInHierarchy) return;
            FixedTickComponent();
        }
    }
}