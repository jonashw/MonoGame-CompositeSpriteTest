using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CompositeSpriteTest
{
    public class CompositeSprite
    {
        public readonly Texture2D Texture;
        public readonly IEnumerable<CompositeSprite> Children;
        public Vector2 RelativePosition;
        public float RelativeRotation;
        public float RelativeScale = 1;
        public Vector2 Origin;
        public float ZIndex;

        public CompositeSprite(Texture2D texture, params CompositeSprite[] children)
        {
            Texture = texture;
            Children = children;
            Origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            draw(spriteBatch, Matrix.Identity);
        }

        private void draw(SpriteBatch spriteBatch, Matrix parentTransform)
        {
            var globalTransform = calculateLocalTransform() * parentTransform;
            Vector2 position;
            float rotation;
            Vector2 scale;

            decomposeMatrix(ref globalTransform, out position, out rotation, out scale);
            spriteBatch.Draw(Texture, position, null, Color.White, rotation, Vector2.Zero, scale, SpriteEffects.None, ZIndex);
            foreach (var child in Children)
            {
                child.draw(spriteBatch, globalTransform);
            }
        }

        private static void decomposeMatrix(ref Matrix matrix, out Vector2 position, out float rotation, out Vector2 scale)
        {
            Vector3 position3, scale3;
            Quaternion rotationQ;
            matrix.Decompose(out scale3, out rotationQ, out position3);
            var direction = Vector2.Transform(Vector2.UnitX, rotationQ);
            rotation = (float) Math.Atan2(direction.Y, direction.X);
            position = new Vector2(position3.X, position3.Y);
            scale = new Vector2(scale3.X, scale3.Y);
        }

        private Matrix calculateLocalTransform()
        {
            return Matrix.CreateTranslation(-Origin.X, -Origin.Y, 0f) 
                * Matrix.CreateScale(RelativeScale, RelativeScale, 1f) 
                * Matrix.CreateRotationZ(RelativeRotation) 
                * Matrix.CreateTranslation(RelativePosition.X, RelativePosition.Y, 0f);
        }
    }
}
