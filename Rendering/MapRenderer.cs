using Minesweeper.Gameplay;

namespace Minesweeper.Rendering;

internal class MapRenderer
{
    private readonly Map owner;

    public MapRenderer(Map owner)
    {
        this.owner = owner;
    }

    public void PrepareMapRender()
    {
        // Render the first row of the table (header row)
        RenderLine headerRow = new();

        for (int x = 0; x <= owner.LengthX; x++)
            headerRow.AppendWithDefaultColour(x % 2 != 0 ? x.ToString("00") : "  ");

        BufferedRenderer.AddLine(headerRow);

        // Render the map (data rows of the table)
        for (int y = 0; y < owner.LengthY; y++)
        {
            RenderLine row = new();
            // Add the row header
            row.AppendWithDefaultColour(y % 2 == 0 ? (y + 1).ToString("00") : "  ");

            // Add the tile textures in this row
            for (int x = 0; x < owner.LengthX; x++)
                row.Append(owner[x, y].Texture);

            BufferedRenderer.AddLine(row);
        }
    }
}
