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
        private readonly CharacterMovementController _movementController = new CharacterMovementController();
        
        public override ObjectTypes ObjectType => ObjectTypes.Character;
        
        private void OnTriggerEnter(Collider other)
        {
            var movable = other.gameObject.GetComponentInParent<BaseGameMovableGameObject>();
            if (movable)
            {
                GameController.Instance.Loose();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            RegisterComponent(_movementController);
            _movementController.EndTurn = EndTurn;
            _movementController.StartTurn = StartTurn;
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
            GameController.Instance.SceneIsReady = () => { _movementController.FillMovableGameObjectsList(); };
           
            GameController.Instance.objectsPool.PollUpdated += () =>
            {
                _movementController.FillMovableGameObjectsList();
            };
        }
    }
}
