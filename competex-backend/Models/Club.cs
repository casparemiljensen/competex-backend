namespace competex_backend.Models
{
    public class Club : IIdentifiable
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string AssociatedSport { get; set; } //ved ikke om denne giver mening. Skal måske ændres til AssociatedSports hvis det er, da klubber kan have flere sportsgrene i mine øjne - Ilum
        public Club(string name, string associatedSport)
        {
            Id = Guid.NewGuid();
            Name = name;
            AssociatedSport = associatedSport;
        }

        public Club(string name, string associatedSport, Guid id)
            : this(name, associatedSport)
        {
            Id = id;
        }
    }
}
