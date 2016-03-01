using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest.Robot
{
    public interface IRobotState
    {
        IRobotState Update(RobotEntity robotEntity, GameTime gameTime, KeyboardState keyboardState);
        void Exit(RobotEntity robotEntity);
        void Enter(RobotEntity robotEntity, GameTime gameTime);
        string Name { get; }
        bool CanMove { get; }
    }
}