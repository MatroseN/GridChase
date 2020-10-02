using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace GridChase {
    class BFS {
        public BFS(Graph graph) {
            this.graph = graph;
        }

        public List<Node> search(Node s, Node e) {
            Queue<Node> queue = new Queue<Node>();
            Node node;
            Node endNode = e;
            Node startNode = s;
            Dictionary<Node, Node> parent = new Dictionary<Node, Node>();

            queue.Enqueue(startNode);

            while (queue.Count > 0) {
                node = queue.Dequeue();
                if (node == endNode) {
                    break;
                }

                foreach (Node edge in node.Edges.Values) {
                    if (!contains(queue, node)){
                        if (!parent.ContainsKey(edge)) {
                            parent.Add(edge, node);
                        }
                        queue.Enqueue(edge);
                    }
                }
            }
            return backtrace(parent, startNode, endNode);
        }

        private List<Node> backtrace(Dictionary<Node, Node> parent, Node start, Node end) {
            List<Node> path = new List<Node>();
            Node node = parent[end];

            while (node != start) {
                path.Add(node);
                node = parent[node];
            }
            path.Add(start);

            path.Reverse();

            return path;
        }


        private bool contains(Queue<Node> q, Node n) {
            foreach (Node node in q) {
                if (node.ID == n.ID) {
                    return true;
                }
            }
            return false;
        }

        private Graph graph;
    }
}