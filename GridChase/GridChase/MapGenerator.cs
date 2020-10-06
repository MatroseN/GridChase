using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml;

namespace GridChase {
    /* 
     Responsible for setting the different positions on the map depending on data fetched from a textfile
    */
    class MapGenerator {
        public MapGenerator(Game game) {
            this.game = game;
        }

        private void generateGrid(Vector2 mapSize, Vector2 blockSize) {
            // Initialize the grid array to fit all map positions
            int i = 0;
            Vector2[] grid = new Vector2[(int)mapSize.Y * (int)mapSize.X];
            for (int y = 0; y < (int)mapSize.Y * (int)blockSize.Y; y += (int)blockSize.Y) {
                for (int x = 0; x < (int)mapSize.X * (int)blockSize.X; x += (int)blockSize.X) {
                    grid[i] = new Vector2(x, y);
                    i++;
                }
            }
            this.grid = grid;
        }

        public void generateMap(List<Entity> entities, string mapName, Vector2 blockSize, List<Vector2> barriers, Vector2 windowSize, List<Weapon> weapons) {
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
                                    } else if (String.Equals(childOfChildNode.Name, "y")) {
                                        Int32.TryParse(childOfChildNode.InnerText, out y);
                                    }
                                }
                                Vector2 mapSize = new Vector2(x, y);
                                generateGrid(mapSize, blockSize);
                            }
                        }
                        break;

                    case "characters":
                        foreach (XmlNode childNode in node) {
                            if (String.Equals(childNode.Name, "player")) {
                                int x = 0;
                                int y = 0;
                                foreach (XmlNode childOfChildNode in childNode) {
                                    if (String.Equals(childOfChildNode.Name, "position")) {
                                        foreach (XmlNode childOfChildOfChild in childOfChildNode)
                                            if (String.Equals(childOfChildOfChild.Name, "x")) {
                                                Int32.TryParse(childOfChildOfChild.InnerText, out x);
                                            } else if (String.Equals(childOfChildOfChild.Name, "y")) {
                                                Int32.TryParse(childOfChildOfChild.InnerText, out y);
                                            }
                                    }
                                }
                                Vector2 position = new Vector2(x, y);
                                entities.Add(new Player(game, position));
                            }else if (String.Equals(childNode.Name, "enemy")) {
                                Direction direction = Direction.right;
                                int x = 0;
                                int y = 0;
                                bool hasKey = false;
                                foreach (XmlNode childOfChildNode in childNode) {
                                    if (String.Equals(childOfChildNode.Name, "position")) {
                                        foreach (XmlNode childOfChildOfChild in childOfChildNode)
                                            if (String.Equals(childOfChildOfChild.Name, "x")) {
                                                Int32.TryParse(childOfChildOfChild.InnerText, out x);
                                            } else if (String.Equals(childOfChildOfChild.Name, "y")) {
                                                Int32.TryParse(childOfChildOfChild.InnerText, out y);
                                            }
                                    }else if (String.Equals(childOfChildNode.Name, "direction")) {
                                        string xmlText = "";
                                        xmlText = childOfChildNode.InnerText;

                                        switch (xmlText) {
                                            case "right":
                                                direction = Direction.right;
                                                break;
                                            case "left":
                                                direction = Direction.left;
                                                break;
                                            case "up":
                                                direction = Direction.up;
                                                break;
                                            case "down":
                                                direction = Direction.down;
                                                break;
                                        }

                                    }else if (String.Equals(childOfChildNode.Name, "key")) {
                                        hasKey = true;
                                    }
                                }
                                Vector2 position = new Vector2(x, y);
                                entities.Add(new Enemy(game, position, direction, hasKey));
                            }
                        }
                        break;
                    case "blocks":
                        foreach (XmlNode childNode in node) {
                            if (String.Equals(childNode.Name, "barrier")) {
                                int x = 0;
                                int y = 0;
                                foreach (XmlNode childOfChildNode in childNode) {
                                    if (String.Equals(childOfChildNode.Name, "position")) {
                                        foreach (XmlNode childOfChildOfChild in childOfChildNode)
                                            if (String.Equals(childOfChildOfChild.Name, "x")) {
                                                Int32.TryParse(childOfChildOfChild.InnerText, out x);
                                            } else if (String.Equals(childOfChildOfChild.Name, "y")) {
                                                Int32.TryParse(childOfChildOfChild.InnerText, out y);
                                            }
                                    }
                                }
                                Vector2 position = new Vector2(x * blockSize.X, y * blockSize.Y);

                                if (position.X >= windowSize.X) {
                                    position = new Vector2(windowSize.X - blockSize.X, position.Y);
                                }
                                if (position.Y >= windowSize.Y) {
                                    position = new Vector2(position.X, position.Y - blockSize.Y);
                                }

                                barriers.Add(position);
                            }else if (String.Equals(childNode.Name, "finnish")) {
                                int x = 0;
                                int y = 0;
                                foreach (XmlNode childOfChildNode in childNode) {
                                    if (String.Equals(childOfChildNode.Name, "position")) {
                                        foreach (XmlNode childOfChildOfChild in childOfChildNode)
                                            if (String.Equals(childOfChildOfChild.Name, "x")) {
                                                Int32.TryParse(childOfChildOfChild.InnerText, out x);
                                            } else if (String.Equals(childOfChildOfChild.Name, "y")) {
                                                Int32.TryParse(childOfChildOfChild.InnerText, out y);
                                            }
                                    }
                                }
                                Vector2 position = new Vector2(x * blockSize.X, y * blockSize.Y);

                                if (position.X >= windowSize.X) {
                                    position = new Vector2(windowSize.X - blockSize.X, position.Y);
                                }
                                if (position.Y >= windowSize.Y) {
                                    position = new Vector2(position.X, position.Y - blockSize.Y);
                                }
                                FinnishPosition = position;
                            }
                        }
                        break;
                    case "weapons":
                        foreach (XmlNode childNode in node) {
                            if (String.Equals(childNode.Name, "baton")) {
                                int x = 0;
                                int y = 0;
                                foreach (XmlNode childOfChildNode in childNode) {
                                    if (String.Equals(childOfChildNode.Name, "position")) {
                                        foreach (XmlNode childOfChildOfChild in childOfChildNode)
                                            if (String.Equals(childOfChildOfChild.Name, "x")) {
                                                Int32.TryParse(childOfChildOfChild.InnerText, out x);
                                            } else if (String.Equals(childOfChildOfChild.Name, "y")) {
                                                Int32.TryParse(childOfChildOfChild.InnerText, out y);
                                            }
                                    }
                                }
                                Vector2 position = new Vector2(x, y);
                                weapons.Add(new Baton(position));
                            }
                        }
                        break;
                }
            }
        }
            
        

        private Vector2[] grid;
        private Game game;

        public Vector2[] Grid {
            get { return this.grid; }
            set { grid = value; }
        }

        public Vector2 FinnishPosition { get; set; }
    }
}