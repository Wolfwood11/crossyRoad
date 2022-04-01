using System;
using Base;
using Cars;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World.Controllers
{
    [Serializable]
    public class RoadLineComponent : BaseComponent
    {
        public GameObject SpawnPoint { get; set; }
        
        private const float MinDelayToSpawn = 5;
        private const float MaxDelayToSpawn = 8;
        private const float MinimalStep = 12;
        private const float MaxStep = 20;
        private const float DeltaStep = 0.2f;

        private float _dt = 0;

        private CarGameObject _lastEmitted = null;
        public void StartEmitCars()
        {
            float pos = 0;
            while (pos < GameController.WorldSize * 2)
            {   
                pos += Random.Range(0, MinimalStep * DeltaStep);
                InstantiateCar(SpawnPoint, pos);
                pos += Random.Range(MinimalStep, MaxStep);
            }
            _dt = Random.Range(MinDelayToSpawn, MaxDelayToSpawn);
        }
        
        private void InstantiateCar(GameObject spawnPoint, float pos)
        {
            var car = GameController.Instance.GetCarGameObject();
            if (!car) return;
            
            car.transform.position =
                spawnPoint.transform.position + spawnPoint.transform.forward * pos;
            car.transform.rotation = spawnPoint.transform.rotation;
            car.SetActive(true);

            var enemy = car.GetComponent<CarGameObject>();
            
            if (_lastEmitted)
            {
                enemy.CarAtForward = _lastEmitted;
            }

            _lastEmitted = null;
            _lastEmitted = enemy;
        }
        
        private void SpawnCar()
        {
            _dt -= Time.deltaTime;
            bool needSpawn = _dt <= 0;
            if (_lastEmitted)
            {
                var distanceToLast = SpawnPoint.transform.position - _lastEmitted.transform.position;
                if (needSpawn)
                {
                    needSpawn = distanceToLast.magnitude >= Random.Range(MinimalStep * 0.7f ,MinimalStep);
                }
                else
                {
                    needSpawn = distanceToLast.magnitude >= MaxStep;
                }
            }
           
            if (needSpawn)
            {
                InstantiateCar(SpawnPoint, 0);
                _dt = Random.Range(MinDelayToSpawn, MaxDelayToSpawn);
            }
        }
        
        protected override void TickComponent()
        {
            SpawnCar();
        }
    }
}