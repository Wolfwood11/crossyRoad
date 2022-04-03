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
        
        public float Speed
        {
            get => speed;
            set => speed = value;
        }
        
        private const float DifficultyStep = 10f;

        protected override void TickComponent()
        {
            var speedFactor = 1f;
            var move = Owner as BaseGameMovableGameObject;
            if (move)
            {
                speedFactor = move.SpeedFactor;
            }
            var currentSpeed = speedFactor * (Speed + Speed * Convert.ToSingle(GameController.Instance.Difficulty) / DifficultyStep);
            Owner.transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed, Space.Self);
        }
    }
}