// 17/01/2012 by Dmitry Vorobyev.

using System.Collections.Generic;

namespace Preference.Engine
{
    /// <summary>
    /// When implemented, represents either a computer or human player.
    /// </summary>
    public abstract class Player
    {
        protected Player(Game game)
        {
            mGame = game;
            mScore = new PlayerScore(game.Options.NumberOfPlayers);
        }

        public abstract Bid MakeBid();
        
        public abstract Bid DeclareContract();

        public abstract IList<Card> Discard();

        public abstract DefenderAction SelectDefenderAction();

        public abstract bool IsOpenCardsWhisting();

        public abstract Card PlayCard();

        public Hand Hand { get; internal set; }

        protected Game Game
        {
            get { return mGame; }
        }

        public PlayerScore Score
        {
            get { return mScore; }
        }

        private readonly Game mGame;
        private readonly PlayerScore mScore;
    }
}
