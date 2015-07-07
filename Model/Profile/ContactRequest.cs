using Model.Attribute;
using Model.Base;
using Model.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Model.Profile
{
    #region Base Section
    public class ContactRequestBase : RequestBase
    {
        [Description("The address of the user.")]
        public string Address { get; set; }

        [RegularExpression("^[A-Za-z0-9_\\+-]+(\\.[A-Za-z0-9_\\+-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)*\\.([A-Za-z]{2,4})$", ErrorMessage = "Invalid email.")]
        [Description("The email address of the user.")]
        public string Email { get; set; }

        [RegularExpression(@"^[+]?([0-9]*[\.\s\-\(\)]|[0-9]+){6,20}$", ErrorMessage = "Invalid fax number.")]
        [StringLength(30, ErrorMessage = "The {0} cannot exceed more than 30 characters.")]
        [Description("The fax number of the user.")]
        public string Fax { get; set; }

        [StringLength(20, ErrorMessage = "The {0} cannot exceed more than 20 characters.")]
        [RegularExpression(@"((\(\d{3}\)?)|(\d{3}))([\s-./]?)(\d{3})([\s-./]?)(\d{4})", ErrorMessage = "Invalid mobile number.")]
        [Description("The mobile number of the user.")]
        public string Mobile { get; set; }

        [StringLength(20, ErrorMessage = "The {0} cannot exceed more than 20 characters.")]
        [RegularExpression(@"^[+]?([0-9]*[\.\s\-\(\)]|[0-9]+){6,15}$", ErrorMessage = "Invalid phone number.")]
        [Description("The phone number of the user.")]
        public string Phone { get; set; }

        [RegularExpression(@"^(([\w]+:)?//)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$",
            ErrorMessage = "Invalid website format")]
        [Description("The website of the user.")]
        public string Website { get; set; }

        [Description("The temporary contact details of the user. After end date is reached, temporary contact is removed.")]
        public TemporaryContactRequest TemporaryContact { get; set; }
    }
    #endregion

    #region Add Section
    public class ContactRequest : ContactRequestBase
    {
        [Description("The chat network details of the user.")]
        public List<ChatNetworkRequest> ChatNetworks { get; set; }

        [Description("The custom contact details of the user.")]
        public List<CustomContactRequest> CustomContacts { get; set; }
    }

    public class TemporaryContactRequest
    {
        [Description("The related company ID of the user for the temporary contact.")]
        public int? CompanyUserId { get; set; }

        [Required]
        [Description("Start date of the stay.")]
        public DateTime StartDate { get; set; }

        [Required]
        [Description("End date of the stay.")]
        public DateTime EndDate { get; set; }
    }

    public class ChatNetworkRequest
    {
        [Required, Description("The chat network ID of the user (obtained from API \"Data/ChatNetworks\").")]
        public SystemContactMarkedType ChatNetworkId { get; set; }

        [Required, StringLength(100, MinimumLength = 5)]
        [Description("The user's user name for the chat network.")]
        public string Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1}", (byte)ChatNetworkId, Value);
        }
    }

    public class CustomContactRequest
    {
        [Required, StringLength(100, MinimumLength = 1), RegularExpression("^[A-Za-z0-9 .'-]+$", ErrorMessage = "Invalid custom contact name.")]
        [Description("The name for the custom contact.")]
        public string Name { get; set; }

        [Required, StringLength(100, MinimumLength = 1)]
        [Description("The value for the custom contact.")]
        public string Value { get; set; }

        [ValidEnum]
        [Description("The type marker representing the type for a single contact field.")]
        public SystemContactMarkedType FieldType { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", Name, Value, 0, (int)FieldType);
        }
    }
    #endregion

    #region Update Section
    public class UpdateContactRequest : ContactRequestBase
    {
        [Required, Description("The unique contact ID for the contact.")]
        public long ContactId { get; set; }

        [Description("List of custom contacts.")]
        public List<CustomUpsertContact> CustomContacts { get; set; }

        [Description("List of chat networks.")]
        public List<CustomUpsertContactChatNetwork> ChatNetworks { get; set; }
    }

    public class UpdateChatNetworkRequest : ChatNetworkRequest
    {
        [Description("The unique ID representing the contact chat information.")]
        public int? ContactChatNetworkId { get; set; }

        [Required, Description("The unique ID representating the custom contact information.")]
        public long ContactCustomId { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", (int)ChatNetworkId, Value, ContactCustomId, (object)ContactChatNetworkId ?? "");
        }
    }

    public class UpdateCustomContactRequest : CustomContactRequest
    {
        [Description("The unique ID representating the custom contact information.")]
        public long? ContactCustomId { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", Name ?? "", Value ?? "", (object)ContactCustomId ?? "", (int)FieldType);
        }
    }

    public class UpdateUserChatNetworkRequest : RequestBase
    {
        [Description("The unique contact ID for the contact.")]
        public long ContactId { get; set; }

        [Description("The chat network details of the user.")]
        public List<UpdateChatNetworkRequest> ChatNetworks { get; set; }

        public override string ToString()
        {
            return (ChatNetworks == null || !ChatNetworks.Any()) ? string.Empty : string.Join("|", ChatNetworks);
        }
    }

    public class UpdateUserCustomContactRequest : RequestBase
    {
        [Description("The unique contact ID for the contact.")]
        public long ContactId { get; set; }

        [Description("The custom contact details of the user.")]
        public List<UpdateCustomContactRequest> CustomContacts { get; set; }

        public override string ToString()
        {
            return (CustomContacts == null || !CustomContacts.Any()) ? string.Empty : string.Join("|", CustomContacts);
        }
    }
    #endregion

    #region Meta Section
    public class MetaChatNetworkRequest : RequestBase
    {
        [Description("The unique contact ID for the contact.")]
        public long ContactId { get; set; }

        [Description("The chat network details of the user.")]
        public List<ChatNetworkRequest> ChatNetworks { get; set; }
    }

    public class MetaCustomContactNetwork : RequestBase
    {
        [Description("The unique contact ID for the contact.")]
        public long ContactId { get; set; }

        [Description("The list of custom contact details of the user.")]
        public List<CustomUpsertContact> CustomContacts { get; set; }
    }

    public class CustomUpsertContact
    {
        [Description("The unique ID representating the custom contact information.")]
        public long? ContactCustomId { get; set; }

        [StringLength(100, MinimumLength = 0), RegularExpression("^[A-Za-z0-9 .'-]+$", ErrorMessage = "Invalid custom contact name.")]
        [Description("The name for the custom contact.")]
        public string Name { get; set; }

        [Required, StringLength(100, MinimumLength = 1)]
        [Description("The value for the custom contact.")]
        public string Value { get; set; }

        [ValidEnum]
        [Description("The type marker representing the type for a single contact field.")]
        public SystemContactMarkedType FieldType { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", Name ?? "", Value ?? "", (object)ContactCustomId ?? "", (int)FieldType);
        }
    }

    public class CustomUpsertContactChatNetwork
    {
        [Description("The chat network ID uniquely representing the contact chat network information.")]
        public int? ContactChatNetworkId { get; set; }

        [Required, StringLength(100, MinimumLength = 1)]
        [Description("The value for the contact chat network.")]
        public string Value { get; set; }

        [Required, ValidEnum]
        [Description("The chat network type (obtained from API \"Data/ChatNetworks\").")]
        public SystemContactMarkedType ChatNetworkId { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", (int)ChatNetworkId, Value ?? "", (object)ContactChatNetworkId ?? "");
        }
    }
    #endregion

    #region Add suggest contact Section
    public class SuggestedContactBaseRequest : RequestBase
    {
        [Required]
        [Description("The unique system provided ID of the target user.")]
        public int TargetUserId { get; set; }

        [Description("The address of the user.")]
        public string Address { get; set; }

        [RegularExpression("^[A-Za-z0-9_\\+-]+(\\.[A-Za-z0-9_\\+-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)*\\.([A-Za-z]{2,4})$", ErrorMessage = "Invalid email.")]
        [Description("The email address of the user.")]
        public string Email { get; set; }

        [RegularExpression(@"^[+]?([0-9]*[\.\s\-\(\)]|[0-9]+){6,20}$", ErrorMessage = "Invalid fax number.")]
        [StringLength(30, ErrorMessage = "The {0} cannot exceed more than 30 characters.")]
        [Description("The fax number of the user.")]
        public string Fax { get; set; }

        [StringLength(20, ErrorMessage = "The {0} cannot exceed more than 20 characters.")]
        [RegularExpression(@"((\(\d{3}\)?)|(\d{3}))([\s-./]?)(\d{3})([\s-./]?)(\d{4})", ErrorMessage = "Invalid mobile number.")]
        [Description("The mobile number of the user.")]
        public string Mobile { get; set; }

        [StringLength(20, ErrorMessage = "The {0} cannot exceed more than 20 characters.")]
        [RegularExpression(@"^[+]?([0-9]*[\.\s\-\(\)]|[0-9]+){6,15}$", ErrorMessage = "Invalid phone number.")]
        [Description("The phone number of the user.")]
        public string Phone { get; set; }

        [RegularExpression(@"^(([\w]+:)?//)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$",
            ErrorMessage = "Invalid website format")]
        [Description("The website of the user.")]
        public string Website { get; set; }

        [Required, Description("The unique contact ID for the contact.")]
        public long ContactId { get; set; }

    }

    public class AddSuggestedChatNetworkRequest : ChatNetworkRequest
    {
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", ChatNetworkId, Value, 0, 0);
        }
    }

    public class AddSuggestContactRequest : SuggestedContactBaseRequest
    {
        [Description("The chat network details of the user.")]
        public List<AddSuggestedChatNetworkRequest> ChatNetworks { get; set; }

        [Description("The custom contact details of the user.")]
        public List<CustomContactRequest> CustomContacts { get; set; }
    }

    public class UpdateSuggestContactRequest : SuggestedContactBaseRequest
    {
        [Description("The chat network details of the user.")]
        public List<UpdateChatNetworkRequest> ChatNetworks { get; set; }

        [Description("The custom contact details of the user.")]
        public List<UpdateCustomContactRequest> CustomContacts { get; set; }
    }

    public class SuggestedContactOperationRequest : RequestBase
    {
        [Required, Description("The unique ID representating the custom contact information.")]
        public long ContactCustomId { get; set; }

        [Required, Range(2, 4), Description("The mode represents '2' for 'append', '3' for 'accept' and '4' for 'reject'.")]
        public byte Mode { get; set; }
    }

    public class BasicContactPersonRequest : RequestBase
    {
        [Required, StringLength(50)]
        [Description("The first name of the user.")]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        [Description("The last name of the user.")]
        public string LastName { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Description("The title of the user.")]
        public string Title { get; set; }
    }

    public class ContactSettingCategoriesRequest : RequestBase
    {
        [Required, Description("The unique contact ID for the contact.")]
        public long ContactId { get; set; }

        [Required, Description("The unique ID representating the custom contact information.")]
        public long ContactCustomId { get; set; }
    }

    public class ContactSettingRequest : RequestBase
    {
        [Required, Description("The unique ID representating the custom contact information.")]
        public int ContactId { get; set; }

        [ValidEnum]
        [Description("The type marker representing the type for a single contact field.")]
        public SystemContactMarkedType FieldType { get; set; }

        [Description("The unique ID representing the contact chat information.")]
        public int ContactChatNetworkId { get; set; }

        [Description("The unique ID representating the custom contact information.")]
        public int ContactCustomId { get; set; }

        [RegularExpression(@"^\d*$", ErrorMessage = "Invalid value format.")]
        [Description("A numeric value that has special meaning for each type of setting.")]
        public int Value { get; set; }

        [Description("List of contacts or categories of the user (when selecting custom list of contacts or categories).")]
        public List<int> Entries { get; set; }

        [Description("An instruction which forces removal of all existing categories/contacts.")]
        public bool Flush { get; set; }
    }

    public class RespondEmploymentRequest : RequestBase
    {
        [Required]
        [Description("The user name or the user ID of the target user.")]
        public string TargetUser { get; set; }

        [Required]
        [Description("An indication as to whether or not the request was accepted.")]
        public bool IsAccepted { get; set; }
    }
    #endregion
}