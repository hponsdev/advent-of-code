namespace AdventOfCode2025.Day02_12
{
    internal class SolutionDay2
    {
        private string[] _input;
        private const string INPUT_FILE_PATH = @"C:\PRIV\Projects\advent\AdventOfCode2025\AdventOfCode2025\Day02-12\adventofcode-input2-a.txt";

        internal SolutionDay2()
        {
            if (!File.Exists(INPUT_FILE_PATH))
                throw new Exception("Impossible de lire le fichier, fichier non existant");

            _input = File.ReadAllLines(INPUT_FILE_PATH);
        }

        internal void SolveFirstExercise()
        {
            string[] idRanges = _input[0].Split(',');
            long result = 0;

            foreach (string idRange in idRanges)
            {
                string[] edges = idRange.Split("-");
                long start = long.Parse(edges[0]);
                long end = long.Parse(edges[1]);

                for (long i = start; i <= end; i++)
                {
                    if (IsThisIntAPalindrome(i))
                        result += i;
                }
            }

            Console.WriteLine(result);
        }

        internal void SolveSecondExercise()
        {
            string[] idRanges = _input[0].Split(',');
            long result = 0;

            foreach (string idRange in idRanges)
            {
                string[] edges = idRange.Split("-");
                long start = long.Parse(edges[0]);
                long end = long.Parse(edges[1]);

                for (long i = start; i <= end; i++)
                {
                    if (IsThisIntAnotherTypeOfPalindrome(i))
                        result += i;
                }
            }

            Console.WriteLine(result);
        }

        private bool IsThisIntAPalindrome(long input)
        {
            string inputString = input.ToString();

            if (inputString.Length % 2 != 0)
                return false;

            int middleIndex = inputString.Length / 2;

            string start = inputString[..middleIndex];
            string end = inputString[middleIndex..];

            return start == end;
        }

        private bool IsThisIntAnotherTypeOfPalindrome(long input)
        {
            string inputString = input.ToString();

            if (inputString.Length == 1)
                return false;

            else if (inputString.Length == 2)
                return IsThisIntAPalindrome(input);

                int middleIndex = inputString.Length / 2;
            string substringTest = "";

            for (int i = 0; i <= middleIndex; i++)
            {
                int curLength = i + 1;
                substringTest += inputString[i];

                if (inputString.Length % curLength == 0)
                {
                    int ratio = inputString.Length / curLength;
                    string computedValue = string.Concat(Enumerable.Repeat(substringTest, ratio));

                    if (computedValue == inputString)
                    {
                        Console.WriteLine(inputString);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
