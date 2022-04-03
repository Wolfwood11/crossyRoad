using Base;
using Base.Enums;
using Cars;
using Cars.Components;
using UnityEngine;

namespace Trees
{
    public class TreeGameObject : BaseGameMovableGameObject
    {
        public override ObjectTypes ObjectType => ObjectTypes.Tree;
    }
}
