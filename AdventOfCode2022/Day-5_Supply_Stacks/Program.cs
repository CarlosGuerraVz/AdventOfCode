using System.Text.RegularExpressions;

namespace Day_5_Supply_Stacks
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

        void ReadStackLine(Stack<char>[] stacks, string line, int stacksNum)
        {
            for(int i =0; i < stacksNum; i++) 
            {
                char c = line[(i * 4) + 1];
                if (c == '1') return;
                if (c != 32) stacks[i].Push(c);
            }
        }

        Stack<char> ReverseStack(Stack<char> stack) 
        {
            Stack<char> temp = new();
            while (stack.Count != 0)
            {
                temp.Push(stack.Pop());
            }
            return temp;
        }

        int[] ParseRule(String line)
        {
            string[] parsed = Regex.Split(line, @"\D+");
            parsed = parsed.Skip(1).ToArray();
            int[] values = Array.ConvertAll(parsed, (s) => int.Parse(s));

            return values;
        }

        void RunRulePart1(int[] rule, Stack<char>[] stacks)
        {
            for (int i = 0; i < rule[0]; i++)
            {
                stacks[rule[2] - 1].Push(stacks[rule[1] - 1].Pop());
            }
        }

        void RunRulePart2(int[] rule, Stack<char>[] stacks)
        {
            Stack<char> temp = new();

            for (int i = 0; i < rule[0]; i++)
            {
                temp.Push(stacks[rule[1] - 1].Pop());
            }
            for (int i = 0; i < rule[0]; i++)
            {
                stacks[rule[2] - 1].Push(temp.Pop());
            }
        }

        void Part1()
        {
            string result = "";
            int stacksNum = 0;
            bool readingRules = false;
            Stack<char>[] stacks = null;

            FileReader(filePath, (line, index) =>
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    if (!readingRules)
                    {
                        if (index == 0)
                        {
                            stacksNum = (line.Length + 1) / 4;

                            //initialize stacks
                            stacks = new Stack<char>[stacksNum];
                            for (int i = 0; i < stacksNum; i++)
                            {
                                stacks[i] = new();
                            }
                        }

                        ReadStackLine(stacks, line, stacksNum);
                    }
                    else
                    {
                        int[] rule = ParseRule(line);

                        //RunRulePart1(rule, stacks);
                        RunRulePart2(rule, stacks);
                    }
                }
                else
                {
                    if (!readingRules)
                    {
                        readingRules = true;
                        for (int i = 0; i < stacksNum; i++)
                        {
                            stacks[i] = ReverseStack(stacks[i]);
                        }
                    }
                }
            });

            for (int i = 0; i < stacksNum; i++)
            {
                result += stacks[i].Peek();
            }

            Console.WriteLine(result);
        }

        static void Main(string[] args)
        {
            Program p = new();
            p.Part1();
        }
    }
}