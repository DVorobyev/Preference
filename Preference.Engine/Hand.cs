// 17/01/2012 by Dmitry Vorobyev.

using System.Collections.Generic;
using System.ComponentModel;

using Preference.Engine.Annotations;

namespace Preference.Engine
{
    /// <summary>
    /// When implemented, represents a hand that holds the cards.
    /// </summary>
    public class Hand : INotifyPropertyChanged
    {
        internal Hand(Game game, int index)
        {
            mGame = game;
            mIndex = index;
            mCards.CollectionChanged += (sender, args) => OnPropertyChanged("CardsAsString");
        }

        internal Hand NextHand { get; set; }
        internal Player Player { get; set; }
        internal Bid? LastBid { get; set; }
        internal DefenderAction? DefenderAction { get; set; }
        
        public int Tricks
        {
            get { return mTricks; }
            internal set
            {
                mTricks = value;
                OnPropertyChanged("Tricks");
            }
        }

        public int Index
        {
            get { return mIndex; }
        }

        public bool IsActive
        {
            get { return mIsActive; }
            internal set
            {
                mIsActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        public bool IsFirstHand
        {
            get { return (mGame.FirstHand == this); }
        }
        
        public Card? PlayedCard
        {
            get { return mPlayedCard; }
            internal set
            {
                mPlayedCard = value;
                OnPropertyChanged("PlayedCard");
            }
        }

        public CardCollection Cards
        {
            get { return mCards; }
        }

        public string CardsAsString
        {
            get { return mCards.ToString(); }
        }

        /// <summary>
        /// Gets whether the hand is open (visible to other players).
        /// </summary>
        public bool IsOpen { get; private set; }

        internal List<CardSuit> VoidSuits
        {
            get { return mVoidSuits; }
        }

        private readonly CardCollection mCards = new CardCollection();
       
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) 
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly Game mGame;
        private readonly int mIndex;
        private bool mIsActive;
        private Card? mPlayedCard;
        private int  mTricks;
        private List<CardSuit> mVoidSuits = new List<CardSuit>(); 
    }
}
