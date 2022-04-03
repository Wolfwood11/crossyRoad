using System;
using System.Collections.Generic;
using Base;
using Base.Enums;
using UnityEngine;
using Utils;
using Object = System.Object;
using Random = UnityEngine.Random;

namespace World
{
    public class MapGenerator : MonoBehaviour
    {
        private const int ActiveDangerZonesCount = 3;
        private const int StartEndZone = 7;
        private const int MaxVisibleWorld = 15;

        private int _currentSize = -StartEndZone;

        readonly ObjectTypes[] _listTypes = new[] { ObjectTypes.SafeZone, ObjectTypes.Road };
        
        private int _targetDangerZonesCount;
        private bool _lastSpawnType = true;

        public void GenerateWorld()
        {
            _currentSize = -StartEndZone;
            _lastSpawnType = true;
            _targetDangerZonesCount = ActiveDangerZonesCount + GameController.Instance.Difficulty;
            
            // safe zone at start
            while (_currentSize <= 0)
            {
                _currentSize += InstantiateWorldLine(_listTypes[0], _currentSize);
            }
            
            AddLinesToWorld();
        }

        private void AddLinesToWorld()
        {
            var isInfinite = GameController.Instance.InfinityMode;
            if (_targetDangerZonesCount <= 0 && !isInfinite) return;
            int totalSteps = 0;
            while (_currentSize < MaxVisibleWorld && (_targetDangerZonesCount > 0 || isInfinite))
            {
                var indexOfType = Convert.ToInt32(_lastSpawnType);
                _targetDangerZonesCount -= indexOfType;
                var addSize = InstantiateWorldLine(_listTypes[indexOfType], _currentSize);
                _currentSize += addSize;
                _lastSpawnType = !_lastSpawnType;
                totalSteps += addSize;
            }
            
            var currentSteps = GameController.Instance.ForwardStepsToWin >= 0
                ? GameController.Instance.ForwardStepsToWin
                : 0;
            var newStepsToWin = currentSteps + totalSteps;
            GameController.Instance.ForwardStepsToWin = newStepsToWin;

            var startSizeOf = _currentSize;
            if (_targetDangerZonesCount <= 0 && !isInfinite)
            {
                while (_currentSize < startSizeOf + StartEndZone)
                {
                    _currentSize += InstantiateWorldLine(_listTypes[0], _currentSize);
                }
                GameController.Instance.ForwardStepsToWin++;
            }
        }

        private static int InstantiateWorldLine(ObjectTypes type, int currentSize)
        {
            var position = new Vector3(0, 0, currentSize);
            var rotation = Quaternion.identity;
            
            var obj = GameController.Instance.InstantiateObjectOfType(type, position, rotation);
            
            var baseWorldItem = obj.GetComponent<BaseWorldItem>();
            if (baseWorldItem)
            {
                baseWorldItem.EmitObjectsOfLine();
                return baseWorldItem.Size;
            }
            return 0;
        }

        public void StepForward()
        {
            _currentSize--;
            AddLinesToWorld();
        }
    }
}
