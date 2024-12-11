using competex_backend.Models;

namespace competex_backend.Common.Helpers
{
    public static class DatabaseHelper
    {
        internal static string GetTableName<T>()
        {
            try
            {
                switch (Activator.CreateInstance<T>())
                {
                    case Entity:
                        return "entities";
                    case SportType:
                        return "sport_types";
                    case CompetitionType:
                        return "competition_types";
                    case ClubMembership:
                        return "club_memberships";
                    case Ekvipage:
                        return "participants";
                    case Match:
                        return "matches";
                    case ScoreResult:
                        return "score_results";
                    default:
                        var i = typeof(T).Name.ToLower() + "s";
                        return i;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Table name not found {ex.Message}");
            }

        }
    }
}
