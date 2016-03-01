using System;
using System.Diagnostics;
using CompositeSpriteTest.Robot.RobotComponents;
using CompositeSpriteTest.Robot.RobotStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest.Robot
{
    public class RobotEntity : IGameEntity
    {
        private readonly CompositeSprite _sprite;
        private readonly RobotWheelsComponent _wheels;
        private IRobotState _state = new EmptyState();
        public readonly RobotArmComponent HindArm;
        public readonly RobotArmComponent ForeArm;
        public Vector2 Position;

        private const float OneDegree = (float) Math.PI/180;
        public const float ThrowingSpeed = 5 * OneDegree;
        public const int RollingSpeed = 4;

        public RobotEntity(CompositeSprite sprite, RobotArmComponent hindArm, RobotArmComponent foreArm, RobotWheelsComponent wheels)
        {
            _sprite = sprite;
            _wheels = wheels;
            HindArm = hindArm;
            ForeArm = foreArm;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            if (_state.CanMove)
            {
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    _sprite.RelativePosition.X -= RollingSpeed;
                    _wheels.RotateDistance(-RollingSpeed);
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    _sprite.RelativePosition.X += RollingSpeed;
                    _wheels.RotateDistance(RollingSpeed);
                }
            }

            var maybeNewState = _state.Update(this, gameTime, keyboardState);
            if (maybeNewState == null)
            {
                return;
            }
            _state.Exit(this);
            maybeNewState.Enter(this, gameTime);
            Debug.WriteLine("Transitioning from {0} to {1}", _state.Name, maybeNewState.Name);
            _state = maybeNewState;
        }
    }
}