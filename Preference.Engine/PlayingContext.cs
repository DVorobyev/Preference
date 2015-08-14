// 18/01/2012 by Dmitry Vorobyev.

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Preference.Engine
{
    public class PlayingContext
    {
        internal PlayingContext(Game game)
        {
            mGame = game;
        }

        public void RegisterCard(Card card)
        {
            ValidateCard(card);

            if (!mLeadingCard.HasValue)
            {
                mLeadingCard = card;
            }
            else
            {
                var initialSuit = mLeadingCard.Value.Suit;
                if ((card.Suit != initialSuit) && (!mGame.ActiveHand.VoidSuits.Contains(initialSuit)))
                    mGame.ActiveHand.VoidSuits.Add(initialSuit);
            }
    
            mGame.ActiveHand.Cards.Remove(card);
            mPlayedCards.Add(card);
            mGame.ActiveHand.PlayedCard = card;

            mCardsUsed++;
        }

        public void ResetRound()
        {
            mPlayedCards.MoveTo(mDiscards);

            foreach (var hand in mGame.Hands)
                hand.PlayedCard = null;

            mLeadingCard = null;
        }

        private void ValidateCard(Card card)
        {
            if ((ExpectedSuit.HasValue) && (ExpectedSuit.Value != card.Suit))
                throw new RuleViolationException("The suit of the card is not allowed.");
        }

        private CardSuit? ExpectedSuit
        {
            get
            {
                if (!InitialSuit.HasValue)
                    return null;

                if (mGame.ActiveHand.Cards.HasSuit(InitialSuit.Value))
                    return InitialSuit;

                if (mGame.GameType == GameType.TrickPlay)
                {
                    CardSuit? trump = mGame.BiddingContext.TrumpSuit;
                    if ((trump.HasValue) && (mGame.ActiveHand.Cards.HasSuit(trump.Value)))
                        return trump;
                }

                return null;    
            }
        }

        internal CardSuit? InitialSuit
        {
            get
            {
                if ((mGame.GameType == GameType.AllPass) && (mCardsUsed < 2))
                    return mGame.Widow[mPlayedCards.Count].Suit;

                return mLeadingCard.HasValue ? mLeadingCard.Value.Suit : (CardSuit?)null;
            }
        }

        public ReadOnlyCollection<Card> PlayedCards
        {
            get { return mPlayedCards.ToList().AsReadOnly(); }
        }

        public int CardsUsed
        {
            get { return mCardsUsed; }
        }

        public int Rounds
        {
            get { return (mCardsUsed / 3); }
        }

        public bool IsRoundComplete
        {
            get { return (mPlayedCards.Count == 3); }
        }

        public bool IsPlayingComplete
        {
            get { return (Rounds == 10); }
        }

        public Card? HighestCard
        {
            get
            {
                Card? highestCard = null;

                foreach (var card in mPlayedCards)
                {
                    if ((!highestCard.HasValue) || (card.IsHigherThan(highestCard.Value, mGame.BiddingContext.TrumpSuit)))
                        highestCard = card;
                }

                return highestCard;   
            }
        }

        public Hand TrickWinner
        {
            get
            {
                if (!HighestCard.HasValue)
                    return null;

                return mGame.Hands.First(hand => hand.PlayedCard.Equals(HighestCard.Value));
            }
        }

        internal CardCollection Discards
        {
            get { return mDiscards; }
        }

        private readonly Game mGame;
        private Card? mLeadingCard;
        private int mCardsUsed;
        private readonly CardCollection mPlayedCards = new CardCollection();
        private readonly CardCollection mDiscards = new CardCollection();
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
