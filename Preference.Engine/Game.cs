// Copyright © 2013 Dmitry Vorobyev.

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

using Preference.Engine.AI;
using Preference.Engine.Annotations;
using Preference.Engine.Rules;

namespace Preference.Engine
{
    /// <summary>
    /// Represents the main game class.
    /// </summary>
    public class Game : INotifyPropertyChanged
    {
        public Game(GameRules rules)
        {
            if (rules == null)
                throw new ArgumentNullException("rules");

            mRules = rules;
        }

        public void Initialize()
        {
            InitializeHands();
            InitializePlayers();
        }

        private void InitializeHands()
        {
            mHands = new Hand[3];

            for (int i = 0; i < mHands.Length; i++)
                mHands[i] = new Hand(this, i);
            
            for (int i = 0; i < mHands.Length; i++)
                mHands[i].NextHand = mHands[(i < mHands.Length - 1) ? i + 1 : 0];

            mDealer = mHands[RandomHelper.Next(mHands.Length)];
        }

        private void InitializePlayers()
        {
            mPlayers = new Player[mOptions.NumberOfPlayers];
            for (int i = 0; i < mPlayers.Length; i++)
                mPlayers[i] = new AIPlayer(this);
        }

        public void Run()
        {
            while (!IsGameComplete())
                RunRound();

            mRules.CalculateFinalScore(this);
        }

        private bool IsGameComplete()
        {
            return mPlayers.All(player => player.Score.Pool == mOptions.MaxPool);
        }

        private void RunRound()
        {
            InitializeRound();
            
            DealCards();
            DoBidding();

            if (mGameType != Engine.GameType.AllPass)
            {
                TakeWidow();
                DropCards();
            }

            if (mGameType == Engine.GameType.TrickPlay)
            {
                MakeFinalBid();
                SelectDefenderActions();
                
                if ((FirstDefender.DefenderAction == DefenderAction.Pass) && (SecondDefender.DefenderAction == DefenderAction.Pass))
                {
                    mRules.CalculateScore(this);
                    return;
                }
            }

            DoPlaying();
            mRules.CalculateScore(this);
        }

        private void InitializeRound()
        {
            mWidow = new CardCollection();
            mBiddingContext = new BiddingContext(this);
            mPlayingContext = new PlayingContext(this);

            for (int i = 0; i < mHands.Length; i++)
                AttachPlayer(mHands[i], mPlayers[i]);

            foreach (var hand in mHands)
            {
                hand.LastBid = null;
                hand.Tricks = 0;
                hand.VoidSuits.Clear();
            }

            mDealer = mDealer.NextHand;

            OnRoundStarted();
        }

        private void DealCards()
        {
            var deck = new CardCollection();

            for (var suit = CardSuit.Spades; suit <= CardSuit.Hearts; suit++)
            {
                for (var rank = CardRank.Seven; rank <= CardRank.Ace; rank++)
                    deck.Add(new Card(suit, rank));
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var card = deck.RandomPick();
                    mHands[i].Cards.Add(card);
                }
            }

            deck.MoveTo(mWidow);
        }

        private void DoBidding()
        {
            ActivateHand(FirstHand);

            while (true)
            {
                var bid = mActiveHand.Player.MakeBid();
                mBiddingContext.RegisterBid(bid);

                if (mBiddingContext.IsBiddingComplete)
                    break;

                ActivateNextBiddingHand();
            }

            mGameType = mBiddingContext.GetBiddingResult();

            if (mGameType != Engine.GameType.AllPass)
                mDeclarer = mBiddingContext.HighestBidMaker;
        }

        private void TakeWidow()
        {
            mWidow.MoveTo(Declarer.Cards);    
        }

        private void DropCards()
        {
            ActivateHand(Declarer);

            var cards = mActiveHand.Player.Discard();

            if (cards.Length != 2)
                throw new RuleViolationException("The player should drop 2 cards.");

            if (!mActiveHand.Cards.Contains(cards[0]) || !mActiveHand.Cards.Contains(cards[1]))
                throw new InvalidOperationException("An attempt to drop a card that is not owned by the player.");

            foreach (var card in cards)
                mActiveHand.Cards.Remove(card);
        }

        private void MakeFinalBid()
        {
            var finalBid = mActiveHand.Player.DeclareContract();
            mBiddingContext.RegisterFinalBid(finalBid);
        }

        private void SelectDefenderActions()
        {
            ActivateHand(FirstDefender);
            SelectDefenderAction();
            
            ActivateHand(SecondDefender);
            SelectDefenderAction();

            if ((FirstDefender.DefenderAction == DefenderAction.Pass) && (SecondDefender.DefenderAction == DefenderAction.Pass))
            {
                ActivateHand(FirstDefender);
                SelectDefenderAction();
            }

            if ((FirstDefender.DefenderAction == DefenderAction.Whist) && (SecondDefender.DefenderAction == DefenderAction.Pass))
                AttachPlayer(SecondDefender, FirstDefender.Player);            
            else if ((FirstDefender.DefenderAction == DefenderAction.Pass) && (SecondDefender.DefenderAction == DefenderAction.Whist))
                AttachPlayer(FirstDefender, SecondDefender.Player);
        }

        private void SelectDefenderAction()
        {
            mActiveHand.DefenderAction = mActiveHand.Player.SelectDefenderAction(); 
        }

        private void DoPlaying()
        {
            ActivateHand(FirstHand);

            while (true)
            {
                Card card = mActiveHand.Player.PlayCard();
                mPlayingContext.RegisterCard(card);

                OnCardPlayed(mActiveHand, card);

                if (mPlayingContext.IsRoundComplete)
                {
                    mPlayingContext.TrickWinner.Tricks++;
                    OnRoundCompleted();
                }

                if (mPlayingContext.IsPlayingComplete)
                {
                    mPlayingContext.ResetRound();
                    break;
                }

                if (mPlayingContext.IsRoundComplete)
                {
                    ActivateHand(mPlayingContext.TrickWinner);
                    mPlayingContext.ResetRound();
                }
                else
                {
                    ActivateNextHand();
                }
            }
        }

        private static void AttachPlayer(Hand hand, Player player)
        {
            hand.Player = player;
            player.Hand = hand;
        }

        internal Hand GetHand(int index)
        {
            return mHands[index];
        }

        private void ActivateNextBiddingHand()
        {
            do
            {
                ActivateNextHand();    
            } 
            while ((mActiveHand.LastBid.HasValue) && (mActiveHand.LastBid.Value.BidType == BidType.Pass));
        }

        private void ActivateNextHand()
        {
            do
            {
                ActivateHand(mActiveHand.NextHand);
            } 
            while ((mOptions.NumberOfPlayers == 4) && (mActiveHand == mDealer));
        }

        private void ActivateHand(Hand hand)
        {
            Debug.Assert(hand != null);

            if (mActiveHand != null)
                mActiveHand.IsActive = false;
            
            mActiveHand = hand;
            mActiveHand.IsActive = true;

            OnPropertyChanged("ActiveHand");
        }

        public void OnCardPlayed(Hand hand, Card card)
        {
            if (CardPlayed != null)
                CardPlayed(this, new PlayingActionEventArgs {Hand = hand, Card = card});
        }

        public void OnRoundStarted()
        {
            if (RoundStarted != null)
                RoundStarted(this, new EventArgs());
        }
        
        public void OnRoundCompleted()
        {
            if (RoundCompleted != null)
                RoundCompleted(this, new EventArgs());
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) 
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameRules Rules
        {
            get { return mRules; }
        }

        public GameOptions Options
        {
            get { return mOptions; }
        }

        public ReadOnlyCollection<Hand> Hands
        {
            get { return Array.AsReadOnly(mHands); }
        }

        public Hand Dealer
        {
            get { return mDealer; }
        }

        public Hand FirstHand
        {
            get { return mDealer.NextHand; }
        }

        public Hand ActiveHand
        {
            get { return mActiveHand; }
        }

        public ReadOnlyCollection<Player> Players
        {
            get { return Array.AsReadOnly(mPlayers); }
        }

        public CardCollection Widow
        {
            get { return mWidow; }
        }

        public BiddingContext BiddingContext
        {
            get { return mBiddingContext; }
        }

        public PlayingContext PlayingContext
        {
            get { return mPlayingContext; }
        }

        public GameType? GameType
        {
            get { return mGameType; }
        }

        public Hand Declarer
        {
            get { return mDeclarer; }
        }

        public Hand FirstDefender
        {
            get { return (Declarer != null) ? Declarer.NextHand : null; }
        }

        public Hand SecondDefender
        {
            get { return (FirstDefender != null) ? FirstDefender.NextHand : null; }
        }

        public bool? AreDefenderCardsOpen
        {
            get
            {
                if (mGameType == Engine.GameType.Misere)
                    return true;

                if ((FirstDefender != null) && (SecondDefender != null))
                    return (FirstDefender.DefenderAction == DefenderAction.Pass) ||
                           (SecondDefender.DefenderAction == DefenderAction.Pass);

                return null;
            }
        }

        public event EventHandler<PlayingActionEventArgs> CardPlayed;
        public event EventHandler RoundStarted;
        public event EventHandler RoundCompleted;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly GameRules mRules;
        private readonly GameOptions mOptions = new GameOptions();
        private Hand[] mHands;
        private Hand mDealer;
        private Hand mActiveHand;
        private Player[] mPlayers;
        private CardCollection mWidow;
        private BiddingContext mBiddingContext;
        private PlayingContext mPlayingContext;
        private GameType? mGameType;
        private Hand mDeclarer;
    }
}
