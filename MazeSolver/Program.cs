using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var maze = Maze.Parse("medium_input.txt");
                maze.TrySolvePath();
                Console.WriteLine(maze.ToString());
                Console.ReadLine();
            }
            catch(Exception exc)
            {

            }
        }
    }
}
