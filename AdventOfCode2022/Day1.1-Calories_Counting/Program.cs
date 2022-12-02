namespace Day1_Calories_Counting
{
    internal class Program
    {
        private const string filePath = "input.txt";

        void FileReader(string filePath, Action<String> loopHandler)
        {
            try
            {
                using StreamReader reader = new(filePath);
                String? line;
                while ((line = reader?.ReadLine()) != null)
                {
                    loopHandler(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void PartOne()
        {
            int currentCalories = 0;
            int maxCalories = 0;

            FileReader(filePath, (string line) =>
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    maxCalories = currentCalories > maxCalories ? currentCalories : maxCalories;
                    currentCalories = 0;
                }
                else
                {
                    currentCalories += int.Parse(line);
                }
            });
            maxCalories = currentCalories > maxCalories ? currentCalories : maxCalories;
            Console.WriteLine(maxCalories);
        }

        void PartTwo()
        {
            int[] top3MaxCalories = { 0, 0, 0 };
            int currentCalories = 0;

            void CompareAndUpdateMax()
            {
                int biggestDiference = 0;
                int biggestDiferenceIndex = -1;

                for (int i = 0; i < 3; i++)
                {
                    int difference = currentCalories - top3MaxCalories[i];
                    if (difference > 0 && difference > biggestDiference)
                    {
                        biggestDiference = difference;
                        biggestDiferenceIndex = i;
                    }
                }
                if (biggestDiferenceIndex >= 0)
                {
                    top3MaxCalories[biggestDiferenceIndex] = currentCalories;
                }
                currentCalories = 0;
            }

            FileReader(filePath, (string line) =>
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    CompareAndUpdateMax();
                }
                else
                {
                    currentCalories += int.Parse(line);
                }
            });
            CompareAndUpdateMax();
            Console.WriteLine(top3MaxCalories.Sum());
        }

        static void Main(string[] args)
        {
            new Program().PartTwo();
        }
    }
}