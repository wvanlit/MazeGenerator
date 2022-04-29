using System.Text;

namespace Mazes.Visualisers;

public class TextVisualiser
{
    public enum MazePiece
    {
        CellBody,
        Corner,

        WallVerticalFilled,
        WallVerticalEmpty,

        WallHorizontalFilled,
        WallHorizontalEmpty,
    }

    protected virtual string PieceLookup(MazePiece piece)
    {
        return piece switch
        {
            MazePiece.CellBody => "   ",
            MazePiece.Corner => "+",
            MazePiece.WallVerticalFilled => "|",
            MazePiece.WallVerticalEmpty => " ",

            MazePiece.WallHorizontalFilled => "---",
            MazePiece.WallHorizontalEmpty => "   ",
            _ => "UNKNOWN PIECE"
        };
    }

    public string Draw(Grid grid)
    {
        var builder = new StringBuilder();

        builder.Append(PieceLookup(MazePiece.Corner));

        foreach (var cell in grid.GetRow(0))
        {
            var northWall = cell.HasLinkedNeighbor(Direction.North)
                ? PieceLookup(MazePiece.WallHorizontalEmpty)
                : PieceLookup(MazePiece.WallHorizontalFilled);
            builder.Append(northWall + PieceLookup(MazePiece.Corner));
        }

        builder.AppendLine("");

        foreach (var row in grid.AllRows)
        {
            var top = "";
            var bottom = PieceLookup(MazePiece.Corner);

            var isFirstColumn = true;

            foreach (var cell in row)
            {
                if (isFirstColumn)
                {
                    var westWall = cell.HasLinkedNeighbor(Direction.West)
                        ? PieceLookup(MazePiece.WallVerticalEmpty)
                        : PieceLookup(MazePiece.WallVerticalFilled);

                    top += westWall;

                    isFirstColumn = false;
                }

                var eastWall = cell.HasLinkedNeighbor(Direction.East)
                    ? PieceLookup(MazePiece.WallVerticalEmpty)
                    : PieceLookup(MazePiece.WallVerticalFilled);

                top += PieceLookup(MazePiece.CellBody) + eastWall;

                var southWall = cell.HasLinkedNeighbor(Direction.South)
                    ? PieceLookup(MazePiece.WallHorizontalEmpty)
                    : PieceLookup(MazePiece.WallHorizontalFilled);

                bottom += southWall + PieceLookup(MazePiece.Corner);
            }

            builder.AppendLine(top);
            builder.AppendLine(bottom);
        }

        return builder.ToString();
    }
}