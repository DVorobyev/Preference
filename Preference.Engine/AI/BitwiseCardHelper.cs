// 20/01/2012 by Dmitry Vorobyev.

using System;
using System.Linq;

namespace Preference.Engine.AI
{
    internal static class BitwiseCardHelper
    {
        internal static int CardCollectionToBits(CardCollection source)
        {
            return source.Aggregate(0, (current, card) => Add(card, current));
        }

        internal static CardCollection BitsToCardCollection(int source)
        {
            var result = new CardCollection();

            for (int i = 0; i < 32; i++)
            {
                int card = (1 << i);
                if ((source & card) != 0)
                    result.Add(BitsToCard(card));
            }

            return result;
        }

        private static int Add(Card card, int bits)
        {
            return Add(bits, CardToBits(card));            
        }

        internal static int Add(int bits, int mask)
        {
            return (bits | mask);
        }

        internal static int Remove(int bits, int mask)
        {
            return (bits & ~mask);
        }

        internal static int CardToBits(Card card)
        {
            return CardToBits(card.Suit, card.Rank);
        }

        private static int CardToBits(CardSuit suit, CardRank rank)
        {
            return 1 << GetOffset(suit, rank);
        }

        private static int GetOffset(CardSuit suit, CardRank rank)
        {
            return (((int)suit << 3) + (int)rank);
        }

        internal static Card BitsToCard(int bits)
        {
            return new Card(GetSuit(bits), GetRank(bits));
        }

//        internal static bool HasSuit(int cards, int suit)
//        {
//            return ((cards & suit) != 0);
//        }
 
        internal static int GetCardsOfSuit(int cards, int suit)
        {
            return (cards & suit);
        }

//
//        internal static int GetCount(int bits)
//        {
//            int count = 0;
//            int mask = 1;
//
//            for (int i = 0; i < 32; i++)
//            {
//                if ((bits & mask) != 0)
//                    count++;
//
//                mask <<= 1;
//            }
//
//            return count;
//        }        
        
        internal static int GetCardCount(int bits)
        {
            int count = 0;
            int mask = 1;

            for (int i = 0; i < 32; i++)
            {
                if ((bits & mask) != 0)
                    count++;

                mask <<= 1;
            }

            return count;
        }

        internal static int GetSuitCount(int cards, CardSuit suit)
        {
            int cardsOfSuit = GetCardsOfSuit(cards, SuitToBits(suit));
            return GetCardCount(cardsOfSuit);
        }          
        
        internal static int[] GetSuitCounts(int cards)
        {
            int[] counts = new int[4];
            for (int i = 0; i < 4; i++)
                counts[i] = GetSuitCount(cards, (CardSuit)i);

            return counts;
        }  
        
        internal static int GetGroupCount(int bits)
        {
            bool isInGroup = false;
            int count = 0;
            int mask = 1;

            for (int i = 0; i < 32; i++)
            {
                if ((bits & mask) != 0)
                {
                    if (!isInGroup)
                    {
                        isInGroup = true;
                        count++;
                    }
                }
                else
                {
                    if (isInGroup)
                        isInGroup = false;
                }

                mask <<= 1;
            }

            return count;
        }

        internal static bool IsCardInGroup(int group, int card)
        {
            return ((group & card) != 0);
        }

        internal static CardSuit GetSuit(int card)
        {
            if ((card & 0x000000FF) != 0)
                return CardSuit.Spades;

            if ((card & 0x0000FF00) != 0)
                return CardSuit.Clubs;

            if ((card & 0x00FF0000) != 0)
                return CardSuit.Diamonds;

            return CardSuit.Hearts;
        }        
        
        internal static int GetSuitAsBits(int card)
        {
            if ((card & 0x000000FF) != 0)
                return 0x000000FF;

            if ((card & 0x0000FF00) != 0)
                return 0x0000FF00;

            if ((card & 0x00FF0000) != 0)
                return 0x00FF0000;

            return unchecked((int)0xFF000000);
        }

        internal static int SuitToBits(CardSuit? suit)
        {
            if (!suit.HasValue)
                return 0;

            switch (suit.Value)
            {
                case CardSuit.Spades:
                    return 0x000000FF;
                case CardSuit.Clubs:
                    return 0x0000FF00;
                case CardSuit.Diamonds:
                    return 0x00FF0000;
                case CardSuit.Hearts:
                    return unchecked((int)0xFF000000);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal static CardRank GetRank(int card)
        {
            if (IsCardInGroup(1 + (1 << 8) + (1 << 16) + (1 << 24), card))
                return CardRank.Seven;            
            
            if (IsCardInGroup(2 + (2 << 8) + (2 << 16) + (2 << 24), card))
                return CardRank.Eight;            
            
            if (IsCardInGroup(4 + (4 << 8) + (4 << 16) + (4 << 24), card))
                return CardRank.Nine;

            if (IsCardInGroup(8 + (8 << 8) + (8 << 16) + (8 << 24), card))
                return CardRank.Ten;

            if (IsCardInGroup(16 + (16 << 8) + (16 << 16) + (16 << 24), card))
                return CardRank.Jack;

            if (IsCardInGroup(32 + (32 << 8) + (32 << 16) + (32 << 24), card))
                return CardRank.Queen;

            if (IsCardInGroup(64 + (64 << 8) + (64 << 16) + (64 << 24), card))
                return CardRank.King;

            if (IsCardInGroup(128 + (128 << 8) + (128 << 16) + (128 << 24), card))
                return CardRank.Ace;

            throw new InvalidOperationException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="suit"></param>
        /// <returns></returns>
        internal static int GetCardSet(int cards, CardSuit suit)
        {
            switch (suit)
            {
                case CardSuit.Spades:
                    return ((cards >> 0) & 0x000000FF);
                case CardSuit.Clubs:
                    return ((cards >> 8) & 0x000000FF);
                case CardSuit.Diamonds:
                    return ((cards >> 16) & 0x000000FF);
                case CardSuit.Hearts:
                    return ((cards >> 24) & 0x000000FF);
                default:
                    throw new ArgumentOutOfRangeException("suit");
            }
        }
    }
}
