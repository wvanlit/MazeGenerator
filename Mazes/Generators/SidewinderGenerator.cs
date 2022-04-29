using Helpers;

namespace Mazes.Generators;

public class SidewinderGenerator : IMazeGenerator
{
    public void GenerateOn(Grid grid)
    {
        foreach (var row in grid.AllRows)
        {
            var run = new List<Cell>();

            foreach (var cell in row)
            {
                run.Add(cell);

                var atEasternBoundary = !cell.HasNeighbor(Direction.East);
                var atNorthernBoundary = !cell.HasNeighbor(Direction.North);

                var shouldCloseOut = atEasternBoundary || (!atNorthernBoundary && grid.Rng.Next(2) == 0);

                if (shouldCloseOut)
                {
                    var member = RandomHelper.Sample(grid.Rng, run);
                    member.TryOnNeighbor(Direction.North, c => c.LinkCell(member));
                    run.Clear();
                }
                else
                {
                    cell.LinkCell(cell.GetNeighbor(Direction.East));
                }
            }
        }
    }
}