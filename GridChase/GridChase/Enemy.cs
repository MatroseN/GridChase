using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GridChase {
    /*
     * Enemies to the PLAYABLE characters
     */
    class Enemy : Entity, Character{
        public Enemy(Game game, Vector2 position) : base(game) {
            this.Position = position;
            this.Health = 1.0f;
            this.Tag = Tag.enemy;
            this.Vision = new Vector2[6];
            // TODO: This should be specified in the map XML data and not here
            Direction = Direction.right;
        }

        #region Monogame Pipeline
        public override void Initialize() {
            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            calculateVision();
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

        #region Enemy methods
        private void calculateVision() {
            switch (Direction) {
                case Direction.right:
                    Vector2 pos = this.Position;
                    int adder = 32;
                    for (int i = 0; i < Vision.Length; i++) {
                        Vision[i] = new Vector2(pos.X + adder, pos.Y);
                        adder += 32;
                    }
                    break;
                case Direction.left:
                    break;
                case Direction.up:
                    break;
                case Direction.down:
                    break;
            }
        }
        #endregion

        public Vector2[] Vision { get; set; }
        public Direction Direction { get; set; }
    }
}