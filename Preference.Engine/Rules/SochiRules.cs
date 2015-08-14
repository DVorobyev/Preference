// Copyright © 2013 Dmitry Vorobyev.

namespace Preference.Engine.Rules
{
    public class SochiRules : GameRules
    {
        protected override WhistingResponsibility WhistingResponsibility
        {
            get { return WhistingResponsibility.Responsible; }
        }

        protected override AllPassGameValueProgression AllPassGameValueProgression
        {
            get { return AllPassGameValueProgression.ArithmeticLimited; }
        }

        protected override bool IsAllPassGameValueDoubled
        {
            get { return false; }
        }

        protected override bool IsAllPassGameTalonOpened
        {
            get { return true; }
        }
    }
}
