using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest.Robot.RobotStates
{
    public class FireRecoveryState : IRobotState
    {
        public string Name { get { return "Fire Recovery"; } }
        public bool CanMove { get { return false; } }
        public void Enter(RobotEntity robotEntity, GameTime gameTime) { }
        public void Exit(RobotEntity robotEntity) { }
        public IRobotState Update(RobotEntity robotEntity, GameTime gameTime, KeyboardState keyboardState)
        {
            if (robotEntity.HindArm.RotationState != RangeState.AtMax)
            {
                robotEntity.HindArm.StepRotation(RobotEntity.ThrowingSpeed/8);
                return null;
            }
            if (robotEntity.ForeArm.RotationState != RangeState.AtMax)
            {
                robotEntity.ForeArm.StepRotation(RobotEntity.ThrowingSpeed);
                return null;
            }
            return new EmptyState();
        }
    }
}