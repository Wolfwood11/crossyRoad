using Base.Enums;
using UnityEngine;

namespace Base
{
    public abstract class BaseWorldItem : BaseGameObject
    {
        [SerializeField] private int size = 1;
        [SerializeField] private Material material;

        public abstract void EmitObjectsOfLine();

        public Material Material => material;

        public int Size => size;
    }
}
