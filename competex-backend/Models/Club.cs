﻿namespace competex_backend.Models
{
    public class Club
    {
        public Guid ClubId { get; set; }
        public string Name { get; set; }
        public string AssociatedSport { get; set; } //ved ikke om denne giver mening. Skal måske ændres til AssociatedSports hvis det er, da klubber kan have flere sportsgrene i mine øjne - Ilum
        public Club(string name, string associatedSport)
        {
            ClubId = Guid.NewGuid();
            Name = name;
            AssociatedSport = associatedSport;
        }

        public Club(string name, string associatedSport, Guid clubId)
            : this(name, associatedSport)
        {
            ClubId = clubId;
        }
    }
}