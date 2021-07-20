using VotesRestApi.Core.Enums;

namespace VotesRestApi.Core.Helper
{
    public static class NominationHelper
    {
        public static string GetNominationDescription(Nomination nomination)
        {
            string description = string.Empty;

            switch (nomination)
            {
                case Nomination.TeamPlayer:
                    description = "Team Player";
                    break;
                case Nomination.TechnicalReferent:
                    description = "Technical Referent";
                    break;
                case Nomination.KeyPlayer:
                    description = "Key Player";
                    break;
                case Nomination.Motivator:
                    description = "Motivator";
                    break;
                case Nomination.Funniest:
                    description = "Funny";
                    break;
            }

            return description;
        }
    }
}
