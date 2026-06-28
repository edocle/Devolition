
namespace mechanics.gears.proto
{
    public class Motor : IRotationSource
    {
        private double _rpm;
        private RotationDirection _direction;

        public int Teeth => -1;

        public Motor(double rpm, RotationDirection direction)
        {
            _rpm = rpm;
            _direction = direction;
        }

        public double GetRPM() => _rpm;
        public RotationDirection GetDirection() => _direction;

        public void SetRPM(double rpm) => _rpm = rpm;
        public void SetDirection(RotationDirection direction) => _direction = direction;
    }
}