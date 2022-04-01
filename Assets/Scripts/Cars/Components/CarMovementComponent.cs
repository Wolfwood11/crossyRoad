using System;
using Base;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cars.Components
{
    [Serializable]
    public class CarMovementComponent : BaseComponent
    {
        [SerializeField] private float speed;

        public float InitialSpeed { get; set; }
        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        private const float MinSpeedDivMultiplayer = -0.05f; // slower on 5%
        private const float MaxSpeedDivMultiplayer = 0.15f; // faster on  15%

        public override void Start()
        {
            base.Start();
            var dV = Speed * 0.05f;
            Speed += Random.Range(MinSpeedDivMultiplayer * Speed, MaxSpeedDivMultiplayer * Speed);
            InitialSpeed = Speed;
        }

        protected override void TickComponent()
        {
            var currentSpeed = Speed + Speed * Convert.ToSingle(GameController.Instance.Difficulty) / 10f;
            Owner.transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed, Space.Self);
        }
    }
}