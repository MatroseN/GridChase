using Microsoft.Xna.Framework;
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
            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }
        #endregion

        #region Character methods
        public void die() {
            throw new NotImplementedException();
        }

        public void move() {
            throw new NotImplementedException();
        }
        #endregion
    }
}