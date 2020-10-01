using System.Collections.Generic;

namespace GridChase {
    class Graph {
        public Graph(int V) {
            Adjacent = new LinkedList<Node>[V];
            initializeList();
        }

        private void initializeList() {
            for (int i = 0; i < Adjacent.Length; i++) {
                Adjacent[i] = new LinkedList<Node>();
            }
        }

        public void addEdge(int v, Node w) {
            Adjacent[v].AddLast(w);
        }

        public LinkedList<Node>[] Adjacent { get; set; }
        public int V { get; set; }
    }
}