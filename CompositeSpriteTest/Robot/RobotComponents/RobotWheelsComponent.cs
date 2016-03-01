using System;

namespace CompositeSpriteTest.Robot.RobotComponents
{
    public class RobotWheelsComponent
    {
        private readonly float _circumference;
        private readonly CompositeSprite[] _sprites;

        public RobotWheelsComponent(CompositeSprite spriteA, CompositeSprite spriteB)
        {
            _sprites = new[]
            {
                spriteA,
                spriteB
            };
            _circumference = (float) (spriteA.Texture.Width*Math.PI);
        }

        public void RotateDistance(int linearDistance)
        {
            var dRotation = (float) ((linearDistance/_circumference) * 2 * Math.PI);
            foreach (var wheel in _sprites)
            {
                wheel.RelativeRotation += dRotation;
            }
        }
    }
}