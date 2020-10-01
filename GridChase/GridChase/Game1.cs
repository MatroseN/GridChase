using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

            _playerBlock = new Block(new Vector2(32, 32));
            _enemyBlock = new Block(new Vector2(32, 32));
            _visionBlock = new Block(new Vector2(32, 32));
            

            foreach (Entity entity in _entities) {
                entity.calculatePosition(_windowSize, _block.size);
            }

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _block.createTexture(GraphicsDevice, pixel => Color.DarkSlateGray);
            _playerBlock.createTexture(GraphicsDevice, pixel => Color.Green);
            _enemyBlock.createTexture(GraphicsDevice, pixel => Color.Red);
            _visionBlock.createTexture(GraphicsDevice, pixel => Color.Yellow);
        }

        private List<Enemy> getEnemies() {
            List<Enemy> enemies = _entities.OfType<Enemy>().ToList();
            return enemies;
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (Entity entity in _entities) {

                if (entity.Position.X >= _windowSize.X) {
                    entity.Position = new Vector2(_windowSize.X - _block.size.X, entity.Position.Y);
                    if(entity.Tag == Tag.enemy) {
                        entity.Direction = Direction.left;
                    }
                }else if (entity.Position.X < 0) {
                    entity.Position = new Vector2(0, entity.Position.Y);
                    if (entity.Tag == Tag.enemy) {
                        entity.Direction = Direction.right;
                    }
                }

                if (entity.Position.Y >= _windowSize.Y - _block.size.X) {
                    entity.Position = new Vector2(entity.Position.X, _windowSize.Y - _block.size.Y);
                    if (entity.Tag == Tag.enemy) {
                        entity.Direction = Direction.up;
                    }
                } else if (entity.Position.Y < 0) {
                    entity.Position = new Vector2(entity.Position.X, 0);
                    if (entity.Tag == Tag.enemy) {
                        entity.Direction = Direction.down;
                    }
                }
                entity.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            // TODO: Add your drawing code here
            foreach (Vector2 pos in _grid) {
                _spriteBatch.Draw(_block.texture, pos, Color.White);
            }

            foreach (Enemy enemy in getEnemies()) {
                foreach (Vector2 pos in enemy.Vision) {
                    _spriteBatch.Draw(_visionBlock.texture, pos, Color.White * 0.5f);
                }
            }

            foreach (Entity entity in _entities) {               

                switch (entity.Tag) {
                    case Tag.player:
                        _spriteBatch.Draw(_playerBlock.texture, entity.Position, Color.White);
                        break;
                    case Tag.enemy:
                        _spriteBatch.Draw(_enemyBlock.texture, entity.Position, Color.White);
                        break;
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Block _block;
        private Block _playerBlock;
        private Block _enemyBlock;
        private Block _visionBlock;
    }
}