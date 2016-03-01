using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest
{
    public class SpriteManipulator : IGameEntity
    {
        private readonly SpriteFont _font;
        private readonly CircularArray<CompositeSprite> _sprite;
        private readonly CircularArray<ManipulationState> _state;

        public SpriteManipulator(SpriteFont font, CircularArray<CompositeSprite> sprite)
        {
            _font = font;
            _sprite = sprite;
            _state = new CircularArray<ManipulationState>(new ManipulationState[]
            {
                new ManipulationState.Rotation(), 
                new ManipulationState.Position(),
                new ManipulationState.Origin()
            });
            KeyboardEventRegistry.OnKeyDown(Keys.N, () => _sprite.Next());
            KeyboardEventRegistry.OnKeyDown(Keys.P, () => _sprite.Prev());
            KeyboardEventRegistry.OnKeyDown(Keys.Enter, () => _state.Next());
        }

        private const float Delta = 1f;
        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.H))
            {
                _state.GetCurrent().Left(_sprite.GetCurrent());
            }
            if (keyboardState.IsKeyDown(Keys.L))
            {
                _state.GetCurrent().Right(_sprite.GetCurrent());
            }
            if (keyboardState.IsKeyDown(Keys.K))
            {
                _state.GetCurrent().Up(_sprite.GetCurrent());
            }
            if (keyboardState.IsKeyDown(Keys.J))
            {
                _state.GetCurrent().Down(_sprite.GetCurrent());
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var sprite = _sprite.GetCurrent();
            spriteBatch.DrawString(_font, "Component: " + sprite.Texture.Name + " (Press N/P to cycle)", new Vector2(5,5), Color.Black);
            spriteBatch.DrawString(_font, "Property Mode: "  + _state.GetCurrent().Name + " (H/J/K/L to manipulate; Enter to cycle)", new Vector2(5,25), Color.Black);
            spriteBatch.DrawString(_font, string.Format("Position: [{0},{1}]",sprite.RelativePosition.X, sprite.RelativePosition.Y), new Vector2(5,50), Color.Black);
            spriteBatch.DrawString(_font, string.Format("Origin:   [{0},{1}]",sprite.Origin.X, sprite.Origin.Y), new Vector2(5,75), Color.Black);
            spriteBatch.DrawString(_font, string.Format("Rotation: {0} rad ({1} deg)",sprite.RelativeRotation, 180 * sprite.RelativeRotation / Math.PI), new Vector2(5,100), Color.Black);
        }

        private abstract class ManipulationState
        {
            private ManipulationState(string name, float delta)
            {
                Name = name;
                _delta = delta;
            }

            public string Name { get; private set; }
            private readonly float _delta;

            public abstract void Up(CompositeSprite sprite);
            public abstract void Down(CompositeSprite sprite);
            public abstract void Left(CompositeSprite sprite);
            public abstract void Right(CompositeSprite sprite);

            public class Origin : ManipulationState
            {
                public Origin() : base("Origin", 1f) {}
                public override void Up   (CompositeSprite sprite) { sprite.Origin.Y -= _delta; }
                public override void Down (CompositeSprite sprite) { sprite.Origin.Y += _delta; }
                public override void Left (CompositeSprite sprite) { sprite.Origin.X -= _delta; }
                public override void Right(CompositeSprite sprite) { sprite.Origin.X += _delta; }
            }

            public class Position : ManipulationState
            {
                public Position() : base("Position", 1f) {}
                public override void Up   (CompositeSprite sprite) { sprite.RelativePosition.Y -= _delta; } 
                public override void Down (CompositeSprite sprite) { sprite.RelativePosition.Y += _delta; }
                public override void Left (CompositeSprite sprite) { sprite.RelativePosition.X -= _delta; }
                public override void Right(CompositeSprite sprite) { sprite.RelativePosition.X += _delta; }
            }

            public class Rotation : ManipulationState
            {
                public Rotation() : base("Rotation", (float)Math.PI/180f) {}
                public override void Up   (CompositeSprite sprite) { sprite.RelativeRotation -= _delta; } 
                public override void Down (CompositeSprite sprite) { sprite.RelativeRotation += _delta; }
                public override void Left (CompositeSprite sprite) { sprite.RelativeRotation -= _delta; }
                public override void Right(CompositeSprite sprite) { sprite.RelativeRotation += _delta; }
            }
        }
    }
}