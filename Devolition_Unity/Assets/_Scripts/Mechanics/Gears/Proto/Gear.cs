
namespace mechanics.gears.proto
{
    public enum RotationDirection
    {
        Clockwise,
        CounterClockwise
    }

    public interface IRotationSource
    {
        int Teeth { get; }
        double GetRPM();
        RotationDirection GetDirection();
    }

    public class Gear : IRotationSource
    {
        private readonly int _teethCount;
        private IRotationSource _parent;

        public Gear(int teethCount, IRotationSource parent = null)
        {
            _teethCount = teethCount;
            _parent = parent;
        }

        public void SetParent(IRotationSource parent)
        {
            _parent = parent;
        }

        //*** Properties ***

        public int Teeth => _teethCount;

        public RotationDirection GetDirection()
        {
            return _parent.GetDirection() == RotationDirection.Clockwise
            ? RotationDirection.CounterClockwise
            : RotationDirection.Clockwise;
        }

        public double GetRPM()
        {
            if (_parent == null)
                return 0;

            double parentRpm = _parent.GetRPM();
            if (_parent.Teeth < 0)
                return parentRpm;

            return parentRpm * _parent.Teeth / _teethCount;
        }
    }
}