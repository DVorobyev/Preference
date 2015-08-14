// Copyright © 2013 Dmitry Vorobyev.

using System;

namespace Preference.Engine
{
    /// <summary>
    /// Represents a gaming card.
    /// </summary>
    public struct Card : IComparable<Card>
    {
        /// <summary>
        /// Initializes the structure.
        /// </summary>
        /// <param name="suit">The suit of the card.</param>
        /// <param name="rank">The rank of the card.</param>
        public Card(CardSuit suit, CardRank rank)
        {
            mSuit = suit;
            mRank = rank;
        }

        internal bool IsHigherThan(Card card, CardSuit? trumpSuit)
        {
            if ((mSuit == card.Suit) && (mRank > card.Rank))
                return true;

            if ((trumpSuit.HasValue) && (mSuit == trumpSuit) && (card.Suit != trumpSuit))
                return true;

            return false;
        }

        public bool Equals(Card other)
        {
            return Equals(other.mSuit, mSuit) && Equals(other.mRank, mRank);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            
            if (obj.GetType() != typeof(Card))
                return false;
            
            return Equals((Card)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (mSuit.GetHashCode() * 397) ^ mRank.GetHashCode();
            }
        }

        public int CompareTo(Card other)
        {
            if (mSuit == other.mSuit)
                return mRank.CompareTo(other.mRank);
            else
                return mSuit.CompareTo(other.mSuit);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", gRankString[(int)mRank], gSuitString[(int)mSuit]);
        }

        /// <summary>
        /// Gets the suit of the card.
        /// </summary>
        public CardSuit Suit
        {
            get { return mSuit; }
        }

        /// <summary>
        /// Gets the rank of the card.
        /// </summary>
        public CardRank Rank
        {
            get { return mRank; }
        }

        private readonly CardSuit mSuit;
        private readonly CardRank mRank;

        private static readonly string[] gSuitString = {"♠", "♣", "♦", "♥"};
        private static readonly string[] gRankString = {"7", "8", "9", "10", "J", "Q", "K", "A"};
    }
}
