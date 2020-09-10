using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace GridChase {
    /* 
     Responsible for setting the different positions on the map depending on data fetched from a textfile
    */
    class MapGenerator {
        public MapGenerator(Game game) {
            this.game = game;
            this.Grid = new List<int>();
        }

        private void generateGrid(Vector2 mapSize) {
            // Initialize the grid array to fit all map positions
            Grid = new List<int>((int)mapSize.X * (int)mapSize.Y);
            int[] gridArray = new int[(int)mapSize.X * (int)mapSize.Y];
            for (int x = 0; x < (int)mapSize.X; x++) {
                for (int y = 0; y < (int)mapSize.Y; y++) {
                    gridArray[y * (int)mapSize.X + x] = 1;
                }
            }

            this.Grid = gridArray.ToList();
        }

        public void generateMap(List<Entity> entities, string mapName) {
            XmlDocument xml = new XmlDocument();
            xml.Load(mapName + ".xml");


            foreach (XmlNode node in xml.SelectSingleNode("map").ChildNodes) {
                switch (node.Name) {
                    case "head":
                        foreach (XmlNode childNode in node.ChildNodes) {
                            if (String.Equals(childNode.Name, "size")) {
                                int x = 0;
                                int y = 0;
                                foreach (XmlNode childOfChildNode in childNode) {
                                    if (String.Equals(childOfChildNode.Name, "x")) {
                                        Int32.TryParse(childOfChildNode.InnerText, out x);
                                    }else if(String.Equals(childOfChildNode.Name, "y")) {
                                        Int32.TryParse(childOfChildNode.InnerText, out y);
                                    }
                                }
                                Vector2 mapSize = new Vector2(x, y);
                                generateGrid(mapSize);
                            }
                        }
                        break;

                    case "characters":
                        foreach (XmlNode childNode in node) {
                            foreach (XmlNode childOfChildNode in childNode) {
                                if (String.Equals(childOfChildNode.Name, "player")) {
                                    int x = 0;
                                    int y = 0;
                                    foreach (XmlNode childOfChildOfchildNode in childOfChildNode) {
                                        if (String.Equals(childOfChildOfchildNode.Name, "position")) {
                                            foreach(XmlNode childOfChildOfChildOfChild in childOfChildOfchildNode)
                                            if (String.Equals(childOfChildOfChildOfChild.Name, "x")) {
                                                Int32.TryParse(childOfChildOfChildOfChild.InnerText, out x);
                                            } else if (String.Equals(childOfChildOfChildOfChild.Name, "y")) {
                                                Int32.TryParse(childOfChildOfChildOfChild.InnerText, out y);
                                            }
                                        }
                                    }
                                    Vector2 position = new Vector2(x, y);
                                    entities.Add(new Player(game, position));
                                }
                            }
                        }
                        break;
                }
            }
        }

        private Game game;
        public List<int> Grid { get; set; }
    }
}