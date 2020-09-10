using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GridChase {
    /* 
     Responsible for setting the different positions on the map depending on data fetched from a textfile
    */
    class MapGenerator {
        public MapGenerator(Game game) {
            this.game = game;
            jsonConverter = new JsonConverter();
        }

        public int[] generateGrid(Vector2 mapSize) {
            // Initialize the grid array to fit all map positions
            int[] grid = new int[(int)mapSize.X * (int)mapSize.Y];

            for (int x = 0; x < (int)mapSize.X; x++) {
                for (int y = 0; y < (int)mapSize.Y; y++) {
                    grid[y * (int)mapSize.X + x] = 1;
                }
            }

            return grid;
        }

        public Dictionary<string, string> fetchMapData(string mapName) {
            Dictionary<string, string> mapData = jsonConverter.toDictionary(mapName);
            return mapData;
        }

        public void generateMap(Dictionary<string, string> map, List<Entity> entities, Vector2 mapSize) {
            // TODO: Get the map info from the map dict
        }

        private Game game;
        private JsonConverter jsonConverter;
    }
}