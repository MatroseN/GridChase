using Microsoft.Xna.Framework;

namespace GridChase {
    public abstract class Weapon {
        public Weapon(Vector2 position) {
            Position = position;
            Pickuped = false;
        }

        public abstract void attack();

        public void calculatePosition(Vector2 windowSize, Vector2 blockSize) {
            Vector2 pos = new Vector2(this.Position.X * blockSize.X, this.Position.Y * blockSize.Y);
            if (pos.X >= windowSize.X) {
                pos = new Vector2(windowSize.X - blockSize.X, pos.Y);
            }
            if (pos.Y >= windowSize.Y) {
                pos = new Vector2(pos.X, pos.Y - blockSize.Y);
            }

            startPos = pos;
            Position = startPos;
        }

        public void tick(GameTime gameTime) {
            IsTick = false;
            TickDelay.Wait(gameTime, () => {
                IsTick = true;
            });
        }

        public Vector2 Position { get; set; }
        public Vector2[] HitBox { get; set; }
        public float Damage { get; set; }
        public Delay TickDelay { get; set; }
        public bool Pickuped { get; set; }
        public Tag Tag { get; set; }

        protected bool IsTick { get; set; }
        protected Vector2 startPos { get; set; }
    }
}