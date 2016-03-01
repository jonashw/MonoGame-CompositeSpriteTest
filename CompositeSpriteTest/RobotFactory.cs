using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CompositeSpriteTest
{
    public static class RobotFactory
    {
        public static Robot Create(ContentManager content, Vector2 position)
        {
            var wheelTexture = content.Load<Texture2D>("wheel");
            var eye = new CompositeSprite(content.Load<Texture2D>("eye"))
            {
                RelativePosition = new Vector2(226, 42),
                ZIndex = 1f,
                RelativeRotation = (float)(Math.PI / 4)
            };

            var wheelA = new CompositeSprite(wheelTexture)
            {
                RelativePosition = new Vector2(41, 149),
                ZIndex = 1f
            };

            var wheelB = new CompositeSprite(wheelTexture)
            {
                RelativePosition = new Vector2(227, 149),
                ZIndex = 1f
            };

            var foreArm = new CompositeSprite(content.Load<Texture2D>("arm-fore"))
            {
                RelativePosition = new Vector2(256, 14),
                Origin = new Vector2(13, 41),
                RelativeRotation = (float)(Math.PI/2),
                ZIndex = 0.1f
            };

            var hindArm = new CompositeSprite(content.Load<Texture2D>("arm-hind"), foreArm)
            {
                RelativePosition = new Vector2(41, 41),
                Origin = new Vector2(12.5f, 71),
                ZIndex = 0.1f
            };

            var body = new CompositeSprite(content.Load<Texture2D>("body"), eye, wheelA, wheelB, hindArm)
            {
                RelativePosition = position,
                ZIndex = 0.5f
            };

            return new Robot(body, hindArm, foreArm, eye, wheelA, wheelB)
            {
                Position = position
            };
        }
    }
}