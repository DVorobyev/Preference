
namespace Preference.Engine.AI.Playing
{
    /// <summary>
    /// A bundle containing a played card and a group the card belongs to.
    /// </summary>
    internal struct Move
    {
        internal Move(int card) : this()
        {
            Card = card;
            Group = card;
        }

        internal Move(int card, int group) : this()
        {
            Card = card;
            Group = group;
        }

        internal int Card { get; private set; }
        internal int Group { get; private set; }

        /// <summary>
        /// A null move.
        /// </summary>
        internal static readonly Move Empty = new Move();
    }
}
