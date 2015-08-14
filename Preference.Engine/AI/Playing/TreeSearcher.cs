// Dmitry Vorobyev

using System.Collections.Generic;

namespace Preference.Engine.AI.Playing
{
    /// <summary>
    /// Performs the Information Set Monte-Carlo Tree Search (ISMCTS).
    /// http://www.aifactory.co.uk/newsletter/2013_01_reduce_burden.htm
    /// Thanks guys for this brilliant algorithm!
    /// </summary>
    internal static class TreeSearcher
    {
        /// <summary>
        /// Performs the tree search.
        /// </summary>
        /// <param name="rootState"></param>
        /// <param name="maxIterations"></param>
        /// <param name="randomizeState"></param>
        /// <returns></returns>
        internal static TreeNode Search(PlayingState rootState, int maxIterations = int.MaxValue, bool randomizeState = true)
        {
            var rootNode = new TreeNode();
            
            HandRandomizer randomizer = null;
            
            if ((randomizeState) && (rootState.NeedRandomize()))
                randomizer = new HandRandomizer(rootState);

            for (var i = 0; i < maxIterations; i++)
            {
                var node = rootNode;

                // 1. Determinize.
                var state = rootState.CloneAndRandomize(randomizer);

                // 2. Select.
                var validMoves = state.GetValidMoves();
                while ((validMoves != 0) && (node.GetUntriedMoves(validMoves) == 0)) // The node is fully expanded and non-terminal.
                {
                    node = node.UcbSelectChild(validMoves);
                    state.DoMove(node.Move);
                    
                    validMoves = state.GetValidMoves();
                }

                // 3. Expand.
                var untriedMoves = node.GetUntriedMoves(validMoves);
                if (untriedMoves != 0)
                {
                    var randomMove = GetRandomMove(untriedMoves, state);
                    var activeIndex = state.ActiveIndex;

                    state.DoMove(randomMove);

                    node = node.AddChild(randomMove, activeIndex);
                }

                // 4. Simulate.
                validMoves = state.GetValidMoves();
                while (validMoves != 0)
                {
                    var randomMove = GetRandomMove(validMoves, state);
                    state.DoMove(randomMove);

                    validMoves = state.GetValidMoves();
                }

                // 5. Backpropagate.
                while (node != null)
                {
                    node.UpdateStatistics(state);
                    node = node.Parent;
                }
            }

            return rootNode.GetBestChild();
        }

        private static Move GetRandomMove(int cards, PlayingState state)
        {
            if (!state.IsActiveHandOpen)
                return GetRandomCard(cards);
            else
                return GetRandomGroup(cards, state.Discards);
        }

        private static Move GetRandomCard(int cards)
        {
            int mask = 1;
            const int maxMoves = 10;
            var moves = new List<Move>(maxMoves);

            for (int i = 0; i < 32; i++)
            {
                if ((cards & mask) != 0)
                    moves.Add(new Move(mask));

                mask <<= 1;
            }

            return RandomHelper.Select(moves);
        }

        private static Move GetRandomGroup(int cards, int discards)
        {
            int mask = 1;
            int card = 0;
            int group = 0;

            const int maxMoves = 10;
            var moves = new List<Move>(maxMoves);

            for (int suitIndex = 0; suitIndex < 4; suitIndex++)
            {
                for (int moveIndex = 0; moveIndex < 8; moveIndex++)
                {
                    if ((cards & mask) != 0)
                    {
                        if (group == 0)
                            card = mask;

                        group |= mask;
                    }
                    else if (((discards & mask) == 0) && (group != 0))
                    {
                        moves.Add(new Move(card, group));
                        group = 0;
                    }

                    mask <<= 1;
                }

                if (group != 0)
                {
                    moves.Add(new Move(card, group));
                    group = 0;
                }
            }

            if (group != 0)
                moves.Add(new Move(card, group));

            return RandomHelper.Select(moves);
        }
    }
}
