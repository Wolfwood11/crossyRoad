using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Character.Components
{
    public class CharacterMovementController : BaseComponent
    {
        public Action EndTurn;
        public Action StartTurn;

        private const float CharacterSpeed = 7;

        private BaseGameMovableGameObject[] _movableGameObjects;
        private BaseWorldItem[] _worldItems;

        private bool _isMove;

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
        
        private static float CalculateMovementStep(ref float inAction)
        {
            var step = Time.deltaTime * CharacterSpeed;
            inAction += step;
            step = inAction > 1 ? step + 1 - inAction : step;
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
            float inAction = 0;
            while (inAction < 1)
            {
                var step = CalculateMovementStep(ref inAction);
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
        
        protected override void TickComponent()
        {
            if (_isMove) return;
            
            if (Input.GetKeyDown(KeyCode.UpArrow) )
            {
                Owner.StartCoroutine(Move(-Vector3.forward, new List<BaseGameObject[]> { _worldItems, _movableGameObjects } , true));
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Owner.StartCoroutine(Move(Vector3.right, new List<BaseGameObject[]> { _movableGameObjects } , false));
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Owner.StartCoroutine(Move(Vector3.left, new List<BaseGameObject[]> { _movableGameObjects } , false));
            }
        }
    }
}