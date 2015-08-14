// Copyright © 2013 Dmitry Vorobyev.

namespace Preference.Engine.AI.Bidding
{
    internal abstract class TrickPlayEvaluator : HandEvaluator
    {
        protected TrickPlayEvaluator(Game game, int declaredTricks) : base(game)
        {
            mDeclaredTricks = declaredTricks;
        }

        protected int DeclaredTricks
        {
            get { return mDeclaredTricks; }
        }

        private readonly int mDeclaredTricks;
    }
}
