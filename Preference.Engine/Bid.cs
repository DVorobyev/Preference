// 17/01/2012 by Dmitry Vorobyev.

using System;
using System.Diagnostics;

namespace Preference.Engine
{
    /// <summary>
    /// Defines a bid declared by a player during bidding.
    /// </summary>
    public struct Bid
    {
        private Bid(BidType bidType) : this()
        {
            mBidType = bidType;
        }

        public Bid(int tricks, CardSuit? trump) : this(BidType.Tricks)
        {
            if ((tricks < 6) || (tricks > 10))
                throw new ArgumentOutOfRangeException("tricks");

            mTricks = tricks;
            mTrump = trump;
        }

        internal Bid GetNextBid()
        {
            Debug.Assert(!IsPass);
            Debug.Assert(!IsTenNoTrump);

            if (!mTrump.HasValue)
            {
                return new Bid(mTricks + 1, CardSuit.Spades);
            }
            else if (mTrump.Value == CardSuit.Hearts)
            {
                return new Bid(mTricks, null);
            }
            else
            {
                return new Bid(mTricks, mTrump + 1);
            }
        }

        public BidType BidType
        {
            get { return mBidType; }
        }

        public int Tricks
        {
            get { return mTricks; }
        }

        public CardSuit? Trump
        {
            get { return mTrump; }
        }

        public bool IsMisere
        {
            get { return (mBidType == BidType.Misere); }
        }
        public bool IsPass
        {
            get { return (mBidType == BidType.Pass); }
        }

        internal bool IsSixSpades
        {
            get { return (mTricks == 6) && (mTrump.HasValue) && (mTrump.Value == CardSuit.Spades); }
        }

        internal bool IsTenNoTrump
        {
            get { return (mTricks == 10) && (!mTrump.HasValue); }
        }

        public static readonly Bid Misere = new Bid(BidType.Misere);
        public static readonly Bid Pass = new Bid(BidType.Pass);

        private readonly BidType mBidType;
        private readonly int mTricks;
        private readonly CardSuit? mTrump;
    }
}
