namespace AdventOfCode2025.Day08_12
{
    internal class SolutionDay8
    {
        private string[] _input;
        private const string INPUT_FILE_PATH = @"D:\Hugo\source\advent-of-code\2025\AdventOfCode2025\Day08-12\adventofcode-input8-a.txt";
        //private const string INPUT_FILE_PATH = @"D:\Hugo\source\advent-of-code\2025\AdventOfCode2025\Day08-12\test-input.txt";

        internal SolutionDay8()
        {
            if (!File.Exists(INPUT_FILE_PATH))
                throw new Exception("Impossible de lire le fichier, fichier non existant");

            _input = File.ReadAllLines(INPUT_FILE_PATH);
        }

        internal void SolveFirstExercise()
        {
            long result = 0;
            
            // Création des objets JunctionBox et stockage dans une liste
            var junctionBoxes = new List<JunctionBox>();
            for (int i = 0; i < _input.Length; i++)
            {
                var coords = _input[i].Trim().Split(',');
                var junctionBox = new JunctionBox(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2]), i);
                junctionBoxes.Add(junctionBox);
            }

            // Création d'une liste des distances entre chaque box, qu'on pourra trier par la suite
            var distances = new List<JunctionBoxCouple>();
            for (int i = 0; i < _input.Length; i++)
            {
                var junctionBox = junctionBoxes[i];

                for (int j = i + 1; j < _input.Length; j++)
                {
                    var otherBox = junctionBoxes[j];
                    var couple = new JunctionBoxCouple(junctionBox, otherBox);
                    distances.Add(couple);
                }
            }

            distances = distances.OrderBy(d => d.Distance).ToList();

            // Création des circuits, à partir d'un IdCircuit défini dans chaque Box
            const int NUMBER_OF_PAIRS_TO_DO = 1000;
            for (int d = 0; d < NUMBER_OF_PAIRS_TO_DO; d++)
            {
                var distance = distances[d];

                int idSecondBox = distance.SecondBox.IdCircuit;
                junctionBoxes.Where(j => j.IdCircuit == idSecondBox).ToList().ForEach(j => j.IdCircuit = distance.FirstBox.IdCircuit);
            }

            // Comptage du nombre de box par circuits, puis tri décroissant, et calcul du résultat à partir des 3 premiers de la liste
            var circuitsCount = new int[_input.Length];

            for (int i = 0; i < _input.Length; i++)
            {
                circuitsCount[junctionBoxes[i].IdCircuit] += 1;
            }

            circuitsCount = circuitsCount.OrderBy(d => d).Reverse().ToArray();
            result = circuitsCount[0] * circuitsCount[1] * circuitsCount[2];

            Console.WriteLine(result);
        }

        internal void SolveSecondExercise()
        {
            long result = 0;

            // Création des objets JunctionBox et stockage dans une liste
            var junctionBoxes = new List<JunctionBox>();
            for (int i = 0; i < _input.Length; i++)
            {
                var coords = _input[i].Trim().Split(',');
                var junctionBox = new JunctionBox(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2]), i);
                junctionBoxes.Add(junctionBox);
            }

            // Création d'une liste des distances entre chaque box, qu'on pourra trier par la suite
            var distances = new List<JunctionBoxCouple>();
            for (int i = 0; i < _input.Length; i++)
            {
                var junctionBox = junctionBoxes[i];

                for (int j = i + 1; j < _input.Length; j++)
                {
                    var otherBox = junctionBoxes[j];
                    var couple = new JunctionBoxCouple(junctionBox, otherBox);
                    distances.Add(couple);
                }
            }

            distances = distances.OrderBy(d => d.Distance).ToList();
            long[] circuitsCount = Enumerable.Repeat(1L, _input.Length).ToArray();
            JunctionBoxCouple lastDistance = null;

            int d = 0;
            while (lastDistance == null && d < distances.Count)
            {
                var distance = distances[d];

                int idSecondBoxId = distance.SecondBox.IdCircuit;

                if (idSecondBoxId != distance.FirstBox.IdCircuit)
                {
                    var secondBoxCircuit = junctionBoxes.Where(j => j.IdCircuit == idSecondBoxId).ToList();

                    circuitsCount[distance.FirstBox.IdCircuit] += secondBoxCircuit.Count;
                    circuitsCount[idSecondBoxId] = 0;

                    secondBoxCircuit.ForEach(j => j.IdCircuit = distance.FirstBox.IdCircuit);

                    if (circuitsCount[distance.FirstBox.IdCircuit] >= _input.Length)
                        lastDistance = distance;
                }   

                d++;
                //Console.WriteLine(string.Join(", ", circuitsCount));
            }
            
            result = lastDistance.FirstBox.X * lastDistance.SecondBox.X;

            Console.WriteLine(result);
        }
    }

    internal class JunctionBox
    {
        public int X {  get; set; }

        public int Y {  get; set; }

        public int Z {  get; set; }

        public int IdCircuit { get; set; }

        public JunctionBox(int x, int y, int z, int idCircuit) 
        {
            X = x; 
            Y = y; 
            Z = z;
            IdCircuit = idCircuit;
        }
    }

    internal class JunctionBoxCouple
    {
        public JunctionBox FirstBox {  get; set; }

        public JunctionBox SecondBox { get; set; }

        public double Distance { get; set; }

        public JunctionBoxCouple(JunctionBox firstBox, JunctionBox secondBox)
        {
            FirstBox = firstBox;
            SecondBox = secondBox;
            InitDistance();
        }

        private void InitDistance()
        {
            double distanceX = Math.Pow(FirstBox.X - SecondBox.X, 2);
            double distanceY = Math.Pow(FirstBox.Y - SecondBox.Y, 2);
            double distanceZ = Math.Pow(FirstBox.Z - SecondBox.Z, 2);

            Distance = Math.Sqrt(distanceX + distanceY + distanceZ);
        }
    }
}
