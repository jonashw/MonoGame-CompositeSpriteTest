using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest.Robot.RobotStates
{
    public class FiredState : IRobotState
    {
        public string Name { get { return "Fired"; } }
        public bool CanMove { get { return false; } }
        private readonly EasyTimer _timer = new EasyTimer(TimeSpan.FromMilliseconds(2000));
        public void Enter(RobotEntity robotEntity, GameTime gameTime)
        {
            _timer.Start(gameTime);
        }
        public void Exit(RobotEntity robotEntity) { }
        public IRobotState Update(RobotEntity robotEntity, GameTime gameTime, KeyboardState keyboardState)
        {
            return _timer.IsFinished(gameTime)
                ? new FireRecoveryState()
                : null;
        }
    }
}
