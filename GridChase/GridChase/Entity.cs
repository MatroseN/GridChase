using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GridChase {
    /*
     * Base class that all entities (enemies, players, game objects (obstacles, powerups, teleporters, etc...) inherit from)
     */
    public abstract class Entity : GameComponent{
        public Entity(Game game) : base(game) {
            this.isGuided = false;
        }

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

        public void guidedMovement(Graph graph, Dictionary<Node, Node> allPaths) {
            if (IsTick) {
                Node node = graph.Adjacent[Position];
                Position = allPaths[node].Position;
            }
        }

        public void tick(GameTime gameTime) {
            IsTick = false;
            TickDelay.Wait(gameTime, () => {
                IsTick = true;
            });
        }

        public Vector2 Position { get; set; }
        public float Health { get; set; }
        public Tag Tag { get; set; }
        public Direction Direction { get; set; }
        public bool isGuided { get; set; }
        public Delay TickDelay { get; set; }
        public Node Node { get; set; }


        // Private Properties
        protected bool IsTick { get; set; }

        protected Vector2 startPos { get; set; }
    }
}