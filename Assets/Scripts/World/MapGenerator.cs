using System;
using System.Collections.Generic;
using Base;
using Base.Enums;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace World
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject[] worldItems;

        private const int DangerZonesCount = 3;
        private const int StartEndZone = 7;
        private void Awake()
        {
            GenerateWorld();
        }

        private void GenerateWorld()
        {
            List<GameObject> safeZones = new List<GameObject>();
            List<GameObject> dangerZones = new List<GameObject>();

            foreach (var item in worldItems)
            {
                var baseWorldItem = item.GetComponent<BaseWorldItem>();
                var zoneType = baseWorldItem.ZoneType;
                if (zoneType == ZoneType.SafeZone)
                {
                    safeZones.Add(item);
                }
                else
                {
                    dangerZones.Add(item);
                }
            }
            
            var currentSize = -StartEndZone;
            bool type = true;
            
            var lists = new[] { safeZones, dangerZones };

            // safe zone at start
            while (currentSize <= 0)
            {
                currentSize = InstantiateWorldLine(lists[0], currentSize);
            }

            //play field
            var currentDangerZonesCount = 0;
            var targetDangerZonesCount = DangerZonesCount + GameController.Instance.Difficulty;
            while (currentDangerZonesCount < targetDangerZonesCount)
            {
                var indexOfType = Convert.ToInt32(type);
                currentDangerZonesCount += indexOfType;
                currentSize = InstantiateWorldLine(lists[indexOfType], currentSize);
                type = !type;
            }

            GameController.Instance.TargetToWin = currentSize;
            
            // safe zone at and
            while (currentSize < GameController.Instance.TargetToWin + StartEndZone)
            {
                currentSize = InstantiateWorldLine(lists[0], currentSize);
            }
        }

        private static int InstantiateWorldLine(List<GameObject> list, int currentSize)
        {
            var obj = Instantiate(list.RandomElement(), new Vector3(0, 0, currentSize), Quaternion.identity);
            var baseWorldItem = obj.GetComponent<BaseWorldItem>();
            currentSize += baseWorldItem.Size;
            return currentSize;
        }
    }
}
