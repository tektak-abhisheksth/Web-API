using System;

namespace Model.Attribute
{
    /// <summary>
    /// Provides additional attributes to classes, methods and properties.
    /// </summary>
    public class MetaDataAttribute : System.Attribute
    {
        public DateTime AddedDate;
        public readonly bool IsPublic;
        public readonly int MarkType;
        public string AliasName;

        /// <summary>
        /// Provides additional attributes to classes, methods and properties.
        /// </summary>
        /// <param name="addedDate">The date the element was created.</param>
        /// <param name="isPublic">Representation if the element is publicly displayable or meant for internal use only.</param>
        /// <param name="markType">Representation if the element is general, web-only or mobile-only. A value of 1 indicates web-only, 2 indicates mobile-only and 3 indicates general.</param>
        /// <param name="aliasName">The alias name, given to methods.</param>
        public MetaDataAttribute(string addedDate = "2015-01-01", bool isPublic = true, int markType = 3, string aliasName = "Default")
        {
            AddedDate = Convert.ToDateTime(addedDate);
            IsPublic = isPublic;
            MarkType = markType;
            AliasName = aliasName;
        }
    }
}
