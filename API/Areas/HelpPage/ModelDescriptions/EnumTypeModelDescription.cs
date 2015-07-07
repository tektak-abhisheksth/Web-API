using System;
using System.Collections.ObjectModel;

namespace API.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
        public string Description { get; set; }
        public DateTime? Added { get; set; }
        public int MarkType { get; set; }
    }
}