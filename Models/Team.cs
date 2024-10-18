namespace competex_backend.Models
{
    internal class Team
    {
        public int TeamId { get; set; }
        bool isPlayer;
        List<Member> members;
        float score = 0;

        public Team(List<Member> members)
        {
            this.members = members;
            EvaluteIsPlayer();
        }

        public Team(Member member)
        {
            this.members = new List<Member>() { member };
            EvaluteIsPlayer();
        }

        public void AddMember(Member member) { 
            this.members.Add(member);
            EvaluteIsPlayer();
        }

        private void EvaluteIsPlayer()
        {
            isPlayer = members.Count < 2;
        }
    }
}
