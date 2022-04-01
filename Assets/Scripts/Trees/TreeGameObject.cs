using Base;
using Cars;
using Cars.Components;
using UnityEngine;

namespace Trees
{
    public class TreeGameObject : BaseGameMovableGameObject
    {
        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (Mathf.Abs(transform.position.x) > GameController.WorldSize + 1)
            {
                var position = transform.position;
                position = new Vector3(-position.x, position.y, position.z);
                transform.position = position;
            }
        }
    }
}
