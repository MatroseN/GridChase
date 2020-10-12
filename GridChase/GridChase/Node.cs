using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GridChase {
    public class Node {
        public Node(Vector2 position, int ID) {
            this.Position = position;
            this.ID = ID;
            Edges = new Dictionary<Direction, Node>();
        }

        public Vector2 Position { get; set; }
        public int ID { get; set; }
        public Dictionary<Direction, Node> Edges { get; set; }
    }
}
