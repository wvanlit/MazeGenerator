namespace Mazes;

public class Cell
{
    public readonly int Row;
    public readonly int Column;

    private readonly Dictionary<Direction, Cell> _neighbors;
    private readonly Dictionary<Cell, bool> _links;

    public Cell(int row, int column)
    {
        Row = row;
        Column = column;

        _links = new Dictionary<Cell, bool>();
        _neighbors = new Dictionary<Direction, Cell>();
    }

    #region Neighbors

    public void AddNeighbor(Direction direction, Cell? cell)
    {
        if (cell is null) return;

        _neighbors.Add(direction, cell);
    }

    public void AddLinkedNeighbor(Direction direction, Cell cell)
    {
        AddNeighbor(direction, cell);
        LinkCell(cell);
    }

    public bool HasNeighbor(Direction direction) => _neighbors.ContainsKey(direction);

    public bool HasLinkedNeighbor(Direction direction) =>
        _neighbors.ContainsKey(direction) && IsLinkedTo(GetNeighbor(direction));
    
    public Cell GetNeighbor(Direction direction) => _neighbors[direction];

    public void TryOnNeighbor(Direction direction, Action<Cell> action)
    {
        if (_neighbors.TryGetValue(direction, out var cell)) 
            action(cell);
    }

    #endregion


    #region Cell Linking

    public List<Cell> Links => _links.Keys.ToList();

    public bool IsLinkedTo(Cell cell) => _links.ContainsKey(cell);

    public void LinkCell(Cell cell, bool bidirectional = true)
    {
        _links.Add(cell, true);
        if (bidirectional) cell.LinkCell(this, false);
    }

    public void UnlinkCell(Cell cell, bool bidirectional = true)
    {
        _links.Remove(cell);
        if (bidirectional) cell.UnlinkCell(this, false);
    }

    #endregion
}