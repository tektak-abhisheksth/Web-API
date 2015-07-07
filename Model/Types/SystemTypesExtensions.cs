
namespace Model.Types
{
    public static class SystemTypesExtensions
    {
        /// <summary>
        /// Indicates if the operation is successful or not.
        /// </summary>
        /// <param name="status">The operation in context.</param>
        /// <returns>Indication if the operation is a success or not.</returns>
        public static bool IsOperationSuccessful(this SystemDbStatus status)
        {
            return status == SystemDbStatus.Selected || status == SystemDbStatus.Inserted ||
                   status == SystemDbStatus.Updated ||
                   status == SystemDbStatus.Deleted || status == SystemDbStatus.Flushed ||
                   status == SystemDbStatus.MetaUpdated || status == SystemDbStatus.NoContent ||
                   status == SystemDbStatus.NotModified;
        }

        /// <summary>
        /// Determines if the given type is a basic simple field type.
        /// </summary>
        /// <param name="value">The type in context.</param>
        /// <returns>Indication if the type is a simple type or not.</returns>
        public static bool IsDefault(this SystemContactMarkedType value)
        {
            return value == SystemContactMarkedType.Address
                   || value == SystemContactMarkedType.Email
                   || value == SystemContactMarkedType.Fax
                   || value == SystemContactMarkedType.Mobile
                   || value == SystemContactMarkedType.Phone
                   || value == SystemContactMarkedType.Website
                   || value == SystemContactMarkedType.CompanyUserId;
        }

        /// <summary>
        /// Determines if the given type is a contact chat network field type.
        /// </summary>
        /// <param name="value">The type in context.</param>
        /// <returns>Indication if the type is a contact chat network type or not.</returns>
        public static bool IsChatNetwork(this SystemContactMarkedType value)
        {
            var v = (int)value;
            return v > 0 && v < 100;
        }
    }
}
