using System;
using System.Collections.Generic;
using System.IO;
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
                if (args.Length < 1 || String.IsNullOrWhiteSpace(args[0]))
                {
                    Console.WriteLine(Resources.NoFileSpecified);
                }
                else
                {
                    var maze = Maze.Parse(args[0]);
                    maze.TrySolvePath();
                    Console.WriteLine(maze.ToString());
                }
                Console.ReadLine();
            }
            catch(FileNotFoundException exc)
            {
                Console.WriteLine(Resources.CouldNotReadFile);
                Console.ReadLine();
            }
            catch(Exception exc)
            {
                Console.WriteLine(Resources.UnexpectedErrorOccurred + Environment.NewLine + exc.ToString());
                Console.ReadLine();
            }
        }
    }
}
