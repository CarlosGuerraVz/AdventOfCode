namespace Day2_RockPaperScissors
{
    internal class Program
    {
        private const string filePath = "input.txt";

        private static int[,] matchTable = new int[,]{
               //X,   Y,   Z
          /*A*/ {1+3, 2+6, 3+0},
          /*B*/ {1+0, 2+3, 3+6},
          /*C*/ {1+6, 2+0, 3+3},
            };

        private static int[,] matchTable2 = new int[,]{
               //X,   Y,   Z
          /*A*/ {3+0, 1+3, 2+6},
          /*B*/ {1+0, 2+3, 3+6},
          /*C*/ {2+0, 3+3, 1+6},
            };

        static void FileReader(string filePath, Action<String> loopHandler)
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
        static int StrToInt(string str)
        {
            char c = (char)str[0];
            if (c > 'C')
            {
                return c - 'X';
            }
            else
            {
                return c - 'A';
            }
        }

        void Run(int[,] matchTable)
        {
            int sum = 0;
            FileReader(filePath, (string line) =>
            { 
                string[] inputs = line.Split(' ');
                int oponent = StrToInt(inputs[0]);
                int player = StrToInt(inputs[1]);

                if(oponent > 2 || oponent < 0 || player > 2 || player < 0)
                {
                    throw new Exception("##Invalid play index");
                }

                sum += matchTable[oponent,player];
            });
            Console.WriteLine(sum);
        }

        static void Main(string[] args)
        {
            Program p = new();
            p.Run(matchTable);//part1
            p.Run(matchTable2);//part2
        }
    }
}