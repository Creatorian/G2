using System.Collections.Generic;

namespace Gnome.Application.Shared
{
    public class ClassificationValue
    {
        //
        // Summary:
        //     Literal that uniquely identifies classification value
        public string Literal { get; set; }

        //
        // Summary:
        //     Name of classification value
        public string Name { get; set; }

        //
        // Summary:
        //     Optional numerical code of classification value
        public string Code { get; set; }

        //
        // Summary:
        //     Description of classification value
        public string Description { get; set; }

        //
        // Summary:
        //     Optional literal of parent classification value if schema is hierarchical
        public string ParentLiteral { get; set; }

        //
        // Summary:
        //     Dictionary of additional properties for classification
        public Dictionary<string, string> AdditionalFields { get; set; }

        //
        // Summary:
        //     Empty constructor
        public ClassificationValue()
        {
        }

        //
        // Summary:
        //     Constructor initializing classification value with code and parent
        //
        // Parameters:
        //   literal:
        //
        //   code:
        //
        //   description:
        //
        //   parentLiteral:
        //
        //   additionalFields:
        public ClassificationValue(string literal, string code, string description, string parentLiteral, Dictionary<string, string> additionalFields)
        {
            Code = code;
            Literal = literal;
            ParentLiteral = parentLiteral;
            Description = description;
            AdditionalFields = additionalFields;
        }

        //
        // Summary:
        //     Minimal initialization of Classification value
        //
        // Parameters:
        //   literal:
        //
        //   description:
        //
        //   additionalFields:
        public ClassificationValue(string literal, string description, Dictionary<string, string> additionalFields)
        {
            Literal = literal;
            Description = description;
            AdditionalFields = additionalFields;
        }
    }
}