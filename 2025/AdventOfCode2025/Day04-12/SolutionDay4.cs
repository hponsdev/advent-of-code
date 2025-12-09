namespace AdventOfCode2025.Day04_12
{
    internal class SolutionDay4
    {
        private string[] _input;
        private const string INPUT_FILE_PATH = @"D:\Hugo\source\advent-of-code\2025\AdventOfCode2025\Day04-12\adventofcode-input4-a.txt";

        internal SolutionDay4()
        {
            if (!File.Exists(INPUT_FILE_PATH))
                throw new Exception("Impossible de lire le fichier, fichier non existant");

            _input = File.ReadAllLines(INPUT_FILE_PATH);
        }

        internal void SolveFirstExercise()
        {
            int result = 0;

            int width = _input[0].Length;
            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    char c = _input[i][j];

                    if (c == '@' && IsThisRollAccessible(i, j, width))
                        result++;
                }
            }
            Console.WriteLine(result);
        }

        private bool IsThisRollAccessible(int iRoll, int jRoll, int width)
        {
            int neighbors = 0;

            for (int i = iRoll - 1; i <= iRoll + 1; i++)
            {
                for (int j = jRoll - 1; j <= jRoll + 1; j++)
                {
                    bool doWeCalculate = true;
                    if (i < 0 || i >= _input.Length || j < 0 || j >= width || (i == iRoll && j == jRoll))
                        doWeCalculate = false;

                    if (doWeCalculate && _input[i][j] == '@')
                        neighbors++;
                }
            }

            return neighbors < 4;
        }
    }
}
