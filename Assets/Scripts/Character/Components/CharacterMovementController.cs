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

        private BaseGameMovableGameObject[] _movableGameObjects;

        public void FillMovableGameObjectsList()
        {
            _movableGameObjects = Object.FindObjectsOfType<BaseGameMovableGameObject>();
        }

        private void SideMove(Vector3 dir)
        {
            foreach (var movableGameObject in _movableGameObjects)
            {
                if (movableGameObject.gameObject.activeInHierarchy)
                {
                    movableGameObject.transform.Translate(dir, Space.World);
                }
            }
        }
        
        protected override void TickComponent()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Owner.transform.Translate(Vector3.forward, Space.Self);
                EndTurn?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SideMove(Vector3.right);
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SideMove(Vector3.left);
            }
        }
    }
}