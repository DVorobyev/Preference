namespace Preference.Engine
{
    /// <summary>
    /// Specifies how strong the AI player is.
    /// </summary>
    public enum GameDifficultyLevel
    {
        Beginner,
        Amateur,
        Moderate,
        Professional,
        Expert,
        /// <summary>
        /// Cheater is essentially the same as Expert, but the AI player knows all invisible cards.
        /// </summary>
        Cheater
    }
}
