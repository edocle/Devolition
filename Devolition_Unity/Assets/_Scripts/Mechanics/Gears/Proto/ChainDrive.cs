
namespace mechanics.gears.proto
{
    public class ChainDrive : IRotationSource
    {
        private readonly IRotationSource _parent;

        public ChainDrive(IRotationSource parent)
        {
            _parent = parent;
        }

        public int Teeth => _parent.Teeth;

        public RotationDirection GetDirection()
        {
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