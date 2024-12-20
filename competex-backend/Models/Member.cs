﻿namespace competex_backend.Models

{
    public class Member : Identifiable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public short Permissions { get; set; } = -1; //Set to correct type when we figure out how to handle permissions
        // public ICollection<ClubMember> ClubMembers { get; set; } = new List<ClubMember>();

        // No club implementation yet. 
        //public List<DbParticipantMember> ParticipantMembers { get; } = [];
    }
}
