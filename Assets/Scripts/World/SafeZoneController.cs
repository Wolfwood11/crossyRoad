using System.Collections.Generic;
using Base;
using Base.Enums;
using UnityEngine;
using World.Controllers;

namespace World
{
    public class SafeZoneController : BaseWorldItem
    {
        public override ObjectTypes ObjectType => ObjectTypes.SafeZone;

        public override void EmitObjectsOfLine()
        {
            var newRoadLine = new TreeLineComponent();
            RegisterComponent(newRoadLine);
            newRoadLine.StartEmitTrees();
        }
    }
}
