using Microsoft.Xna.Framework;
using System;

namespace GridChase {
    class Player : Entity, Character {
        public Player(Game game) : base(game) {

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
