using Base;
using Cars.Components;
using UnityEngine;

namespace Cars
{
    public class CarGameObject : BaseGameMovableGameObject
    {
        [SerializeField]private CarMovementComponent movementController = new CarMovementComponent();

        public CarGameObject CarAtForward { get; set; }
        
        private const float MaxDistanceToForward = 5f;


        public float GetSpeed()
        {
            return movementController.Speed;
        }
        protected override void Awake()
        {
            base.Awake();
            RegisterComponent(movementController);
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (CarAtForward && CarAtForward.gameObject.activeInHierarchy)
            {
                var dist = CarAtForward.transform.position - transform.position;
                
                if (dist.magnitude == 0)
                {
                    Deactivate();
                }
                
                if (dist.magnitude < MaxDistanceToForward)
                {
                    movementController.Speed = CarAtForward.GetSpeed() * 0.8f;
                }
                
                if (dist.magnitude > 1.2f * MaxDistanceToForward  && movementController.Speed < movementController.InitialSpeed)
                {
                    movementController.Speed = movementController.InitialSpeed;
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
