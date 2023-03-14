namespace Minesweeper.Rendering;

/// <summary>
/// A piece of data the buffered renderer can chomp up to render some characters into the console window
/// </summary>
internal struct RenderLine
{
    public LineChunk this[int index]
    {
        get => new(chars[index], colours[index]);
        set
        {
            chars = chars.Remove(index, 1).Insert(index, value.c.ToString());
            colours[index] = value.colour;
        }
    }
    public int Length { get => chars.Length; }

    /// <summary>
    /// The characters to be rendered
    /// </summary>
    private string chars;
    /// <summary>
    /// The colours to be used (the i-th character in the render text has the i-th colour in the colours array)
    /// </summary>
    private ConsoleColor[] colours;

    public RenderLine(string chars, ConsoleColor[] colours)
    {
        this.chars = chars;
        this.colours = colours;
    }

    public RenderLine(string chars)
    {
        this.chars = chars;
        colours = Enumerable.Repeat(BufferedRenderer.COLOUR_DEFAULT, chars.Length).ToArray();
    }

    public RenderLine()
    {
        chars = "";
        colours = Array.Empty<ConsoleColor>();
    }

    /// <summary>
    /// Appends another render line to the end of this one
    /// </summary>
    /// <param name="other">The other render line to append</param>
    public void Append(RenderLine other)
    {
        chars += other.chars;
        foreach (ConsoleColor c in other.colours)
            colours = colours.Append(c).ToArray();
    }

    /// <summary>
    /// Appends a raw string to the end of this render line using the default colour
    /// </summary>
    /// <param name="text">The text to be appended</param>
    public void AppendWithDefaultColour(string text)
    {
        chars += text;
        for (int i = 0; i < text.Length; i++)
            colours = colours.Append(BufferedRenderer.COLOUR_DEFAULT).ToArray();
    }
}

public struct LineChunk
{
    public char c;
    public ConsoleColor colour;

    public LineChunk(char c, ConsoleColor colour)
    {
        this.c = c;
        this.colour = colour;
    }
}