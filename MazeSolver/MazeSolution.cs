using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    /// <summary>
    /// Represents an attempt at solving a Maze
    /// </summary>
    public class MazeSolution
    {
        private static Array Directions = Enum.GetValues(typeof(Direction));
        /// <summary>
        /// The maze that this solution corresponds with
        /// </summary>
        public Maze Parent { get; private set; }
        /// <summary>
        /// boolean indicator as to whether the maze has been successfully solved.
        /// </summary>
        public bool IsSolved { get; private set; }
        /// <summary>
        /// Initializes an Instance of Maze Solution that will use the specified Maze
        /// </summary>
        /// <param name="toSolve"></param>
        public MazeSolution(Maze toSolve)
        {
            Parent = toSolve;
        }
        /// <summary>
        /// Attempt to Solve the maze
        /// </summary>
        public void AttemptSolution()
        {
            MazePoint start = Parent.MazePoints[Parent.Start.Y][Parent.Start.X];
            IsSolved = CanBeSolved(Direction.South, start);
        }
        private bool CanBeSolved(Direction lastDirection, MazePoint currentPoint)
        {
            currentPoint.HasBeenReached = true;
            if(currentPoint.Equals(Parent.End))
            {
                currentPoint.IsPartOfSolution = true;
                return true;
            }
            // Try the last Direction we travelled in first
            // This was introduced to find quicker solutions in very simple mazes i.e. mazes containing few walls
            if(IsDirectionValid(lastDirection, currentPoint))
            {
                return true;
            }
            // If the Last Direction wasn't valid try the remaining directions
            foreach (Direction direction in Directions)
            {
                if(direction == lastDirection)
                {
                    // Skip the Last Direction because we have already tried it
                    continue;
                }
                if(IsDirectionValid(direction, currentPoint))
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsDirectionValid(Direction direction, MazePoint currentPoint)
        {
            MazePoint nextPoint;
            if (GetNextPoint(direction, currentPoint, out nextPoint))
            {
                if (CanBeSolved(direction, nextPoint))
                {
                    currentPoint.IsPartOfSolution = true;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Returns a boolean indicating whether a solution attempt can move from the specified <paramref name="currentPoint"/> in a specified direciton. <paramref name="nextPoint"/> will be set if the appropriate point if the Direction is valid
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="currentPoint"></param>
        /// <returns></returns>
        public bool GetNextPoint(Direction direction, MazePoint currentPoint, out MazePoint nextPoint)
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
            nextPoint = Parent.MazePoints[mp.Y - 1][mp.X];
            return nextPoint.CanBeVisited();
        }
        private bool CanMoveSouth(MazePoint mp, out MazePoint nextPoint)
        {
            if (mp.Y == Parent.Height - 1)
            {
                nextPoint = MazePoint.Empty;
                return false;
            }
            nextPoint = Parent.MazePoints[mp.Y + 1][mp.X];
            return nextPoint.CanBeVisited();
        }
        private bool CanMoveEast(MazePoint mp, out MazePoint nextPoint)
        {
            if (mp.X == Parent.Width - 1)
            {
                nextPoint = MazePoint.Empty;
                return false;
            }
            nextPoint = Parent.MazePoints[mp.Y][mp.X + 1];
            return nextPoint.CanBeVisited();
        }
        private bool CanMoveWest(MazePoint mp, out MazePoint nextPoint)
        {
            if (mp.X == 0)
            {
                nextPoint = MazePoint.Empty;
                return false;
            }
            nextPoint = Parent.MazePoints[mp.Y][mp.X - 1];
            return nextPoint.CanBeVisited();
        }
    }

}
