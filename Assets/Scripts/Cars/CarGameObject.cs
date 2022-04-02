using Base;
using Base.Enums;
using Cars.Components;
using UnityEngine;

namespace Cars
{
    public class CarGameObject : BaseGameMovableGameObject
    {
        [SerializeField]private CarMovementComponent movementController = new CarMovementComponent();

        public CarGameObject CarAtForward { get; set; }
        public override ObjectTypes ObjectType => ObjectTypes.Car;
        
        private const float MaxDistanceToForward = 5f;
        private const float SlowFactor = 0.8f;
        private const float SlowZoneOutFactor = 1.2f;
        
        public float GetSpeed()
        {
            return movementController.Speed;
        }
        
        protected override void Awake()
        {
            base.Awake();
            RegisterComponent(movementController);
        }
        
        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (CarAtForward && CarAtForward.gameObject.activeInHierarchy)
            {
                var dist = CarAtForward.transform.position - transform.position;
                
                if (dist.magnitude < MaxDistanceToForward)
                {
                    movementController.Speed = CarAtForward.GetSpeed() * SlowFactor;
                }
                
                if (dist.magnitude > SlowZoneOutFactor * MaxDistanceToForward  && movementController.Speed < movementController.InitialSpeed)
                {
                    movementController.Speed = movementController.InitialSpeed;
                }
                
                if (dist.magnitude < 2)
                {
                    Deactivate();
                }
            }
            else if (movementController.Speed < movementController.InitialSpeed)
            {
                movementController.Speed = movementController.InitialSpeed;
            }
            
            var loc = transform.position.x;
            if (Mathf.Abs(loc) > GameController.WorldSize)
            {
                Deactivate();
            }
        }
    }
}
