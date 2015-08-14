using System;

namespace Preference.Engine.AI.Bidding
{
    /// <summary>
    /// Bundles a number of tricks and a probability of that outcome.
    /// </summary>
    internal struct TrickProbability
    {
        internal TrickProbability(int tricks, double probability)
        {
            mTricks = tricks;
            mProbability = probability;
        }

        internal TrickProbability And(TrickProbability operand)
        {
            return new TrickProbability(Tricks + operand.Tricks, Probability * operand.Probability);
        }

        internal int Tricks
        {
            get { return mTricks; }
        }

        internal double Probability
        {
            get { return mProbability; }
        }

        private readonly int mTricks;
        private readonly double mProbability;
    }
}
