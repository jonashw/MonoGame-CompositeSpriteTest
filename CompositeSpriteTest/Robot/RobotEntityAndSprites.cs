using System.Collections.Generic;

namespace CompositeSpriteTest.Robot
{
    public class RobotEntityAndSprites
    {
        public readonly RobotEntity Entity;
        public readonly IEnumerable<CompositeSprite> Sprites;

        public RobotEntityAndSprites(RobotEntity entity, IEnumerable<CompositeSprite> sprites)
        {
            Entity = entity;
            Sprites = sprites;
        }
    }
}