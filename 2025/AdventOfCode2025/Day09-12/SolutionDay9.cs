using System;

namespace AdventOfCode2025.Day09_12
{
    internal class SolutionDay9
    {
        private string[] _input;
        private const string INPUT_FILE_PATH = @"D:\Hugo\source\advent-of-code\2025\AdventOfCode2025\Day09-12\adventofcode-input9-a.txt";
        //private const string INPUT_FILE_PATH = @"D:\Hugo\source\advent-of-code\2025\AdventOfCode2025\Day09-12\test-input.txt";

        internal SolutionDay9()
        {
            if (!File.Exists(INPUT_FILE_PATH))
                throw new Exception("Impossible de lire le fichier, fichier non existant");

            _input = File.ReadAllLines(INPUT_FILE_PATH);
        }

        internal void SolveFirstExercise()
        {
            // Méthode naive, pour tester :
            long result = 0;

            var redTiles = new List<RedTile>();

            foreach (var row in _input) 
            {
                string[] coords = row.Trim().Split(',');
                redTiles.Add(new RedTile(int.Parse(coords[0]), int.Parse(coords[1])));
            }

            for (int i = 0; i < redTiles.Count - 1; i++)
            {
                var tile = redTiles[i];
                for (int j = i + 1; j < redTiles.Count; j++) 
                {
                    var otherTile = redTiles[j];
                    long area = GetRectangleArea(tile.X, tile.Y, otherTile.X, otherTile.Y);
                    if (area > result)
                        result = area;
                }
            }
   
            Console.WriteLine(result);
        }

        internal void SolveSecondExercise()
        {
            // Méthode naive, pour tester :
            long result = 0;

            long[,] grid = CreateGrid();
            ShowGrid(grid);

            // Remplissage des tuiles vertes
            grid = FloodFill(grid, 9, 4);

            ShowGrid(grid);
            Console.WriteLine(result);
        }

        private long GetRectangleArea(int x1, int y1, int x2, int y2) 
        { 
            return (Math.Abs(x1 - x2) + 1L) * (Math.Abs(y1 - y2) + 1L);
        }

        private long[,] CreateGrid()
        {
            var redTiles = new List<RedTile>();
            int maxX = 0;
            int maxY = 0;

            foreach (var row in _input)
            {
                string[] coords = row.Trim().Split(',');
                int x = int.Parse(coords[0]);
                int y = int.Parse(coords[1]);
                if (x > maxX) maxX = x;
                if (y > maxY) maxY = y;
                redTiles.Add(new RedTile(x, y));
            }

            // Création des contours des tuiles vertes
            long[,] grid = new long[maxX + 1, maxY + 1]; // 0 = vide, 1 = vert, 2 = rouge

            int lastTileX = redTiles[^1].X;
            int lastTileY = redTiles[^1].Y;
            grid[lastTileX, lastTileY] = 2;

            for (int i = 0; i < redTiles.Count; i++)
            {
                var tile = redTiles[i];
                if (tile.X == lastTileX)
                {
                    int startY = Math.Min(tile.Y, lastTileY);
                    int endY = Math.Max(tile.Y, lastTileY);
                    for (int j = startY + 1; j < endY; j++)
                    {
                        grid[tile.X, j] = 1;
                    }
                }
                else if (tile.Y == lastTileY)
                {
                    int startX = Math.Min(tile.X, lastTileX);
                    int endX = Math.Max(tile.X, lastTileX);
                    for (int j = startX + 1; j < endX; j++)
                    {
                        grid[j, tile.Y] = 1;
                    }
                }
                else
                {
                    throw new ArgumentException("tiles non alignées");
                }
                grid[tile.X, tile.Y] = 2;

                lastTileX = tile.X;
                lastTileY = tile.Y;
            }

            return grid;
        }

        private long[,] FloodFill(long[,] grid, int startX, int startY)
        {
            int n = grid.GetLength(0);
            int m = grid.GetLength(1);

            var stack = new Stack<(int x, int y)>();
            stack.Push((startX, startY));

            while (stack.Count > 0)
            {
                var (x, y) = stack.Pop();

                if (x < 0 || x >= n || y < 0 || y >= m)
                    continue;

                if (grid[x, y] == 1 || grid[x, y] == 2)
                    continue;

                grid[x, y] = 1;

                stack.Push((x + 1, y));
                stack.Push((x - 1, y));
                stack.Push((x, y + 1));
                stack.Push((x, y - 1));
            }

            return grid;
        }

        private void ShowGrid(long[,] grid)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                string line = "";
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    line += grid[x, y];
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    internal class RedTile
    {
        internal int X { get; set; }

        internal int Y { get; set; }

        internal RedTile(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
