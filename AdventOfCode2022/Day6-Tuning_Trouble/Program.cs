using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace Day6_Tuning_Trouble
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
                    if(String.IsNullOrEmpty(line)) break;
                    loopHandler(line, index);
                    index++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        bool CheckIfMarker(int[] alphabetMask)
        {
            foreach(int i in alphabetMask)
            {
                if (i > 1) return false; 
            }
            return true;
        }

        void Run(int distinctCharactersNum)
        {
            FileReader(filePath, (line, index) =>
            {
                int[] alphabetMask = Enumerable.Repeat(0, 26).ToArray();

                int rightIndex = 0;
                int leftIndex = 0;
                for ( ; rightIndex < line.Length; rightIndex++)
                {
                    int charPos = (int)(line[rightIndex] - 'a');
                    alphabetMask[charPos]++;

                    if (rightIndex >= distinctCharactersNum -1)
                    {
                        if (CheckIfMarker(alphabetMask))
                        {
                            Console.WriteLine(rightIndex+1);
                            Console.WriteLine(line.Substring(leftIndex, distinctCharactersNum));
                            break;
                        }
                        //remove last character as the window moves
                        int leftCharPos = (int)(line[leftIndex] - 'a');
                        alphabetMask[leftCharPos]--;
                        leftIndex++;
                    }
                }
            });
        }

        static void Main(string[] args)
        {
            Program p = new();
            p.Run(14);
        }
    }
}