using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GridChase {
    class Node {
        public Node(Vector2 position, Node[] edges) {
            this.Position = position;
        }

        public Vector2 Position { get; set; }
        public int ID { get; set; }
    }
}
