using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GridChase {
    class Graph {
        public Graph() {
            Adjacent = new Dictionary<Vector2, Node>();
        }

        public void addEdges() {
            foreach (KeyValuePair<Vector2, Node> entry in Adjacent) {
                Vector2 up = new Vector2(entry.Value.Position.X, entry.Value.Position.Y - 32);
                Vector2 down = new Vector2(entry.Value.Position.X, entry.Value.Position.Y + 32);
                Vector2 right = new Vector2(entry.Value.Position.X + 32, entry.Value.Position.Y);
                Vector2 left = new Vector2(entry.Value.Position.X - 32, entry.Value.Position.Y);
                Node tempNode;

                if (Adjacent.TryGetValue(up, out tempNode)) {
                    entry.Value.Edges.Add(Direction.up, tempNode);
                }

                if (Adjacent.TryGetValue(down, out tempNode)) {
                    entry.Value.Edges.Add(Direction.down, tempNode);
                }

                if (Adjacent.TryGetValue(right, out tempNode)) {
                    entry.Value.Edges.Add(Direction.right, tempNode);
                }

                if (Adjacent.TryGetValue(left, out tempNode)) {
                    entry.Value.Edges.Add(Direction.left, tempNode);
                }
            }
        }

        public Dictionary<Vector2, Node> Adjacent { get; set; }
    }
}