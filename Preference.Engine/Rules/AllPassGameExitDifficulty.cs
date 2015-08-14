namespace Preference.Engine.Rules
{
    public enum AllPassGameExitDifficulty
    {
        /// <summary>
        /// The minimum bid required to exit the all pass game is constant (6, 6, 6, 6, ...).
        /// </summary>
        Easy,
        /// <summary>
        /// The minimum bid required to exit the all pass game increases to 7 tricks (6, 7, 7, 7, ...).
        /// </summary>
        Medium,
        /// <summary>
        /// The minimum bid required to exit the all pass game increases to 8 tricks (6, 7, 8, 8, ...).
        /// </summary>
        Difficult
    }
}
