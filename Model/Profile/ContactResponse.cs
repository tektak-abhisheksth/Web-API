using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Model.Types;

namespace Model.Profile
{
    public class ContactResponse
    {
        [Description("The unique contact ID for the contact.")]
        public long ContactId { get; set; }

        [Description("The list of contact chat network IDs.")]
        public List<int> ContactChatNetworks { get; set; }

        [Description("The list of custom contact IDs.")]
        public List<long> ContactCustoms { get; set; }
    }

    public class ContactSuggestionResponse
    {
        [Description("The address of the user.")]
        public long? Address { get; set; }

        [Description("The email address of the user.")]
        public long? Email { get; set; }

        [Description("The fax number of the user.")]
        public long? Fax { get; set; }

        [Description("The mobile number of the user.")]
        public long? Mobile { get; set; }

        [Description("The phone number of the user.")]
        public long? Phone { get; set; }

        [Description("The website of the user.")]
        public long? Website { get; set; }

        [Description("The chat network IDs of the user.")]
        public IEnumerable<long> ChatNetworks { get; set; }

        [Description("The custom contact details of the user.")]
        public IEnumerable<long> CustomContacts { get; set; }
    }

    public class ContactSuggestions
    {
        [Description("The suggestions made for address field.")]
        public IEnumerable<SuggestedContactResponse> Address { get; set; }

        [Description("The suggestions made for email field.")]
        public IEnumerable<SuggestedContactResponse> Email { get; set; }

        [Description("The suggestions made for fax field.")]
        public IEnumerable<SuggestedContactResponse> Fax { get; set; }

        [Description("The suggestions made for mobile field.")]
        public IEnumerable<SuggestedContactResponse> Mobile { get; set; }

        [Description("The suggestions made for phone field.")]
        public IEnumerable<SuggestedContactResponse> Phone { get; set; }

        [Description("The suggestions made for website field.")]
        public IEnumerable<SuggestedContactResponse> Website { get; set; }

        [Description("The suggestions made for chat networks.")]
        public IEnumerable<SuggestedContactResponse> ChatNetworks { get; set; }

        [Description("The custom suggestions.")]
        public IEnumerable<SuggestedContactResponse> Customs { get; set; }

    }

    public class SuggestedContactResponse
    {
        [Description("The unique ID representating the custom contact information.")]
        public long ContactCustomId { get; set; }

        [Description("The unique contact ID for the contact.")]
        public int ContactId { get; set; }

        [Description("The contact type of the contact.")]
        public SystemContactType ContactTypeId { get; set; }

        [Description("The unique system provided ID of the user.")]
        public int UserId { get; set; }

        [Description("The unique system provided name of the user.")]
        public string UserName { get; set; }

        [Description("The display name of the user.")]
        public string DisplayName { get; set; }

        [Description("The picture of the user.")]
        public string Picture { get; set; }

        [IgnoreDataMember]
        [Description("The type marker representing the type for a single contact field.")]
        public SystemContactMarkedType FieldType { get; set; }

        [Description("The name for the custom contact.")]
        public string Name { get; set; }

        [Description("The value for the custom contact.")]
        public string Value { get; set; }

        [Description("The approved original name for the custom contact.")]
        public string OriginalName { get; set; }

        [Description("The approved original value for the custom contact.")]
        public string OriginalValue { get; set; }

        [Description("The unique ID representing the contact chat information.")]
        public int? ContactChatNetworkId { get; set; }

        [IgnoreDataMember]
        [Description("Any available custom contact ID of the target user, uniquely representing an existing custom contact information. If this value is populated, then the current suggestion was made as an update/change on existing approved custom contact information. An invalid value means the suggestion was purely added afresh by current user.")]
        public long? ReferralContactCustomId { get; set; }

        [Description("The date when the suggestion was made.")]
        public DateTime Added { get; set; }

        public bool IsEdited()
        {
            return (FieldType.IsDefault() && ReferralContactCustomId.HasValue && ReferralContactCustomId.Value < 0)
                   || (FieldType.IsChatNetwork() && ContactChatNetworkId.HasValue)
                   || (FieldType == SystemContactMarkedType.Custom && ReferralContactCustomId.HasValue);
        }
    }

    public class Contact
    {
        [Description("The home contact information of the user.")]
        public ContactInformation Home { get; set; }

        [Description("The work contact information of the user.")]
        public ContactInformation Office { get; set; }

        [Description("The list of temporary contact information of the user.")]
        public IEnumerable<TemporaryContactInformation> Temporary { get; set; }
    }

    #region Suggestion Models
    public class FieldSuggestion<T>
    {
        [Description("The unique custom contact/suggestion ID for the suggestion.")]
        public long ContactCustomId { get; set; }

        [Description("The field type of the suggestion.")]
        public SystemContactMarkedType FieldType { get; set; }

        [Description("The suggested new value by the current user.")]
        public T Value { get; set; }

        [Description("Current suggestion status of the suggestion.")]
        public SystemApprovalStatus Approved { get; set; }
    }

    public class ChatNetworkFieldSuggestion : FieldSuggestion<string>
    {
        [Description("Any available contact chat network ID of the target user, uniquely representing an existing chat network information. If this value is populated, then the current suggestion was made as an update/change on existing approved chat network information. An invalid value means the suggestion was purely added afresh by current user.")]
        public int? ContactChatNetworkId { get; set; }
    }

    public class CustomContactFieldSuggestion : FieldSuggestion<string>
    {
        [Description("The name information for the custom contact.")]
        public string Name { get; set; }
        [Description("Any available custom contact ID of the target user, uniquely representing an existing custom contact information. If this value is populated, then the current suggestion was made as an update/change on existing approved custom contact information. An invalid value means the suggestion was purely added afresh by current user.")]
        public long? ReferralContactCustomId { get; set; }
    }
    #endregion

    public class ContactFieldBasic<T>
    {
        [Description("The original value for the given field.")]
        public T Value { get; set; }



        [Required, Description("An indication as to whether or not the field is visible, according to the user setting for this field.")]
        public bool Visibility { get; set; }
    }

    public class ContactField<T> : ContactFieldBasic<T>
    {
        public ContactField()
        {
            CustomContacts = new List<CustomContactField>();
        }

        [Description("Information on suggestion previously made by the current user, if any.")]
        public FieldSuggestion<T> Suggestion { get; set; }

        [Description("The list of custom contacts.")]
        public List<CustomContactField> CustomContacts { get; set; }
    }

    public class ContactInformation
    {
        [Description("The unique contact ID for the contact.")]
        public long ContactId { get; set; }

        [IgnoreDataMember]
        [Description("The unique system provided ID of the user.")]
        public int UserId { get; set; }

        [Description("The contact type ID representing the type of the contact.")]
        public SystemContactType ContactTypeId { get; set; }

        [Description("The address.")]
        public ContactField<string> Address { get; set; }

        [Description("The email address.")]
        public ContactField<string> Email { get; set; }

        [Description("The fax number.")]
        public ContactField<string> Fax { get; set; }

        [Description("The mobile number.")]
        public ContactField<string> Mobile { get; set; }

        [Description("The contact phone number.")]
        public ContactField<string> Phone { get; set; }

        [Description("The web address of the user.")]
        public ContactField<string> Website { get; set; }

        [Description("The list of chat network names.")]
        public List<ChatNetworkField> ChatNetworks { get; set; }

        [Description("The list of custom contacts.")]
        public List<CustomContactField> CustomContacts { get; set; }

        [Description("The suggestion counts.")]
        public int SuggestionCounts { get; set; }
    }

    public class TemporaryContactInformation : ContactInformation
    {
        [Description("The unique user ID of a business user. If provided, refers the destination company where the temporary shift is occuring.")]
        public ContactFieldBasic<int?> CompanyUserId { get; set; }

        public string CompanyName { get; set; }

        [Description("The start date of the temporary contact.")]
        public DateTime? StartDate { get; set; }

        [Description("The end date of the temporary contact. Contact is deleted from server after end date is crossed.")]
        public DateTime? EndDate { get; set; }

        public ContactInformation GetParent()
        {
            return new ContactInformation
            {
                Address = Address,
                ChatNetworks = ChatNetworks,
                ContactId = ContactId,
                CustomContacts = CustomContacts,
                ContactTypeId = ContactTypeId,
                Email = Email,
                Fax = Fax,
                Mobile = Mobile,
                Phone = Phone,
                Website = Website,
                SuggestionCounts = SuggestionCounts
            };
        }
    }

    public class ContactChatNetwork
    {
        [Description("The unique ID for the chat network.")]
        public int ContactChatNetworkId { get; set; }

        [Description("The chat network type ID.")]
        public SystemContactMarkedType ChatNetworkId { get; set; }

        [Description("The name of the contact for the chat network.")]
        public string Value { get; set; }
    }

    public class ContactCustom
    {
        [Description("The unique ID for the custom contact.")]
        public long ContactCustomId { get; set; }

        [Description("The name field for the custom contact.")]
        public string Name { get; set; }

        [Description("The value for the custom contact.")]
        public string Value { get; set; }

        [Description("The field type of the custom contact.")]
        public SystemContactMarkedType FieldType { get; set; }
    }

    public class ChatNetworkField
    {
        [Description("The actual field information for the chat network made by the target user.")]
        public ContactChatNetwork Original { get; set; }

        [Description("The suggested field information for the chat network made by the current user. This is present if a suggestion was made by the current user.")]
        public ChatNetworkFieldSuggestion Suggestion { get; set; }
    }

    public class CustomContactField
    {
        [Description("The actual field information for the custom contact made by the target user.")]
        public ContactCustom Original { get; set; }

        [Description("The suggested field information for the custom contact made by the current user. This is present if a suggestion was made by the current user.")]
        public CustomContactFieldSuggestion Suggestion { get; set; }
    }

    public class ContactSettingResponse
    {
        [Description("The unique ID representating the custom contact information.")]
        public long ContactCustomId { get; set; }

        [Description("The unique ID representing the contact chat information.")]
        public long ContactChatNetworkId { get; set; }

        [Description("The type marker representing the type for a single contact field.")]
        public SystemContactMarkedType FieldType { get; set; }

        [Description("The default value.")]
        public bool IsDefault { get; set; }

        [Description("The basic primary value.")]
        public bool IsBasicPrimary { get; set; }

        [Description("The setting value.")]
        public SystemSettingValue Value { get; set; }
    }
    public class ContactSettingCategoriesResponse
    {
        public int UserCategoryTypeId { get; set; }

        [Description("The category name.")]
        public string Name { get; set; }

        [Description("The description of the category.")]
        public string Description { get; set; }
    }

    public class ContactSettingFriendsResponse
    {
        [Description("The unique system provided ID of the user.")]
        public int UserId { get; set; }

        [Description("The unique system provided name of the user.")]
        public string UserName { get; set; }

        [Description("The type of users in the iLoop system.")]
        public SystemUserType UserTypeId { get; set; }

        [Description("The name of the user.")]
        public string Name { get; set; }

        [Description("The picture of the user.")]

        public string Picture { get; set; }

        [Description("The title of the user.")]

        public string Title { get; set; }
    }
}
