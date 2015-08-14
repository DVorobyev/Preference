// Dmitry Vorobyev

using System;

namespace Preference.Engine.AI.Playing
{
    /// <summary>
    /// A helper class that simplifies access to frequently used game features in a performance critical context.
    /// </summary>
    internal class PlayingContext
    {
        internal PlayingContext(Game game)
        {
            Game = game;

            if (game.GameType != GameType.AllPass)
                mDeclarerIndex = game.Declarer.Index;
 
            TrumpSuit = BitwiseCardHelper.SuitToBits(game.BiddingContext.TrumpSuit);
            InitialSuit = BitwiseCardHelper.SuitToBits(game.PlayingContext.InitialSuit);

            SetStateEvaluator();
        }

        private void SetStateEvaluator()
        {
            const int maxTricks = 10;

            switch (Game.GameType.Value)
            {
                case GameType.TrickPlay:
                    if (!Game.AreDefenderCardsOpen.Value)
                    {
                        StateEvaluator = (state, playerIndex) => state.Hands[playerIndex].Tricks;
                    }
                    else
                    {
                        StateEvaluator = (state, playerIndex) =>
                        {
                            if (playerIndex == mDeclarerIndex)
                                return state.Hands[playerIndex].Tricks;
                            else
                                return maxTricks - state.Hands[mDeclarerIndex].Tricks;
                        };
                    }

                    break;
                case GameType.Misere:
                    StateEvaluator = (state, playerIndex) =>
                    {
                        if (playerIndex == mDeclarerIndex)
                            return maxTricks - state.Hands[playerIndex].Tricks;
                        else
                            return state.Hands[mDeclarerIndex].Tricks;
                    };

                    break;
                case GameType.AllPass:
                    StateEvaluator = (state, playerIndex) => maxTricks - state.Hands[playerIndex].Tricks;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal Game Game { get; private set; }
        internal int TrumpSuit { get; private set; }
        internal int InitialSuit { get; private set; }
        internal Func<PlayingState, int, int> StateEvaluator { get; private set; }

        private readonly int mDeclarerIndex;
    }
}
