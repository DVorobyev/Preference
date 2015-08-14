// 17/01/2012 by Dmitry Vorobyev.

using System.Collections.Generic;

using Preference.Engine.AI.Bidding;
using Preference.Engine.AI.Playing;

namespace Preference.Engine.AI
{
    internal class AIPlayer : Player
    {
        internal AIPlayer(Game game) : base(game)
        {
            mBiddingAdviser = new BiddingAdviser(game);
            mPlayingAdviser = new PlayingAdviser(game);
        }

        public override Bid MakeBid()
        {
            return mBiddingAdviser.MakeBid();
        }

        public override Bid DeclareContract()
        {
            return new Bid(6, CardSuit.Spades);
        }

        public override IList<Card> Discard()
        {
            return new[] { Hand.Cards[0], Hand.Cards[1] };
        }

        public override DefenderAction SelectDefenderAction()
        {
            if (Hand == Game.FirstDefender)
                return DefenderAction.Whist;
            else
                return DefenderAction.Whist;
        }

        public override bool IsOpenCardsWhisting()
        {
            // TODO Only open cards whisting is supported at the moment.
            return true;
        }

        public override Card PlayCard()
        {
            return mPlayingAdviser.PlayCard();
        }

        private readonly BiddingAdviser mBiddingAdviser;
        private readonly PlayingAdviser mPlayingAdviser;
    }
}
