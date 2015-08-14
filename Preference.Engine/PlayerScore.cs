// 21/01/2012 by Dmitry Vorobyev.

using System.Collections.ObjectModel;

namespace Preference.Engine
{
    public class PlayerScore
    {
        public PlayerScore(int numberOfPlayers)
        {
            mWhistPoints = new int[numberOfPlayers];
        }

        internal void IncreasePool(int amount)
        {
            mPool += amount;
        }

        internal void IncreaseDump(int amount)
        {
            mPool += amount;
        }

        internal void IncreaseWhistPoints(int index, int amount)
        {
            mWhistPoints[index] += amount;
        }

        public int Pool
        {
            get { return mPool; }
        }

        public int Dump
        {
            get { return mDump; }
        }

        public ReadOnlyCollection<int> WhistPoints
        {
            get { return new ReadOnlyCollection<int>(mWhistPoints); }
        }

        private int mPool;
        private int mDump;
        private readonly int[] mWhistPoints;
    }
}
