using System.Drawing;
using System.Xml;
using Svg;

namespace Mazes.Visualisers;

public class SvgVisualiser
{
    public Color BackgroundColor = Color.White;
    public Color WallColor = Color.Black;

    public int CellSize = 10;

    public string Draw(Grid grid)
    {
        var padding = CellSize * 2;
        var width = grid.Columns * CellSize;
        var height = grid.Rows * CellSize;

        var doc = new SvgDocument
        {
            ViewBox = new SvgViewBox()
            {
                Width = width + padding,
                Height = height + padding,
                MinX = -padding / 2f,
                MinY = -padding / 2f,
            },
            Width = width + padding,
            Height = height + padding,
            Children =
            {
                CreateBackground(width, height, padding),
                GenerateLines(grid)
            },
            Color = new SvgColourServer(BackgroundColor)
        };

        return doc.GetXML();
    }

    public SvgElement GenerateLines(Grid grid)
    {
        var group = new SvgGroup();

        foreach (var cell in grid.AllCells)
        {
            var x1 = cell.Column * CellSize;
            var y1 = cell.Row * CellSize;
            var x2 = (cell.Column + 1) * CellSize;
            var y2 = (cell.Row + 1) * CellSize;

            if (!cell.HasLinkedNeighbor(Direction.North))
                group.Children.Add(CreateLine(x1, y1, x2, y1));

            if (!cell.HasLinkedNeighbor(Direction.West))
                group.Children.Add(CreateLine(x1, y1, x1, y2));

            if (!cell.HasLinkedNeighbor(Direction.East))
                group.Children.Add(CreateLine(x2, y1, x2, y2));

            if (!cell.HasLinkedNeighbor(Direction.South))
                group.Children.Add(CreateLine(x1, y2, x2, y2));
        }

        return group;
    }

    public void DrawToFile(Grid grid, string filename)
    {
        var xml = Draw(grid);

        File.WriteAllText(filename, xml);
    }

    private SvgLine CreateLine(int x1, int y1, int x2, int y2)
    {
        var line = new SvgLine
        {
            StartX = x1,
            StartY = y1,
            EndX = x2,
            EndY = y2,
            Stroke = new SvgColourServer(WallColor),
            StrokeWidth = 1f,
            StrokeLineCap = SvgStrokeLineCap.Round
        };

        return line;
    }

    private SvgRectangle CreateBackground(int width, int height, int padding)
    {
        var bg = new SvgRectangle()
        {
            Width = width + padding,
            Height = height + padding,
            X = -padding / 2f,
            Y = -padding / 2f,
            Fill = new SvgColourServer(BackgroundColor),
        };

        return bg;
    }
}