using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public abstract class BaseGameMovableGameObject : BaseGameObject
    {
        public float SpeedFactor { get; set; } = 1;
    }
}
