namespace AdventOfCode2025.Day05_12
{
    internal class SolutionDay5
    {
        private string[] _input;
        private const string INPUT_FILE_PATH = @"D:\Hugo\source\advent-of-code\2025\AdventOfCode2025\Day05-12\adventofcode-input5-a.txt";

        internal SolutionDay5()
        {
            if (!File.Exists(INPUT_FILE_PATH))
                throw new Exception("Impossible de lire le fichier, fichier non existant");

            _input = File.ReadAllLines(INPUT_FILE_PATH);
        }

        internal void SolveFirstExercise()
        {
            int result = 0;

            var freshRanges = new List<(long start, long end)>();

            string inputLine = _input[0];
            int i = 0;

            while (!string.IsNullOrWhiteSpace(inputLine))
            {
                string[] range = inputLine.TrimEnd().Split('-');
                freshRanges.Add((long.Parse(range[0]), long.Parse(range[1])));
                i++;
                inputLine = _input[i];
            }

            for (int j = i + 1; j < _input.Length; j++)
            {
                long id = long.Parse(_input[j].Trim());
                if (freshRanges.Any(r => r.start <= id && id <= r.end))
                    result++;
            }

            Console.WriteLine(result);
        }

        internal void SolveSecondExercise()
        {
            var freshSet = new HashSet<long>();
            var freshRanges = new List<(long start, long end)>();

            string inputLine = _input[0];
            int i = 0;

            while (!string.IsNullOrWhiteSpace(inputLine))
            {
                string[] range = inputLine.TrimEnd().Split('-');
                long start = long.Parse(range[0]);
                long end = long.Parse(range[1]);

                for (long j = start; j <= end; j++)
                {
                    freshSet.Add(j);
                }
                //freshRanges.Add((long.Parse(range[0]), long.Parse(range[1])));
                i++;
                inputLine = _input[i];
            }


            Console.WriteLine(freshSet.Count);
        }
    }
}
