namespace Minesweeper.Gameplay;

internal class ZeroChunk
{
    /// <summary>
    /// All the tile positions
    /// </summary>
    public Position[] Positions { get; init; }
    /// <summary>
    /// How many tiles fall into this zero chunk
    /// </summary>
    public int Size { get => Positions.Length; }

    public ZeroChunk(Position[] positions)
    {
        Positions = positions;
    }
}
