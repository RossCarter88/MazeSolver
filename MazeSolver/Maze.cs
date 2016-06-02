using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    public class Maze
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        public MazePoint[][] MazePoints { get; set; }
        private MazeSolution _mazeSolution;
        /// <summary>
        /// Creates a Maze from the specified file path
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static Maze Parse(string _path)
        {
            if(!File.Exists(_path))
            {
                throw new FileNotFoundException();
            }
            string[] fileLines = System.IO.File.ReadAllLines(_path);
            Maze maze = new Maze();
            // 0 == Width and Height
            maze.ParseWidthAndHeight(fileLines[0]);
            // 1 == Start X and Y
            maze.Start = ParsePoint(fileLines[1]);
            // 2 == End X and Y
            maze.End = ParsePoint(fileLines[2]);
            // 3 onwards is Maze details
            maze.MazePoints = new MazePoint[maze.Height][];
            // Parse each line and add to jagged array
            int mazeYIndex = 0;
            for (int i = 3; i < fileLines.Length; i++ )
            {
                var rowPoints = new MazePoint[maze.Width];
                int[] passableFlags = ParseLine(fileLines[i]);
                for (int x = 0; x < maze.Width; x++)
                {
                    rowPoints[x] = new MazePoint(x, mazeYIndex, passableFlags[x] == 1);
                }
                maze.MazePoints[mazeYIndex] = rowPoints;
                mazeYIndex++;
            }
            return maze;
        }
        #region Parsing Helper Methods
        private void ParseWidthAndHeight(string line)
        {
            int[] parsedInts = ParseLine(line);
            Width = parsedInts[0];
            Height = parsedInts[1];
        }
        private static Point ParsePoint(string line)
        {
            int[] parsedInts = ParseLine(line);
            Point returned = new Point(parsedInts[0], parsedInts[1]);
            return returned;
        }
        private static int[] ParseLine(string line)
        {
            string[] lineElements = line.Split(new string [] {" "}, StringSplitOptions.RemoveEmptyEntries);
            List<int> parsedLineElements = new List<int>();
            foreach(var lineValue in lineElements)
            {
                parsedLineElements.Add(int.Parse(lineValue));
            }
            return parsedLineElements.ToArray();
        }
        #endregion
        /// <summary>
        /// Initiates an attempt at solving the maze
        /// </summary>
        public void TrySolvePath()
        {
            _mazeSolution = new MazeSolution(this);
            _mazeSolution.AttemptSolution();
        }
        /// <summary>
        /// Creates and returns a string representation of the current Maze
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if(_mazeSolution == null || !_mazeSolution.IsSolved)
            {
                return Resources.CannotBeSolved;
            }
            StringBuilder sb = new StringBuilder();
            foreach(MazePoint[] row in MazePoints)
            {
                foreach(MazePoint point in row)
                {
                    sb.Append(point.ToString(this));
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
        
    }
}
