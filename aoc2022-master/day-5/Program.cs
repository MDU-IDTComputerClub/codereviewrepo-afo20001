using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day_5_solution
{
    class MoveProcedure
    {
        public int Count { get; }
        public int Source { get; }
        public int Destination { get; }

        public MoveProcedure (int count, int source, int destination)
        {
            Count = count;
            Source = source - 1;
            Destination = destination - 1;
        }

        //Not done
        //Try using LINQ
        //Call above constructor??
        public MoveProcedure (string plaintextProcedure)
        {
            string[] temp = plaintextProcedure.Split(" ");

            Count = int.Parse(temp[1]);
            Source = int.Parse(temp[3]) - 1;
            Destination = int.Parse(temp[5]) - 1;
        }
    }
    class Cargo
    {
        Stack<char>[] Stacks;
        
        public Cargo(string fileLocation)
        {
            List<string> cargoLayout = extractCargoLayoutFromFile(fileLocation);

            int stackCount = (cargoLayout[0].Length + 1) / 4;
            initializeFields(stackCount);

            populateStacksWith(cargoLayout);
        }

        //not done
        public LinkedList<char> GetTopCrates()
        {
            LinkedList<char> topCrates = new LinkedList<char>();

            foreach (Stack<char> stack in Stacks)
            {
                topCrates.AddLast(stack.Peek());
            }

            return topCrates;
        }

        public void MoveCrates (MoveProcedure moveProcedure )
        {            
            for (int i = 1; i <= moveProcedure.Count ; i++)
            {
                Stacks[moveProcedure.Destination].Push(Stacks[moveProcedure.Source].Pop());
            }
        }

        private void printCargoLayout()
        {
            //implement?
        }

        private List<string> extractCargoLayoutFromFile(string fileLocation)
        {
            IEnumerator<string> lines = File.ReadLines(fileLocation).GetEnumerator();
            List<string> cargoLayout = new List<string>();

            while (lines.MoveNext() && lines.Current.Length != 0)
            {
                cargoLayout.Add(lines.Current);
            }

            //remove last line with unnecessary data 
            cargoLayout.RemoveAt(cargoLayout.Count - 1);

            return cargoLayout;
        }

        private void initializeFields(int stackCount)
        {
            Stacks = new Stack<char>[stackCount];

            for (int i = 0; i < stackCount; i++)
            {
                Stacks[i] = new Stack<char>();
            }
        }

        
        private void populateStacksWith(List<string> cargoLayout)
        {
            for (int i = cargoLayout.Count - 1; i >= 0; i--)
            {
                for (int j = 1; j < cargoLayout[i].Length; j += 4)
                {
                    if (char.IsLetter(cargoLayout[i][j]))
                    {
                        Stacks[j/4].Push(cargoLayout[i][j]);
                    }
                }
            }
        }
    }
    

    class Program
    {
        static void Main(string[] args)
        {
            Cargo cargo = new Cargo(@"..\..\..\..\input");            

            foreach (var line in File.ReadLines(@"..\..\..\..\input"))
            {
                if (line.Length != 35 && line.Length != 0)
                {
                    cargo.MoveCrates(new MoveProcedure(line));
                }
            }

            //everyting from here and marked not done
            var apa = cargo.GetTopCrates();

            foreach (char c in apa)
            {
                Console.Write(c);
            }
        }
    }
}
