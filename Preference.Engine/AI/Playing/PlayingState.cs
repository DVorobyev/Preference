using System.Collections.Generic;

namespace Preference.Engine.AI.Playing
{
    internal class PlayingState
    {
        internal PlayingState(PlayingContext context)
        {
            mContext = context;
            
            Game game = context.Game;

            for (int i = 0; i < HandCount; i++)
                mHands[i] = new HandState(game.GetHand(i));

            mActiveIndex = game.ActiveHand.Index;
            mDiscards = BitwiseCardHelper.CardCollectionToBits(game.PlayingContext.Discards);
            mLeadingSuit = context.InitialSuit;
        }

        internal void DoMove(Move move)
        {
            // Register the move.
            ActiveHand.Move = move;
            // Remove the played card from the player's cards.
            ActiveHand.Cards &= ~move.Card;
            
            // Update the leading suit.
            if (mLeadingSuit == 0)
                mLeadingSuit = BitwiseCardHelper.GetSuitAsBits(move.Card);

            ActivateNextHand();

            // If the next player has already played a card, then the trick is over.
            if (ActiveHand.Move.Card != 0)
            {
                mActiveIndex = GetTrickWinner();
                ActiveHand.Tricks++;

                for (int i = 0; i < HandCount; i++)
                {
                    mDiscards |= mHands[i].Move.Card;
                    mHands[i].Move = Move.Empty;
                }

                mLeadingSuit = 0;
            }
        }

        private int GetTrickWinner()
        {
            // ActiveHand should be the leading hand by the time the method is called.
            int trickWinner = mActiveIndex;
            int highestCard = ActiveHand.Move.Card;

            ActivateNextHand();
            if (IsCardHigher(ActiveHand.Move.Card, highestCard))
            {
                trickWinner = mActiveIndex;
                highestCard = ActiveHand.Move.Card;
            }

            ActivateNextHand();
            if (IsCardHigher(ActiveHand.Move.Card, highestCard))
                trickWinner = mActiveIndex;

            return trickWinner;
        }

        private bool IsCardHigher(int card, int highestCard)
        {
            int suit = BitwiseCardHelper.GetSuitAsBits(card);
            int highestSuit = BitwiseCardHelper.GetSuitAsBits(highestCard);

            if (suit == highestSuit)
                return ((uint)card > (uint)highestCard);

            int trumpSuit = mContext.TrumpSuit;
            if (trumpSuit != 0)
            {
                if ((suit == trumpSuit) && (highestSuit != trumpSuit))
                    return true;

                if ((suit != trumpSuit) && (highestSuit == trumpSuit))
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Returns a bit mask representing all moves that are valid for the current state.
        /// </summary>
        /// <returns></returns>
        internal int GetValidMoves()
        {
            int cards = ActiveHand.Cards;

            int initialSuit = GetInitialSuit();
            if (initialSuit == 0)
            {
                // No initial suit yet, any card is allowed.
                return cards;
            }

            int cardsOfInitialSuit = cards & initialSuit;
            if (cardsOfInitialSuit != 0)
            {
                // The player owns cards of the initial suit, should play them.
                return cardsOfInitialSuit;
            }

            int cardsOfTrumpSuit = cards & mContext.TrumpSuit;
            if (cardsOfTrumpSuit != 0)
            {
                // The player owns cards of the trump suit, should play them.
                return cardsOfTrumpSuit;
            }

            // Any card is allowed.
            return cards;
        }

        private void ActivateNextHand()
        {
            mActiveIndex = (mActiveIndex + 1) % HandCount;
        }

        private int GetInitialSuit()
        {
//            if (mGame.GameType == GameType.AllPass)
//            {
//                if (MoveCount == 0) // TODO Error!
//                    return mGame.Widow[0].Suit;
//
//                if (MoveCount == 1)
//                    return mGame.Widow[1].Suit;
//            }

            return mLeadingSuit;
        }

        /// <summary>
        /// Returns the value of the state for the specified hand.
        /// </summary>
        /// <param name="handIndex"></param>
        /// <returns></returns>
        internal double Evaluate(int handIndex)
        {
            return gNormalizedValues[mContext.StateEvaluator(this, handIndex)];    // Normalize to [0; 1].
        }

        private PlayingState Clone()
        {
            var clone = new PlayingState(mContext);

            for (int i = 0; i < HandCount; i++)
                clone.mHands[i] = mHands[i].Clone();
            
            clone.mActiveIndex = mActiveIndex;
            clone.mLeadingSuit = mLeadingSuit;
            clone.mDiscards = mDiscards;

            return clone;
        }

        /// <summary>
        /// Returns a clone of the state replacing closed (not visible) hands with random card distributions.
        /// </summary>
        /// <param name="randomizer">Null to clone without randomizing.</param>
        /// <returns></returns>
        internal PlayingState CloneAndRandomize(HandRandomizer randomizer)
        {
            PlayingState clone = Clone();
            
            if (randomizer != null)
                randomizer.Randomize();

            return clone;
        }

        /// <summary>
        /// Returns true if at least one hand on this state should be randomized, false otherwise.
        /// </summary>
        /// <returns></returns>
        internal bool NeedRandomize()
        {
            IList<int> handsToRandomize = GetHandsToRandomize();
            return (handsToRandomize.Count > 0);
        }

        /// <summary>
        /// Returns a list of indices of hands that should be randomized.
        /// </summary>
        /// <returns></returns>
        internal IList<int> GetHandsToRandomize()
        {
            var hands = new List<int>();

            for (int i = 0; i < HandCount; i++)
            {
                if ((i != mActiveIndex) && (!Game.GetHand(i).IsOpen))
                    hands.Add(i);
            }

            return hands;
        }

        internal HandState[] Hands
        {
            get { return mHands; }
        }

        internal int ActiveIndex
        {
            get { return mActiveIndex; }
        }

        internal HandState ActiveHand
        {
            get { return mHands[mActiveIndex]; }
        }

        internal bool IsActiveHandOpen
        {
            get { return mContext.Game.GetHand(mActiveIndex).IsOpen; }
        }

        internal int Discards
        {
            get { return mDiscards; }
        }

        internal Game Game
        {
            get { return mContext.Game; }
        }

        private readonly PlayingContext mContext;
        private readonly HandState[] mHands = new HandState[HandCount];
        private int mActiveIndex;
        private int mLeadingSuit;
        private int mDiscards;

        private const int HandCount = 3;

        private static readonly double[] gNormalizedValues = {.0, .1, .2, .3, .4, .5, .6, .7, .8, .9, 1.0};
    }
}
