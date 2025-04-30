using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.Shared
{
    public enum PropertyLocation
    {
        FORM,
        FORM_FILE,
        FORM_FILE_NAME,
        QUERY,
        PATH,
        BODY,
        HEADER,
        TOKEN,
        IDENTITY_PROVIDER,
        USER_SUBJECT,
        USER_SUBJECT_WITHOUT_IDP,
        PROVIDED_PARAMS
    }
}
