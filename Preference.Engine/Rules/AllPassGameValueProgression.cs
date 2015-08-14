// Copyright © 2013 Dmitry Vorobyev.

namespace Preference.Engine.Rules
{
    /// <summary>
    /// Specifies whether and how all pass game's value increases over new rounds of the game.
    /// </summary>
    public enum AllPassGameValueProgression
    {
        /// <summary>
        /// The value of the all pass game is constant (x1, x1, x1, x1, ...).
        /// </summary>
        None,
        /// <summary>
        /// The value of the all pass game increases using arithmetic progression and stops increasing after the 3rd round
        /// (x1, x2, x3, x3, ...).
        /// </summary>
        ArithmeticLimited,
        /// <summary>
        /// The value of the all pass game increases using arithmetic progression and never stops increasing 
        /// (x1, x2, x3, x4, ...).
        /// </summary>
        ArithmeticUnlimited,
        /// <summary>
        /// The value of the all pass game increases using geometric progression and stops increasing after the 3rd round
        /// (x1, x2, x4, x4, ...).
        /// </summary>
        GeometricLimited,
        /// <summary>
        /// The value of the all pass game increases using geometric progression and never stops increasing
        /// (x1, x2, x4, x8, ...).
        /// </summary>
        GeometricUnlimited
    }
}
