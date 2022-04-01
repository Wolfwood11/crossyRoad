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
        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (Mathf.Abs(transform.position.x) > GameController.WorldSize + 2)
            {
                var position = transform.position;
                position = new Vector3(-position.x, position.y, position.z);
                transform.position = position;
            }
        }
    }
}
