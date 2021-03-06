﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest.Robot.RobotStates
{
    public class LoadedState : IRobotState
    {
        public string Name { get { return "Loaded"; } }
        public bool CanMove { get { return true; } }
        public void Enter(RobotEntity robotEntity, GameTime gameTime) { }
        public void Exit(RobotEntity robotEntity) { }
        public IRobotState Update(RobotEntity robotEntity, GameTime gameTime, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                return new FiringState();
            }
            return null;
        }
    }
}