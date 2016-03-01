using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest.Robot.RobotStates
{
    public class ScoopingState : IRobotState
    {
        public string Name { get { return "Scooping"; } }
        public bool CanMove { get { return false; } }
        public void Enter(RobotEntity robotEntity, GameTime gameTime) { } 
        public void Exit(RobotEntity robotEntity) { } 

        public IRobotState Update(RobotEntity robotEntity, GameTime gameTime, KeyboardState keyboardState)
        {
            if (robotEntity.ForeArm.RotationState != RangeState.AtMin)
            {
                robotEntity.ForeArm.StepRotation(-RobotEntity.ThrowingSpeed);
            }
            else
            {
                return new LoadedState();
            }
            return null;
        }
    }
}