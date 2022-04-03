using Base;
using Base.Enums;
using Cars.Components;
using UnityEngine;

namespace Cars
{
    public class CarGameObject : BaseGameMovableGameObject
    {
        [SerializeField]private CarMovementComponent movementController = new CarMovementComponent();
        
        public override ObjectTypes ObjectType => ObjectTypes.Car;
        
        public void SetSpeedMFactor(float factor)
        {
            movementController.SpeedFactor = factor;
        }
        
        protected override void Awake()
        {
            base.Awake();
            RegisterComponent(movementController);
        }
    }
}
