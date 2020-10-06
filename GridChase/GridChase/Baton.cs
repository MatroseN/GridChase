using Microsoft.Xna.Framework;

namespace GridChase {
    class Baton : Weapon{
        public Baton(Vector2 position) : base(position) {
            this.Position = position;
            this.HitBox = new Vector2[0];
            this.Damage = 1.0f;
            this.Tag = Tag.baton;
        }

        public override void attack(Vector2 pos, Direction dir) {
            hit(pos, dir);
        }

        private void hit(Vector2 pos, Direction dir) {
            Vector2 position = pos;
            this.HitBox = new Vector2[1];
            switch (dir) {
                case Direction.up:
                    position = new Vector2(position.X, position.Y - 32);
                    HitBox[0] = position;
                    break;
                case Direction.down:
                    position = new Vector2(position.X, position.Y + 32);
                    HitBox[0] = position;
                    break;
                case Direction.left:
                    position = new Vector2(position.X - 32, position.Y);
                    HitBox[0] = position;
                    break;
                case Direction.right:
                    position = new Vector2(position.X + 32, position.Y);
                    HitBox[0] = position;
                    break;
            }
        }

        private void toss() {
            throw new System.NotImplementedException();
        }
    }
}
