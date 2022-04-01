using Base.Enums;
using UnityEngine;

namespace Base
{
    public class BaseWorldItem : BaseGameObject
    {
        [SerializeField] private int size = 1;
        [SerializeField] private Material material;
        [SerializeField] private ZoneType zoneType;

        public ZoneType ZoneType => zoneType;

        public Material Material => material;

        public int Size => size;
    }
}
