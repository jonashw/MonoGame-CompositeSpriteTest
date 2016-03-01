namespace CompositeSpriteTest.Robot.RobotComponents
{
    public class RobotArmComponent
    {
        public readonly CompositeSprite ArmSprite;
        private readonly CompositeSprite _eyeSprite;
        private readonly float _minRotation;
        private readonly float _maxRotation;

        public RobotArmComponent(CompositeSprite armSprite, CompositeSprite eyeSprite, float minRotation, float maxRotation)
        {
            ArmSprite = armSprite;
            _eyeSprite = eyeSprite;
            _minRotation = minRotation;
            _maxRotation = maxRotation;
        }

        public RangeState RotationState
        {
            get
            {
                // ReSharper disable CompareOfFloatsByEqualityOperator
                return ArmSprite.RelativeRotation == _minRotation
                    ? RangeState.AtMin
                    : ArmSprite.RelativeRotation == _maxRotation
                        ? RangeState.AtMax
                        : RangeState.BetweenMinAndMax;
                // ReSharper restore CompareOfFloatsByEqualityOperator
            }
        }

        public void StepRotation(float dRotation)
        {
            var oldRotation = ArmSprite.RelativeRotation;
            var newRotation = MathUtil.Clamp(
                _minRotation,
                oldRotation + dRotation,
                _maxRotation);

            ArmSprite.RelativeRotation = newRotation;
            //Keep eye looking at the moving arm.
            _eyeSprite.RelativeRotation += (newRotation - oldRotation);
        }
    }
}