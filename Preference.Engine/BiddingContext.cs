// 18/01/2012 by Dmitry Vorobyev.

using System.Linq;

namespace Preference.Engine
{
    public class BiddingContext
    {
        internal BiddingContext(Game game)
        {
            mGame = game;
        }

        internal void RegisterBid(Bid bid)
        {
            mBids++;

            if (bid.BidType == BidType.Pass)
            {
                mPasses++;
            }
            else
            {
                mHighestBid = bid;
                mHighestBidMaker = mGame.ActiveHand;
            }
        }

        internal void RegisterFinalBid(Bid finalBid)
        {
            mContract = finalBid;
        }

        internal GameType GetBiddingResult()
        {
            if (!mHighestBid.HasValue)
                return GameType.AllPass;
            else if (mHighestBid.Value.BidType == BidType.Misere)
                return GameType.Misere;
            else
                return GameType.TrickPlay;
        }

        /// <summary>
        /// Gets whether the active player may bid misere.
        /// </summary>
        internal bool IsMisereAllowed
        {
            get
            {
                // Misere is allowed if the player hasn't made any bids and no one has declared misere yet.
                return 
                    (!mGame.ActiveHand.LastBid.HasValue &&
                     !mGame.Hands.Any(hand => (hand.LastBid.HasValue && hand.LastBid.Value.IsMisere)));
            }
        }

        internal bool IsBiddingComplete
        {
            get { return (Rounds > 0) && (Passes > 1); }
        }

        public int Rounds
        {
            get { return mBids / 3; }
        }

        public int Bids
        {
            get { return mBids; }
        }

        public int Passes
        {
            get { return mPasses; }
        }

        public Bid? HighestBid
        {
            get { return mHighestBid; }
        }

        public Hand HighestBidMaker
        {
            get { return mHighestBidMaker; }
        }

        public Bid? Contract
        {
            get { return mContract; }
        }

        internal CardSuit? TrumpSuit
        {
            get
            {
                if (!Contract.HasValue)
                    return null;

                return (Contract.Value.BidType == BidType.Tricks ? Contract.Value.Trump : null);
            }
        }

        private readonly Game mGame;
        private int mBids;
        private int mPasses;
        private Bid? mHighestBid;
        private Hand mHighestBidMaker;
        private Bid? mContract;
    }
}
