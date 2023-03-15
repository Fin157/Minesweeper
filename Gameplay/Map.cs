﻿using Minesweeper.Gameplay.TileSystem;
using Minesweeper.Rendering;

namespace Minesweeper.Gameplay;

/// <summary>
/// A class representing a game field consisting of tiles
/// </summary>
internal class Map
{
    /// <summary>
    /// X size of the map
    /// </summary>
    public int LengthX { get => tiles.GetLength(1); }
    /// <summary>
    /// Y size of the map
    /// </summary>
    public int LengthY { get => tiles.GetLength(0); }
    /// <summary>
    /// Accesses individual tiles of the map
    /// </summary>
    /// <param name="x">The X position of the tile</param>
    /// <param name="y">The Y position of the tile</param>
    /// <returns>A tile to use in read/write operations</returns>
    public Tile this[int x, int y]
    {
        get => tiles[x, y];
        set => tiles[x, y] = value;
    }

    private readonly Tile[,] tiles;
    private readonly MapRenderer renderer;
    private readonly List<ZeroChunk> zeroChunks;

    private Map(int sizeX, int sizeY)
    {
        tiles = new Tile[sizeX, sizeY];
        renderer = new(this);
        zeroChunks = new();
    }

    /// <summary>
    /// Checks if the input position is inside the map
    /// </summary>
    /// <param name="pos">The position to be checked</param>
    /// <returns>True if the position fulfills the conditions, otherwise false</returns>
    public bool IsPositionValid(Position pos) => pos.x >= 0 && pos.x < tiles.GetLength(1) && pos.y >= 0 && pos.y < tiles.GetLength(0);

    /// <summary>
    /// Tells the renderer to fill the buffer with tile textures
    /// </summary>
    public void PrepareRender() => renderer.PrepareMapRender();

    #region Static map generation methods
    /// <summary>
    /// Generates the game field, fills it with mines and exposes a small part of the map with "zero tiles"
    /// </summary>
    /// <param name="mapSizeX">The X size of the map</param>
    /// <param name="mapSizeY">The Y size of the map</param>
    /// <param name="mineCount">Controls how many mines will be generated</param>
    /// <returns>The newly generated map</returns>
    public static Map GenerateMap(int mapSizeX, int mapSizeY, int mineCount)
    {
        Map map = new(mapSizeX, mapSizeY);

        // Populate the map with tiles
        List<Position> available = InitMap(map);
        List<Position> clearTiles = PlaceMines(map, available, mineCount);
        CalculateClearTiles(map, clearTiles);
        ExposeLargestChunk(map.zeroChunks);

        return map;
    }

    private static List<Position> InitMap(Map map)
    {
        List<Position> available = new();

        for (int y = 0; y < map.LengthY; y++)
        {
            for (int x = 0; x < map.LengthX; x++)
            {
                available.Add(new(x, y));
                map[x, y] = new ClearTile(x, y, IsDarker(new(x, y)));
            }
        }

        return available;
    }

    /// <summary>
    /// Places the specified amount of mines onto the field
    /// </summary>
    /// <param name="map">The map to be worked with</param>
    /// <param name="mineCount">How many mines we want to be placed</param>
    /// <returns>All positions without mines</returns>
    private static List<Position> PlaceMines(Map map, List<Position> available, int mineCount)
    {
        Random r = new();

        // Fill the map randomly with the specified number of mines
        for (int i = 0; i < mineCount; i++)
        {
            // Get a random position from the available positions list
            int posIndex = r.Next(available.Count);

            // Consume the position (remove it from the list)
            Position minePos = available[posIndex];
            map[minePos.x, minePos.y] = new MineTile(minePos.x, minePos.y, IsDarker(minePos));

            available.RemoveAt(posIndex);
        }

        return available;
    }

    /// <summary>
    /// Calculates how many mines there are around each clear tile on the map
    /// </summary>
    /// <param name="map">The map to be worked with</param>
    /// <param name="clearTiles">All the tiles that aren't mines and have to be checked</param>
    private static void CalculateClearTiles(Map map, List<Position> clearTiles)
    {
        // An array of positions considered to be neighbours to a tile (relative positions to it)
        Position[] neighboursRelative =
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
            ZeroChunk? chunk = null;
            List<Position> neighboursAbsolute = new();

            // Check all the surrounding positions for mines
            foreach (Position n in neighboursRelative)
            {
                Position neighbourAbsolute = pos.Combine(n);
                if (map.IsPositionValid(neighbourAbsolute))
                {
                    neighboursAbsolute.Add(neighbourAbsolute);
                    Tile neighbourTile = map[neighbourAbsolute.x, neighbourAbsolute.y];

                    if (neighbourTile is MineTile)
                        minesAround++;
                    else if (neighbourTile is ClearTile)
                    {
                        ZeroChunk? neighbourChunk = (neighbourTile as ClearTile)?.Chunk;
                        if (neighbourChunk != null)
                            chunk = neighbourChunk;
                    }
                }
            }

            // Update the clear tile
            (map[pos.x, pos.y] as ClearTile).MinesAround = minesAround;

            // Check if this tile is a zero tile (DONE)
            // If it is, find out if any neighbouring tile belongs to a zero chunk
            // If not, create a new chunk and put this tile into it
            // If yes, adopt that zero chunk
            // Expand the zero chunk to neighbouring tiles as well

            if (minesAround == 0)
            {
                if (chunk == null)
                {
                    chunk = new();
                    map.zeroChunks.Add(chunk);
                }

                chunk.AddTile(map[pos.x, pos.y]);

                foreach (Position n in neighboursAbsolute)
                    chunk.AddTile(map[n.x, n.y]);
            }
        }
    }

    private static void ExposeLargestChunk(List<ZeroChunk> zeroChunks)
    {
        int currentLargest = 0;
        ZeroChunk? current = null;

        foreach (ZeroChunk c in zeroChunks)
        {
            if (c.Size > currentLargest)
            {
                currentLargest = c.Size;
                current = c;
            }
        }

        current?.Expose();
    }

    /// <summary>
    /// Checks if a tile at the specified position should be rendered darker or not
    /// </summary>
    /// <param name="pos">A position where the tile is</param>
    /// <returns>True if the tile should be rendered darker, otherwise false</returns>
    private static bool IsDarker(Position pos)
    {
        // Controls if the odd X values in this row should be dark (1) or light (0)
        int areOddDark = pos.y % 2;
        return pos.x % 2 == areOddDark;
    }
    #endregion
}
