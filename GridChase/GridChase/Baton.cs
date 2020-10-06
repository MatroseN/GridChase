using Microsoft.Xna.Framework;

namespace GridChase {
    class Baton : Weapon, Meele{
        public Baton(Vector2 position) : base(position) {
            this.Position = position;
            this.HitBox = new Vector2[2];
            this.Damage = 1.0f;
            this.Tag = Tag.baton;
        }

        public override void attack() {
            throw new System.NotImplementedException();
        }

        public void hit() {
            throw new System.NotImplementedException();
        }

        public void toss() {
            throw new System.NotImplementedException();
        }
    }
}
