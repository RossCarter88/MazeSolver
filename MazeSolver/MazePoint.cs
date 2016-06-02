using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    /// <summary>
    /// Represents a single position with a Maze
    /// </summary>
    public class MazePoint
    {
        public static readonly MazePoint Empty = new MazePoint();
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool HasBeenReached { get; set; }
        public bool IsWall { get; set; }
        public bool IsPartOfSolution { get; set; }
        /// <summary>
        /// Default Constructor - used for Empty record only
        /// </summary>
        private MazePoint()
        {
        }
        /// <summary>
        /// Intiailizes an Instance of Maze Point for the specified X, Y locations and indicating whether or not it is a wall
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="isWall"></param>
        public MazePoint(int x, int y, bool isWall)
        {
            X = x;
            Y = y;
            IsWall = isWall;
        }
        /// <summary>
        /// Returns a boolean indicating whether or not the Drawing.Point has the same X and Y values as the Maze Point
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Equals(Point p)
        {
            return p.X == this.X && p.Y == this.Y;
        }
        /// <summary>
        /// Indicates whether the MazePoint is not a Wall (!<paramref name="IsWall"/>) and has not been reached already (!<paramref name="HasBeenReached"/>)
        /// </summary>
        /// <returns></returns>
        public bool CanBeVisited()
        {
            return !HasBeenReached && !IsWall;
        }
        /// <summary>
        /// Returns a string representation of the Maze Point using the Maze parent to check for Start and End points
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public string ToString(Maze parent)
        {
            if (this.Equals(parent.Start))
            {
                return "S";
            }
            if (this.Equals(parent.End))
            {
                return "E";
            }
            if (this.IsWall)
            {
                return "#";
            }
            if (IsPartOfSolution)
            {
                return "X";
            }
            return " ";
        }

    }
}
