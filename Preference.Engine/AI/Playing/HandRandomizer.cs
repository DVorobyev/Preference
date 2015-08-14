using System.Collections.Generic;
using System.Diagnostics;

namespace Preference.Engine.AI.Playing
{
    /// <summary>
    /// Randomly distributes unknown cards to opponents' hands.
    /// </summary>
    internal class HandRandomizer
    {
        internal HandRandomizer(PlayingState state)
        {
            mState = state;
            CalculateCardSets();
        }

        private void CalculateCardSets()
        {
            mHandsToRandomize = mState.GetHandsToRandomize();
            int handCount = mHandsToRandomize.Count;

            mCardCounts = new int[handCount];
            mCardSets = new CardSet[handCount];

            // We know own cards.
            int knownCards = mState.ActiveHand.Cards;
            
            // We know just played cards.
            for (int i = 0; i < 3; i++)
            {
                if (mState.Hands[i] != mState.ActiveHand)
                    knownCards |= mState.Hands[i].Move.Card;
            }

            // We know discarded cards.
            knownCards |= mState.Discards;

            for (int i = 0; i < handCount; i++)
            {
                int handIndex = mHandsToRandomize[i];
                Hand hand = mState.Game.GetHand(handIndex);

                mCardCounts[i] = hand.Cards.Count;

                int cardSet = ~knownCards;
                // Remove suits that the hand is known to miss.
                foreach (var suit in hand.VoidSuits)
                    cardSet &= ~BitwiseCardHelper.SuitToBits(suit);

                mCardSets[i] = new CardSet(cardSet);
            }
        }

        /// <summary>
        /// Assigns random cards to the hands on the playing state that should be randomized.
        /// </summary>
        internal void Randomize()
        {
            for (int i = 0; i < mHandsToRandomize.Count; i++)
            {
                int handIndex = mHandsToRandomize[i];
                HandState hand = mState.Hands[handIndex];
                hand.Cards = 0;

                CardSet cardSet = mCardSets[i].Clone();

                for (int j = 0; j < mCardCounts[i]; j++)
                {
                    int card = cardSet.GetRandomCard();

                    // The following code handles a situation when there are two hands to distribute and the first hand
                    // takes too many "shared" (available to both hands) cards so that the second hand might be left without
                    // enough cards to select. I don't like the way it is currently handled but let it be for the time being.
                    if ((mHandsToRandomize.Count == 2) && (i == 0) && (mCardSets[1].HasCard(card)))
                    {
                        if (mCardSets[1].Count == mCardCounts[1])
                        {
                            // No more "shared" cards for the first hand.
                            cardSet.RemoveCards(mCardSets[1].Cards);
                            card = cardSet.GetRandomCard();
                        }
                        else
                        {
                            mCardSets[1].RemoveCard(card);
                        }
                    }

                    Debug.Assert(card != 0);

                    cardSet.RemoveCard(card);
                    hand.Cards |= card;
                }
            }

            // Make sure there are no intersections.
            Debug.Assert((mState.Hands[0].Cards & mState.Hands[1].Cards & mState.Hands[2].Cards) == 0);
        }

        private class CardSet
        {
            internal CardSet(int cards)
            {
                mCards = cards;
                mCount = BitwiseCardHelper.GetCardCount(cards);
            }

            internal int GetRandomCard()
            {
                Debug.Assert(mCount > 0);

                int index = RandomHelper.Next(mCount);
                int mask = 1;
                int count = 0;

                for (int i = 0; i < 32; i++)
                {
                    if ((mCards & mask) != 0)
                    {
                        if (count == index)
                            return mask;

                        count++;
                    }

                    mask <<= 1;
                }

                return 0;
            }

            internal bool HasCard(int card)
            {
                return ((mCards & card) != 0);
            }

            internal void RemoveCard(int card)
            {
                mCards &= ~card;
                mCount--;
            }

            internal void RemoveCards(int cards)
            {
                mCards &= ~cards;
                mCount = BitwiseCardHelper.GetCardCount(mCards);
            }

            internal CardSet Clone()
            {
                return (CardSet)MemberwiseClone();
            }

            internal int Cards
            {
                get { return mCards; }
            }

            internal int Count
            {
                get { return mCount; }
            }

            private int mCards;
            private int mCount;
        }

        private readonly PlayingState mState;
        private IList<int> mHandsToRandomize;
        private int[] mCardCounts;
        private CardSet[] mCardSets;
    }
}
