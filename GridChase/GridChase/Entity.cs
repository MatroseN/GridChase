using Microsoft.Xna.Framework;

namespace GridChase {
    /*
     * Base class that all entities (enemies, players, game objects (obstacles, powerups, teleporters, etc...) inherit from)
     */
    public abstract class Entity : GameComponent{
        public Entity(Game game) : base(game) {

        }

        public Vector2 Position { get; set; }
        public float Health { get; set; }
        public Tag Tag { get; set; }
    }
}