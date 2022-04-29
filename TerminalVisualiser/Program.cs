using Mazes;
using Mazes.Generators;
using Mazes.Visualisers;

var grid = new Grid(6, 10);
var generator = new SidewinderGenerator();

generator.GenerateOn(grid);

// Add Entrance & Exit at the outside of the maze
var start = grid.CellAt(0, 0)!;
var end = grid.CellAt(grid.Rows - 1, grid.Columns - 1)!;

start.AddLinkedNeighbor(Direction.North, new Cell(-1, -1));
end.AddLinkedNeighbor(Direction.South, new Cell(-1, -1));

var visualiser = new SvgVisualiser();
var textVis = new UnicodeTextVisualiser();

Console.WriteLine(textVis.Draw(grid));
visualiser.DrawToFile(grid, "D:\\Programming\\Projects\\MazeGenerator\\TerminalVisualiser\\maze.svg");