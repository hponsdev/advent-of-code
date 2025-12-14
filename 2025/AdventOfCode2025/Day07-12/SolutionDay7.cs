namespace AdventOfCode2025.Day07_12
{
    internal class SolutionDay7
    {
        private char[][] _input;
        private string[][] _inputS;
        private const string INPUT_FILE_PATH = @"D:\Hugo\source\advent-of-code\2025\AdventOfCode2025\Day07-12\adventofcode-input7-a.txt";
        //private const string INPUT_FILE_PATH = @"D:\Hugo\source\advent-of-code\2025\AdventOfCode2025\Day07-12\test-input.txt";

        private int _sIndex;
        Dictionary<(int, int), long> _memo = new();

        internal SolutionDay7()
        {
            if (!File.Exists(INPUT_FILE_PATH))
                throw new Exception("Impossible de lire le fichier, fichier non existant");

            string[] lines = File.ReadAllLines(INPUT_FILE_PATH);
            _input = new char[lines.Length][];
            _inputS = new string[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                _input[i] = lines[i].ToCharArray();
                _inputS[i] = lines[i].Select(c => c.ToString()).ToArray();
            }
        }

        internal void SolveFirstExercise()
        {
            long result = 0;
            int lineLength = _input[0].Length;

            for (int i = 0; i < _input.Length - 1; i++)
            {
                for (int j = 0; j < lineLength; j++)
                {
                    if (_input[i][j] == 'S')
                    {
                        _input[i + 1][j] = 'b';
                        _sIndex = j;
                        _input[i][j] = 'b'; // Utile pour simplifier le tableau pour l'exercice 2
                    }
                    else if (_input[i][j] == 'b')
                    {
                        if (_input[i + 1][j] == '.' || _input[i + 1][j] == 'b')
                        {
                            _input[i + 1][j] = 'b';
                        }
                        else
                        {
                            result++;
                            _input[i + 1][j - 1] = 'b';
                            _input[i + 1][j + 1] = 'b';
                        }
                    }
                }
            }

            Console.WriteLine(result);
        }

        internal void SolveSecondExercise()
        {
            SolveFirstExercise();
            Console.WriteLine(CountPathsFromNode(0, _sIndex));
        }

        //internal void SolveSecondExercise()
        //{
        //    int lineCount = _inputS.Length;
        //    int lineLength = _inputS[0].Length;

        //    long[,] values = new long[lineCount, lineLength];

        //    for (int j = 0; j < lineLength; j++)
        //    {
        //        if (_inputS[0][j] == "S")
        //            values[0,j] = 1;
        //        else
        //            values[0,j] = 0;
        //    }

        //    for (int i = 0; i < _inputS.Length - 1; i++)
        //    {
        //        for (int j = 0; j < lineLength; j++)
        //        {       
        //            if (_inputS[i + 1][j] == ".")
        //            {
        //                values[i + 1, j] += values[i, j];
        //            }
        //            else if (_inputS[i + 1][j] == "^")
        //            {
        //                values[i + 1, j - 1] += values[i, j];
        //                values[i + 1, j + 1] += values[i, j];
        //            }
        //        }
        //    }

        //    long result = 0;
        //    for (int k = 0; k < lineLength; k++)
        //    {
        //        result += values[lineCount - 1, k];
        //    }

        //    Console.WriteLine(result);
        //}

        /// <summary>
        /// Solution avec récursivié. Temps d'exécution beaucoup trop long
        /// Avec mémoisation, sûrement viable
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private long CountPathsFromNode(int i, int j)
        {
            if (i == _input.Length - 1)
            {
                return 1;
            }

            if (_memo.TryGetValue((i, j), out long result))
            {
                return result;
            }

            if (_input[i + 1][j] == 'b')
                return CountPathsFromNode(i + 1, j);

            long result2 = CountPathsFromNode(i + 1, j - 1) + CountPathsFromNode(i + 1, j + 1);
            _memo[(i, j)] = result2;
            return result2;
        }
    }
}
