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
        private readonly CharacterMovementController _movementController = new CharacterMovementController();

        public override ObjectTypes ObjectType => ObjectTypes.Character;
        
        private void OnTriggerEnter(Collider other)
        {
            var movable = other.gameObject.GetComponentInParent<BaseGameMovableGameObject>();
            if (movable)
            {
                IsPaused = true;
                GameController.Instance.ShowLoosePopup();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            RegisterComponent(_movementController);
            _movementController.EndTurn = EndTurn;
        }

        private void EndTurn()
        {
            if (transform.position.z >= GameController.Instance.TargetToWin)
            {
                GameController.Instance.Difficulty++;
                GameController.Instance.ShowWinPopup();
                IsPaused = true;
            }
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            GameController.Instance.OnSceneReady();
            _movementController.FillMovableGameObjectsList();
        }
    }
}
