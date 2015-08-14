// Copyright © 2013 Dmitry Vorobyev.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Preference.Engine.AI.Bidding
{
    /// <summary>
    /// Evaluates hand's strength when a trump game is declared.
    /// </summary>
    internal class TrumpGameEvaluator : TrickPlayEvaluator
    {
        internal TrumpGameEvaluator(Game game, int declaredTricks, CardSuit trumpSuit) : base(game, declaredTricks)
        {
            mTrumpSuit = trumpSuit;
        }

        protected override double EvaluateCards(int cards, int discards)
        {
            Debug.Assert(BitwiseCardHelper.GetCardCount(cards) == 10);

            int trumpCount = BitwiseCardHelper.GetSuitCount(cards, mTrumpSuit);
            
            // Don't consider trump game if the number of trumps is less than 4.
            // TODO Consider 3-3-3-1.
            if (trumpCount < 4)
                return double.MinValue;

            IEnumerable<TrickProbability> totalProbabilities = Enumerable.Empty<TrickProbability>();

            // Calculate sum of tricks with corresponding probabilities in all suits.
            for (var suit = CardSuit.Spades; suit <= CardSuit.Hearts; suit++)
            {
                int cardSet = BitwiseCardHelper.GetCardSet(cards, suit);
                IEnumerable<TrickProbability> probabilities = TrickPlayProbabilities.GetTrickProbabilities(cardSet);
                
                // Combine probabilities..
                totalProbabilities = totalProbabilities.Combine(probabilities);
            }

            bool isEasyLevel = (Game.Options.DifficultyLevel == GameDifficultyLevel.Beginner) ||
                               (Game.Options.DifficultyLevel == GameDifficultyLevel.Amateur);
            
            // The following stuff is too complex for weak players.
            if (!isEasyLevel)
            {
                totalProbabilities = ConsiderTrumpAdvancing(totalProbabilities);
                totalProbabilities = ConsiderTrickTaking(totalProbabilities);
            }

            return totalProbabilities.Expectation(
                 tricks => Game.Rules.GetGameCost(
                    GameType.TrickPlay,
                    Game.Options.NumberOfPlayers,
                    DeclaredTricks,
                    tricks));
        }

        private IEnumerable<TrickProbability> ConsiderTrumpAdvancing(IEnumerable<TrickProbability> probabilities)
        {
            throw new NotImplementedException();    
        }

        private IEnumerable<TrickProbability> ConsiderTrickTaking(IEnumerable<TrickProbability> probabilities)
        {
            throw new NotImplementedException();
        }

        private readonly CardSuit mTrumpSuit;
    }
}
