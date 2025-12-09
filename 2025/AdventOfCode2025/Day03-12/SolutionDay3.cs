using System.ComponentModel;

namespace AdventOfCode2025.Day03_12
{
    internal class SolutionDay3
    {
        private string[] _input;
        private const string INPUT_FILE_PATH = @"C:\PRIV\Projects\advent\AdventOfCode2025\AdventOfCode2025\Day03-12\adventofcode-input3-a.txt";

        internal SolutionDay3()
        {
            if (!File.Exists(INPUT_FILE_PATH))
                throw new Exception("Impossible de lire le fichier, fichier non existant");

            _input = File.ReadAllLines(INPUT_FILE_PATH);
        }

        internal void SolveFirstExercise()
        {
            int result = 0;

            foreach (string line in _input)
            {
                result += GetBankJoltage(line);
            }

            Console.WriteLine(result);
        }

        internal void SolveSecondExercise()
        {
            int result = 0;

            foreach (string line in _input)
            {
                result += GetBankJoltage2(line);
            }

            Console.WriteLine(result);
        }

        private int GetBankJoltage(string line)
        {
            int jolt1 = 0;
            int jolt2 = 0;
            int jolt1Index = 0;

            for (int i = 0; i < line.Length - 1; i++)
            {
                int jolt = (int)Char.GetNumericValue(line[i]);
                if (jolt > jolt1)
                {
                    jolt1 = jolt;
                    jolt1Index = i;
                }   
            }

            for (int j = jolt1Index + 1; j < line.Length; j++)
            {
                int jolt = (int)Char.GetNumericValue(line[j]);
                if (jolt > jolt2)
                    jolt2 = jolt;
            }

            return jolt1 * 10 + jolt2;
        }

        private int GetBankJoltage2(string line)
        {
            int joltPrevious = 0;
            int joltTemp = 0;
            int joltPreviousIndex = 0;

            for (int time = 0; time < 12; time++)
            {
                for (int i = joltPreviousIndex; i < line.Length - 11 + time; i++)
                {
                    int jolt = (int)Char.GetNumericValue(line[i]);
                    if (jolt > joltTemp)
                    {
                        joltTemp = jolt;
                        joltPreviousIndex = i;
                    }
                }
            }
            return result;
        }
    }
}
