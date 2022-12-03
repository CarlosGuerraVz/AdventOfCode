using System.Collections;

namespace Day3_Rucksack_Reorganization
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

        static int CharToPriority(char c)
        {
            int position = (int)(c - 'a');
            position = position < 0 ? position + 58: position;
            return position;
        }

        static BitArray RegisterChars(string compartment)
        {
            BitArray foundInComp = new BitArray(52);

            foreach (char c in compartment)
            {
                int priority = CharToPriority(c);
                foundInComp[priority] = true;
            }
            return foundInComp;
        }

        void Part1()
        {
            int sum = 0;

            FileReader(filePath, (string line, int index) =>
            {
                if (line.Length % 2 != 0)
                {
                    throw new Exception("Uneven number of items");
                }
                int compSize = line.Length / 2;
                

                //int[] foundInBothCount = new int[52];
                BitArray foundInFirstComp = RegisterChars(line.Substring(0, compSize));
                BitArray foundInSecondComp = RegisterChars(line.Substring(compSize));

                foundInFirstComp.And(foundInSecondComp);

                for (int i = 0; i < 52; i++)
                {
                    if (foundInFirstComp[i]) sum += i + 1; 
                }
            });
            Console.WriteLine(sum);
        }

        void Part2()
        {
            int sum = 0;
            string[] sacksContents = new string[3];

            FileReader(filePath, (string line, int index) =>
            {
                int elfNum = (index + 1) % 3;
                sacksContents[elfNum] = line;

                //process the 3 elfs
                if (elfNum == 0)
                {
                    BitArray foundInFirstElf = RegisterChars(sacksContents[0]);
                    BitArray foundInSecondElf = RegisterChars(sacksContents[1]);
                    BitArray foundInThirdElf = RegisterChars(sacksContents[2]);

                    foundInFirstElf.And(foundInSecondElf.And(foundInThirdElf));

                    for (int i = 0; i < 52; i++)
                    {
                        if (foundInFirstElf[i]) sum += i + 1;
                    }
                }
            });
            Console.WriteLine(sum);
        }

        static void Main(string[] args)
        {
            Program p = new();
            p.Part2();
        }
    }
}