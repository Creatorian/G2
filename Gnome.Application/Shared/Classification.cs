using System.Collections.Generic;

namespace Gnome.Application.Shared
{
    public class Classification : ClassificationInfo
    {
        public List<ClassificationValue> Values { get; set; }

        public Classification()
        {
        }

        public Classification(string schemaId, string name, string description, string additionalFields, List<ClassificationValue> values) : base(schemaId, name, description, additionalFields)
        {
            base.SchemaId = schemaId;
            base.Name = name;
            base.Description = description;
            base.AdditionalFields = additionalFields;
            Values = values;
        }
    }
}