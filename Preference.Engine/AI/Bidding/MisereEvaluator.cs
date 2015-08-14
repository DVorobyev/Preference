// Copyright © 2013 Dmitry Vorobyev.

using System.Collections.Generic;
using System.Linq;

namespace Preference.Engine.AI.Bidding
{
    internal class MisereEvaluator : HandEvaluator
    {
        internal MisereEvaluator(Game game) : base(game)
        {
        }

        protected override double EvaluateCards(int cards, int discards)
        {
            IEnumerable<TrickProbability> probabilities = Enumerable.Empty<TrickProbability>();
            
            for (var suit = CardSuit.Spades; suit <= CardSuit.Hearts; suit++)
            {
                var probabilitiesForSuit = new List<TrickProbability>();

                double probability = GetFailureProbabilityForSuit(cards, discards, suit);
                
                // TODO Here I assume that each unsafe card suit may give 1 trick max. It is not always true since the declarer
                // may take more than 1 trick per suit. Hence the overall expectation may be higher than in reality.
                probabilitiesForSuit.Add(new TrickProbability(1, probability));
                probabilitiesForSuit.Add(new TrickProbability(0, 1.0 - probability));

                probabilities = probabilities.Combine(probabilitiesForSuit);
            }

            return probabilities.Expectation(
                tricks => Game.Rules.GetGameCost(
                    GameType.Misere,
                    Game.Options.NumberOfPlayers,
                    0,
                    tricks));
        }

        private double GetFailureProbabilityForSuit(int cards, int discards, CardSuit suit)
        {
            int cardSet = BitwiseCardHelper.GetCardSet(cards, suit);
            int discardsCount = BitwiseCardHelper.GetSuitCount(discards, suit);
            int otherSuitsDistribution = CardSuitDistribution.GetDistribution(cards, discards, suit);

            return MisereProbabilities.GetFailureProbability(cardSet, discardsCount, otherSuitsDistribution, IsFirstHand);
        }

        internal double GetDecisionThreshold()
        {
            return ThresholdBase;
        }


        /// <summary>
        /// Accordingly to different Preference literature sources, 15 whist points is the minimal mathematical expectation
        /// of a misere game to be considered worth playing. 
        /// </summary>
        private const double ThresholdBase = 15.0;
    }
}
