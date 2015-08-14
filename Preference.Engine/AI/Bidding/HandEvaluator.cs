// Copyright © 2013 Dmitry Vorobyev.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Preference.Engine.AI.Bidding
{
    /// <summary>
    /// Calculates hand's value in terms of a certain game type.
    /// </summary>
    internal abstract class HandEvaluator
    {
        protected HandEvaluator(Game game)
        {
            mGame = game;
        }

        /// <summary>
        /// Calculates a mathematical expectation of the active hand for a certain game type.
        /// Uses the expecti-max algorithm to compute the value among all possible widow cards and further discards.
        /// Updates the <see cref="Expectation"/> property.
        /// </summary>
        /// <returns></returns>
        internal void Evaluate()
        {
            int ownCards = BitwiseCardHelper.CardCollectionToBits(mGame.ActiveHand.Cards);
            int unknownCards = ~ownCards;

            Debug.Assert(BitwiseCardHelper.GetCardCount(unknownCards) == 22);

            var values = new List<double>();

            var enumerator = new TwoCardsEnumerator(unknownCards);
            while (enumerator.MoveNext())
            {
                int ownCardsWithWidow = ownCards | enumerator.Current;
                double value = EvaluateDiscards(ownCardsWithWidow);
                values.Add(value);
            }

            Expectation = values.Average();
        }

        private double EvaluateDiscards(int cards)
        {
            Debug.Assert(BitwiseCardHelper.GetCardCount(cards) == 12);

            var values = new List<double>();

            var enumerator = new TwoCardsEnumerator(cards);
            while (enumerator.MoveNext())
            {
                int cardsAfterDiscarding = cards & ~enumerator.Current;
                double value = EvaluateCards(cardsAfterDiscarding, enumerator.Current);
                values.Add(value);
            }

            return values.Max();
        }

        /// <summary>
        /// Returns a mathematical expectation of the hand for the specified cards and discards.
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="discards"></param>
        /// <returns></returns>
        protected abstract double EvaluateCards(int cards, int discards);

        protected Game Game
        {
            get { return mGame; }
        }

        protected bool IsFirstHand
        {
            get { return mGame.ActiveHand.IsFirstHand; }
        }

        internal double Expectation { get; private set; }

        /// <summary>
        /// Enumerates all possible combinations of two cards among the given card set.
        /// Internal because exposed for unit testing.
        /// </summary>
        internal class TwoCardsEnumerator
        {
            internal TwoCardsEnumerator(int cardSet)
            {
                mCardSet = cardSet;
            }

            /// <summary>
            /// Moves to the next combination of two cards. Returns false if there are no combinations anymore.
            /// </summary>
            /// <returns></returns>
            internal bool MoveNext()
            {
                if (mCard1 == 0)
                {
                    mCard1 = GetNextBit(0);
                    if (mCard1 == 0)
                        return false;

                    mCard2 = mCard1;
                }

                mCard2 = GetNextBit(mCard2);
                if (mCard2 == 0)
                {
                    mCard1 = GetNextBit(mCard1);
                    if (mCard1 == 0)
                        return false;

                    mCard2 = GetNextBit(mCard1);
                    if (mCard2 == 0)
                        return false;
                }

                return true;
            }

            private int GetNextBit(int mask)
            {
                mask = (mask != 0) ? mask << 1 : 1; 
                
                while (mask != 0)
                {
                    if ((mCardSet & mask) != 0)
                        return mask;
                    
                    mask <<= 1;
                }

                return 0;
            }

            /// <summary>
            /// Gets the current combination of two cards.
            /// </summary>
            internal int Current
            {
                get { return (mCard1 | mCard2); }
            }

            private readonly int mCardSet;
            private int mCard1;
            private int mCard2;
        }

        private readonly Game mGame;
    }
}
