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
                this.Position = new Vector2(this.Position.X - 32, this.Position.Y);
            }

            if ((currentKeyboardState.IsKeyDown(Keys.D) && previousKeyboardState.IsKeyUp(Keys.D)) || (currentKeyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Right))){
                this.Position = new Vector2(this.Position.X + 32, this.Position.Y);
            }

            if ((currentKeyboardState.IsKeyDown(Keys.W) && previousKeyboardState.IsKeyUp(Keys.W)) || (currentKeyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))) {
                this.Position = new Vector2(this.Position.X, this.Position.Y - 32);
            }

            if ((currentKeyboardState.IsKeyDown(Keys.S) && previousKeyboardState.IsKeyUp(Keys.S)) || (currentKeyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down))) {
                this.Position = new Vector2(this.Position.X, this.Position.Y + 32);
            }

            previousKeyboardState = currentKeyboardState;
        }
        #endregion

        #region Player methods
        // Initializes the XML positional value to a window dependent positional value
        #endregion

        // Current and previous keyboard and mouse states
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        private MouseState currentMouseState;
        private MouseState previousMouseState;
    }
}