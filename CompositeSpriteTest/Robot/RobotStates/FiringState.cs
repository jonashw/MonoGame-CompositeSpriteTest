using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest.Robot.RobotStates
{
    public class FiringState : IRobotState
    {
        public string Name { get { return "Firing"; } }
        public bool CanMove { get { return false; } }
        public void Enter(RobotEntity robotEntity, GameTime gameTime) { }
        public void Exit(RobotEntity robotEntity) { }
        public IRobotState Update(RobotEntity robotEntity, GameTime gameTime, KeyboardState keyboardState)
        {
            if(robotEntity.HindArm.RotationState != RangeState.AtMin)
            {
                robotEntity.HindArm.StepRotation(-2 * RobotEntity.ThrowingSpeed);
            }
            else
            {
                return new FiredState();
            }
            return null;
        }
    }
}