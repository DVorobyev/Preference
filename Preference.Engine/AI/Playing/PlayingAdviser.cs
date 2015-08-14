// 20/01/2012 by Dmitry Vorobyev.

namespace Preference.Engine.AI.Playing
{
    /// <summary>
    /// Represents a facade for the playing part of the AI.
    /// </summary>
    internal class PlayingAdviser
    {
        internal PlayingAdviser(Game game)
        {
            mGame = game;
        }

        internal Card PlayCard()
        {
            var context = new PlayingContext(mGame);
            var rootState = new PlayingState(context);
            
            var validMoves = rootState.GetValidMoves();

            // Exit if only one move is possible.
            if (BitwiseCardHelper.GetGroupCount(validMoves) == 1)
                return GetRandomCard(validMoves);

            bool isCheater = (mGame.Options.DifficultyLevel == GameDifficultyLevel.Cheater);
            var bestNode = TreeSearcher.Search(rootState, 100000, !isCheater);

            return GetRandomCard(bestNode.Move.Group);
        }

        private static Card GetRandomCard(int cards)
        {
            var cardCollection = BitwiseCardHelper.BitsToCardCollection(cards);
            return RandomHelper.Select(cardCollection);
        }

        private readonly Game mGame;
    }
}
