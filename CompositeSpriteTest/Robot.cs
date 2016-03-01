using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest
{
    public class Robot : IGameEntity
    {
        public Vector2 Position;
        public readonly CompositeSprite EntireSprite;
        public readonly CompositeSprite HindArm;
        public readonly CompositeSprite ForeArm;
        public readonly CompositeSprite Eye;
        public readonly CompositeSprite WheelASprite;
        public readonly CompositeSprite WheelBSprite;
        public readonly IEnumerable<CompositeSprite> WheelSprites;
        private readonly float _wheelCircumference;

        public Robot(CompositeSprite entireSprite, CompositeSprite hindArm, CompositeSprite foreArm, CompositeSprite eye, CompositeSprite wheelASprite, CompositeSprite wheelBSprite)
        {
            EntireSprite = entireSprite;
            HindArm = hindArm;
            ForeArm = foreArm;
            Eye = eye;
            WheelASprite = wheelASprite;
            WheelBSprite = wheelBSprite;
            WheelSprites = new[] {wheelASprite, wheelBSprite};
            _wheelCircumference = (float) (wheelASprite.Texture.Width*Math.PI);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            EntireSprite.Draw(spriteBatch);
        }

        private const float OneDegree = (float) Math.PI/180;
        private const int RollingSpeed = 4;
        private const float ThrowingSpeed = 5 * OneDegree;
        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                EntireSprite.RelativePosition.X -= RollingSpeed;
                rotateWheels(-RollingSpeed);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                EntireSprite.RelativePosition.X += RollingSpeed;
                rotateWheels(RollingSpeed);
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (ForeArm.RelativeRotation > 0)
                {
                    ForeArm.RelativeRotation -= ThrowingSpeed;
                    Eye.RelativeRotation -= ThrowingSpeed;
                }
                else if(HindArm.RelativeRotation > -Math.PI/2)
                {
                    HindArm.RelativeRotation -= ThrowingSpeed;
                    Eye.RelativeRotation -= ThrowingSpeed;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (HindArm.RelativeRotation < 0)
                {
                    HindArm.RelativeRotation += ThrowingSpeed;
                    Eye.RelativeRotation += ThrowingSpeed;
                }
                else if (ForeArm.RelativeRotation < Math.PI/2)
                {
                    ForeArm.RelativeRotation += ThrowingSpeed;
                    Eye.RelativeRotation += ThrowingSpeed;
                }
            }

            ForeArm.RelativeRotation = clamp(
                0f,
                ForeArm.RelativeRotation,
                (float) Math.PI/2);

            HindArm.RelativeRotation = clamp(
                (float) -Math.PI/2,
                HindArm.RelativeRotation,
                0);
        }

        private void rotateWheels(int linearDistance)
        {
            var dRotation = (float) ((linearDistance/_wheelCircumference) * 2 * Math.PI);
            foreach (var wheel in WheelSprites)
            {
                wheel.RelativeRotation += dRotation;
            }
        }

        /// <summary>
        /// Clamps a value between two extremes.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="current"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static float clamp(float min, float current, float max)
        {
            return Math.Min(max, Math.Max(min, current));
        }
    }
}