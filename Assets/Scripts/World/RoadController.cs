using System.Collections.Generic;
using Base;
using UnityEngine;
using World.Controllers;

namespace World
{
    public class RoadController : BaseWorldItem
    {
        [SerializeField] private GameObject[] spawnPoints;
        public void StartEmittingCars()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                var newRoadLine = new RoadLineComponent();
                RegisterComponent(newRoadLine);
                newRoadLine.SpawnPoint = spawnPoint;
                newRoadLine.StartEmitCars();
            }
        }
    }
}
