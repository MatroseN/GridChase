﻿using Microsoft.Xna.Framework;
using System;

namespace GridChase {
    class Enemy : Entity, Character{
        public Enemy(Game1 game) : base(game) {

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