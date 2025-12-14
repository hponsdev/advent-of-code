namespace AdventOfCode2025.Day06_12
{
    internal class SolutionDay6
    {
        private string[] _input;
        private const string INPUT_FILE_PATH = @"D:\Hugo\source\advent-of-code\2025\AdventOfCode2025\Day06-12\adventofcode-input6-a.txt";

        internal SolutionDay6()
        {
            if (!File.Exists(INPUT_FILE_PATH))
                throw new Exception("Impossible de lire le fichier, fichier non existant");

            _input = File.ReadAllLines(INPUT_FILE_PATH);
        }

        internal void SolveFirstExercise()
        {
            long result = 0;

            int problemsLength = _input.Length - 1;
            var numberOfProblems = _input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Count();
            var problems = new int[numberOfProblems, problemsLength];

            for (int i = 0; i < problemsLength; i++)
            {
                var values = _input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < values.Length; j++) 
                {
                    problems[j, i] = int.Parse(values[j]);
                }
            }

            var operations = _input[problemsLength].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int k = 0; k < operations.Length; k++)
            {
                long resultTemp = operations[k] == "+" ? 0 : 1;
                for (int l = 0; l < problemsLength; l++)
                {
                    if (operations[k] == "+")
                        resultTemp += problems[k, l];
                    else
                        resultTemp *= problems[k, l];
                }
                result += resultTemp;
            }

            Console.WriteLine(result);
        }
    }
}
