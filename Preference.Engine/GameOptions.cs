// 17/01/2012 by Dmitry Vorobyev.

using System;

namespace Preference.Engine
{
    /// <summary>
    /// Contains different game options such as the number of players etc.
    /// </summary>
    public class GameOptions
    {
        public int NumberOfPlayers
        {
            get { return mNumberOfPlayers; }
            set
            {
                if ((value < 3) || (value > 4))
                    throw new InvalidOperationException("Number of players should be 3 to 4.");

                mNumberOfPlayers = value;
            }
        }

        public int MaxPool
        {
            get { return mMaxPool; }
            set
            {
                if ((value < 1) || (value > 1000))
                    throw new InvalidOperationException("Maximum pool value should be 1 to 1000.");
                
                mMaxPool = value;
            }
        }

        /// <summary>
        /// Gets or sets how the AI player strong is.
        /// </summary>
        public GameDifficultyLevel DifficultyLevel
        {
            get { return mDifficultyLevel; }
            set { mDifficultyLevel = value; }
        }

        private int mNumberOfPlayers = 3;
        private int mMaxPool = 10;
        private GameDifficultyLevel mDifficultyLevel = GameDifficultyLevel.Moderate;
    }
}
