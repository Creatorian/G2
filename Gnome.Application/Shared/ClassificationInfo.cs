using System.Text.Json.Serialization;

namespace Gnome.Application.Shared
{
    public class ClassificationInfo
    {
        public string SchemaId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string AdditionalFields { get; set; }
        [JsonIgnore] public string ServiceName { get; set; }

        public ClassificationInfo() { }

        public ClassificationInfo(string schemaId, string name, string description, string additionalFields)
        {
            SchemaId = schemaId;
            Name = name;
            Description = description;
            AdditionalFields = additionalFields;
        }
    }
}