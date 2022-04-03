using System;
using Base;
using Base.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World.Controllers
{
    [Serializable]
    public class TreeLineComponent : BaseComponent
    {
        private const float MinimalStep = 5;
        private const float MaxStep = 10;
        private const float DeadZone = 5;
        public void StartEmitTrees()
        {
            var halfLen = GameController.WorldSize;
            float pos = -halfLen + 1;
            while (pos < halfLen - 1)
            {   
                pos += Mathf.Clamp(Random.Range(0, MinimalStep), 0, halfLen );
                if (Mathf.Abs(pos) > DeadZone)
                {
                    InstantiateTree(pos);
                    pos += Random.Range(MinimalStep, MaxStep);
                }
            }
        }
        
        private void InstantiateTree(float pos)
        {
            var transform = Owner.transform;
            var position = transform.position - transform.right * pos + transform.up;
            GameController.Instance.InstantiateObjectOfType(ObjectTypes.Tree, position, Quaternion.identity);
        }
    }
}