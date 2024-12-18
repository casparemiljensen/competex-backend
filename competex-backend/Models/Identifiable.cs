using competex_backend.Models;

public class Identifiable : IIdentifiable
{
    public Guid Id { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is Identifiable otherIdentifiable)
        {
            return this.Id == otherIdentifiable.Id; // Compare by Id (or whatever property defines uniqueness)
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
