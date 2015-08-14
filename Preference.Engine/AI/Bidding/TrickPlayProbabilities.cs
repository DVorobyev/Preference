using System.Collections.Generic;
using System.Diagnostics;

namespace Preference.Engine.AI.Bidding
{
    internal static class TrickPlayProbabilities
    {
        internal static IEnumerable<TrickProbability> GetTrickProbabilities(int cardSet)
        {
            Debug.Assert(cardSet <= CardSet.MaxValue);
            return gTrickProbabilities[cardSet];
        }

        private static void AddEntry(int cardSet, int tricks, double probability)
        {
            gTrickProbabilities[cardSet].Add(new TrickProbability(tricks, probability));
        }

        private static void SetUp()
        {
            gTrickProbabilities.Initialize();

            // CSVoid
            AddEntry(CardSet.CSVoid, 0, 1.0);

            // CS7
            AddEntry(CardSet.CS7, 0, 1.0);

            // CS8
            AddEntry(CardSet.CS8, 0, 1.0);

            // CS78
            AddEntry(CardSet.CS78, 0, 1.0);

            // CS9
            AddEntry(CardSet.CS9, 0, 1.0);

            // CS79
            AddEntry(CardSet.CS79, 0, 1.0);

            // CS89
            AddEntry(CardSet.CS89, 0, 1.0);

            // CS789
            AddEntry(CardSet.CS789, 0, 1.0);

            // CS10
            AddEntry(CardSet.CS10, 0, 1.0);

            // CS710
            AddEntry(CardSet.CS710, 0, 1.0);

            // CS810
            AddEntry(CardSet.CS810, 0, 1.0);

            // CS7810
            AddEntry(CardSet.CS7810, 0, 1.0);

            // CS910
            AddEntry(CardSet.CS910, 0, 1.0);

            // CS7910
            AddEntry(CardSet.CS7910, 0, 1.0);

            // CS8910
            AddEntry(CardSet.CS8910, 0, 1.0);

            // CS78910
            AddEntry(CardSet.CS78910, 0, .125);
            AddEntry(CardSet.CS78910, 1, .5);
            AddEntry(CardSet.CS78910, 2, .375);

            // CSJ
            AddEntry(CardSet.CSJ, 0, 1.0);

            // CS7J
            AddEntry(CardSet.CS7J, 0, 1.0);

            // CS8J
            AddEntry(CardSet.CS8J, 0, 1.0);

            // CS78J
            AddEntry(CardSet.CS78J, 0, 1.0);

            // CS9J
            AddEntry(CardSet.CS9J, 0, 1.0);

            // CS79J
            AddEntry(CardSet.CS79J, 0, 1.0);

            // CS89J
            AddEntry(CardSet.CS89J, 0, 1.0);

            // CS789J
            AddEntry(CardSet.CS789J, 0, .125);
            AddEntry(CardSet.CS789J, 1, .5);
            AddEntry(CardSet.CS789J, 2, .375);

            // CS10J
            AddEntry(CardSet.CS10J, 0, 1.0);

            // CS710J
            AddEntry(CardSet.CS710J, 0, 1.0);

            // CS810J
            AddEntry(CardSet.CS810J, 0, 1.0);

            // CS7810J
            AddEntry(CardSet.CS7810J, 0, .125);
            AddEntry(CardSet.CS7810J, 1, .5);
            AddEntry(CardSet.CS7810J, 2, .375);

            // CS910J
            AddEntry(CardSet.CS910J, 0, 1.0);

            // CS7910J
            AddEntry(CardSet.CS7910J, 0, .125);
            AddEntry(CardSet.CS7910J, 1, .5);
            AddEntry(CardSet.CS7910J, 2, .375);

            // CS8910J
            AddEntry(CardSet.CS8910J, 1, .625);
            AddEntry(CardSet.CS8910J, 2, .375);

            // CS78910J
            AddEntry(CardSet.CS78910J, 2, .25);
            AddEntry(CardSet.CS78910J, 3, .75);

            // CSQ
            AddEntry(CardSet.CSQ, 0, 1.0);

            // CS7Q
            AddEntry(CardSet.CS7Q, 0, 1.0);

            // CS8Q
            AddEntry(CardSet.CS8Q, 0, 1.0);

            // CS78Q
            AddEntry(CardSet.CS78Q, 0, .9375);
            AddEntry(CardSet.CS78Q, 1, .0625);

            // CS9Q
            AddEntry(CardSet.CS9Q, 0, 1.0);

            // CS79Q
            AddEntry(CardSet.CS79Q, 0, .9375);
            AddEntry(CardSet.CS79Q, 1, .0625);

            // CS89Q
            AddEntry(CardSet.CS89Q, 0, .9375);
            AddEntry(CardSet.CS89Q, 1, .0625);

            // CS789Q
            AddEntry(CardSet.CS789Q, 1, .625);
            AddEntry(CardSet.CS789Q, 2, .375);

            // CS10Q
            AddEntry(CardSet.CS10Q, 0, 1.0);

            // CS710Q
            AddEntry(CardSet.CS710Q, 0, .9375);
            AddEntry(CardSet.CS710Q, 1, .0625);

            // CS810Q
            AddEntry(CardSet.CS810Q, 0, .9375);
            AddEntry(CardSet.CS810Q, 1, .0625);

            // CS7810Q
            AddEntry(CardSet.CS7810Q, 1, .625);
            AddEntry(CardSet.CS7810Q, 2, .375);

            // CS910Q
            AddEntry(CardSet.CS910Q, 0, .9375);
            AddEntry(CardSet.CS910Q, 1, .0625);

            // CS7910Q
            AddEntry(CardSet.CS7910Q, 1, .625);
            AddEntry(CardSet.CS7910Q, 2, .375);

            // CS8910Q
            AddEntry(CardSet.CS8910Q, 1, .625);
            AddEntry(CardSet.CS8910Q, 2, .375);

            // CS78910Q
            AddEntry(CardSet.CS78910Q, 2, .25);
            AddEntry(CardSet.CS78910Q, 3, .75);

            // CSJQ
            AddEntry(CardSet.CSJQ, 0, 1.0);

            // CS7JQ
            AddEntry(CardSet.CS7JQ, 0, .8125);
            AddEntry(CardSet.CS7JQ, 1, .1875);

            // CS8JQ
            AddEntry(CardSet.CS8JQ, 0, .75);
            AddEntry(CardSet.CS8JQ, 1, .25);

            // CS78JQ
            AddEntry(CardSet.CS78JQ, 0, .125);
            AddEntry(CardSet.CS78JQ, 1, .25);
            AddEntry(CardSet.CS78JQ, 2, .625);

            // CS9JQ
            AddEntry(CardSet.CS9JQ, 0, .5);
            AddEntry(CardSet.CS9JQ, 1, .5);

            // CS79JQ
            AddEntry(CardSet.CS79JQ, 0, .125);
            AddEntry(CardSet.CS79JQ, 1, .25);
            AddEntry(CardSet.CS79JQ, 2, .625);

            // CS89JQ
            AddEntry(CardSet.CS89JQ, 0, .125);
            AddEntry(CardSet.CS89JQ, 1, .25);
            AddEntry(CardSet.CS89JQ, 2, .625);

            // CS789JQ
            AddEntry(CardSet.CS789JQ, 2, .25);
            AddEntry(CardSet.CS789JQ, 3, .75);

            // CS10JQ
            AddEntry(CardSet.CS10JQ, 1, 1.0);

            // CS710JQ
            AddEntry(CardSet.CS710JQ, 1, .125);
            AddEntry(CardSet.CS710JQ, 2, .875);

            // CS810JQ
            AddEntry(CardSet.CS810JQ, 1, .125);
            AddEntry(CardSet.CS810JQ, 2, .875);

            // CS7810JQ
            AddEntry(CardSet.CS7810JQ, 3, 1.0);

            // CS910JQ
            AddEntry(CardSet.CS910JQ, 2, 1.0);

            // CS7910JQ
            AddEntry(CardSet.CS7910JQ, 3, 1.0);

            // CS8910JQ
            AddEntry(CardSet.CS8910JQ, 3, 1.0);

            // CS78910JQ
            AddEntry(CardSet.CS78910JQ, 4, .5);
            AddEntry(CardSet.CS78910JQ, 5, .5);

            // CSK
            AddEntry(CardSet.CSK, 0, 1.0);

            // CS7K
            AddEntry(CardSet.CS7K, 0, .96875);
            AddEntry(CardSet.CS7K, 1, .03125);

            // CS8K
            AddEntry(CardSet.CS8K, 0, .96875);
            AddEntry(CardSet.CS8K, 1, .03125);

            // CS78K
            AddEntry(CardSet.CS78K, 0, .6875);
            AddEntry(CardSet.CS78K, 1, .3125);

            // CS9K
            AddEntry(CardSet.CS9K, 0, .96875);
            AddEntry(CardSet.CS9K, 1, .03125);

            // CS79K
            AddEntry(CardSet.CS79K, 0, .6875);
            AddEntry(CardSet.CS79K, 1, .3125);

            // CS89K
            AddEntry(CardSet.CS89K, 0, .6875);
            AddEntry(CardSet.CS89K, 1, .3125);

            // CS789K
            AddEntry(CardSet.CS789K, 0, .125);
            AddEntry(CardSet.CS789K, 1, .375);
            AddEntry(CardSet.CS789K, 2, .5);

            // CS10K
            AddEntry(CardSet.CS10K, 0, .96875);
            AddEntry(CardSet.CS10K, 1, .03125);

            // CS710K
            AddEntry(CardSet.CS710K, 0, .6875);
            AddEntry(CardSet.CS710K, 1, .3125);

            // CS810K
            AddEntry(CardSet.CS810K, 0, .6875);
            AddEntry(CardSet.CS810K, 1, .3125);

            // CS7810K
            AddEntry(CardSet.CS7810K, 0, .125);
            AddEntry(CardSet.CS7810K, 1, .375);
            AddEntry(CardSet.CS7810K, 2, .5);

            // CS910K
            AddEntry(CardSet.CS910K, 0, .6875);
            AddEntry(CardSet.CS910K, 1, .3125);

            // CS7910K
            AddEntry(CardSet.CS7910K, 0, .125);
            AddEntry(CardSet.CS7910K, 1, .375);
            AddEntry(CardSet.CS7910K, 2, .5);

            // CS8910K
            AddEntry(CardSet.CS8910K, 0, .125);
            AddEntry(CardSet.CS8910K, 1, .375);
            AddEntry(CardSet.CS8910K, 2, .5);

            // CS78910K
            AddEntry(CardSet.CS78910K, 2, .25);
            AddEntry(CardSet.CS78910K, 3, .5);
            AddEntry(CardSet.CS78910K, 4, .25);

            // CSJK
            AddEntry(CardSet.CSJK, 0, .9375);
            AddEntry(CardSet.CSJK, 1, .0625);

            // CS7JK
            AddEntry(CardSet.CS7JK, 0, .6875);
            AddEntry(CardSet.CS7JK, 1, .3125);

            // CS8JK
            AddEntry(CardSet.CS8JK, 0, .6875);
            AddEntry(CardSet.CS8JK, 1, .3125);

            // CS78JK
            AddEntry(CardSet.CS78JK, 0, .125);
            AddEntry(CardSet.CS78JK, 1, .25);
            AddEntry(CardSet.CS78JK, 2, .625);

            // CS9JK
            AddEntry(CardSet.CS9JK, 0, .0625);
            AddEntry(CardSet.CS9JK, 1, .9375);

            // CS79JK
            AddEntry(CardSet.CS79JK, 0, .125);
            AddEntry(CardSet.CS79JK, 1, .25);
            AddEntry(CardSet.CS79JK, 2, .625);

            // CS89JK
            AddEntry(CardSet.CS89JK, 0, .125);
            AddEntry(CardSet.CS89JK, 1, .25);
            AddEntry(CardSet.CS89JK, 2, .625);

            // CS789JK
            AddEntry(CardSet.CS789JK, 2, .25);
            AddEntry(CardSet.CS789JK, 3, .5);
            AddEntry(CardSet.CS789JK, 4, .25);

            // CS10JK
            AddEntry(CardSet.CS10JK, 1, .9375);
            AddEntry(CardSet.CS10JK, 2, .0625);

            // CS710JK
            AddEntry(CardSet.CS710JK, 1, .125);
            AddEntry(CardSet.CS710JK, 2, .75);
            AddEntry(CardSet.CS710JK, 3, .125);

            // CS810JK
            AddEntry(CardSet.CS810JK, 1, .125);
            AddEntry(CardSet.CS810JK, 2, .75);
            AddEntry(CardSet.CS810JK, 3, .125);

            // CS7810JK
            AddEntry(CardSet.CS7810JK, 3, .5);
            AddEntry(CardSet.CS7810JK, 4, .5);

            // CS910JK
            AddEntry(CardSet.CS910JK, 2, .875);
            AddEntry(CardSet.CS910JK, 3, .125);

            // CS7910JK
            AddEntry(CardSet.CS7910JK, 3, .5);
            AddEntry(CardSet.CS7910JK, 4, .5);

            // CS8910JK
            AddEntry(CardSet.CS8910JK, 3, .5);
            AddEntry(CardSet.CS8910JK, 4, .5);

            // CS78910JK
            AddEntry(CardSet.CS78910JK, 4, .5);
            AddEntry(CardSet.CS78910JK, 5, .5);

            // CSQK
            AddEntry(CardSet.CSQK, 1, 1.0);

            // CS7QK
            AddEntry(CardSet.CS7QK, 1, .9375);
            AddEntry(CardSet.CS7QK, 2, .0625);

            // CS8QK
            AddEntry(CardSet.CS8QK, 1, .9375);
            AddEntry(CardSet.CS8QK, 2, .0625);

            // CS78QK
            AddEntry(CardSet.CS78QK, 1, .125);
            AddEntry(CardSet.CS78QK, 2, .375);
            AddEntry(CardSet.CS78QK, 3, .5);

            // CS9QK
            AddEntry(CardSet.CS9QK, 1, .875);
            AddEntry(CardSet.CS9QK, 2, .125);

            // CS79QK
            AddEntry(CardSet.CS79QK, 1, .125);
            AddEntry(CardSet.CS79QK, 2, .375);
            AddEntry(CardSet.CS79QK, 3, .5);

            // CS89QK
            AddEntry(CardSet.CS89QK, 1, .125);
            AddEntry(CardSet.CS89QK, 2, .375);
            AddEntry(CardSet.CS89QK, 3, .5);

            // CS789QK
            AddEntry(CardSet.CS789QK, 3, .25);
            AddEntry(CardSet.CS789QK, 4, .75);

            // CS10QK
            AddEntry(CardSet.CS10QK, 1, .625);
            AddEntry(CardSet.CS10QK, 2, .375);

            // CS710QK
            AddEntry(CardSet.CS710QK, 1, .125);
            AddEntry(CardSet.CS710QK, 2, .375);
            AddEntry(CardSet.CS710QK, 3, .5);

            // CS810QK
            AddEntry(CardSet.CS810QK, 1, .125);
            AddEntry(CardSet.CS810QK, 2, .375);
            AddEntry(CardSet.CS810QK, 3, .5);

            // CS7810QK
            AddEntry(CardSet.CS7810QK, 3, .25);
            AddEntry(CardSet.CS7810QK, 4, .75);

            // CS910QK
            AddEntry(CardSet.CS910QK, 1, .125);
            AddEntry(CardSet.CS910QK, 2, .375);
            AddEntry(CardSet.CS910QK, 3, .5);

            // CS7910QK
            AddEntry(CardSet.CS7910QK, 3, .25);
            AddEntry(CardSet.CS7910QK, 4, .75);

            // CS8910QK
            AddEntry(CardSet.CS8910QK, 3, .25);
            AddEntry(CardSet.CS8910QK, 4, .75);

            // CS78910QK
            AddEntry(CardSet.CS78910QK, 5, 1.0);

            // CSJQK
            AddEntry(CardSet.CSJQK, 2, 1.0);

            // CS7JQK
            AddEntry(CardSet.CS7JQK, 2, .125);
            AddEntry(CardSet.CS7JQK, 3, .875);

            // CS8JQK
            AddEntry(CardSet.CS8JQK, 2, .125);
            AddEntry(CardSet.CS8JQK, 3, .875);

            // CS78JQK
            AddEntry(CardSet.CS78JQK, 4, 1.0);

            // CS9JQK
            AddEntry(CardSet.CS9JQK, 2, .125);
            AddEntry(CardSet.CS9JQK, 3, .875);

            // CS79JQK
            AddEntry(CardSet.CS79JQK, 4, 1.0);

            // CS89JQK
            AddEntry(CardSet.CS89JQK, 4, 1.0);

            // CS789JQK
            AddEntry(CardSet.CS789JQK, 5, 1.0);

            // CS10JQK
            AddEntry(CardSet.CS10JQK, 3, 1.0);

            // CS710JQK
            AddEntry(CardSet.CS710JQK, 4, 1.0);

            // CS810JQK
            AddEntry(CardSet.CS810JQK, 4, 1.0);

            // CS7810JQK
            AddEntry(CardSet.CS7810JQK, 5, 1.0);

            // CS910JQK
            AddEntry(CardSet.CS910JQK, 4, 1.0);

            // CS7910JQK
            AddEntry(CardSet.CS7910JQK, 5, 1.0);

            // CS8910JQK
            AddEntry(CardSet.CS8910JQK, 5, 1.0);

            // CS78910JQK
            AddEntry(CardSet.CS78910JQK, 6, 1.0);

            // CSA
            AddEntry(CardSet.CSA, 1, 1.0);

            // CS7A
            AddEntry(CardSet.CS7A, 1, 1.0);

            // CS8A
            AddEntry(CardSet.CS8A, 1, 1.0);

            // CS78A
            AddEntry(CardSet.CS78A, 1, 1.0);

            // CS9A
            AddEntry(CardSet.CS9A, 1, 1.0);

            // CS79A
            AddEntry(CardSet.CS79A, 1, 1.0);

            // CS89A
            AddEntry(CardSet.CS89A, 1, 1.0);

            // CS789A
            AddEntry(CardSet.CS789A, 1, .125);
            AddEntry(CardSet.CS789A, 2, .5);
            AddEntry(CardSet.CS789A, 3, .375);

            // CS10A
            AddEntry(CardSet.CS10A, 1, 1.0);

            // CS710A
            AddEntry(CardSet.CS710A, 1, 1.0);

            // CS810A
            AddEntry(CardSet.CS810A, 1, 1.0);

            // CS7810A
            AddEntry(CardSet.CS7810A, 1, .125);
            AddEntry(CardSet.CS7810A, 2, .5);
            AddEntry(CardSet.CS7810A, 3, .375);

            // CS910A
            AddEntry(CardSet.CS910A, 1, 1.0);

            // CS7910A
            AddEntry(CardSet.CS7910A, 1, .125);
            AddEntry(CardSet.CS7910A, 2, .5);
            AddEntry(CardSet.CS7910A, 3, .375);

            // CS8910A
            AddEntry(CardSet.CS8910A, 1, .125);
            AddEntry(CardSet.CS8910A, 2, .5);
            AddEntry(CardSet.CS8910A, 3, .375);

            // CS78910A
            AddEntry(CardSet.CS78910A, 3, .25);
            AddEntry(CardSet.CS78910A, 4, .75);

            // CSJA
            AddEntry(CardSet.CSJA, 1, 1.0);

            // CS7JA
            AddEntry(CardSet.CS7JA, 1, 1.0);

            // CS8JA
            AddEntry(CardSet.CS8JA, 1, 1.0);

            // CS78JA
            AddEntry(CardSet.CS78JA, 1, .125);
            AddEntry(CardSet.CS78JA, 2, .5);
            AddEntry(CardSet.CS78JA, 3, .375);

            // CS9JA
            AddEntry(CardSet.CS9JA, 1, 1.0);

            // CS79JA
            AddEntry(CardSet.CS79JA, 1, .125);
            AddEntry(CardSet.CS79JA, 2, .5);
            AddEntry(CardSet.CS79JA, 3, .375);

            // CS89JA
            AddEntry(CardSet.CS89JA, 1, .125);
            AddEntry(CardSet.CS89JA, 2, .5);
            AddEntry(CardSet.CS89JA, 3, .375);

            // CS789JA
            AddEntry(CardSet.CS789JA, 3, .25);
            AddEntry(CardSet.CS789JA, 4, .75);

            // CS10JA
            AddEntry(CardSet.CS10JA, 1, .8125);
            AddEntry(CardSet.CS10JA, 2, .1875);

            // CS710JA
            AddEntry(CardSet.CS710JA, 1, .125);
            AddEntry(CardSet.CS710JA, 2, .25);
            AddEntry(CardSet.CS710JA, 3, .625);

            // CS810JA
            AddEntry(CardSet.CS810JA, 1, .125);
            AddEntry(CardSet.CS810JA, 2, .25);
            AddEntry(CardSet.CS810JA, 3, .625);

            // CS7810JA
            AddEntry(CardSet.CS7810JA, 3, .25);
            AddEntry(CardSet.CS7810JA, 4, .75);

            // CS910JA
            AddEntry(CardSet.CS910JA, 1, .125);
            AddEntry(CardSet.CS910JA, 2, .25);
            AddEntry(CardSet.CS910JA, 3, .625);

            // CS7910JA
            AddEntry(CardSet.CS7910JA, 3, .25);
            AddEntry(CardSet.CS7910JA, 4, .75);

            // CS8910JA
            AddEntry(CardSet.CS8910JA, 3, .25);
            AddEntry(CardSet.CS8910JA, 4, .75);

            // CS78910JA
            AddEntry(CardSet.CS78910JA, 5, .5);
            AddEntry(CardSet.CS78910JA, 6, .5);

            // CSQA
            AddEntry(CardSet.CSQA, 1, .96875);
            AddEntry(CardSet.CSQA, 2, .03125);

            // CS7QA
            AddEntry(CardSet.CS7QA, 1, .6875);
            AddEntry(CardSet.CS7QA, 2, .3125);

            // CS8QA
            AddEntry(CardSet.CS8QA, 1, .6875);
            AddEntry(CardSet.CS8QA, 2, .3125);

            // CS78QA
            AddEntry(CardSet.CS78QA, 1, .125);
            AddEntry(CardSet.CS78QA, 2, .5);
            AddEntry(CardSet.CS78QA, 3, .375);

            // CS9QA
            AddEntry(CardSet.CS9QA, 1, .6875);
            AddEntry(CardSet.CS9QA, 2, .3125);

            // CS79QA
            AddEntry(CardSet.CS79QA, 1, .125);
            AddEntry(CardSet.CS79QA, 2, .5);
            AddEntry(CardSet.CS79QA, 3, .375);

            // CS89QA
            AddEntry(CardSet.CS89QA, 1, .125);
            AddEntry(CardSet.CS89QA, 2, .5);
            AddEntry(CardSet.CS89QA, 3, .375);

            // CS789QA
            AddEntry(CardSet.CS789QA, 3, .25);
            AddEntry(CardSet.CS789QA, 4, .5);
            AddEntry(CardSet.CS789QA, 5, .25);

            // CS10QA
            AddEntry(CardSet.CS10QA, 1, .375);
            AddEntry(CardSet.CS10QA, 2, .625);

            // CS710QA
            AddEntry(CardSet.CS710QA, 1, .125);
            AddEntry(CardSet.CS710QA, 2, .25);
            AddEntry(CardSet.CS710QA, 3, .625);

            // CS810QA
            AddEntry(CardSet.CS810QA, 1, .125);
            AddEntry(CardSet.CS810QA, 2, .25);
            AddEntry(CardSet.CS810QA, 3, .625);

            // CS7810QA
            AddEntry(CardSet.CS7810QA, 3, .25);
            AddEntry(CardSet.CS7810QA, 4, .5);
            AddEntry(CardSet.CS7810QA, 5, .25);

            // CS910QA
            AddEntry(CardSet.CS910QA, 1, .125);
            AddEntry(CardSet.CS910QA, 2, .25);
            AddEntry(CardSet.CS910QA, 3, .625);

            // CS7910QA
            AddEntry(CardSet.CS7910QA, 3, .25);
            AddEntry(CardSet.CS7910QA, 4, .5);
            AddEntry(CardSet.CS7910QA, 5, .25);

            // CS8910QA
            AddEntry(CardSet.CS8910QA, 3, .25);
            AddEntry(CardSet.CS8910QA, 4, .5);
            AddEntry(CardSet.CS8910QA, 5, .25);

            // CS78910QA
            AddEntry(CardSet.CS78910QA, 5, .5);
            AddEntry(CardSet.CS78910QA, 6, .5);

            // CSJQA
            AddEntry(CardSet.CSJQA, 2, .9375);
            AddEntry(CardSet.CSJQA, 3, .0625);

            // CS7JQA
            AddEntry(CardSet.CS7JQA, 3, .875);
            AddEntry(CardSet.CS7JQA, 4, .125);

            // CS8JQA
            AddEntry(CardSet.CS8JQA, 3, .875);
            AddEntry(CardSet.CS8JQA, 4, .125);

            // CS78JQA
            AddEntry(CardSet.CS78JQA, 4, .75);
            AddEntry(CardSet.CS78JQA, 5, .25);

            // CS9JQA
            AddEntry(CardSet.CS9JQA, 3, .875);
            AddEntry(CardSet.CS9JQA, 4, .125);

            // CS79JQA
            AddEntry(CardSet.CS79JQA, 4, .75);
            AddEntry(CardSet.CS79JQA, 5, .25);

            // CS89JQA
            AddEntry(CardSet.CS89JQA, 4, .75);
            AddEntry(CardSet.CS89JQA, 5, .25);

            // CS789JQA
            AddEntry(CardSet.CS789JQA, 5, .5);
            AddEntry(CardSet.CS789JQA, 6, .5);

            // CS10JQA
            AddEntry(CardSet.CS10JQA, 3, .875);
            AddEntry(CardSet.CS10JQA, 4, .125);

            // CS710JQA
            AddEntry(CardSet.CS710JQA, 4, .75);
            AddEntry(CardSet.CS710JQA, 5, .25);

            // CS810JQA
            AddEntry(CardSet.CS810JQA, 4, .75);
            AddEntry(CardSet.CS810JQA, 5, .25);

            // CS7810JQA
            AddEntry(CardSet.CS7810JQA, 5, .5);
            AddEntry(CardSet.CS7810JQA, 6, .5);

            // CS910JQA
            AddEntry(CardSet.CS910JQA, 4, .75);
            AddEntry(CardSet.CS910JQA, 5, .25);

            // CS7910JQA
            AddEntry(CardSet.CS7910JQA, 5, .5);
            AddEntry(CardSet.CS7910JQA, 6, .5);

            // CS8910JQA
            AddEntry(CardSet.CS8910JQA, 5, .5);
            AddEntry(CardSet.CS8910JQA, 6, .5);

            // CS78910JQA
            AddEntry(CardSet.CS78910JQA, 7, 1.0);

            // CSKA
            AddEntry(CardSet.CSKA, 2, 1.0);

            // CS7KA
            AddEntry(CardSet.CS7KA, 2, 1.0);

            // CS8KA
            AddEntry(CardSet.CS8KA, 2, 1.0);

            // CS78KA
            AddEntry(CardSet.CS78KA, 2, .125);
            AddEntry(CardSet.CS78KA, 3, .5);
            AddEntry(CardSet.CS78KA, 4, .375);

            // CS9KA
            AddEntry(CardSet.CS9KA, 2, 1.0);

            // CS79KA
            AddEntry(CardSet.CS79KA, 2, .125);
            AddEntry(CardSet.CS79KA, 3, .5);
            AddEntry(CardSet.CS79KA, 4, .375);

            // CS89KA
            AddEntry(CardSet.CS89KA, 2, .125);
            AddEntry(CardSet.CS89KA, 3, .5);
            AddEntry(CardSet.CS89KA, 4, .375);

            // CS789KA
            AddEntry(CardSet.CS789KA, 4, .25);
            AddEntry(CardSet.CS789KA, 5, .75);

            // CS10KA
            AddEntry(CardSet.CS10KA, 2, .9375);
            AddEntry(CardSet.CS10KA, 3, .0625);

            // CS710KA
            AddEntry(CardSet.CS710KA, 2, .125);
            AddEntry(CardSet.CS710KA, 3, .5);
            AddEntry(CardSet.CS710KA, 4, .375);

            // CS810KA
            AddEntry(CardSet.CS810KA, 2, .125);
            AddEntry(CardSet.CS810KA, 3, .5);
            AddEntry(CardSet.CS810KA, 4, .375);

            // CS7810KA
            AddEntry(CardSet.CS7810KA, 4, .25);
            AddEntry(CardSet.CS7810KA, 5, .75);

            // CS910KA
            AddEntry(CardSet.CS910KA, 2, .125);
            AddEntry(CardSet.CS910KA, 3, .5);
            AddEntry(CardSet.CS910KA, 4, .375);

            // CS7910KA
            AddEntry(CardSet.CS7910KA, 4, .25);
            AddEntry(CardSet.CS7910KA, 5, .75);

            // CS8910KA
            AddEntry(CardSet.CS8910KA, 4, .25);
            AddEntry(CardSet.CS8910KA, 5, .75);

            // CS78910KA
            AddEntry(CardSet.CS78910KA, 6, 1.0);

            // CSJKA
            AddEntry(CardSet.CSJKA, 2, .6875);
            AddEntry(CardSet.CSJKA, 3, .3125);

            // CS7JKA
            AddEntry(CardSet.CS7JKA, 2, .125);
            AddEntry(CardSet.CS7JKA, 3, .5);
            AddEntry(CardSet.CS7JKA, 4, .375);

            // CS8JKA
            AddEntry(CardSet.CS8JKA, 2, .125);
            AddEntry(CardSet.CS8JKA, 3, .5);
            AddEntry(CardSet.CS8JKA, 4, .375);

            // CS78JKA
            AddEntry(CardSet.CS78JKA, 4, .25);
            AddEntry(CardSet.CS78JKA, 5, .75);

            // CS9JKA
            AddEntry(CardSet.CS9JKA, 2, .125);
            AddEntry(CardSet.CS9JKA, 3, .5);
            AddEntry(CardSet.CS9JKA, 4, .375);

            // CS79JKA
            AddEntry(CardSet.CS79JKA, 4, .25);
            AddEntry(CardSet.CS79JKA, 5, .75);

            // CS89JKA
            AddEntry(CardSet.CS89JKA, 4, .25);
            AddEntry(CardSet.CS89JKA, 5, .75);

            // CS789JKA
            AddEntry(CardSet.CS789JKA, 6, 1.0);

            // CS10JKA
            AddEntry(CardSet.CS10JKA, 3, .5);
            AddEntry(CardSet.CS10JKA, 4, .5);

            // CS710JKA
            AddEntry(CardSet.CS710JKA, 4, .25);
            AddEntry(CardSet.CS710JKA, 5, .75);

            // CS810JKA
            AddEntry(CardSet.CS810JKA, 4, .25);
            AddEntry(CardSet.CS810JKA, 5, .75);

            // CS7810JKA
            AddEntry(CardSet.CS7810JKA, 6, 1.0);

            // CS910JKA
            AddEntry(CardSet.CS910JKA, 4, .25);
            AddEntry(CardSet.CS910JKA, 5, .75);

            // CS7910JKA
            AddEntry(CardSet.CS7910JKA, 6, 1.0);

            // CS8910JKA
            AddEntry(CardSet.CS8910JKA, 6, 1.0);

            // CS78910JKA
            AddEntry(CardSet.CS78910JKA, 7, 1.0);

            // CSQKA
            AddEntry(CardSet.CSQKA, 3, 1.0);

            // CS7QKA
            AddEntry(CardSet.CS7QKA, 3, .125);
            AddEntry(CardSet.CS7QKA, 4, .875);

            // CS8QKA
            AddEntry(CardSet.CS8QKA, 3, .125);
            AddEntry(CardSet.CS8QKA, 4, .875);

            // CS78QKA
            AddEntry(CardSet.CS78QKA, 5, 1.0);

            // CS9QKA
            AddEntry(CardSet.CS9QKA, 3, .125);
            AddEntry(CardSet.CS9QKA, 4, .875);

            // CS79QKA
            AddEntry(CardSet.CS79QKA, 5, 1.0);

            // CS89QKA
            AddEntry(CardSet.CS89QKA, 5, 1.0);

            // CS789QKA
            AddEntry(CardSet.CS789QKA, 6, 1.0);

            // CS10QKA
            AddEntry(CardSet.CS10QKA, 3, .125);
            AddEntry(CardSet.CS10QKA, 4, .875);

            // CS710QKA
            AddEntry(CardSet.CS710QKA, 5, 1.0);

            // CS810QKA
            AddEntry(CardSet.CS810QKA, 5, 1.0);

            // CS7810QKA
            AddEntry(CardSet.CS7810QKA, 6, 1.0);

            // CS910QKA
            AddEntry(CardSet.CS910QKA, 5, 1.0);

            // CS7910QKA
            AddEntry(CardSet.CS7910QKA, 6, 1.0);

            // CS8910QKA
            AddEntry(CardSet.CS8910QKA, 6, 1.0);

            // CS78910QKA
            AddEntry(CardSet.CS78910QKA, 7, 1.0);

            // CSJQKA
            AddEntry(CardSet.CSJQKA, 4, 1.0);

            // CS7JQKA
            AddEntry(CardSet.CS7JQKA, 5, 1.0);

            // CS8JQKA
            AddEntry(CardSet.CS8JQKA, 5, 1.0);

            // CS78JQKA
            AddEntry(CardSet.CS78JQKA, 6, 1.0);

            // CS9JQKA
            AddEntry(CardSet.CS9JQKA, 5, 1.0);

            // CS79JQKA
            AddEntry(CardSet.CS79JQKA, 6, 1.0);

            // CS89JQKA
            AddEntry(CardSet.CS89JQKA, 6, 1.0);

            // CS789JQKA
            AddEntry(CardSet.CS789JQKA, 7, 1.0);

            // CS10JQKA
            AddEntry(CardSet.CS10JQKA, 5, 1.0);

            // CS710JQKA
            AddEntry(CardSet.CS710JQKA, 6, 1.0);

            // CS810JQKA
            AddEntry(CardSet.CS810JQKA, 6, 1.0);

            // CS7810JQKA
            AddEntry(CardSet.CS7810JQKA, 7, 1.0);

            // CS910JQKA
            AddEntry(CardSet.CS910JQKA, 6, 1.0);

            // CS7910JQKA
            AddEntry(CardSet.CS7910JQKA, 7, 1.0);

            // CS8910JQKA
            AddEntry(CardSet.CS8910JQKA, 7, 1.0);

            // CS78910JQKA
            AddEntry(CardSet.CS78910JQKA, 8, 1.0);
        }

        static TrickPlayProbabilities()
        {
            SetUp();
        }

        private static readonly List<TrickProbability>[] gTrickProbabilities = new List<TrickProbability>[CardSet.MaxValue + 1]; 
    }
}
