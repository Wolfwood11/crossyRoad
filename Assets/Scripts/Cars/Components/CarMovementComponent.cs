using System;
using Base;
using Base.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cars.Components
{
    [Serializable]
    public class CarMovementComponent : BaseComponent
    {
        [SerializeField] private float speed;

        public float SpeedFactor { get; set; } = 1;
        public float Speed
        {
            get => speed;
            set => speed = value;
        }
        
        private const float DifficultyStep = 10f;

        protected override void TickComponent()
        {
            var currentSpeed = SpeedFactor * (Speed + Speed * Convert.ToSingle(GameController.Instance.Difficulty) / DifficultyStep);
            Owner.transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed, Space.Self);

            ProcessCarGameFieldState();
        }

        private void ProcessCarGameFieldState()
        {
            if (!(Mathf.Abs(Owner.transform.position.x) > GameController.WorldSize + 2)) return;
            
            var position = Owner.transform.position;
            var pos = Mathf.Clamp(-position.x, -GameController.WorldSize, GameController.WorldSize);
            position = new Vector3(pos, position.y, position.z);
            
            GameObject gObject = GameController.Instance.InstantiateObjectOfType(ObjectTypes.Car, position, Owner.transform.rotation);
            Owner.Deactivate();
            
            if (!gObject) return;
            
            var car = gObject.GetComponent<CarGameObject>();
            car.SetSpeedMFactor(SpeedFactor);
        }
    }
}