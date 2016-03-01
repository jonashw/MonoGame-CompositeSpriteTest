using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly List<IGameEntity> _entities = new List<IGameEntity>();
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 720,
                PreferredBackBufferWidth = 1080
            };
            Content.RootDirectory = "Content";
        }

        private CompositeSprite _selectedSprite;
        private IEnumerable<CompositeSprite> _selectableSprites;
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var robot = RobotFactory.Create(Content,new Vector2(573,591));
            _font = Content.Load<SpriteFont>("Consolas");
            _selectedSprite = robot.EntireSprite;
            _selectableSprites = new[]
            {
                robot.ForeArm,
                robot.HindArm,
                robot.EntireSprite,
                robot.Eye
            }.Concat(robot.WheelSprites);

            _entities.Add(
                new SpriteManipulator(
                    _font,
                    new CircularArray<CompositeSprite>(_selectableSprites.ToArray())));

            _entities.Add(robot);

            var groundTexture = RectangleTextureFactory.Create(
                GraphicsDevice,
                GraphicsDevice.Viewport.Width,
                20,
                new Color(255, 110, 64));

            _entities.Add(
                new Ground(
                    groundTexture,
                    new Vector2(0, GraphicsDevice.Viewport.Height - groundTexture.Height)));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            var keyboardState = Keyboard.GetState();
            KeyboardEventRegistry.Update(keyboardState);

            foreach (var entity in _entities)
            {
                entity.Update(gameTime, keyboardState);
            }

            base.Update(gameTime);
        }

        private readonly Color _bg = new Color(255,255,141);

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_bg);
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (var entity in _entities)
            {
                entity.Draw(gameTime, _spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}