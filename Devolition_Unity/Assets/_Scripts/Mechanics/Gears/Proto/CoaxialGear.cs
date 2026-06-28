
using mechanics.gears.proto;

namespace mechanics.Gears.Proto
{
    public class CoaxialGear : IRotationSource
    {
        private readonly int _teethCount;
        private readonly IRotationSource _parent;

        public int Teeth => _teethCount;

        public CoaxialGear(int teethCount, IRotationSource parent = null)
        {
            _teethCount = teethCount;
            _parent = parent;
        }

        public RotationDirection GetDirection()
        {
            if (_parent == null)
                return RotationDirection.Clockwise;

            return _parent.GetDirection();
        }

        public double GetRPM()
        {
            if (_parent == null)
                return 0;

            return _parent.GetRPM();
        }
    }
}