using System;
using Base;
using Base.Enums;
using Cars;
using Character.Components;
using UnityEngine;
using UnityEngine.Assertions.Must;
using World;

namespace Character
{
    public class GameCharacter : BaseGameObject
    {
        [SerializeField] private Animator animator;
        private readonly CharacterMovementComponent _movementComponent = new KeyboardCharacterMovementComponent();
        
        public override ObjectTypes ObjectType => ObjectTypes.Character;
        
        private void OnTriggerEnter(Collider other)
        {
            var movable = other.gameObject.GetComponentInParent<BaseGameMovableGameObject>();
            if (movable)
            {
                _movementComponent.StopMovement();
                GameController.Instance.Loose();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            RegisterComponent(_movementComponent);
            _movementComponent.EndTurn = EndTurn;
            _movementComponent.StartTurn = StartTurn;
        }

        private void StartTurn()
        {
            //animator.SetTrigger("Jump");
        }

        private void EndTurn()
        {
            
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            GameController.Instance.OnSceneReady();
            GameController.Instance.SceneIsReady = () => 
            { 
                StopAllCoroutines();
                _movementComponent.StopMovement();
                _movementComponent.FillMovableGameObjectsList(); 
            };
           
            GameController.Instance.objectsPool.PollUpdated += () =>
            {
                _movementComponent.FillMovableGameObjectsList();
            };
        }
    }
}
