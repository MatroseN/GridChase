using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GridChase {
    /*
     * PLAYABLE character
     */
    class Player : Entity, Character {
        public Player(Game game, Vector2 position) : base(game) {
            this.Position = position;
            this.Health = 1.0f;
            this.Tag = Tag.player;
        }

        #region Monogame Pipeline
        public override void Initialize() {
            previousKeyboardState = Keyboard.GetState();
            currentKeyboardState = Keyboard.GetState();
            previousMouseState = Mouse.GetState();
            currentMouseState = Mouse.GetState();

            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            move();
            if (Weapon != null) {
                attack();
            }
            base.Update(gameTime);
        }
        #endregion

        #region Character methods
        public void die() {
            throw new NotImplementedException();
        }

        public void move() {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();

            if ((currentKeyboardState.IsKeyDown(Keys.A) && previousKeyboardState.IsKeyUp(Keys.A)) || (currentKeyboardState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyUp(Keys.Left))) {
                Direction = Direction.left;
                if (Node.Edges.ContainsKey(Direction)) {
                    Node = Node.Edges[Direction];
                    this.Position = Node.Position;
                }
            }

            if ((currentKeyboardState.IsKeyDown(Keys.D) && previousKeyboardState.IsKeyUp(Keys.D)) || (currentKeyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Right))){
                Direction = Direction.right;
                if (Node.Edges.ContainsKey(Direction)) {
                    Node = Node.Edges[Direction];
                    this.Position = Node.Position;
                }
            }

            if ((currentKeyboardState.IsKeyDown(Keys.W) && previousKeyboardState.IsKeyUp(Keys.W)) || (currentKeyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))) {
                Direction = Direction.up;
                if (Node.Edges.ContainsKey(Direction)) {
                    Node = Node.Edges[Direction];
                    this.Position = Node.Position;
                }
            }

            if ((currentKeyboardState.IsKeyDown(Keys.S) && previousKeyboardState.IsKeyUp(Keys.S)) || (currentKeyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down))) {
                Direction = Direction.down;
                if (Node.Edges.ContainsKey(Direction)) {
                    Node = Node.Edges[Direction];
                    this.Position = Node.Position;
                }
            }

            previousKeyboardState = currentKeyboardState;
        }
        #endregion

        #region Player methods

        public void attack() {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Space)) {
                Weapon.attack(Position, Direction);
                ISAttack = true;
            } else {
                Weapon.HitBox = new Vector2[0];
                ISAttack = false;
            }
            previousKeyboardState = currentKeyboardState;
        }

        private void lose() {

        }
        #endregion

        public bool ISAttack { get; set; }

        // Current and previous keyboard and mouse states
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        private MouseState currentMouseState;
        private MouseState previousMouseState;
    }
}