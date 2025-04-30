using System.Runtime.Serialization;

namespace Gnome.Application.Shared
{
    public enum ValidationErrorCode
    {
        //
        // Summary:
        //     Value supplied exceeds maximum allowed length
        [EnumMember(Value = "max-length")]
        MaxLength,
        //
        // Summary:
        //     Value supplied does not meet minimum length
        [EnumMember(Value = "min-length")]
        MinLength,
        //
        // Summary:
        //     Mandatory field or parameter was not supplied
        [EnumMember(Value = "required")]
        Required,
        //
        // Summary:
        //     Value supplied was out of allowed range
        [EnumMember(Value = "out-of-range")]
        OutOfRange,
        //
        // Summary:
        //     Value supplied does not have expected format
        [EnumMember(Value = "invalid-format")]
        InvalidFormat,
        //
        // Summary:
        //     Value supplied does not belong to enumeration
        [EnumMember(Value = "unknown-enum")]
        UnknownEnum,
        //
        // Summary:
        //     Value supplied does not belong to classification
        [EnumMember(Value = "not-on-list")]
        NotOnList,
        //
        // Summary:
        //     Value supplied does not conform to check digit validation
        [EnumMember(Value = "check-digit-invalid")]
        CheckDigitInvalid,
        //
        // Summary:
        //     Parameter must be used with other parameters that were not supplied
        [EnumMember(Value = "combination-required")]
        CombinationRequired,
        //
        // Summary:
        //     Parameter is read-only
        [EnumMember(Value = "read-only")]
        ReadOnly
    }
}