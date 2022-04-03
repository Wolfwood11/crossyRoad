using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Character.Components
{
    public abstract class CharacterMovementComponent : BaseComponent
    {
        public Action EndTurn;
        public Action StartTurn;

        private const float CharacterSpeed = 7;

        private BaseGameMovableGameObject[] _movableGameObjects;
        private BaseWorldItem[] _worldItems;


        public void StopMovement()
        {
            _isMove = false;
            MoveProgress = 1;
            Owner.StopAllCoroutines();
        }
        private bool _isMove;

        private float MoveProgress { get; set;  } = 1;

        private void TranslateListOfObjects(Vector3 value, BaseGameObject[] list)
        {
            foreach (var gameObject in list)
            {
                if (gameObject.gameObject.activeInHierarchy)
                {
                    gameObject.transform.Translate(value, Space.World);
                }
            }
        }
        
        private float CalculateMovementStep()
        {
            float step = Time.deltaTime * CharacterSpeed;
            MoveProgress += step;
            step = MoveProgress > 1 ? step + 1 - MoveProgress : step;
            return step;
        }

        public void FillMovableGameObjectsList()
        {
            _movableGameObjects = GameController.Instance.objectsPool.GetObjectsOfType<BaseGameMovableGameObject>().ToArray();
            _worldItems = GameController.Instance.objectsPool.GetObjectsOfType<BaseWorldItem>().ToArray();
        }

        private IEnumerator Move(Vector3 dir, List<BaseGameObject[]> objectsList, bool isForward)
        {
            StartTurn?.Invoke();
            _isMove = true;
            MoveProgress = 0;
            while (MoveProgress < 1)
            {
                var step = CalculateMovementStep();
                var moveStep = dir * step;
                foreach (var list in objectsList)
                {
                    TranslateListOfObjects(moveStep, list);
                }
                
                yield return null;
            }

            _isMove = false;
            EndTurn?.Invoke();
            if (isForward)
            {
                GameController.Instance.StepForward();
            }
        }

        protected abstract bool IsForwardInput();
        protected abstract bool IsLeftInput();
        protected abstract bool IsRightInput();
        
        protected override void TickComponent()
        {
            if (_isMove) return;
            
            if (IsForwardInput())
            {
                Owner.StartCoroutine(Move(-Vector3.forward, new List<BaseGameObject[]> { _worldItems, _movableGameObjects } , true));
            }
            
            if (IsLeftInput())
            {
                Owner.StartCoroutine(Move(Vector3.right, new List<BaseGameObject[]> { _movableGameObjects } , false));
            }
            
            if (IsRightInput())
            {
                Owner.StartCoroutine(Move(Vector3.left, new List<BaseGameObject[]> { _movableGameObjects } , false));
            }
        }
    }
}