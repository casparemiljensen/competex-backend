namespace competex_backend.Models
{
    public class Field : IIdentifiable
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public SurfaceType? Surface { get; set; } = SurfaceType.Unknown;

        public Field(string name)
        {
            Name = name;
            Id = new Guid();
        }
        public Field(string name, Guid id)
            :this(name)
        {
            Id = id;
        }
    }
}