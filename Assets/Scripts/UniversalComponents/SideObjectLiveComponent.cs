using Base;
using Base.Enums;
using Cars;
using UnityEngine;

namespace UniversalComponents
{
    public class SideObjectLiveComponent : BaseComponent
    {
        protected override void TickComponent()
        {
            base.TickComponent();
            if (!(Mathf.Abs(Owner.transform.position.x) > GameController.WorldSize)) return;
            
            var position = Owner.transform.position;
            var pos = Mathf.Clamp(-position.x, -GameController.WorldSize, GameController.WorldSize);
            position = new Vector3(pos, position.y, position.z);
            
            GameObject gObject = GameController.Instance.InstantiateObjectOfType(Owner.ObjectType, position, Owner.transform.rotation);
            Owner.Deactivate();
            
            if (!gObject) return;
            var movable = gObject.GetComponent<BaseGameMovableGameObject>();
            var currentMovable = Owner as BaseGameMovableGameObject;
            if (movable && currentMovable)
            {
                movable.SpeedFactor = currentMovable.SpeedFactor;
            }
        }
    }
}