using System.Linq;

namespace AdventOfCode2025.Day08_12
{
    internal class SolutionDay8
    {
        private string[] _input;
        //private const string INPUT_FILE_PATH = @"D:\Hugo\source\advent-of-code\2025\AdventOfCode2025\Day08-12\adventofcode-input8-a.txt";
        private const string INPUT_FILE_PATH = @"D:\Hugo\source\advent-of-code\2025\AdventOfCode2025\Day08-12\test-input.txt";

        internal SolutionDay8()
        {
            if (!File.Exists(INPUT_FILE_PATH))
                throw new Exception("Impossible de lire le fichier, fichier non existant");

            _input = File.ReadAllLines(INPUT_FILE_PATH);
        }

        internal void SolveFirstExercise()
        {
            long result = 0;

            var circuits = new List<List<JunctionBox>>();
            var distances = new List<JunctionBoxCouple>();

            for (int i = 0; i < _input.Length; i++)
            {
                var coords = _input[i].Trim().Split(',');
                var junctionBox = new JunctionBox(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2]));

                for (int j = i + 1; j < _input.Length; j++)
                {
                    var otherCoords = _input[j].Trim().Split(',');
                    var otherBox = new JunctionBox(int.Parse(otherCoords[0]), int.Parse(otherCoords[1]), int.Parse(otherCoords[2]));
                    var couple = new JunctionBoxCouple(junctionBox, otherBox);
                    distances.Add(couple);
                }
            }

            distances = distances.OrderBy(d => d.Distance).ToList();

            for (int j = 0; j < 10; j++)
            {
                var distance = distances[j];

                if (circuits.Any(c => c.Contains(distance.FirstBox) && c.Contains(distance.SecondBox)))
                    continue;

                if (circuits.Any(c => c.Contains(distance.FirstBox) || c.Contains(distance.SecondBox)))
                {

                } 
                else
                {
                    var circuit = new List<JunctionBox> { distance.FirstBox, distance.SecondBox };
                    circuits.Add(circuit);
                }                   
            }

            Console.WriteLine(result);
        }
    }

    internal class JunctionBox
    {
        public int X {  get; set; }

        public int Y {  get; set; }

        public int Z {  get; set; }

        public JunctionBox(int x, int y, int z) 
        {
            X = x; 
            Y = y; 
            Z = z;
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
