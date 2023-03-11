namespace Minesweeper;

internal class BufferedRenderer
{
    private static Queue<string> buffer = new();

    /// <summary>
    /// Adds a line to the renderer queue to be rendered in the next cycle
    /// </summary>
    /// <param name="line">The string to be added</param>
    public static void AddLine(string line)
    {
        buffer.Enqueue(line);
    }

    public static void Render()
    {
        while (buffer.Count > 0)
        {
            string line = buffer.Dequeue();
            Console.WriteLine(line);
        }
    }
}