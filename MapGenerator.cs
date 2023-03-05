namespace Minesweeper
{
    /// <summary>
    /// A class that can generate maps of tiles to be played on
    /// </summary>
    internal static class MapGenerator
    {
        /// <summary>
        /// Generates the game field and fills it with mines
        /// </summary>
        /// <param name="mapSizeX">The X size of the map</param>
        /// <param name="mapSizeY">The Y size of the map</param>
        /// <param name="mineCount">Controls how many mines will be generated</param>
        /// <returns>The newly generated map</returns>
        public static Tile[,] GenerateMap(int mapSizeX, int mapSizeY, int mineCount)
        {
            Tile[,] map = new Tile[mapSizeX, mapSizeY];

            // Populate the map with tiles
            List<Position> clearTiles = PlaceMines(map, mineCount);
            CalculateClearTiles(map, clearTiles);

            return map;
        }

        /// <summary>
        /// Places the specified amount of mines onto the field
        /// </summary>
        /// <param name="map">The map to be worked with</param>
        /// <param name="mineCount">How many mines we want to be placed</param>
        /// <returns>All positions without mines</returns>
        private static List<Position> PlaceMines(Tile[,] map, int mineCount)
        {
            // Prepare a list of all the positions where a mine can be placed
            // Initially stores all the positions of the map
            List<Position> available = new();
            for (int y = 0; y < map.GetLength(0); y++)
                for (int x = 0; x < map.GetLength(1); x++)
                    available.Add(new(x, y));
            Random r = new();

            // Fill the map randomly with the specified number of mines
            for (int i = 0; i < mineCount; i++)
            {
                // Get a random position from the available positions list
                int posIndex = r.Next(available.Count);

                // Consume the position (remove it from the list)
                Position minePos = available[posIndex];
                map[minePos.x, minePos.y] = new MineTile(minePos.x, minePos.y);

                available.RemoveAt(posIndex);
            }

            return available;
        }
        
        /// <summary>
        /// Calculates how many mines there are around each clear tile on the map
        /// </summary>
        /// <param name="map">The map to be worked with</param>
        /// <param name="clearTiles">All the tiles that aren't mines and have to be checked</param>
        private static void CalculateClearTiles(Tile[,] map, List<Position> clearTiles)
        {
            // An array of positions considered to be neighbours
            Position[] neighbours =
            {
                new(-1, -1),
                new(0, -1),
                new(1, -1),
                new(1, 0),
                new(1, 1),
                new(0, 1),
                new(-1, 1),
                new(-1, 0)
            };

            // Loop through the whole map
            foreach (Position pos in clearTiles)
            {
                int minesAround = 0;

                // Check all the surrounding positions for mines
                foreach (Position n in neighbours)
                {
                    Position neighbourAbsolute = pos.Combine(n);
                    if (map[neighbourAbsolute.x, neighbourAbsolute.y] is MineTile)
                        minesAround++;
                }

                // Place the clear tile
                map[pos.x, pos.y] = new ClearTile(pos.x, pos.y, minesAround);
            }
        }
    }
}
