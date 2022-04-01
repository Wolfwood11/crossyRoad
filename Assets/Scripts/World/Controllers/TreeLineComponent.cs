using System;
using Base;
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
            float pos = -halfLen;
            while (pos < halfLen)
            {   
                pos += Random.Range(0, MinimalStep);
                if (Mathf.Abs(pos) > DeadZone)
                {
                    InstantiateCar(pos);
                    pos += Random.Range(MinimalStep, MaxStep);
                }
            }
        }
        
        private void InstantiateCar(float pos)
        {
            var car = GameController.Instance.GetTreeGameObject();
            car.transform.position =
                Owner.transform.position - Owner.transform.right * pos + Owner.transform.up;
            car.transform.rotation = Quaternion.identity;
            car.SetActive(true);
        }
    }
}