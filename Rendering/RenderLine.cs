namespace Minesweeper.Rendering;

/// <summary>
/// A piece of data the buffered renderer can chomp up to render some characters into the console window
/// </summary>
internal struct RenderLine
{
    /// <summary>
    /// The characters to be rendered
    /// </summary>
    public string chars;
    /// <summary>
    /// The colours to be used (the i-th character in the render text has the i-th colour in the colours array)
    /// </summary>
    public ConsoleColor[] colours;

    /// <summary>
    /// The default colour to be used as default
    /// </summary>
    private const ConsoleColor COLOUR_DEFAULT = ConsoleColor.Black;

    public RenderLine(string chars, ConsoleColor[] colours)
    {
        this.chars = chars;
        this.colours = colours;
    }

    /// <summary>
    /// Appends another render line at the end of this one
    /// </summary>
    /// <param name="other">The other render line to append</param>
    public void Append(RenderLine other)
    {
        chars += other.chars;
        foreach (ConsoleColor c in other.colours)
            colours = colours.Append(c).ToArray();
    }

    /// <summary>
    /// Appends a raw string at the end of this render line using the default colour
    /// </summary>
    /// <param name="text">The text to be appended</param>
    public void AppendWithDefaultColour(string text)
    {
        chars += text;
        for (int i = 0; i < text.Length; i++)
            colours = colours.Append(COLOUR_DEFAULT).ToArray();
    }
}