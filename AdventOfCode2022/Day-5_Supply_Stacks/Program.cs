namespace Day_5_Supply_Stacks
{
    internal class Program
    {
        private const string filePath = "input2.txt";

        static void FileReader(string filePath, Action<String, int> loopHandler)
        {
            try
            {
                using StreamReader reader = new(filePath);
                String? line;
                int index = 0;
                while ((line = reader?.ReadLine()) != null)
                {
                    loopHandler(line, index);
                    index++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void Part1()
        {
            int sum = 0;
            FileReader(filePath, (line, index) =>
            {
                
            });
            Console.WriteLine(sum);
        }

        static void Main(string[] args)
        {
            Program p = new();
            p.Part1();
        }
    }
}