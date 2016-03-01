using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest
{
    public class Ground : IGameEntity
    {
        private readonly Texture2D _texture;
        private readonly Vector2 _position;

        public Ground(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, null, null, 0f, null, null, SpriteEffects.None, 0);
        }
    }
}