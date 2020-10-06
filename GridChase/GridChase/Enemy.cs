using Microsoft.Xna.Framework;
using System;

namespace GridChase {
    /*
     * Enemies to the PLAYABLE characters
     */
    class Enemy : Entity, Character {
        public Enemy(Game game, Vector2 position, Direction direction, bool hasKey) : base(game) {
            this.Position = position;
            this.Health = 1.0f;
            visionLength = 15;
            this.Tag = Tag.enemy;
            this.Vision = new Vector2[visionLength];
            this.IsTick = false;
            this.HasKey = hasKey;
            TickDelay = new Delay(300.0);
            Direction = direction;
        }

        #region Monogame Pipeline
        public override void Initialize() {
            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            tick(gameTime);
            if (!isGuided) {
                calculateVision();
            }
            if (IsTick && !isGuided) {
                move();
            }
            
            base.Update(gameTime);
        }
        #endregion

        #region Character methods
        public void die() {
        }

        public void move() {
            if (Node.Edges.ContainsKey(Direction)) {
                Position = Node.Edges[Direction].Position;
                Node = Node.Edges[Direction];
            } else {
                switch (Direction) {
                    case Direction.up:
                        Direction = Direction.down;
                        break;
                    case Direction.down:
                        Direction = Direction.up;
                        break;
                    case Direction.left:
                        Direction = Direction.right;
                        break;
                    case Direction.right:
                        Direction = Direction.left;
                        break;
                }
            }
        }
        #endregion

        #region Enemy methods
        private void calculateVision() {
            Vector2 pos = this.Position;
            Node tempNode = Node;
            Vision = new Vector2[visionLength];
            for (int i = 0; i < Vision.Length; i++) {
                if (!tempNode.Edges.ContainsKey(Direction)) {
                    Vision = new Vector2[i];
                } else {
                    tempNode = tempNode.Edges[Direction];
                }
            }

            switch (Direction) {
                case Direction.right:
                    int adder = 32;
                    for (int i = 0; i < Vision.Length; i++) {
                        Vision[i] = new Vector2(pos.X + adder, pos.Y);
                        adder += 32;
                    }
                    break;
                case Direction.left:
                    adder = 32;
                    for (int i = 0; i < Vision.Length; i++) {
                        Vision[i] = new Vector2(pos.X - adder, pos.Y);
                        adder += 32;
                    }
                    break;
                case Direction.up:
                    adder = 32;
                    for (int i = 0; i < Vision.Length; i++) {
                        Vision[i] = new Vector2(pos.X, pos.Y - adder);
                        adder += 32;
                    }
                    break;
                case Direction.down:
                    adder = 32;
                    for (int i = 0; i < Vision.Length; i++) {
                        Vision[i] = new Vector2(pos.X, pos.Y + adder);
                        adder += 32;
                    }
                    break;
            }
        }

        #endregion
        // Public properties
        public Vector2[] Vision { get; set; }
        public bool IsDead { get; set; }

        private int visionLength;
    }
}