using Mazes;
using Mazes.Generators;

var grid = new Grid(6, 40);
var generator = new SidewinderGenerator();

generator.GenerateOn(grid);

// Add Entrance & Exit at the outside of the maze
var start = grid.CellAt(0, 0)!;
var end = grid.CellAt(grid.Rows - 1, grid.Columns - 1)!;

start.AddLinkedNeighbor(Direction.North, new Cell(-1, -1));
end.AddLinkedNeighbor(Direction.South, new Cell(-1, -1));

Console.Write(grid.ToString());