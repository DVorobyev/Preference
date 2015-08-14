// Dmitry Vorobyev

using System;
using System.Collections.Generic;

namespace Preference.Engine.AI.Playing
{
    /// <summary>
    /// Represents an Information Set Monte Carlo Tree node.
    /// </summary>
    internal class TreeNode
    {
        /// <summary>
        /// Adds a child node to this node.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="activeIndex"></param>
        /// <returns>A new child node.</returns>
        internal TreeNode AddChild(Move move, int activeIndex)
        {
            var child = new TreeNode
            {
                Move = move,
                ActiveIndex = activeIndex,
                Parent = this
            };

            if (FirstChild == null)
            {
                FirstChild = child;
                LastChild = child;
            }
            else
            {
                LastChild.NextSibling = child;
                LastChild = child;
            }

            return child;
        }

        /// <summary>
        /// Returns a bit mask representing all moves among the specified valid moves for which this node does not have children.
        /// </summary>
        /// <returns></returns>
        internal int GetUntriedMoves(int validMoves)
        {
            int triedMoves = GetTriedMoves();
            return ~triedMoves & validMoves;
        }

        private int GetTriedMoves()
        {
            int triedMoves = 0;
            var child = FirstChild;
            
            while (child != null)
            {
                triedMoves |= child.Move.Group;
                child = child.NextSibling;
            }

            return triedMoves;
        }

        /// <summary>
        /// Returns a child node selected using the UCB1 formula, filtered by the specified bit mask representing valid moves.
        /// </summary>
        /// <param name="validMoves"></param>
        /// <returns></returns>
        internal TreeNode UcbSelectChild(int validMoves)
        {
            TreeNode selectedNode = null;
            var bestValue = double.MinValue;
            var child = FirstChild;

            while (child != null)
            {
                if ((child.Move.Card & validMoves) != 0)
                {
                    double uctValue = child.WinRate + UcbFactor * Math.Sqrt(Math.Log(VisitCount) / child.VisitCount);

                    if (uctValue > bestValue)
                    {
                        bestValue = uctValue;
                        selectedNode = child;
                    }
                }

                child = child.NextSibling;
            }

            return selectedNode;
        }
        
        /// <summary>
        /// Updates the statistics for this node using the specified state.
        /// </summary>
        /// <param name="state"></param>
        internal void UpdateStatistics(PlayingState state)
        {
            VisitCount++;
            mTotalValue += state.Evaluate(ActiveIndex);
        }

        /// <summary>
        /// Returns a child node that has maximal number of visits.
        /// Randomly selects one if multiple nodes with similar number of visits are found.
        /// </summary>
        /// <returns></returns>
        internal TreeNode GetBestChild()
        {
            var currentChild = FirstChild;
            int maxVisits = int.MinValue;

            // Step 1: detect maxVisits.
            while (currentChild != null)
            {
                if (currentChild.VisitCount > maxVisits)
                    maxVisits = currentChild.VisitCount;

                currentChild = currentChild.NextSibling;
            }

            // Step 2: collect all child nodes with equal maxVisits.
            currentChild = FirstChild;
            var bestChildren = new List<TreeNode>();

            while (currentChild != null)
            {
                if (currentChild.VisitCount == maxVisits)
                    bestChildren.Add(currentChild);

                currentChild = currentChild.NextSibling;
            }

            // Step 3: choose a random child among peers.
            return RandomHelper.Select(bestChildren);
        }

        internal double WinRate
        {
            get { return mTotalValue / VisitCount; }
        }

        internal Move Move { get; private set; }
        internal int ActiveIndex { get; private set; }
        internal TreeNode Parent { get; private set; }
        internal TreeNode NextSibling { get; set; }
        internal TreeNode FirstChild { get; set; }
        internal TreeNode LastChild { get; set; }
        internal int VisitCount { get; private set; }

        private double mTotalValue;

        /// <summary>
        /// A constant factor representing balancing between exploitation and exploration.
        /// 0.7 (approximately sqrt(2) / 2) usually works well.
        /// </summary>
        private const double UcbFactor = 0.7;
    }
}
