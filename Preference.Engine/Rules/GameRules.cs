// Copyright © 2013 Dmitry Vorobyev.

using System;

namespace Preference.Engine.Rules
{
    /// <summary>
    /// Represents a set of game rules that determine particular game variation, in particular, how the player score
    /// is calculated.
    /// </summary>
    public abstract class GameRules
    {
        protected GameRules()
        {
            ResetAllPassGame();
        }

        internal void CalculateScore(Game game)
        {
            switch (game.GameType.Value)
            {
                case GameType.TrickPlay:
                    CalculateTrickPlayScore(game);
                    break;
                case GameType.Misere:
                    CalculateMisereScore(game);
                    break;
                case GameType.AllPass:
                    CalculateAllPassScore(game);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CalculateTrickPlayScore(Game game)
        {
            Hand declarer = game.Declarer;
            
            int declaredTricks = game.BiddingContext.Contract.Value.Tricks;
            int declarerDelta = declarer.Tricks - declaredTricks;
            int gameValue = GetTrickGameValue(declaredTricks);
            int trickValue = GetTrickValue(declaredTricks);

            if (declarerDelta >= 0)
                declarer.Player.Score.IncreasePool(gameValue);
            else
                declarer.Player.Score.IncreaseDump(-declarerDelta * trickValue);

            int consolation = (declarerDelta < 0) ? -declarerDelta * trickValue : 0;
            int whisterLostTrickValue = (WhistingResponsibility == WhistingResponsibility.Responsible)
                ? trickValue
                : trickValue / 2;

            bool bothDefendersPlayed = (game.FirstDefender.DefenderAction == DefenderAction.Whist) &&
                                       (game.SecondDefender.DefenderAction == DefenderAction.Whist);

            if (bothDefendersPlayed)
            {
                game.FirstDefender.Player.Score.IncreaseWhistPoints(
                    declarer.Index,
                    game.FirstDefender.Tricks * trickValue + consolation);

                game.SecondDefender.Player.Score.IncreaseWhistPoints(
                    declarer.Index,
                    game.SecondDefender.Tricks * trickValue + consolation);
            }
            else
            {
                int defenderTricksTotal = game.FirstDefender.Tricks + game.SecondDefender.Tricks;

                Hand whistedDefender = (game.FirstDefender.DefenderAction == DefenderAction.Whist)
                    ? game.FirstDefender
                    : game.SecondDefender;

                Hand passedDefender = (game.FirstDefender.DefenderAction == DefenderAction.Pass)
                    ? game.FirstDefender
                    : game.SecondDefender;

                switch (WhistingType)
                {
                    case WhistingType.Gentleman:
                        int whistPoints = defenderTricksTotal * gameValue / 2 + consolation;
                        whistedDefender.Player.Score.IncreaseWhistPoints(declarer.Index, whistPoints);
                        passedDefender.Player.Score.IncreaseWhistPoints(declarer.Index, whistPoints);
                        break;
                    case WhistingType.Greedy:
                        whistedDefender.Player.Score.IncreaseWhistPoints(
                            declarer.Index, 
                            defenderTricksTotal * gameValue + consolation);
                        passedDefender.Player.Score.IncreaseWhistPoints(declarer.Index, consolation);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                int defendersObligation = GetDefendersObligation(declaredTricks);
                int defendersDelta = defenderTricksTotal - defendersObligation;

                if (defendersDelta < 0)
                    whistedDefender.Player.Score.IncreaseDump(-defendersDelta * whisterLostTrickValue);
            }
        }

        private int GetDefendersObligation(int declaredTricks)
        {
            switch (declaredTricks)
            {
                case 6:
                    return 4;
                case 7:
                    return 2;
                case 8:
                case 9:
                    return 1;
                case 10:
                    return (IsWhistingOnTenTricksGame) ? 1 : 0; 
                default:
                    throw new ArgumentOutOfRangeException("declaredTricks");
            }    
        }

        private void CalculateMisereScore(Game game)
        {
            Hand declarer = game.Declarer;

            if (declarer.Tricks == 0)
                declarer.Player.Score.IncreasePool(BaseValueOfMisereGame);
            else
                declarer.Player.Score.IncreaseDump(BaseValueOfMisereGame * declarer.Tricks);
        }

        private void CalculateAllPassScore(Game game)
        {
            int value = mAllPassGameBaseValue;

            if (IsAllPassGameValueDoubled)
                value *= 2;

            for (int i = 0; i < 3; i++)
            {
                Hand hand = game.GetHand(i);
                if (hand.Tricks == 0)
                    hand.Player.Score.IncreasePool(value);
                else
                    hand.Player.Score.IncreaseDump(value * hand.Tricks);
            }
        }

        internal void CalculateFinalScore(Game game)
        {
            throw new System.NotImplementedException();
        }

        internal double GetGameValue(GameType gameType, int numberOfPlayers, int tricksDeclared, int tricksTaken)
        {
            throw new System.NotImplementedException();
        }

        internal void RegisterAllPassGame()
        {
            mAllPassGameRound++;

            switch (AllPassGameValueProgression)
            {
                case AllPassGameValueProgression.None:
                    // Do nothing.
                    break;
                case AllPassGameValueProgression.ArithmeticLimited:
                    if (mAllPassGameRound < 3)
                        mAllPassGameBaseValue++;
                    break;
                case AllPassGameValueProgression.ArithmeticUnlimited:
                    mAllPassGameBaseValue++;
                    break;
                case AllPassGameValueProgression.GeometricLimited:
                    if (mAllPassGameRound < 3)
                        mAllPassGameBaseValue *= 2;
                    break;
                case AllPassGameValueProgression.GeometricUnlimited:
                    mAllPassGameBaseValue *= 2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (AllPassGameExitDifficulty)
            {
                case AllPassGameExitDifficulty.Easy:
                    // Do nothing.
                    break;
                case AllPassGameExitDifficulty.Medium:
                    if (mAllPassExitTricks < 7)
                        mAllPassExitTricks++;
                    break;
                case AllPassGameExitDifficulty.Difficult:
                    if (mAllPassExitTricks < 8)
                        mAllPassExitTricks++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal void ResetAllPassGame()
        {
            mAllPassGameRound = 0;
            mAllPassGameBaseValue = BaseValueOfAllPassGame;
            mAllPassExitTricks = 6;
        }

        private static int GetTrickGameValue(int tricksDeclared)
        {
            switch (tricksDeclared)
            {
                case 6:
                    return BaseValueOf6TricksGame;
                case 7:
                    return BaseValueOf7TricksGame;
                case 8:
                    return BaseValueOf8TricksGame;
                case 9:
                    return BaseValueOf9TricksGame;
                case 10:
                    return BaseValueOf10TricksGame;
                default:
                    throw new ArgumentOutOfRangeException("tricksDeclared");
            }
        }

        protected abstract int GetTrickValue(int tricksDeclared);

        protected abstract WhistingType WhistingType { get; }

        protected abstract WhistingResponsibility WhistingResponsibility { get; }

        protected abstract bool IsWhistingOnTenTricksGame { get; }

        /// <summary>
        /// Gets a value indicating whether and how all pass game's value increases over new rounds of the game.
        /// </summary>
        protected abstract AllPassGameValueProgression AllPassGameValueProgression { get; }

        protected abstract AllPassGameExitDifficulty AllPassGameExitDifficulty { get; }

        /// <summary>
        /// Gets whether the base value of the all pass game is 2 instead of 1.
        /// </summary>
        protected abstract bool IsAllPassGameValueDoubled { get; }
        
        /// <summary>
        /// Gets whether the talon is opened at the beginning of the all pass game.
        /// </summary>
        protected abstract bool IsAllPassGameTalonOpened { get; }

        private int mAllPassGameRound;
        private int mAllPassGameBaseValue;
        private int mAllPassExitTricks;

        private const int BaseValueOf6TricksGame = 2;
        private const int BaseValueOf7TricksGame = 4;
        private const int BaseValueOf8TricksGame = 6;
        private const int BaseValueOf9TricksGame = 8;
        private const int BaseValueOf10TricksGame = 10;
        private const int BaseValueOfMisereGame = 10;
        private const int BaseValueOfAllPassGame = 1;
    }
}
