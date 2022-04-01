using System.Collections.Generic;
using Base;
using UnityEngine;
using World.Controllers;

namespace World
{
    public class SafeZoneController : BaseWorldItem
    {
        public void StartEmittingTrees()
        {
            var newRoadLine = new TreeLineComponent();
                RegisterComponent(newRoadLine);
                newRoadLine.StartEmitTrees();
        }
    }
}
