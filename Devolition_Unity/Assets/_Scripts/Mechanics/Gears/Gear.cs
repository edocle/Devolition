using System;
using System.Collections.Generic;

namespace mechanics.gears
{
    public class Gear
    {
        float _rotation = 0f;
        float Rotation { get { return _rotation; } set {  _rotation = value; OnRotationUpdated?.Invoke(); } }

        public Action OnRotationUpdated;

        float _scale = 1f;
        public float Scale { get { return _scale; } }

        bool _turnClockWise = false;
        public bool TurnClockWise { get { return _turnClockWise; } }

        public Gear(float scale, bool turnClockWise = false)
        {
            _scale = scale;
            _turnClockWise = turnClockWise;
        }

        public void Rotate(float rotation)
        {
            if (!_turnClockWise)
                rotation = -rotation;

            Rotation += rotation;
            UpdateLinkedGears(rotation);
        }

        List<Gear> _linkedGears = null;

        public void AddGear(Gear gear)
        {
            if (_linkedGears == null)
            {
                _linkedGears = new List<Gear>();
            }
            _linkedGears.Add(gear);
        }

        void UpdateLinkedGears(float rotation)
        {
            if (_linkedGears == null || _linkedGears.Count == 0)
                return;

            foreach (var gear in _linkedGears)
            {
                float linkedRotation = rotation * (_scale / gear.Scale);
                gear.Rotate(linkedRotation);
            }
        }
    }
}