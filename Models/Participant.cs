namespace competex_backend.Models
{
    public class Participant
    {
        public Guid ParticipantId { get; set; }
        public string Name { get; set; }

        public Participant(string name)
        {
            ParticipantId = Guid.NewGuid();
            Name = name;
        }
    }
}