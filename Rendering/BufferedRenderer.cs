namespace Minesweeper.Rendering;

/// <summary>
/// A renderer used by the whole game to render stuff at once at the end of every game loop cycle
/// </summary>
internal static class BufferedRenderer
{
    /// <summary>
    /// A queue where all the data to be rendered is stored
    /// </summary>
    private static readonly Queue<RenderLine> renderQueue = new();

    /// <summary>
    /// Adds a render line to the render queue
    /// </summary>
    /// <param name="line">A piece of data for the renderer</param>
    public static void AddLine(RenderLine line)
    {
        renderQueue.Enqueue(line);
    }

    /// <summary>
    /// Renders all the data currently waiting in the queue
    /// </summary>
    public static void Render()
    {
        Console.Clear();

        while (renderQueue.Count > 0)
        {
            RenderLine line = renderQueue.Dequeue();
            for (int i = 0; i < line.chars.Length; i++)
            {
                char c = line.chars[i];
                ConsoleColor charColour = line.colours[i];
                if (Console.BackgroundColor != charColour)
                    Console.BackgroundColor = charColour;
                Console.Write(c);
            }
            Console.WriteLine();
        }
    }
}