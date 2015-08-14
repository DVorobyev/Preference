// 17/01/2012 by Dmitry Vorobyev.

using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Preference.Engine
{
    /// <summary>
    /// Represents a collection of cards.
    /// </summary>
    public class CardCollection : ObservableCollection<Card>
    {
        public bool HasSuit(CardSuit suit)
        {
            return this.Any(card => card.Suit == suit);
        }

        internal Card RandomPick()
        {
            int cardIndex = RandomHelper.Next(Count);
            
            Card card = this[cardIndex];
            RemoveAt(cardIndex);
            
            return card;
        }

        internal void MoveTo(CardCollection destination)
        {
            foreach (var card in this)
                destination.Add(card);

            Clear();
        }

        public override string ToString()
        {
            if (Count == 0)
                return string.Empty;

            var sortedCards = this.OrderBy(card => card.Suit).ThenBy(card => card.Rank);
            var firstCard = sortedCards.First();
            
            var builder = new StringBuilder();
            Card? previousCard = null;
            
            foreach (var card in sortedCards)
            {
                if (!card.Equals(firstCard))
                {
                    string space = (previousCard.HasValue) && (previousCard.Value.Suit != card.Suit) ? "   " : " ";
                    builder.Append(space);
                }

                builder.Append(card);
                previousCard = card;
            }

            return builder.ToString();
        }
    }
}
