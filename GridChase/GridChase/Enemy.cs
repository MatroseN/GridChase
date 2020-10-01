﻿using Microsoft.Xna.Framework;
using System;

namespace GridChase {
    /*
     * Enemies to the PLAYABLE characters
     */
    class Enemy : Entity, Character {
        public Enemy(Game game, Vector2 position, Direction direction) : base(game) {
            this.Position = position;
            this.Health = 1.0f;
            this.Tag = Tag.enemy;
            this.Vision = new Vector2[6];
            this.IsTick = false;
            TickDelay = new Delay(500.0);
            Direction = direction;
        }

        #region Monogame Pipeline
        public override void Initialize() {
            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            tick(gameTime);
            calculateVision();
            if (IsTick) {
                move();
            }
            
            base.Update(gameTime);
        }
        #endregion

        #region Character methods
        public void die() {
            throw new NotImplementedException();
        }

        public void move() {
            switch (Direction) {
                case Direction.right:
                    Position = new Vector2(Position.X + 32, Position.Y);
                    break;
                case Direction.left:
                    Position = new Vector2(Position.X - 32, Position.Y);
                    break;
                case Direction.up:
                    Position = new Vector2(Position.X, Position.Y - 32);
                    break;
                case Direction.down:
                    Position = new Vector2(Position.X, Position.Y + 32);
                    break;
            }
        }
        #endregion

        #region Enemy methods
        private void calculateVision() {
            Vector2 pos = this.Position;
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

        private void tick(GameTime gameTime) {
            IsTick = false;
            TickDelay.Wait(gameTime, () => {
                IsTick = true;
            });
        }
        #endregion
        // Public properties
        public Vector2[] Vision { get; set; }
        public Delay TickDelay { get; set; }
         
        // Private Properties
        private bool IsTick { get; set; }
    }
}