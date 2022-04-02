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

        private BaseGameMovableGameObject[] _movableGameObjects;

        private bool isMove;

        private IEnumerator MoveForward()
        {
            StartTurn?.Invoke();
            isMove = true;
            float inAction = 0;
            while (inAction < 1)
            {
                var step = CalculateMovementStep(ref inAction);
                Owner.transform.Translate(Vector3.forward * step, Space.Self);
                yield return null;
            }

            isMove = false;
            EndTurn?.Invoke();
        }

        private static float CalculateMovementStep(ref float inAction)
        {
            var step = Time.deltaTime * 7;
            inAction += Time.deltaTime * 7;
            step = inAction > 1 ? step + 1 - inAction : step;
            return step;
        }

        public void FillMovableGameObjectsList()
        {
            _movableGameObjects = Object.FindObjectsOfType<BaseGameMovableGameObject>();
        }

        private IEnumerator SideMove(Vector3 dir)
        {
            StartTurn?.Invoke();
            isMove = true;
            float inAction = 0;
            while (inAction < 1)
            {
                var step = CalculateMovementStep(ref inAction);
                foreach (var movableGameObject in _movableGameObjects)
                {
                    if (movableGameObject.gameObject.activeInHierarchy)
                    {
                        movableGameObject.transform.Translate(dir * step, Space.World);
                    }
                }
                yield return null;
            }

            isMove = false;
            EndTurn?.Invoke();
        }
        
        protected override void TickComponent()
        {
            if (isMove) return;
            
            if (Input.GetKeyDown(KeyCode.UpArrow) )
            {
                Owner.StartCoroutine(MoveForward());
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Owner.StartCoroutine(SideMove(Vector3.right));
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Owner.StartCoroutine(SideMove(Vector3.left));
            }
        }
    }
}