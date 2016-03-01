using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest
{
    public interface IGameEntity
    {
        void Update(GameTime gameTime, KeyboardState keyboardState);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}