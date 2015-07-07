using System;

namespace API.Areas.HelpPage.ModelDescriptions
{
    public class EnumValueDescription
    {
        public string Documentation { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public DateTime? AddedDate { get; set; }

        public int MarkType { get; set; }

    }
}