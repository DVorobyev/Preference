using System;

namespace Preference.Engine.AI.Bidding
{
    internal static class CardSuitDistribution
    {
        internal static int GetDistribution(int cards, int discards, CardSuit? exceptSuit)
        {
            throw new NotImplementedException();    
        }

        private static int Create(
            int suit1,
            int discards1,
            int suit2,
            int discards2,
            int suit3,
            int discards3,
            int suit4,
            int discards4)
        {
            int distribution = 0;

            distribution |= ((suit1 << 0) | (discards1 << 6));
            distribution |= ((suit2 << 8) | (discards2 << 14));
            distribution |= ((suit3 << 16) | (discards3 << 22));
            distribution |= ((suit4 << 24) | (discards4 << 30));

            return distribution;
        }

        internal static readonly int SD_70_20_02 = Create(7, 0, 2, 0, 0, 2, 0, 0);
        internal static readonly int SD_70_22_00 = Create(7, 0, 2, 2, 0, 0, 0, 0);
        internal static readonly int SD_60_30_02 = Create(6, 0, 3, 0, 0, 2, 0, 0);
        internal static readonly int SD_60_22_10 = Create(6, 0, 2, 2, 1, 0, 0, 0);
        internal static readonly int SD_60_21_11 = Create(6, 0, 2, 1, 1, 1, 0, 0);
        internal static readonly int SD_60_20_12 = Create(6, 0, 2, 0, 1, 2, 0, 0);
        internal static readonly int SD_50_40_02 = Create(5, 0, 4, 0, 0, 2, 0, 0);
        internal static readonly int SD_50_31_11 = Create(5, 0, 3, 1, 1, 1, 0, 0);
        internal static readonly int SD_50_30_12 = Create(5, 0, 3, 0, 1, 2, 0, 0);
        internal static readonly int SD_50_22_20 = Create(5, 0, 2, 2, 2, 0, 0, 0);
        internal static readonly int SD_50_20_22 = Create(5, 0, 2, 0, 2, 2, 0, 0);
        internal static readonly int SD_40_40_12 = Create(4, 0, 4, 0, 1, 2, 0, 0);
        internal static readonly int SD_40_30_22 = Create(4, 0, 3, 0, 2, 2, 0, 0);
        internal static readonly int SD_31_30_31 = Create(3, 1, 3, 0, 3, 1, 0, 0);
        internal static readonly int SD_30_31_31 = Create(3, 0, 3, 1, 3, 1, 0, 0);
        internal static readonly int SD_31_31_30 = Create(3, 1, 3, 1, 3, 0, 0, 0);
        
        internal static readonly int SD_70_10_11 = Create(7, 0, 1, 0, 1, 1, 0, 0);
        internal static readonly int SD_70_11_10 = Create(7, 0, 1, 1, 1, 0, 0, 0);
        internal static readonly int SD_70_21_00 = Create(7, 0, 2, 1, 0, 0, 0, 0);
        internal static readonly int SD_70_20_01 = Create(7, 0, 2, 0, 0, 1, 0, 0);
        internal static readonly int SD_60_30_01 = Create(6, 0, 3, 0, 0, 1, 0, 0);
        internal static readonly int SD_60_21_01 = Create(6, 0, 2, 1, 0, 1, 0, 0);
        internal static readonly int SD_60_20_11 = Create(6, 0, 2, 0, 1, 1, 0, 0);
        internal static readonly int SD_50_40_01 = Create(5, 0, 4, 0, 0, 1, 0, 0);
        internal static readonly int SD_50_31_10 = Create(5, 0, 3, 1, 1, 0, 0, 0);
        internal static readonly int SD_50_30_11 = Create(5, 0, 3, 0, 1, 1, 0, 0);
        internal static readonly int SD_50_20_21 = Create(5, 0, 2, 0, 2, 1, 0, 0);
        internal static readonly int SD_50_21_20 = Create(5, 0, 2, 1, 2, 0, 0, 0);
        internal static readonly int SD_40_40_11 = Create(4, 0, 4, 0, 1, 1, 0, 0);
        internal static readonly int SD_40_30_21 = Create(4, 0, 3, 0, 2, 1, 0, 0);
        internal static readonly int SD_30_30_31 = Create(3, 0, 3, 0, 3, 1, 0, 0);
        internal static readonly int SD_30_31_30 = Create(3, 0, 3, 1, 3, 0, 0, 0);
        internal static readonly int SD_31_30_30 = Create(3, 1, 3, 0, 3, 0, 0, 0);
        
        internal static readonly int SD_70_10_10 = Create(7, 0, 1, 0, 1, 0, 0, 0);
        internal static readonly int SD_70_20_00 = Create(7, 0, 2, 0, 0, 0, 0, 0);
        internal static readonly int SD_60_30_00 = Create(6, 0, 3, 0, 0, 0, 0, 0);
        internal static readonly int SD_60_20_10 = Create(6, 0, 2, 0, 1, 0, 0, 0);
        internal static readonly int SD_50_40_00 = Create(5, 0, 4, 0, 0, 0, 0, 0);
        internal static readonly int SD_50_30_10 = Create(5, 0, 3, 0, 1, 0, 0, 0);
        internal static readonly int SD_50_20_20 = Create(5, 0, 2, 0, 2, 0, 0, 0);
        internal static readonly int SD_40_40_10 = Create(4, 0, 4, 0, 1, 0, 0, 0);
        internal static readonly int SD_40_30_20 = Create(4, 0, 3, 0, 2, 0, 0, 0);
        internal static readonly int SD_30_30_30 = Create(3, 0, 3, 0, 3, 0, 0, 0);
        
        internal static readonly int SD_60_11_11 = Create(6, 0, 1, 1, 1, 1, 0, 0);
        internal static readonly int SD_60_22_00 = Create(6, 0, 2, 2, 0, 0, 0, 0);
        internal static readonly int SD_60_20_02 = Create(6, 0, 2, 0, 0, 2, 0, 0);
        internal static readonly int SD_50_30_02 = Create(5, 0, 3, 0, 0, 2, 0, 0);
        internal static readonly int SD_50_21_11 = Create(5, 0, 2, 1, 1, 1, 0, 0);
        internal static readonly int SD_50_20_12 = Create(5, 0, 2, 0, 1, 2, 0, 0);
        internal static readonly int SD_40_40_02 = Create(4, 0, 4, 0, 0, 2, 0, 0);
        internal static readonly int SD_40_30_12 = Create(4, 0, 3, 0, 1, 2, 0, 0);
        internal static readonly int SD_40_20_22 = Create(4, 0, 2, 0, 2, 2, 0, 0);
        internal static readonly int SD_40_22_20 = Create(4, 0, 2, 2, 2, 0, 0, 0);
        internal static readonly int SD_40_21_21 = Create(4, 0, 2, 1, 2, 1, 0, 0);
        internal static readonly int SD_30_30_22 = Create(3, 0, 3, 0, 2, 2, 0, 0);

        internal static readonly int SD_Any = -1;
    }
}
