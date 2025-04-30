using System;

namespace Gnome.Application.Shared
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class Enumeration : Attribute
    {
        public Classification EnumerationMetaData { get; }

        public Enumeration (string schemaId, string name)
        {
            EnumerationMetaData = new Classification
            {
                SchemaId = schemaId,
                Name = name,
                ServiceName = null
            };
        }
    }
}