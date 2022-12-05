using System.Collections;

namespace Day4_Camp_Cleanup
{
    internal class Program
    {
        private const string filePath = "input.txt";

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

        bool RangeIsInclusive(string[] ranges)
        {
            int[] range1 = Array.ConvertAll(ranges[0].Split('-'), (s) => int.Parse(s));
            int[] range2 = Array.ConvertAll(ranges[1].Split('-'), (s) => int.Parse(s));

            if ((range1[0] <= range2[0] && range1[1] >= range2[1]) || (range2[0] <= range1[0] && range2[1] >= range1[1]))
            {
                return true;
            }
            return false;
        }
        bool RangeIsInConflict(string[] ranges)
        {
            int[] range1 = Array.ConvertAll(ranges[0].Split('-'), (s) => int.Parse(s));
            int[] range2 = Array.ConvertAll(ranges[1].Split('-'), (s) => int.Parse(s));

            if (
                (range1[0] >= range2[0] && range1[0] <= range2[1]) ||
                (range1[1] >= range2[0] && range1[1] <= range2[1]) ||
                (range2[0] >= range1[0] && range2[0] <= range1[1]) ||
                (range2[1] >= range1[0] && range2[1] <= range1[1]) 
                )
            {
                return true;
            }
            return false;
        }


        void Run()
        {
            int sum = 0;
            FileReader(filePath, (line, index) =>
            {
                string[] ranges = line.Split(',');
                //if (RangeIsInclusive(ranges))
                if (RangeIsInConflict(ranges))
                {
                    sum ++;
                }
            });
            Console.WriteLine(sum);
        }

        static void Main(string[] args)
        {
            Program p = new();
            p.Run();
        }
    }
}