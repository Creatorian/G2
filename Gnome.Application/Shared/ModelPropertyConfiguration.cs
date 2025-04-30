using System.Collections.Generic;

namespace Gnome.Application.Shared
{
    public class ModelPropertyConfiguration
    {
        public readonly string PropertyName;

        public string ParameterName { get; private set; }

        public PropertyLocation PropertyLocation { get; private set; }

        public object DefaultValue { get; private set; }

        public int? MinIntegerValue { get; private set; }

        public int? MaxIntegerValue { get; private set; }

        public bool UrlDecode { get; private set; }

        public bool SlashDecode { get; private set; }

        public List<object> ValidValues { get; private set; }

        public ModelPropertyConfiguration(string propertyName)
        {
            PropertyName = propertyName;
        }

        public ModelPropertyConfiguration FromPath()
        {
            PropertyLocation = PropertyLocation.PATH;
            return this;
        }

        public ModelPropertyConfiguration FromQuery()
        {
            PropertyLocation = PropertyLocation.QUERY;
            return this;
        }

        public ModelPropertyConfiguration FromToken()
        {
            PropertyLocation = PropertyLocation.TOKEN;
            return this;
        }

        public ModelPropertyConfiguration FromUserSubject()
        {
            PropertyLocation = PropertyLocation.USER_SUBJECT;
            return this;
        }

        public ModelPropertyConfiguration FromUserSubjectWithoutIdp()
        {
            PropertyLocation = PropertyLocation.USER_SUBJECT_WITHOUT_IDP;
            return this;
        }

        public ModelPropertyConfiguration FromIdentityProvider()
        {
            PropertyLocation = PropertyLocation.IDENTITY_PROVIDER;
            return this;
        }

        public ModelPropertyConfiguration FromProvidedParams()
        {
            PropertyLocation = PropertyLocation.PROVIDED_PARAMS;
            return this;
        }

        public ModelPropertyConfiguration FromForm()
        {
            PropertyLocation = PropertyLocation.FORM;
            return this;
        }

        public ModelPropertyConfiguration FromFormFile()
        {
            PropertyLocation = PropertyLocation.FORM_FILE;
            return this;
        }

        public ModelPropertyConfiguration FromFormFileName()
        {
            PropertyLocation = PropertyLocation.FORM_FILE_NAME;
            return this;
        }

        public ModelPropertyConfiguration FromBody()
        {
            PropertyLocation = PropertyLocation.BODY;
            return this;
        }

        public ModelPropertyConfiguration FromHeader()
        {
            PropertyLocation = PropertyLocation.HEADER;
            return this;
        }

        public ModelPropertyConfiguration HasParameterName(string name)
        {
            ParameterName = name;
            return this;
        }

        public ModelPropertyConfiguration HasDefaultValue(object value)
        {
            DefaultValue = value;
            return this;
        }

        public ModelPropertyConfiguration HasMaxIntegerValue(int value)
        {
            MaxIntegerValue = value;
            return this;
        }

        public ModelPropertyConfiguration HasMinIntegerValue(int value)
        {
            MinIntegerValue = value;
            return this;
        }

        public ModelPropertyConfiguration DecodeUrl(bool value = true, bool decodeOnlySlash = false)
        {
            UrlDecode = value;
            SlashDecode = decodeOnlySlash;
            return this;
        }

        public ModelPropertyConfiguration HasValidValues(List<object> values)
        {
            ValidValues = values;
            return this;
        }
    }
}