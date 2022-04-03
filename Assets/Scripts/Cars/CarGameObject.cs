using Base;
using Base.Enums;
using Cars.Components;
using UnityEngine;
using UniversalComponents;

namespace Cars
{
    public class CarGameObject : BaseGameMovableGameObject
    {
        [SerializeField]private CarMovementComponent movementController = new CarMovementComponent();
        private readonly SideObjectLiveComponent _sideLiveComponent = new SideObjectLiveComponent();
        public override ObjectTypes ObjectType => ObjectTypes.Car;
        
        protected override void Awake()
        {
            base.Awake();
            RegisterComponent(movementController);
            RegisterComponent(_sideLiveComponent);
        }
    }
}
