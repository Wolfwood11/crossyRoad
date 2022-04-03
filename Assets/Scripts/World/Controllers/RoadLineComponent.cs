using System;
using Base;
using Base.Enums;
using Cars;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World.Controllers
{
    [Serializable]
    public class RoadLineComponent : BaseComponent
    {
        public GameObject SpawnPoint { get; set; }
        
        private const float MinimalStep = 14;
        private const float MaxStep = 20;

        private const float MinSpeedFactor = 0.7f;
        private const float MaxSpeedFactor = 1.5f;

        private float _speedFactor = 1;
        
        public void StartEmitCars()
        {
            float pos = Random.Range(0 ,MinimalStep);
            float maxPos = GameController.WorldSize * 2 - MinimalStep + pos;
            
            _speedFactor = Random.Range(MinSpeedFactor, MaxSpeedFactor);
            
            while (pos < maxPos)
            {
                InstantiateCar(SpawnPoint, pos);
                pos += Random.Range(MinimalStep, MaxStep);
            }
        }
        
        private void InstantiateCar(GameObject spawnPoint, float pos)
        {
            var position =  spawnPoint.transform.position + spawnPoint.transform.forward * pos;
            var rotation = spawnPoint.transform.rotation;
            var gObject = GameController.Instance.InstantiateObjectOfType(ObjectTypes.Car, position, rotation);
            if (!gObject) return;
            var car = gObject.GetComponent<CarGameObject>();
            car.SetSpeedMFactor(_speedFactor);
        }
    }
}