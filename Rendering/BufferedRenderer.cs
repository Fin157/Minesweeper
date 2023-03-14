namespace Minesweeper.Rendering;

/// <summary>
/// A renderer used by the whole game to render stuff at the right time
/// </summary>
internal static class BufferedRenderer
{
    public const ConsoleColor COLOUR_DEFAULT = ConsoleColor.Black;

    /// <summary>
    /// A queue where all the data to be rendered is stored
    /// </summary>
    private static readonly Queue<RenderLine> mainQueue = new();
    private static readonly Queue<RenderLine> additionalQueue = new();

    /// <summary>
    /// Adds a render line to the main render queue
    /// </summary>
    /// <param name="line">A piece of data for the renderer</param>
    public static void AddToMain(RenderLine line) => mainQueue.Enqueue(line);
    /// <summary>
    /// Adds a render line to the additional queue
    /// </summary>
    /// <param name="line">A piece of data for the renderer</param>
    public static void AddToAdditional(RenderLine line) => additionalQueue.Enqueue(line);

    /// <summary>
    /// Renders all the data currently waiting in the queue
    /// </summary>
    public static void Render()
    {
        Console.Clear();

        // Render stuff from the main queue first
        RenderQueue(mainQueue);

        // Render stuff from the additional queue
        RenderQueue(additionalQueue);
    }

    /// <summary>
    /// Renders the specified queue
    /// </summary>
    /// <param name="queue">The queue to be rendered</param>
    private static void RenderQueue(Queue<RenderLine> queue)
    {
        while (queue.Count > 0)
        {
            RenderLine line = queue.Dequeue();
            for (int i = 0; i < line.Length; i++)
            {
                LineChunk lc = line[i];
                if (Console.BackgroundColor != lc.colour)
                    Console.BackgroundColor = lc.colour;
                Console.Write(lc.c);
            }
            Console.WriteLine();
        }

        Console.BackgroundColor = COLOUR_DEFAULT;
    }
}