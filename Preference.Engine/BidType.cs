// 17/01/2012 by Dmitry Vorobyev.

namespace Preference.Engine
{
    /// <summary>
    /// Specifies a type of a bid declared by a player.
    /// </summary>
    public enum BidType
    {
        /// <summary>
        /// A regular bid consisting of the number of tricks to be won.
        /// </summary>
        Tricks,
        /// <summary>
        /// A misere bid.
        /// </summary>
        Misere,
        /// <summary>
        /// A pass bid. 
        /// </summary>
        Pass
    }
}
