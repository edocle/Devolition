using UnityEngine;

namespace mechanics.gears
{
    public class GearView : MonoBehaviour
    {
        private Transform _transform;

        Gear _gear;

        public GearView(Gear gear)
        {
            _gear = gear;
        }

        void Init()
        {
            _transform = transform;
        }
    }
}
