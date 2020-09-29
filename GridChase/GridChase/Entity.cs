using Microsoft.Xna.Framework;

namespace GridChase {
    /*
     * Base class that all entities (enemies, players, game objects (obstacles, powerups, teleporters, etc...) inherit from)
     */
    public abstract class Entity : GameComponent{
        public Entity(Game game) : base(game) {

        }

        public void calculatePosition(Vector2 windowSize, Vector2 blockSize) {
            Vector2 pos = new Vector2(this.Position.X * blockSize.X, this.Position.Y * blockSize.Y);
            if (pos.X >= windowSize.X) {
                pos = new Vector2(windowSize.X - blockSize.X, pos.Y);
            }
            if (pos.Y >= windowSize.Y) {
                pos = new Vector2(pos.X, pos.Y - blockSize.Y);
            }

            Position = pos;
        }

        public Vector2 Position { get; set; }
        public float Health { get; set; }
        public Tag Tag { get; set; }
    }
}