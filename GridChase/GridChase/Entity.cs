using Microsoft.Xna.Framework;

namespace GridChase {
    public abstract class Entity : GameComponent{
        public Entity(Game game) : base(game) {

        }

        public Vector2 Position { get; set; }
    }
}