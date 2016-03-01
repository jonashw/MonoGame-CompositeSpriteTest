using System;
using CompositeSpriteTest.Robot.RobotComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CompositeSpriteTest.Robot
{
    public static class RobotFactory
    {
        public static RobotEntityAndSprites Create(ContentManager content, Vector2 position)
        {
            /* This method wires all the Robot's components together.  
             * The individual sprites are composed in a parent/child hierarchy to allow group transformations.
             * The setup here is the sort of thing that one could do at design time with Spine or Spriter. 
             *
             * The Robot's sprites are not publicly reachable through Robot itself, since they are implementation details.
             * To allow debugging and run-time tweaking of the sprite configurations,
             * we make the Sprites available separately, at the time of Robot creation.  */
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

            var robot = new RobotEntity(
                body,
                new RobotArmComponent(hindArm, eye, -(float) Math.PI/2, 0),
                new RobotArmComponent(foreArm, eye, 0, (float) Math.PI/2),
                new RobotWheelsComponent(wheelA, wheelB))
            {
                Position = position
            };

            return new RobotEntityAndSprites(
                robot,
                new[] {body, hindArm, foreArm, wheelA, wheelB, eye});
        }
    }
}