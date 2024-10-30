namespace competex_backend.Models
{
    public class Field
    {
        public Guid FieldId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public SurfaceType? Surface { get; set; } = SurfaceType.Unknown;

        public Field(string name)
        {
            Name = name;
            FieldId = new Guid();
        }
        public Field(string name, Guid fieldId)
            :this(name)
        {
            FieldId = fieldId;
        }
    }
}