// 28/01/2012 by Dmitry Vorobyev.

namespace Preference.Engine.AI.Playing
{
    internal class HandState
    {
        internal HandState(Hand source)
        {
            Cards = BitwiseCardHelper.CardCollectionToBits(source.Cards);

            if (source.PlayedCard.HasValue)
                Move = new Move(BitwiseCardHelper.CardToBits(source.PlayedCard.Value));

            Tricks = source.Tricks;
        }

        internal HandState Clone()
        {
            return (HandState)MemberwiseClone();
        }

        internal int Cards { get; set; }
        internal Move Move { get; set; }
        internal int Tricks { get; set; }
    }
}
