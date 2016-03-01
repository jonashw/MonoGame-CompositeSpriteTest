using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CompositeSpriteTest
{
    public static class RectangleTextureFactory
    {
        public static Texture2D Create(GraphicsDevice graphicsDevice, int width, int height, Color color)
        {
            var rect = new Texture2D(graphicsDevice, width, height);
            var data = new Color[width * height];
            for (var i = 0; i < data.Length; ++i)
            {
                data[i] = color;
            }
            rect.SetData(data);
            return rect;
        }
    }
}