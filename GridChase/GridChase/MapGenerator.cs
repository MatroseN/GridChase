

using Microsoft.Xna.Framework;

namespace GridChase {
    /* 
     Responsible for setting the different positions on the map depending on data fetched from a textfile
    */
    class MapGenerator {
        public MapGenerator() {

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

        public void generateMap(char[] map) {
            foreach (char cell in map) {
                switch (cell) {
                    case '*':
                        // TODO: Add a player to the Entity list
                        break;
                }
            }
        }
    }
}