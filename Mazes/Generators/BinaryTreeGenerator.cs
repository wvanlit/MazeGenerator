using Helpers;

namespace Mazes.Generators;

public class BinaryTreeGen : IMazeGenerator
{
    public void GenerateOn(Grid grid)
    {
        foreach (var cell in grid.AllCells)
        {
            var neighbors = new List<Cell>() { };

            cell.TryOnNeighbor(Direction.North, c => neighbors.Add(c));
            cell.TryOnNeighbor(Direction.East, c => neighbors.Add(c));

            if (!neighbors.Any()) continue;
            
            var neighbor = RandomHelper.Sample(grid.Rng, neighbors);

            cell.LinkCell(neighbor);
        }
    }
}