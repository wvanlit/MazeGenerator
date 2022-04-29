using System.Text;
using Helpers;

namespace Mazes;

public class Grid
{
    public readonly int Rows;
    public readonly int Columns;

    private readonly Cell[,] _grid;

    public Random Rng { get; set; } = new ();

    public Grid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;

        _grid = PrepareGrid();

        ConnectNeighbors();
    }


    #region Grid / Cell Creation

    private Cell[,] PrepareGrid()
    {
        var gridCells = new Cell[Rows, Columns];

        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                gridCells[row, column] = new Cell(row, column);
            }
        }

        return gridCells;
    }

    private void ConnectNeighbors()
    {
        foreach (var cell in AllCells)
        {
            cell.AddNeighbor(Direction.North, CellAt(cell.Row - 1, cell.Column));
            cell.AddNeighbor(Direction.South, CellAt(cell.Row + 1, cell.Column));
            cell.AddNeighbor(Direction.West, CellAt(cell.Row, cell.Column - 1));
            cell.AddNeighbor(Direction.East, CellAt(cell.Row, cell.Column + 1));
        }
    }

    #endregion

    #region Cell Access

    public IEnumerable<Cell> AllCells => Enumerable.Range(0, Rows).SelectMany(GetRow);

    public IEnumerable<IEnumerable<Cell>> AllRows => Enumerable.Range(0, Rows).Select(GetRow);
    public IEnumerable<IEnumerable<Cell>> AllColumns => Enumerable.Range(0, Columns).Select(GetColumn);

    public IEnumerable<Cell> GetRow(int rowIndex) => Enumerable.Range(0, Columns).Select(i => _grid[rowIndex, i]);
    public IEnumerable<Cell> GetColumn(int columIndex) => Enumerable.Range(0, Rows).Select(i => _grid[i, columIndex]);


    public Cell? CellAt(int row, int column) =>
        MathHelper.IsInBounds(row, Rows, column, Columns) ? _grid[row, column] : null;

    #endregion

    #region Grid Info

    public int Size => Rows * Columns;

    public Cell RandomCell() => CellAt(Rng.Next(Rows), Rng.Next(Columns))!;

    public void SetSeed(int seed) => Rng = new Random(seed);

    public override string ToString()
    {
        const string cellBody = "   ";
        const string corner = "+";
        
        const string wallVertical = "|";
        const string wallVerticalEmpty = " ";
        
        const string wallHorizontal = "---";
        const string wallHorizontalEmpty = "   ";
        
        var builder = new StringBuilder();

        builder.Append(corner);
        
        foreach (var cell in GetRow(0))
        {
            var northWall = cell.HasLinkedNeighbor(Direction.North) ? wallHorizontalEmpty : wallHorizontal;
            builder.Append(northWall + corner);
        }
        
        builder.AppendLine("");

        foreach (var row in AllRows)
        {
            var top = "";
            var bottom = "+";

            var isFirstColumn = true;
            
            foreach (var cell in row)
            {
                if (isFirstColumn)
                {
                    var westWall = cell.HasLinkedNeighbor(Direction.West) ? wallVerticalEmpty : wallVertical;
                    top += westWall;
                    isFirstColumn = false;
                }
                
                var eastWall = cell.HasLinkedNeighbor(Direction.East) ? wallVerticalEmpty : wallVertical;
                top += cellBody + eastWall;

                var southWall = cell.HasLinkedNeighbor(Direction.South) ? wallHorizontalEmpty : wallHorizontal;
                bottom += southWall + corner;
            }

            builder.AppendLine(top);
            builder.AppendLine(bottom);
        }

        return builder.ToString();
    }

    #endregion
}