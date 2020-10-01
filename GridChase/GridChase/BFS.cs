using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace GridChase {
    class BFS {
        public BFS(Graph graph, Node start) {
            this.graph = graph;
            this.start = start;
            Queue = new LinkedList<Node>();
            Visited = new bool[graph.V];
            initializeQueue();
        }

        public void initializeQueue() {
            for (int i = 0; i < Visited.Length; i++) {
                Visited[i] = false;
            }
        }

        public void traverse() {
            Visited[start.ID] = true;
            Queue.AddLast(start);

            while (Queue.Any()) {

                // Dequeue vetex
                start = Queue.First();
                Queue.RemoveFirst();

                LinkedList<Node> list = graph.Adjacent[start.ID];

                foreach (Node  node in list) {
                    if (!Visited[node.ID]) {
                        Visited[node.ID] = true;
                        Queue.AddLast(node);
                    }
                }
            }
        }

        private Node start;
        private Graph graph;

        public bool[] Visited { get; set; }
        public LinkedList<Node> Queue { get; set; }
    }
}