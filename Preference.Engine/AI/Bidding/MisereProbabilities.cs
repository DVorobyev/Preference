using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Preference.Engine.AI.Bidding
{
    internal static class MisereProbabilities
    {
        internal static double GetFailureProbability(int cardSet, int discardsCount, int otherSuitsDistribution, bool isOwnMove)
        {
            const double defaultProbability = 1.0;
            double probability;

            Key key = GetKey(cardSet, discardsCount, otherSuitsDistribution, isOwnMove);
            
            // Whenever a probability is not found in the table, this means one of the following:
            // 1) The card set is completely unsafe.
            // 2) The combination of input parameters is unexpected (such as weird discards etc.), so the case should be skipped.
            // That's why 1.0 is returned in these cases.
            return (gProbabilityTable.TryGetValue(key, out probability)) ? probability : defaultProbability;
        }

        private static KeyProvider GetKeyProvider(int cardSet, bool isOwnMove)
        {
            switch (cardSet)
            {
                case CardSet.CS79JA:
                case CardSet.CS79QKA:
                case CardSet.CS710JQKA:
                case CardSet.CS79KA:
                case CardSet.CS710JQK:
                case CardSet.CS710QKA:
                    return KeyProvider.KP2;
                case CardSet.CS8:
                case CardSet.CS9:
                case CardSet.CS10:
                case CardSet.CSJ:
                case CardSet.CS710:
                case CardSet.CS7J:
                case CardSet.CS7Q:
                    return (isOwnMove) ? KeyProvider.KP2 : KeyProvider.KP3;
                case CardSet.CS89:
                case CardSet.CS810:
                case CardSet.CS8J:
                case CardSet.CS79Q:
                case CardSet.CS710J:
                case CardSet.CS710Q:
                case CardSet.CS79K:
                case CardSet.CS710K:
                case CardSet.CS79QK:
                case CardSet.CS79QA:
                case CardSet.CS710JQ:
                case CardSet.CS710QK:
                case CardSet.CS710QA:
                    return KeyProvider.KP3;
                default:
                    return KeyProvider.KP1;
            }
        }

        private static Key GetKey(int cardSet, int discardsCount = 0, int otherSuitsDistribution = 0, bool isOwnMove = false)
        {
            Debug.Assert(cardSet <= CardSet.MaxValue);
            
            KeyProvider provider = GetKeyProvider(cardSet, isOwnMove);
            return provider.CreateKey(cardSet, discardsCount, otherSuitsDistribution, isOwnMove);
        }

        private static void AddEntry(int cardSet, double probability)
        {
            Key key = GetKey(cardSet);
            gProbabilityTable.Add(key, probability);
        }

        private static void AddEntry(int cardSet, int discardsCount, double probability)
        {
            Key key = GetKey(cardSet, discardsCount);
            gProbabilityTable.Add(key, probability);          
        }

        private static void AddEntry(
            int cardSet,
            int discardsCount,
            int otherSuitsDistribution,
            bool isOwnMove,
            double probability)
        {
            Key key = GetKey(cardSet, discardsCount, otherSuitsDistribution, isOwnMove);
            gProbabilityTable.Add(key, probability);
        }

        private static void SetUp()
        {
            // Add safe card sets.

            AddEntry(CardSet.CS7, .0);
            AddEntry(CardSet.CS78, .0);
            AddEntry(CardSet.CS79, .0);
            AddEntry(CardSet.CS789, .0);
            AddEntry(CardSet.CS7810, .0);
            AddEntry(CardSet.CS7910, .0);
            AddEntry(CardSet.CS78910, .0);
            AddEntry(CardSet.CS78J, .0);
            AddEntry(CardSet.CS79J, .0);
            AddEntry(CardSet.CS789J, .0);
            AddEntry(CardSet.CS7810J, .0);
            AddEntry(CardSet.CS7910J, .0);
            AddEntry(CardSet.CS78910J, .0);
            AddEntry(CardSet.CS789Q, .0);
            AddEntry(CardSet.CS7810Q, .0);
            AddEntry(CardSet.CS7910Q, .0);
            AddEntry(CardSet.CS78910Q, .0);
            AddEntry(CardSet.CS78JQ, .0);
            AddEntry(CardSet.CS79JQ, .0);
            AddEntry(CardSet.CS789JQ, .0);
            AddEntry(CardSet.CS7810JQ, .0);
            AddEntry(CardSet.CS7910JQ, .0);
            AddEntry(CardSet.CS78910JQ, .0);
            AddEntry(CardSet.CS789K, .0);
            AddEntry(CardSet.CS7810K, .0);
            AddEntry(CardSet.CS7910K, .0);
            AddEntry(CardSet.CS78910K, .0);
            AddEntry(CardSet.CS78JK, .0);
            AddEntry(CardSet.CS79JK, .0);
            AddEntry(CardSet.CS789JK, .0);
            AddEntry(CardSet.CS7810JK, .0);
            AddEntry(CardSet.CS7910JK, .0);
            AddEntry(CardSet.CS78910JK, .0);
            AddEntry(CardSet.CS789QK, .0);
            AddEntry(CardSet.CS7810QK, .0);
            AddEntry(CardSet.CS7910QK, .0);
            AddEntry(CardSet.CS78910QK, .0);
            AddEntry(CardSet.CS78JQK, .0);
            AddEntry(CardSet.CS79JQK, .0);
            AddEntry(CardSet.CS789JQK, .0);
            AddEntry(CardSet.CS7810JQK, .0);
            AddEntry(CardSet.CS7910JQK, .0);
            AddEntry(CardSet.CS78910JQK, .0);
            AddEntry(CardSet.CS78910A, .0);
            AddEntry(CardSet.CS789JA, .0);
            AddEntry(CardSet.CS7810JA, .0);
            AddEntry(CardSet.CS7910JA, .0);
            AddEntry(CardSet.CS78910JA, .0);
            AddEntry(CardSet.CS789QA, .0);
            AddEntry(CardSet.CS7810QA, .0);
            AddEntry(CardSet.CS7910QA, .0);
            AddEntry(CardSet.CS78910QA, .0);
            AddEntry(CardSet.CS78JQA, .0);
            AddEntry(CardSet.CS79JQA, .0);
            AddEntry(CardSet.CS789JQA, .0);
            AddEntry(CardSet.CS7810JQA, .0);
            AddEntry(CardSet.CS7910JQA, .0);
            AddEntry(CardSet.CS78910JQA, .0);
            AddEntry(CardSet.CS789KA, .0);
            AddEntry(CardSet.CS7810KA, .0);
            AddEntry(CardSet.CS7910KA, .0);
            AddEntry(CardSet.CS78910KA, .0);
            AddEntry(CardSet.CS78JKA, .0);
            AddEntry(CardSet.CS79JKA, .0);
            AddEntry(CardSet.CS789JKA, .0);
            AddEntry(CardSet.CS7810JKA, .0);
            AddEntry(CardSet.CS7910JKA, .0);
            AddEntry(CardSet.CS78910JKA, .0);
            AddEntry(CardSet.CS789QKA, .0);
            AddEntry(CardSet.CS7810QKA, .0);
            AddEntry(CardSet.CS7910QKA, .0);
            AddEntry(CardSet.CS78910QKA, .0);
            AddEntry(CardSet.CS78JQKA, .0);
            AddEntry(CardSet.CS79JQKA, .0);
            AddEntry(CardSet.CS789JQKA, .0);
            AddEntry(CardSet.CS7810JQKA, .0);
            AddEntry(CardSet.CS7910JQKA, .0);
            AddEntry(CardSet.CS78910JQKA, .0);

            AddEntry(CardSet.CS79JA, 0, 8.67);
            AddEntry(CardSet.CS79JA, 1, .0);
            AddEntry(CardSet.CS79JA, 2, .0);

            AddEntry(CardSet.CS79QKA, 0, 21.05);
            AddEntry(CardSet.CS79QKA, 1, .0);
            AddEntry(CardSet.CS79QKA, 2, .0);

            AddEntry(CardSet.CS710JQKA, 0, 47.37);
            AddEntry(CardSet.CS710JQKA, 1, .0);
            AddEntry(CardSet.CS710JQKA, 2, .0);

            AddEntry(CardSet.CS79KA, 0, 58.20);
            AddEntry(CardSet.CS79KA, 1, 21.05);
            AddEntry(CardSet.CS79KA, 2, .0);

            AddEntry(CardSet.CS710JQK, 0, 73.68);
            AddEntry(CardSet.CS710JQK, 1, 47.37);
            AddEntry(CardSet.CS710JQK, 2, .0);

            AddEntry(CardSet.CS710QKA, 0, 47.37);
            AddEntry(CardSet.CS710QKA, 1, .0);
            AddEntry(CardSet.CS710QKA, 2, .0);

            // Table A1-0.
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_Any, true, 0.31);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_Any, true, 52.94);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_Any, true, 72.96);
            AddEntry(CardSet.CSJ, 0, CardSuitDistribution.SD_Any, true, 91.64);

            // Table A1-1.
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_Any, true, 1.08);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_Any, true, 53.72);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_Any, true, 80.03);
            AddEntry(CardSet.CSJ, 1, CardSuitDistribution.SD_Any, true, 92.41);

            // Table A1-2.
            
            AddEntry(CardSet.CS8, 2, CardSuitDistribution.SD_Any, true, 3.25);
            AddEntry(CardSet.CS9, 2, CardSuitDistribution.SD_Any, true, 55.88);
            AddEntry(CardSet.CS10, 2, CardSuitDistribution.SD_Any, true, 82.20);
            AddEntry(CardSet.CSJ, 2, CardSuitDistribution.SD_Any, true, 94.58);

            // Table A2-0.
            
            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_Any, true, 53.72);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_Any, true, 80.03);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_Any, true, 92.41);

            // Table A2-1.
            
            AddEntry(CardSet.CS710, 1, CardSuitDistribution.SD_Any, true, 55.88);
            AddEntry(CardSet.CS7J, 1, CardSuitDistribution.SD_Any, true, 82.20);
            AddEntry(CardSet.CS7Q, 1, CardSuitDistribution.SD_Any, true, 94.58);

            // Table A2-2.
            
            AddEntry(CardSet.CS710, 2, CardSuitDistribution.SD_Any, true, 61.30);
            AddEntry(CardSet.CS7J, CardSuitDistribution.SD_Any, 2, true, 87.62);

            // Table B1-0.
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_70_20_02, false, 1.74);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_70_20_02, false, 54.13);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_70_20_02, false, 80.21);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_70_22_00, false, 2.79);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_70_22_00, false, 54.84);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_70_22_00, false, 80.65);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_60_30_02, false, 6.69);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_60_30_02, false, 56.89);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_60_30_02, false, 81.55);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_60_22_10, false, 4.47);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_60_22_10, false, 55.67);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_60_22_10, false, 80.92);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_60_21_11, false, 3.52);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_60_21_11, false, 55.12);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_60_21_11, false, 80.62);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_60_20_12, false, 3.35);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_60_20_12, false, 55.03);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_60_20_12, false, 80.57);  
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_50_40_02, false, 12.13);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_50_40_02, false, 59.24);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_50_40_02, false, 82.44);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_50_31_11, false, 10.79);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_50_31_11, false, 58.74);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_50_31_11, false, 82.35);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_50_30_12, false, 7.90);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_50_30_12, false, 57.33);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_50_30_12, false, 81.69);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_50_20_22, false, 5.67);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_50_20_22, false, 56.11);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_50_20_22, false, 81.03);     
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_50_22_20, false, 5.67);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_50_22_20, false, 56.11);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_50_22_20, false, 81.03);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_40_40_12, false, 12.05);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_40_40_12, false, 58.99);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_40_40_12, false, 82.21);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_40_30_22, false, 9.04);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_40_30_22, false, 57.74);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_40_30_22, false, 81.81);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_40_30_22, false, 9.04);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_40_30_22, false, 57.74);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_40_30_22, false, 81.81);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_31_30_31, false, 13.45);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_31_30_31, false, 60.28);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_31_30_31, false, 83.11);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_31_31_30, false, 13.45);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_31_31_30, false, 60.28);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_31_31_30, false, 83.11);
            
            AddEntry(CardSet.CS8, 0, CardSuitDistribution.SD_30_31_31, false, 13.45);
            AddEntry(CardSet.CS9, 0, CardSuitDistribution.SD_30_31_31, false, 60.28);
            AddEntry(CardSet.CS10, 0, CardSuitDistribution.SD_30_31_31, false, 83.11);

            // Table B1-1.
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_70_10_11, false, 4.95);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_70_10_11, false, 56.81);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_70_10_11, false, 82.35);  
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_70_11_10, false, 4.95);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_70_11_10, false, 56.81);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_70_11_10, false, 82.35);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_70_21_00, false, 5.79);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_70_21_00, false, 57.34);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_70_21_00, false, 82.65);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_70_20_01, false, 5.07);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_70_20_01, false, 56.89);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_70_20_01, false, 82.40);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_60_30_01, false, 15.44);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_60_30_01, false, 62.13);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_60_30_01, false, 84.64);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_60_21_01, false, 8.41);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_60_21_01, false, 58.30);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_60_21_01, false, 82.57);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_60_20_11, false, 7.75);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_60_20_11, false, 57.94);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_60_20_11, false, 82.38);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_50_40_01, false, 21.92);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_50_40_01, false, 64.68);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_50_40_01, false, 85.38);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_50_31_10, false, 21.16);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_50_31_10, false, 64.53);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_50_31_10, false, 85.38);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_50_30_11, false, 17.98);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_50_30_11, false, 63.38);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_50_30_11, false, 85.27);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_50_20_21, false, 10.41);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_50_20_21, false, 59.12);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_50_20_21, false, 82.98);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_50_21_20, false, 10.41);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_50_21_20, false, 59.12);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_50_21_20, false, 82.98);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_40_40_11, false, 21.68);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_40_40_11, false, 64.25);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_40_40_11, false, 84.99);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_40_30_21, false, 18.58);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_40_30_21, false, 63.25);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_40_30_21, false, 84.89);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_30_30_31, false, 27.36);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_30_30_31, false, 68.13);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_30_30_31, false, 87.33);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_30_31_30, false, 27.36);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_30_31_30, false, 68.13);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_30_31_30, false, 87.33);
            
            AddEntry(CardSet.CS8, 1, CardSuitDistribution.SD_31_30_30, false, 27.36);
            AddEntry(CardSet.CS9, 1, CardSuitDistribution.SD_31_30_30, false, 68.13);
            AddEntry(CardSet.CS10, 1, CardSuitDistribution.SD_31_30_30, false, 87.33);

            // Table B1-2.
            
            AddEntry(CardSet.CS8, 2, CardSuitDistribution.SD_70_10_10, false, 11.92);
            AddEntry(CardSet.CS9, 2, CardSuitDistribution.SD_70_10_10, false, 62.38);
            AddEntry(CardSet.CS10, 2, CardSuitDistribution.SD_70_10_10, false, 86.53);
            
            AddEntry(CardSet.CS8, 2, CardSuitDistribution.SD_70_20_00, false, 12.54);
            AddEntry(CardSet.CS9, 2, CardSuitDistribution.SD_70_20_00, false, 62.74);
            AddEntry(CardSet.CS10, 2, CardSuitDistribution.SD_70_20_00, false, 86.70);
            
            AddEntry(CardSet.CS8, 2, CardSuitDistribution.SD_60_30_00, false, 28.36);
            AddEntry(CardSet.CS9, 2, CardSuitDistribution.SD_60_30_00, false, 69.62);
            AddEntry(CardSet.CS10, 2, CardSuitDistribution.SD_60_30_00, false, 88.72);
            
            AddEntry(CardSet.CS8, 2, CardSuitDistribution.SD_60_20_10, false, 15.20);
            AddEntry(CardSet.CS9, 2, CardSuitDistribution.SD_60_20_10, false, 62.64);
            AddEntry(CardSet.CS10, 2, CardSuitDistribution.SD_60_20_10, false, 85.27);
            
            AddEntry(CardSet.CS8, 2, CardSuitDistribution.SD_50_40_00, false, 34.41);
            AddEntry(CardSet.CS9, 2, CardSuitDistribution.SD_50_40_00, false, 71.66);
            AddEntry(CardSet.CS10, 2, CardSuitDistribution.SD_50_40_00, false, 89.32);
            
            AddEntry(CardSet.CS8, 2, CardSuitDistribution.SD_50_30_10, false, 33.17);
            AddEntry(CardSet.CS9, 2, CardSuitDistribution.SD_50_30_10, false, 72.38);
            AddEntry(CardSet.CS10, 2, CardSuitDistribution.SD_50_30_10, false, 90.40);
            
            AddEntry(CardSet.CS8, 2, CardSuitDistribution.SD_50_20_20, false, 18.21);
            AddEntry(CardSet.CS9, 2, CardSuitDistribution.SD_50_20_20, false, 64.42);
            AddEntry(CardSet.CS10, 2, CardSuitDistribution.SD_50_20_20, false, 86.72);
            
            AddEntry(CardSet.CS8, 2, CardSuitDistribution.SD_40_40_10, false, 33.79);
            AddEntry(CardSet.CS9, 2, CardSuitDistribution.SD_40_40_10, false, 70.81);
            AddEntry(CardSet.CS10, 2, CardSuitDistribution.SD_40_40_10, false, 88.34);
            
            AddEntry(CardSet.CS8, 2, CardSuitDistribution.SD_40_30_20, false, 32.39);
            AddEntry(CardSet.CS9, 2, CardSuitDistribution.SD_40_30_20, false, 71.09);
            AddEntry(CardSet.CS10, 2, CardSuitDistribution.SD_40_30_20, false, 89.18);
            
            AddEntry(CardSet.CS8, 2, CardSuitDistribution.SD_30_30_30, false, 51.31);
            AddEntry(CardSet.CS9, 2, CardSuitDistribution.SD_30_30_30, false, 82.06);
            AddEntry(CardSet.CS10, 2, CardSuitDistribution.SD_30_30_30, false, 94.59);

            // Table B2-0.
            
            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_60_11_11, false, 14.72);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_60_11_11, false, 52.41);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_60_11_11, false, 76.86);
            AddEntry(CardSet.CS89, 0, CardSuitDistribution.SD_60_11_11, false, 25.88);
            AddEntry(CardSet.CS810, 0, CardSuitDistribution.SD_60_11_11, false, 28.21);
            AddEntry(CardSet.CS8J, 0, CardSuitDistribution.SD_60_11_11, false, 59.33);

            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_60_22_00, false, 20.30);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_60_22_00, false, 56.63);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_60_22_00, false, 79.67);
            AddEntry(CardSet.CS89, 0, CardSuitDistribution.SD_60_22_00, false, 33.75);
            AddEntry(CardSet.CS810, 0, CardSuitDistribution.SD_60_22_00, false, 36.43);
            AddEntry(CardSet.CS8J, 0, CardSuitDistribution.SD_60_22_00, false, 64.51);

            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_60_20_02, false, 15.19);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_60_20_02, false, 52.77);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_60_20_02, false, 77.10);
            AddEntry(CardSet.CS89, 0, CardSuitDistribution.SD_60_20_02, false, 26.67);
            AddEntry(CardSet.CS810, 0, CardSuitDistribution.SD_60_20_02, false, 28.85);
            AddEntry(CardSet.CS8J, 0, CardSuitDistribution.SD_60_20_02, false, 59.82);

            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_50_30_02, false, 32.82);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_50_30_02, false, 65.93);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_50_30_02, false, 85.80);
            AddEntry(CardSet.CS89, 0, CardSuitDistribution.SD_50_30_02, false, 50.92);
            AddEntry(CardSet.CS810, 0, CardSuitDistribution.SD_50_30_02, false, 54.58);
            AddEntry(CardSet.CS8J, 0, CardSuitDistribution.SD_50_30_02, false, 75.61);

            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_50_21_11, false, 19.33);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_50_21_11, false, 56.19);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_50_21_11, false, 80.22);
            AddEntry(CardSet.CS89, 0, CardSuitDistribution.SD_50_21_11, false, 33.85);
            AddEntry(CardSet.CS810, 0, CardSuitDistribution.SD_50_21_11, false, 36.77);
            AddEntry(CardSet.CS8J, 0, CardSuitDistribution.SD_50_21_11, false, 65.06);

            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_50_20_12, false, 17.77);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_50_20_12, false, 54.93);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_50_20_12, false, 79.40);
            AddEntry(CardSet.CS89, 0, CardSuitDistribution.SD_50_20_12, false, 31.45);
            AddEntry(CardSet.CS810, 0, CardSuitDistribution.SD_50_20_12, false, 34.16);
            AddEntry(CardSet.CS8J, 0, CardSuitDistribution.SD_50_20_12, false, 63.46);

            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_40_40_02, false, 32.91);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_40_40_02, false, 63.72);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_40_40_02, false, 82.33);
            AddEntry(CardSet.CS89, 0, CardSuitDistribution.SD_40_40_02, false, 46.47);
            AddEntry(CardSet.CS810, 0, CardSuitDistribution.SD_40_40_02, false, 48.30);
            AddEntry(CardSet.CS8J, 0, CardSuitDistribution.SD_40_40_02, false, 70.79);

            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_40_30_12, false, 31.56);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_40_30_12, false, 63.99);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_40_30_12, false, 83.66);
            AddEntry(CardSet.CS89, 0, CardSuitDistribution.SD_40_30_12, false, 48.00);
            AddEntry(CardSet.CS810, 0, CardSuitDistribution.SD_40_30_12, false, 50.57);
            AddEntry(CardSet.CS8J, 0, CardSuitDistribution.SD_40_30_12, false, 72.67);

            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_40_20_22, false, 23.72);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_40_20_22, false, 58.52);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_40_20_22, false, 80.47);
            AddEntry(CardSet.CS89, 0, CardSuitDistribution.SD_40_20_22, false, 38.15);
            AddEntry(CardSet.CS810, 0, CardSuitDistribution.SD_40_20_22, false, 40.46);
            AddEntry(CardSet.CS8J, 0, CardSuitDistribution.SD_40_20_22, false, 66.68);

            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_40_22_20, false, 23.72);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_40_22_20, false, 58.52);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_40_22_20, false, 80.47);
            AddEntry(CardSet.CS89, 0, CardSuitDistribution.SD_40_22_20, false, 38.15);
            AddEntry(CardSet.CS810, 0, CardSuitDistribution.SD_40_22_20, false, 40.46);
            AddEntry(CardSet.CS8J, 0, CardSuitDistribution.SD_40_22_20, false, 66.68);

            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_40_21_21, false, 21.35);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_40_21_21, false, 56.77);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_40_21_21, false, 79.02);
            AddEntry(CardSet.CS89, 0, CardSuitDistribution.SD_40_21_21, false, 35.64);
            AddEntry(CardSet.CS810, 0, CardSuitDistribution.SD_40_21_21, false, 36.64);
            AddEntry(CardSet.CS8J, 0, CardSuitDistribution.SD_40_21_21, false, 64.73);

            AddEntry(CardSet.CS710, 0, CardSuitDistribution.SD_30_30_22, false, 38.79);
            AddEntry(CardSet.CS7J, 0, CardSuitDistribution.SD_30_30_22, false, 71.73);
            AddEntry(CardSet.CS7Q, 0, CardSuitDistribution.SD_30_30_22, false, 89.17);
            AddEntry(CardSet.CS89, 0, CardSuitDistribution.SD_30_30_22, false, 62.66);
            AddEntry(CardSet.CS810, 0, CardSuitDistribution.SD_30_30_22, false, 66.99);
            AddEntry(CardSet.CS8J, 0, CardSuitDistribution.SD_30_30_22, false, 83.00);

            // Table B2-1.
        }

        static MisereProbabilities()
        {
            SetUp();
        }

        private abstract class KeyProvider
        {
            internal abstract Key CreateKey(int cardSet, int discardsCount, int otherSuitsDistribution, bool isOwnMove);

            internal static readonly KeyProvider KP1 = new KeyProvider1();
            internal static readonly KeyProvider KP2 = new KeyProvider2();
            internal static readonly KeyProvider KP3 = new KeyProvider3();
        }

        /// <summary>
        /// A key provider only considering the specified card set.
        /// </summary>
        private class KeyProvider1 : KeyProvider
        {
            internal override Key CreateKey(int cardSet, int discardsCount, int otherSuitsDistribution, bool isOwnMove)
            {
                return new Key(cardSet);
            }
        }

        /// <summary>
        /// A key provider considering the specified card set and the number of discarded cards.
        /// </summary>
        private class KeyProvider2 : KeyProvider
        {
            internal override Key CreateKey(int cardSet, int discardsCount, int otherSuitsDistribution, bool isOwnMove)
            {
                return new Key(cardSet, discardsCount);
            }
        }

        /// <summary>
        /// A key provider considering the specified card set, the number of discarded cards, the distribution of other suits,
        /// and whether the move is own.
        /// </summary>
        private class KeyProvider3 : KeyProvider
        {
            internal override Key CreateKey(int cardSet, int discardsCount, int otherSuitsDistribution, bool isOwnMove)
            {
                return new Key(cardSet, discardsCount, otherSuitsDistribution, isOwnMove);
            }
        }

        /// <summary>
        /// Represents a composite key used to fetch a probability of a certain card set from the probability table.
        /// Using <see cref="Tuple{T1,T2,T3,T4}"/> because it provides built-in hash code calculation.
        /// </summary>
        private class Key : Tuple<int, int, int, bool>
        {
            internal Key(int cardSet, int discardsCount = 0, int otherSuitsDistribution = 0, bool isOwnMove = false) :
                base(cardSet, discardsCount, otherSuitsDistribution, isOwnMove)
            {
            }
        }

        private static readonly Dictionary<Key, double> gProbabilityTable =  new Dictionary<Key, double>();
    }
}
