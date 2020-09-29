using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace GridChase {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _windowSize;
        private MapGenerator _mapGenerator;
        private List<Entity> _entities;
        private Vector2[] _grid;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            _mapGenerator = new MapGenerator(this);
            _entities = new List<Entity>();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            _windowSize = new Vector2(800, 800);

            _graphics.PreferredBackBufferWidth = (int)_windowSize.X;
            _graphics.PreferredBackBufferHeight = (int)_windowSize.Y;
            _graphics.ApplyChanges();

            Vector2 blockSize = new Vector2(32, 32);
            _mapGenerator.generateMap(_entities,"/Side Projects/GridChase/GridChase/GridChase/Maps/Test/0", blockSize);
            _grid = _mapGenerator.Grid;
            _block = new Block(new Vector2(32, 32));

            _player = new Block(new Vector2(32, 32));
            _enemy = new Block(new Vector2(32, 32));

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _block.createTexture(GraphicsDevice, pixel => Color.DarkSlateGray);
            _player.createTexture(GraphicsDevice, pixel => Color.Green);
            _enemy.createTexture(GraphicsDevice, pixel => Color.Red);
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            foreach (Vector2 pos in _grid) {
                _spriteBatch.Draw(_block.texture, pos, Color.White);
            }

            foreach (Entity entity in _entities) {
                Vector2 pos = new Vector2(entity.Position.X * _block.size.X, entity.Position.Y * _block.size.Y);
                if (pos.X >= _windowSize.X) {
                    pos = new Vector2(_windowSize.X - _player.size.X, pos.Y);
                }
                if (pos.Y >= _windowSize.Y) {
                    pos = new Vector2(pos.X, pos.Y - _player.size.Y);
                }

                switch (entity.Tag) {
                    case Tag.player:
                        _spriteBatch.Draw(_player.texture, pos, Color.White);
                        break;
                    case Tag.enemy:
                        _spriteBatch.Draw(_enemy.texture, pos, Color.White);
                        break;
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Block _block;
        private Block _player;
        private Block _enemy;
    }
}