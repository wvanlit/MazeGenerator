namespace Mazes.Visualisers;

public class UnicodeTextVisualiser : TextVisualiser
{
    protected override string PieceLookup(MazePiece piece)
    {
        return piece switch
        {
            MazePiece.CellBody => "   ",
            MazePiece.Corner => "+",
            MazePiece.WallVerticalFilled => "\u2502",
            MazePiece.WallVerticalEmpty => " ",

            MazePiece.WallHorizontalFilled => "\u2500\u2500\u2500",
            MazePiece.WallHorizontalEmpty => "   ",
            _ => "UNKNOWN PIECE"
        };
    }
}