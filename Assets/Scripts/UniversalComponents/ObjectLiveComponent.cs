using Base;

namespace UniversalComponents
{
    public class ObjectLiveComponent : BaseComponent
    {
        private const float MinZLocation = -10;

        protected override void TickComponent()
        {
            base.TickComponent();
            var z = Owner.transform.position.z;
            if (z < MinZLocation)
            {
                Owner.Deactivate();
            }
        }
    }
}