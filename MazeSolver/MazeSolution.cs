using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    public class MazeSolution
    {
        private Maze parent;
        public bool IsSolved { get; private set; }
        public MazeSolution(Maze toSolve)
        {
            parent = toSolve;
        }
        static Array Directions = Enum.GetValues(typeof(Direction));
        internal void TrySolve()
        {
            MazePoint start = parent.MazePoints[parent.Start.Y][parent.Start.X];
            IsSolved = CanBeSolved(start);
        }
        private bool CanBeSolved(MazePoint currentPoint)
        {
            currentPoint.HasBeenReached = true;
            if(currentPoint.Equals(parent.End))
            {
                currentPoint.IsPartOfSolution = true;
                return true;
            }
            foreach (Direction dirr in Directions)
            {
                MazePoint nextPoint;
                if(CanMoveInDirection(dirr, currentPoint, out nextPoint))
                {
                    if(CanBeSolved(nextPoint))
                    {
                        currentPoint.IsPartOfSolution = true;
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Indicates whether a solver can move in a direciton
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="currentPoint"></param>
        /// <returns></returns>
        public bool CanMoveInDirection(Direction direction, MazePoint currentPoint, out MazePoint nextPoint)
        {
            switch (direction)
            {
                case Direction.North:
                    return CanMoveNorth(currentPoint, out nextPoint);
                case Direction.South:
                    return CanMoveSouth(currentPoint, out nextPoint);
                case Direction.East:
                    return CanMoveEast(currentPoint, out nextPoint);
                case Direction.West:
                    return CanMoveWest(currentPoint, out nextPoint);
            }
            throw new NotImplementedException();
        }

        private bool CanMoveNorth(MazePoint mp, out MazePoint nextPoint)
        {
            if (mp.Y == 0)
            {
                nextPoint = MazePoint.Empty;
                return false;
            }
            nextPoint = parent.MazePoints[mp.Y - 1][mp.X];
            return nextPoint.CanBeVisited();
        }
        private bool CanMoveSouth(MazePoint mp, out MazePoint nextPoint)
        {
            if (mp.Y == parent.Height - 1)
            {
                nextPoint = MazePoint.Empty;
                return false;
            }
            nextPoint = parent.MazePoints[mp.Y + 1][mp.X];
            return nextPoint.CanBeVisited();
        }
        private bool CanMoveEast(MazePoint mp, out MazePoint nextPoint)
        {
            if (mp.X == parent.Width - 1)
            {
                nextPoint = MazePoint.Empty;
                return false;
            }
            nextPoint = parent.MazePoints[mp.Y][mp.X + 1];
            return nextPoint.CanBeVisited();
        }
        private bool CanMoveWest(MazePoint mp, out MazePoint nextPoint)
        {
            if (mp.X == 0)
            {
                nextPoint = MazePoint.Empty;
                return false;
            }
            nextPoint = parent.MazePoints[mp.Y][mp.X - 1];
            return nextPoint.CanBeVisited();
        }
    }
    public class MazePoint
    {
        public static readonly MazePoint Empty = new MazePoint();
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool HasBeenReached { get; set; }
        public bool IsPassable { get; set; }
        public bool IsPartOfSolution { get; set; }
        private MazePoint()
        {

        }
        public MazePoint(int x, int y, bool passable) 
        {
            X = x;
            Y = y;
            IsPassable = passable;
        }
        public bool Equals(Point p)
        {
            return p.X == this.X && p.Y == this.Y;
        }
        /// <summary>
        /// Indicates whether the MazePoint <paramref name="IsPassable"/> and not <paramref name="HasBeenReached"/>
        /// </summary>
        /// <returns></returns>
        public bool CanBeVisited()
        {
            return !HasBeenReached && IsPassable;
        }
        public string ToString(Maze parent)
        {
            if(this.Equals(parent.Start))
            {
                return "S";
            }
            if(this.Equals(parent.End))
            {
                return "E";
            }
            if(!this.IsPassable)
            {
                return "#";
            }
            if(IsPartOfSolution)
            {
                return "X";
            }
            return " ";
        }

    }
    public enum Direction
    {
        North,
        South,
        East,
        West
    }
}
