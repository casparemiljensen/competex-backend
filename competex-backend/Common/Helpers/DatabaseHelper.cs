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
                    default:
                        Console.WriteLine("Unknown type: " + typeof(T).Name);
                        throw new TypeAccessException();
                }
            }
            catch (Exception)
            {
                return typeof(T).Name.ToLower() + "s";
            }

        }
    }
}
