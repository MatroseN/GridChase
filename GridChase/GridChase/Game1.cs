using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace GridChase {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _windowSize;
        private MapGenerator _mapGenerator;
        private List<Entity> _entities;
        private List<Vector2> _barriers;
        private List<Weapon> _weapons;
        private Vector2[] _grid;
        private Graph _graph;
        private BFS _BFS;
        private bool _isFinnished;
        private List<Node> _testShortestPath;
        private Dictionary<Node, Node> _allPaths;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            _mapGenerator = new MapGenerator(this);
            _entities = new List<Entity>();
            _barriers = new List<Vector2>();
            _weapons = new List<Weapon>();
            _graph = new Graph();
            _graph.Adjacent = new Dictionary<Vector2, Node>();
            _BFS = new BFS(_graph);
            _isFinnished = false;
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
            _mapGenerator.generateMap(_entities,"/Side Projects/GridChase/GridChase/GridChase/Maps/Test/0", blockSize, _barriers, _windowSize, _weapons);
            _grid = _mapGenerator.Grid;
            _block = new Block(new Vector2(32, 32));

            for (int i = 0; i < _grid.Length; i++) {
                if (!checkBarriers(_grid[i])) {
                    _graph.Adjacent.Add(_grid[i], new Node(_grid[i], i));
                }
            }

            _graph.addEdges();

            _testShortestPath = _BFS.shortestPath(_graph.Adjacent[new Vector2(5 * 32, 2 * 32)], _graph.Adjacent[new Vector2(12 * 32, 11 * 32)]);

            _playerBlock = new Block(new Vector2(32, 32));
            _enemyBlock = new Block(new Vector2(32, 32));
            _visionBlock = new Block(new Vector2(32, 32));
            _barrierBlock = new Block(new Vector2(32, 32));
            _testShortestPathBlock = new Block(new Vector2(32, 32));
            _finnishBlock = new Block(new Vector2(32, 32));
            _batonBlock = new Block(new Vector2(32, 32));

            foreach (Entity entity in _entities) {
                entity.calculatePosition(_windowSize, _block.size);
            }

            foreach (Weapon weapon in _weapons) {
                weapon.calculatePosition(_windowSize, _block.size);
            }


            foreach (Player player in getPlayer()) {
                player.setNode(_graph);
            }

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _block.createTexture(GraphicsDevice, pixel => Color.DarkSlateGray);
            _playerBlock.createTexture(GraphicsDevice, pixel => Color.Blue);
            _enemyBlock.createTexture(GraphicsDevice, pixel => Color.Red);
            _visionBlock.createTexture(GraphicsDevice, pixel => Color.Yellow);
            _testShortestPathBlock.createTexture(GraphicsDevice, pixel => Color.Blue);
            _barrierBlock.createTexture(GraphicsDevice, pixel => Color.Black);
            _finnishBlock.createTexture(GraphicsDevice, pixel => Color.Green);
            _batonBlock.createTexture(GraphicsDevice, pixel => Color.LightGray);
        }

        private bool checkBarriers(Vector2 position) {
            foreach (Vector2 pos in _barriers) {
                if(pos == position) {
                    return true;
                }
            }
            return false;
        }

        private List<Enemy> getEnemies() {
            List<Enemy> enemies = _entities.OfType<Enemy>().ToList();
            return enemies;
        }

        private List<Player> getPlayer() {
            List<Player> players = _entities.OfType<Player>().ToList();
            return players;
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Vector2 playerPos = new Vector2(_windowSize.X - 32, _windowSize.Y - 32);

            foreach (Entity entity in _entities) {
                if (entity.Position.X >= _windowSize.X) {
                    if (entity.Tag == Tag.enemy) {
                        entity.Position = new Vector2(_windowSize.X - _block.size.X, entity.Position.Y);
                        entity.Direction = Direction.left;
                    }
                }else if (entity.Position.X < 0) {
                    if (entity.Tag == Tag.enemy) {
                        entity.Position = new Vector2(0, entity.Position.Y);
                        entity.Direction = Direction.right;
                    }
                }

                if (entity.Position.Y >= _windowSize.Y - _block.size.X) {
                    if (entity.Tag == Tag.enemy) {
                        entity.Position = new Vector2(entity.Position.X, _windowSize.Y - _block.size.Y);
                        entity.Direction = Direction.up;
                    }
                } else if (entity.Position.Y < 0) {
                    if (entity.Tag == Tag.enemy) {
                        entity.Position = new Vector2(entity.Position.X, 0);
                        entity.Direction = Direction.down;
                    }
                }

                if (entity.isGuided && entity.Tag == Tag.enemy) {
                    if (entity.Position != playerPos) {
                        _allPaths = _BFS.allPaths(_graph.Adjacent[playerPos]);
                        entity.guidedMovement(_graph, _allPaths);
                    }
                }

                if (entity.Tag == Tag.player) {
                    playerPos = entity.Position;
                }

                if (entity.HasKey && entity.Tag == Tag.player && playerPos == _mapGenerator.FinnishPosition) {
                    _isFinnished = true;
                }

                entity.Update(gameTime);
            }


            foreach (Enemy enemy in getEnemies()) {
                if (!enemy.isGuided) {
                    foreach (Vector2 pos in enemy.Vision) {
                        if (pos == playerPos) {
                            enemy.isGuided = true;
                            enemy.TickDelay = new Delay(200.0);
                            enemy.Vision = new Vector2[0];
                        }
                    }
                }
            }

            foreach (Weapon weapon in _weapons) {
                if (!weapon.Pickuped && weapon.Position == playerPos) {
                    weapon.Pickuped = true;
                    getPlayer()[0].Weapon = weapon;
                }
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

            foreach (Weapon weapon in _weapons) {
                if (!weapon.Pickuped) {
                    if (weapon.Tag == Tag.baton) {
                        _spriteBatch.Draw(_batonBlock.texture, weapon.Position, Color.White);
                    }
                }
            }

            _spriteBatch.Draw(_finnishBlock.texture, _mapGenerator.FinnishPosition, Color.White);

            foreach (Vector2 pos in _barriers) {
                _spriteBatch.Draw(_barrierBlock.texture, pos, Color.White);
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

            foreach (Enemy enemy in getEnemies()) {
                foreach (Vector2 pos in enemy.Vision) {
                    _spriteBatch.Draw(_visionBlock.texture, pos, Color.White * 0.5f);
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Block _block;
        private Block _playerBlock;
        private Block _enemyBlock;
        private Block _visionBlock;
        private Block _testShortestPathBlock;
        private Block _barrierBlock;
        private Block _finnishBlock;
        private Block _batonBlock;
    }
}