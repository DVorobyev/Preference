using System.Collections.Generic;

namespace Preference.Engine.AI.Bidding
{
    /// <summary>
    /// Represents a facade for the bidding part of the AI.
    /// </summary>
    internal class BiddingAdviser
    {
        internal BiddingAdviser(Game game)
        {
            mGame = game;
        }

        internal Bid MakeBid()
        {
//            if (mBid.HasValue)
//                return mBid.Value;

            // First try misere.
//            if (mGame.BiddingContext.IsMisereAllowed && IsMisereAcceptable())
//                return Bid.Misere;
//
//            // Then calculate a maximum possible bid for a trick game.
//            CalculateMaxBid();

            // If there's nothing to bid, pass.
            if (!mMaxBid.HasValue)
                return Bid.Pass;


            // Can't beat the last bid, pass.
            return Bid.Pass;
        }

//        private bool IsMisereAcceptable()
//        {
//            var evaluator = new MisereEvaluator(mGame.ActiveHand);
//            TrickGameValue value = evaluator.Evaluate();
//
//            return EvaluationAnalyzer.IsMisereAcceptable(value);
//        }
//
//        private void CalculateMaxBid()
//        {
//            var values = new List<TrickGameValue>();
//
//            EvaluateTrumpGames(values);
//            EvaluateNoTrumpGame(values);
//
//            TrickGameValue bestValue = EvaluationAnalyzer.SelectBestValueForTrickGame(values);
//            
//            if (bestValue != null)
//                mMaxBid = new Bid(bestValue.BestValue, (CardSuit?)bestValue.Tag);
//        }
//
//        private void EvaluateTrumpGames(ICollection<TrickGameValue> values)
//        {
//            for (var suit = CardSuit.Spades; suit <= CardSuit.Hearts; suit++)
//            {
//                var evaluator = new TrumpGameEvaluator(mGame, suit);
//                TrickGameValue value = evaluator.Evaluate();
//                
//                if (value != null)
//                {
//                    value.Tag = suit;
//                    values.Add(value);
//                }
//            }
//        }
//
//        private void EvaluateNoTrumpGame(ICollection<TrickGameValue> values)
//        {
//            var evaluator = new NoTrumpGameEvaluator(mGame.ActiveHand);
//            TrickGameValue value = evaluator.Evaluate();
//
//            if (value != null)
//                values.Add(value);
//        }

        private readonly Game mGame;
        private Bid? mMaxBid;
    }
}
