namespace AdventOfCode2025.Day01_12
{
    internal class SolutionDay1
    {
        private string[] _input;
        private const string INPUT_FILE_PATH = @"C:\PRIV\Projects\advent\AdventOfCode2025\AdventOfCode2025\Day01-12\adventofcode-input1-a.txt";

        internal SolutionDay1()
        {
            if (!File.Exists(INPUT_FILE_PATH))
                throw new Exception("Impossible de lire le fichier, fichier non existant");

            _input = File.ReadAllLines(INPUT_FILE_PATH);
        }

        internal void SolveFirstExercise()
        {
            int dialState = 100000050;
            int result = 0;

            foreach (string rotation in _input)
            {
                int distance = int.Parse(rotation.Substring(1));
                distance = rotation[0] == 'L' ? - distance : distance;

                dialState += distance;

                if (dialState % 100 == 0)
                {
                    result++;
                }
            }

            Console.WriteLine(result);
        }

        internal void SolveSecondExercise()
        {
            // Ne fonctionne pas, retourne un résultat plus grand que la méthode naive (juste en-dessous)
            // Il faudrait exécuter les deux en parallèle et voir à quel moment le result diverge
            // Je m'arrête là pour commencer l'exercice 2 
            int dialState = 100000050;
            int result = 0;

            foreach (string rotation in _input)
            {
                int distance = int.Parse(rotation.Substring(1));
                distance = rotation[0] == 'L' ? -distance : distance;

                int realdialState = dialState % 100;
                dialState += distance;
                int tempResult = realdialState + distance;

                if (tempResult == 0)
                {
                    result++;
                }
                else if (tempResult > 0)
                {
                    int numberOfZeros = tempResult / 100;
                    result += numberOfZeros;
                }
                else
                {
                    tempResult = -tempResult;
                    int numberOfZeros = (tempResult / 100) + 1;
                    result += numberOfZeros;
                }
            }

            Console.WriteLine(result);
        }

        internal void SolveSecondExerciseSlow()
        {
            int dialState = 50;
            int result = 0;

            foreach (string rotation in _input)
            {
                int distance = int.Parse(rotation.Substring(1));
                bool isRight = rotation[0] == 'R';

                while (distance > 0)
                {
                    dialState = isRight ? dialState + 1 : dialState - 1;

                    if (dialState == 100)
                        dialState = 0;

                    if (dialState == -1)
                        dialState = 99;

                    if (dialState == 0)
                        result++;

                    distance--;
                }
            }

            Console.WriteLine(result);
        }
    }
}
