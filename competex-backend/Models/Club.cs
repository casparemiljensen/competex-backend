namespace competex_backend.Models
{
    public class Club : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        public string AssociatedSport { get; set; } = string.Empty;
        //ved ikke om denne giver mening. Skal måske ændres til AssociatedSports hvis det er, da klubber kan have flere sportsgrene i mine øjne - Ilum
    }
}
