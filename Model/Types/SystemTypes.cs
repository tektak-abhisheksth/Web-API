using Model.Attribute;
using System.ComponentModel;
using System.Net;

namespace Model.Types
{
    #region Internal Invisible types
    [Description("Provides all possible responses that system can generate. This list is bound to the database, please do not modify.")]
    [MetaData("2014-07-01", false)]
    public enum SystemDbStatus : byte
    {
        [Description("Defines occurrence of some exception during the operation.")]
        GeneralError = 0,

        [Description("Defines a result set is being returned.")]
        Selected = 1,

        [Description("Defines one or more data entries being added to the system.")]
        Inserted = 2,

        [Description("Defines existing data being updated in the system.")]
        Updated = 3,

        [Description("Defines a resource being deleted.")]
        Deleted = 4,

        [Description("Defines an entire set of related entries being flushed.")]
        Flushed = 5,

        [Description("Defines metadata of an existing entity being updated in the system.")]
        MetaUpdated = 6,

        [Description("Defines no content is being returned or no content found in the system.")]
        NoContent = 7,

        [Description("Defines the content as not being modified since the last edit.")]
        NotModified = 8,

        [Description("Defines restriction to any resource.")]
        Forbidden = 9,

        [Description("Defines any operation currently in a pending state.")]
        Pending = 10,

        [Description("Defines a duplicate request being made.")]
        Duplicate = 11,

        [Description("Defines a resource as not been found or removed from the system.")]
        NotFound = 12,

        [Description("Defines any one-time operation being currently being repeated or any usless request being made to the system.")]
        Idempotent = 13,

        [Description("Defines invalid user credentials.")]
        Unauthorized = 14,

        [Description("Defines any operation being performed that is not supported by the system.")]
        NotSupported = 15
    }

    [Description("The available information that system can store in session on a per-user basis.")]
    [MetaData("2014-07-01", false)]
    public enum SystemSessionEntity : byte
    {
        UserId = 0,
        UserName = 1,
        UserTypeId = 2,
        DeviceTypeId = 3,
        DeviceId = 4,
        LoginToken = 5
    }

    [Description("The tables containing different data types.")]
    [MetaData("2015-05-19", false)]
    public struct SystemDataTable
    {
        public static string AcademicConcentration { get { return "AcademicConcentration"; } }
        public static string AcademicDegreeType { get { return "AcademicDegreeType"; } }
        public static string AcademicInstitute { get { return "AcademicInstitute"; } }
        public static string ChatNetwork { get { return "ChatNetwork"; } }
        public static string Department { get { return "Department"; } }
        public static string Industry { get { return "Industry"; } }
        public static string Language { get { return "Language"; } }
        public static string Nationality { get { return "Nationality"; } }
        public static string Position { get { return "Position"; } }
        public static string Religion { get { return "Religion"; } }
        public static string SkillType { get { return "SkillType"; } }
        public static string State { get { return "State"; } }
        public static string Town { get { return "Town"; } }
    }
    #endregion


    [Description("The type of users in the iLoop system.")]
    [MetaData("2014-07-24")]
    public enum SystemUserType : byte
    {
        [Description("Defines person user type.")]
        Person = 1,
        [Description("Defines company user type.")]
        Business = 2
    }

    [Description("Http status codes that are returned by iLoop system to clients.")]
    [MetaData("2014-08-18")]
    public enum SystemStatusCode
    {
        [Description("Defines that the request was processed successfully. Equivalent to HTTP status code 200.")]
        Success = HttpStatusCode.OK,

        [Description("Defines that a new resource was created successfully. Equivalent to HTTP status code 201.")]
        Created = HttpStatusCode.Created,

        [Description("Defines that a resource was processed successfully and response is intentionally blank. Equivalent to HTTP status code 204.")]
        NoContent = HttpStatusCode.NoContent,

        [Description("Defines that a resource was not modified since the last request and so the response is intentionally blank. Equivalent to HTTP status code 304.")]
        NotModified = HttpStatusCode.NotModified,

        [Description("Defines that the request sent is invalid or missing information. Equivalent to HTTP status code 400.")]
        Invalid = HttpStatusCode.BadRequest,

        [Description("Defines that a resource access requires authentication. Equivalent to HTTP status code 401.")]
        Unauthorized = HttpStatusCode.Unauthorized,

        [Description("Defines that a request is valid but will not be served by the server. Equivalent to HTTP status code 403.")]
        Forbidden = HttpStatusCode.Forbidden,

        [Description("Defines that a resource was not found in the server. Equivalent to HTTP status code 404.")]
        NotFound = HttpStatusCode.NotFound,

        [MetaData("2015-07-06")]
        [Description("Defines that a user has sent too many requests in a given amount of time. Equivalent to HTTP status code 429.")]
        TooManyRequests = 429,

        [Description("Defines that some error occured on the server during request processing. Equivalent to HTTP status code 500.")]
        ServerError = HttpStatusCode.InternalServerError
    }

    [Description("The sub codes returned during login, indicating the login status.")]
    [MetaData("2014-08-01")]
    public enum SystemAccountSubCode : byte
    {
        [Description("Defines account authentication being successful.")]
        Normal = 0,
        [Description("Defines that user doesn't exist in the system.")]
        UserNotExist = 1,
        [Description("Defines a registered user who hasn't completed his/her/its registration process.")]
        NotYetApproved = 2,
        [Description("Defines account being locked due to multiple unsuccessful attempts.")]
        Locked = 3,
        [Description("Defines the account as been deactivated by the user.")]
        Deactivated = 4,
        [Description("Defines the password provided as being incorrect.")]
        WrongPassword = 5
    }

    [Description("The gender of personal user.")]
    [MetaData("2014-08-01")]
    public enum SystemGender : byte
    {
        [Description("Defines a male personal member.")]
        Male = 1,
        [Description("Defines a female personal member.")]
        Female = 2
    }

    [Description("Defines the contact types in the system.")]
    [MetaData("2015-02-24")]
    public enum SystemContactType : byte
    {
        [Description("Defines personal/home contact type.")]
        Home = 1,

        [Description("Defines business/office/company contact type.")]
        Office = 2,

        [Description("Defines a temporary \"Home\" contact type.")]
        TemporaryHome = 3,

        [Description("Defines a temporary \"Office\" contact type.")]
        TemporaryOffice = 4
    }

    [Description("Defines the friendship status with other users.")]
    [MetaData("2014-07-28")]
    public enum SystemFriendshipStatus : byte
    {
        [Description("Defines both parties as not friends.")]
        NotFriend = 0,
        [Description("Defines incoming pending request.")]
        FriendshipRequestReceived = 1,
        [Description("Defines outgoing pending request.")]
        FriendRequestSent = 2,
        [Description("Defines both parties as friends.")]
        Friend = 3
    }

    [Description("Defines notification types used by the system.")]
    [MetaData("2014-08-05", markType: 1)]
    public enum SystemNotificationType : byte
    {
        [Description("Defines a thumbs up or a thumbs down is received.")]
        Thumbs = 1,
        [Description("Defines a direct profile view.")]
        ProfileView = 5,
        [Description("Defines a friendship request being accepted."), MetaData]
        Friendship = 6,
        [Description("Defines a link account request being accepted.")]
        LinkedAccount = 8,
        [Description("Defines a user being added to a group.")]
        AddedToGroup = 10,
        [Description("Defines a comment is received.")]
        Comment = 11,
        [Description("Defines an associated group's name being changed.")]
        GroupNameChanged = 12,
        [Description("Defines an associated group's description being changed.")]
        GroupDescriptionChanged = 13,
        [Description("Defines an associated group's privacy being changed.")]
        GroupPrivacyChanged = 14,
        [Description("Defines an associated group's picture being changed.")]
        GroupPictureChanged = 15,
        [Description("Defines a child company having accepted the add as parent request.")]
        AddedAsParentCompany = 16,
        [Description("Defines a parent company having accepted the add as child request.")]
        AddedAsChildCompany = 17,
        [Description("Defines a user being promoted to a group's administrator.")]
        PromotedAsGroupAdmin = 18,
        [Description("Defines a personal user's add as employee request being accepted.")]
        AddedAsEmployee = 19,
        [Description("Defines a business user's add as employer request being accepted.")]
        AddedAsEmployer = 20,
        [Description("Defines a group being deleted by a super administrator.")]
        DeletedGroup = 22,
        [Description("Defines an associated event's time being changed.")]
        EventTimeChanged = 23,
        [Description("Defines an associated event's venue being changed.")]
        EventLocationChanged = 24,
        [Description("Defines an associated event being canceled.")]
        CanceledEvent = 25,
        [Description("Defines a profile view by first searching by position.")]
        ProfileViewedPositional = 26,
        [Description("Defines a profile view by first searching by skill.")]
        ProfileViewedSkill = 27,
        [Description("Defines a promote employee request being accepted.")]
        PromotedEmployee = 28,
        [Description("Defines an associated event's name being changed.")]
        EventNameChanged = 29,
        [Description("Defines a personal user's employment information being changed.")]
        UpdatedEmployeeInformation = 30,
        [Description("Defines an event being created from business user's existing service(s).")]
        EventGeneratedForService = 31
    }

    [Description("Defines request types used by the system.")]
    [MetaData("2014-08-05", markType: 1)]
    public enum SystemRequestType
    {
        [Description("Defines a request for friendship.")]
        [MetaData]
        Friendship = 1001,
        [Description("Defines a request for addition of suggested skill(s).")]
        SkillSuggestion = 1002,
        [Description("Defines a request for linked account.")]
        LinkAccount = 1021,
        [Description("Defines a request from a business user to a personal user to be added as its employee.")]
        AddAsEmployee = 1022,
        [Description("Defines a request from a business user to an existing employee to be promoted.")]
        PromoteEmployee = 1023,
        [Description("Defines a request from a personal user to a business user to add him/her as its employee.")]
        AddAsEmployer = 1024,
        [Description("Defines a request from a personal employee to agree updating of updated employee information.")]
        ChangedWorkInformation = 1025,
        [Description("Defines a request from a personal user to a business user to add him/her as its ex-employee.")]
        AddAsExemployer = 1026,
        [Description("Defines a request from a business user to another business user to add it as its parent company.")]
        AddAsParentCompany = 1031,
        [Description("Defines a request from a business user to another business user to add it as its branch company.")]
        AddAsChildCompany = 1032,
        [Description("Defines a request to (super) admin of a private group of any member addition request(s).")]
        AddToGroup = 1041,
        [Description("Defines a request for an event.")]
        EventInvitation = 1051,
        [Description("Defines a request for a contact update."), MetaData("2015-02-09")]
        ContactUpdate = 1061
    }

    [Description("The setting codes for personal user in iLoop system.")]
    [MetaData("2014-08-14")]
    public enum SystemSettingPerson : byte
    {
        [Description("Defines setting related to message reception restriction.")]
        ReceiveMessages = 6,
        [Description("Defines setting related to friendship request restriction.")]
        ReceiveConnectionRequests = 9,
        [Description("Defines setting related to automatically accepting friendship request from mobile address book contacts.")]
        AutomaticallyAcceptRequestFromAddressBookContacts = 12,
        [Description("Defines setting related to automatically sending friendship request to mobile address book contacts.")]
        AutomaticallySendConnectionRequestToAddressBookContacts = 13,
        [Description("Defines setting related to allowing adding user into chat groups.")]
        AllowToAddMeInChatGroup = 17,
        [Description("Defines setting related to allowing others to forward user's message(s).")]
        AllowReceiversToForwardMessage = 51,
        [Description("Defines setting related to sending/receiving seen status of message.")]
        DisplaySeenStatus = 52,
        [Description("Defines setting related to receiving mobile notifications.")]
        MuteMobileNotifications = 53,
        [Description("Defines setting related to notifying any of the mobile contact's sign up.")]
        NotifyOnSignUp = 54,
        [Description("Defines setting related to number of view counts of a disposable message.")]
        ViewCount = 55,
        [Description("Defines setting related to duration of a view of a disposable message.")]
        ViewDuration = 56,
        [Description("Defines setting related to duration the disposable message setting pop-up is displayed until set to default.")]
        PopUpDisplayDuration = 57
    }

    [Description("Defines the meaning of values returned for each setting type.")]
    [MetaData("2014-08-14")]
    public enum SystemSettingValue : byte
    {
        [Description("Defines setting being allowed to nobody.")]
        NoOne = 0,

        [Description("Defines setting being allowed to everybody.")]
        Public = 1,

        [Description("Defines setting being allowed to friends only.")]
        Friends = 3,

        [Description("Defines setting being restricted to only the provided categories.")]
        Category = 4,

        [Description("Defines setting being restricted to only the provided contacts.")]
        Contact = 5,

        [Description("Defines setting being restricted to user only.")]
        Private = 6,

        [Description("Defines setting being not allowed.")]
        No = 10,

        [Description("Defines setting being allowed.")]
        Yes = 11,

        ///// <summary>
        ///// Not available at any time.
        ///// </summary>
        //NotAvailable = 20,

        ///// <summary>
        ///// Available between the provided time range.
        ///// </summary>
        //AvailableBetween = 21,

        ///// <summary>
        ///// Available at all times.
        ///// </summary>
        //Available = 22,

        ///// <summary>
        ///// Permitted to direct parent only.
        ///// </summary>
        //ImmediateParentOnly = 30,

        ///// <summary>
        ///// Permitted to all ancestors.
        ///// </summary>
        //AllParents = 31,

        [Description("Defines a numeric value for the setting.")]
        Numeric = 50
    };

    //[Description("The event response that any invited user can provide.")]
    //[MetaData("2014-07-24")]
    //public enum SystemEventResponse : byte
    //{
    //    [Description("Defines invitee's response as not attending.")]
    //    NotAttending = 0,
    //    [Description("Defines invitee's response as attending.")]
    //    Attending = 1,
    //    [Description("Defines invitee's response as may be attending.")]
    //    MayBeAttending = 2
    //}

    [Description("The code values assigned to days in a week.")]
    [MetaData("2015-01-08")]
    public enum SystemDayOfWeek : byte
    {
        [Description("Defines Sunday of the week.")]
        Sunday = 0,
        [Description("Defines Monday of the week.")]
        Monday = 1,
        [Description("Defines Tuesday of the week.")]
        Tuesday = 2,
        [Description("Defines Wednesday of the week.")]
        Wednesday = 3,
        [Description("Defines Thursday of the week.")]
        Thursday = 4,
        [Description("Defines Friday of the week.")]
        Friday = 5,
        [Description("Defines Saturday of the week.")]
        Saturday = 6
    }

    [Description("The relationship types of personal users.")]
    [MetaData("2014-07-24")]
    public enum SystemRelationshipStatus : byte
    {
        [Description("Defines the relationship status as being single.")]
        Single = 1,
        [Description("Defines the relationship status as being in a relationship.")]
        InARelationship = 2,
        [Description("Defines the relationship status as being engaged.")]
        Engaged = 3,
        [Description("Defines the relationship status as being married.")]
        Married = 4,
        [Description("Defines the relationship status as being complicated.")]
        ItIsComplicated = 5,
        [Description("Defines the relationship status as being in an open relationship.")]
        InAnOpenRelationship = 6,
        [Description("Defines the relationship status as being widowed.")]
        Widowed = 7,
        [Description("Defines the relationship status as being separated.")]
        Separated = 8,
        [Description("Defines the relationship status as being divorced.")]
        Divorced = 9
    }

    [Description("The ownership type of business user.")]
    [MetaData("2014-07-24")]
    public enum SystemOwnershipType : byte
    {
        [Description("Defines a govenment organization.")]
        Government = 1,
        [Description("Defines a non-profit organization.")]
        NonProfit = 2,
        [Description("Defines a private organization.")]
        Private = 3,
        [Description("Defines a public organization.")]
        Public = 4
    }

    [Description("The employee type of personal user.")]
    [MetaData("2014-07-24")]
    public enum SystemEmployeeType : byte
    {
        [Description("Defines the employee as a full-timer.")]
        FullTimer = 1,
        [Description("Defines the employee as a part-timer.")]
        PartTimer = 2,
        [Description("Defines the employee as a volunteer.")]
        Volunteer = 3
    }

    [Description("The employer type of business user.")]
    [MetaData("2014-07-24")]
    public enum SystemEmployerType : byte
    {
        [Description("Defines the employer as a direct employer.")]
        Direct = 1,
        [Description("Defines the employer as for an instutitional and/or educational propose.")]
        Education = 2,
        [Description("Defines the employer as an indirect employer.")]
        Indirect = 3,
        [Description("Defines the employer as an internet based online employer.")]
        Internet = 4,
        [Description("Defines the employer as a trainer employer.")]
        Training = 5
    }

    [Description("The language-proficiency level of personal user.")]
    [MetaData("2014-07-24")]
    public enum SystemProficiencyType : byte
    {
        [Description("Defines the langauage proficiency as very basic.")]
        ElementaryProficiency = 1,
        [Description("Defines the langauage proficiency as being averagely intermediate.")]
        LimitedWorkingProficiency = 2,
        [Description("Defines the langauage proficiency as being intermediately professional.")]
        ProfessionalWorkingProficiency = 3,
        [Description("Defines the langauage proficiency as full professional.")]
        FullProfessionalProficiency = 4,
        [Description("Defines the langauage proficiency as native or near to native.")]
        NativeOrBilingualProficiency = 5
    }

    [Description("The work-availability status of user.")]
    [MetaData("2014-07-24")]
    public enum SystemUserAvailabilityCode : byte
    {
        [Description("Defines user as being available.")]
        Available = 1,
        [Description("Defines user as being busy.")]
        Busy = 2,
        [Description("Defines user as being out of office.")]
        OutOfOffice = 3
    }

    [Description("Represents the selection of contacts or groups for rule selection.")]
    [MetaData("2014-11-14")]
    public enum SystemUserSelection : byte
    {
        [Description("Defines the selection of None.")]
        None = 0,
        [Description("Defines the selection of contacts.")]
        Contacts = 1,
        [Description("Defines the selection of groups.")]
        Groups = 2
    }

    [Description("The selection of rule criterion for user based off the selection of contacts for rule selection.")]
    [MetaData("2014-11-14")]
    public enum SystemRuleTypeUser : byte
    {
        [MetaData("2015-06-12")]
        [Description("Defines no-selection.")]
        None = 0,
        [Description("Defines the provided list should exactly match with targets specified for new and/or existing message instances.")]
        Matches = 1,
        [Description("Defines the provided list should be a subset of targets specified for new and/or existing message instances.")]
        Contains = 2,
        [Description("Defines the provided list should have at least one element matching the targets specified for new and/or existing message instances.")]
        Any = 3
    }

    [Description("The selection of rule criterion for subject for rule selection.")]
    [MetaData("2014-11-14")]
    public enum SystemRuleTypeSubject : byte
    {
        [Description("Defines no subject matching is needed.")]
        None = 0,
        [Description("Defines the subject to match exactly (case-insensitive) with the subject of new and/or existing message instances.")]
        Matches = 1,
        [Description("Defines the subject to be contained in the subject of new and/or existing message instances.")]
        Contains = 2
    }

    [Description("The selection of mode of operation for instance operation.")]
    [MetaData("2014-12-10")]
    public enum SystemInstanceOperation : byte
    {
        [Description("Defines a delete operation.")]
        Delete = 0,
        [Description("Defines a leave operation.")]
        Leave = 1,
        [Description("Defines a leave and delete operation.")]
        LeaveDelete = 2
    }

    //[Description("The image sizes supported by the system. This is deprecated and will be removed.")]
    //[MetaData("2014-07-24")]
    //public enum SystemImageSize : byte
    //{
    //    [Description("Defines a 40 * 40 sized image")]
    //    Size40 = 40,
    //    [Description("Defines a 56 * 56 sized image")]
    //    Size56 = 56,
    //    [Description("Defines a 94 * 94 sized image")]
    //    Size94 = 94,
    //    [Description("Defines a 170 * 170 sized image")]
    //    Size170 = 170
    //}

    [Description("The image sizes supported by the system.")]
    [MetaData("2015-01-21")]
    public enum SystemImageSizeCode : byte
    {
        [Description("Defines a 40 * 40 sized image.")]
        FortyForty = 0,
        [Description("Defines a 46 * 46 sized image.")]
        FiftysixFiftysix = 1,
        [Description("Defines a 94 * 94 sized image.")]
        NinetyfourNinetyfour = 2,
        [Description("Defines a 170 * 170 sized image.")]
        OneseventyOneseventy = 3,
        [Description("Defines a 50 * 50 sized image.")]
        FiftyFifty = 4,
        [Description("Defines a 75 * 75 sized image.")]
        SeventyfiveSeventyfive = 5,
        [Description("Defines a 100 * 100 sized image.")]
        HundredHundred = 6,
        [Description("Defines a 150 * 150 sized image.")]
        OnefiftyOnefifty = 7,
        [Description("Defines a 120 * 120 sized image.")]
        OnetwentyOnetwenty = 8,
        [Description("Defines a 180 * 180 sized image.")]
        OneeightyOneeighty = 9,
        [Description("Defines a 240 * 240 sized image.")]
        TwofortyTwoforty = 10,
        [Description("Defines a 360 * 360 sized image.")]
        ThreesixtyThreesixty = 11,
        [Description("Defines a 200 * 200 sized image.")]
        TwohundredTwohundred = 12,
        [Description("Defines a 300 * 300 sized image.")]
        ThreehundredThreehundred = 13,
        [Description("Defines a 400 * 400 sized image.")]
        FourhundredFourhundred = 14,
        [Description("Defines a 600 * 600 sized image.")]
        SixhundredSixhundred = 15,
        [Description("Defines the original image.")]
        Original = 16
    }

    [Description("The type of work schedule.")]
    [MetaData("2015-01-27")]
    public enum SystemWorkSchedule : byte
    {
        [Description("Defines work schedule to be different for each day in a week.")]
        Flexible = 0,
        [Description("Defines work schedule to be fixed for each day in a week.")]
        Fixed = 1
    }

    [Description("The type marker representing the type for a single contact field.")]
    [MetaData("2015-02-24")]
    public enum SystemContactMarkedType : byte
    {
        [Description("Defines a custom field.")]
        Custom = 0,
        [Description("Defines an AIM chat network field type.")]
        Aim = 1,
        [Description("Defines a Google talk chat network field type.")]
        GoogleTalk = 2,
        [Description("Defines an ICQ chat network field type.")]
        Icq = 3,
        [Description("Defines a Jabbar chat network field type.")]
        Jabber = 4,
        [Description("Defines an MSN chat network field type.")]
        Msn = 5,
        [Description("Defines a Net Meeting chat network field type.")]
        NetMeeting = 6,
        [Description("Defines a QQ chat network field type.")]
        Qq = 7,
        [Description("Defines a Skype chat network field type.")]
        Skype = 8,
        [Description("Defines a Yahoo! chat network field type.")]
        Yahoo = 9,
        [Description("Defines an address field type.")]
        Address = 101,
        [Description("Defines an email field type.")]
        Email = 102,
        [Description("Defines a fax field type.")]
        Fax = 103,
        [Description("Defines a mobile field type.")]
        Mobile = 104,
        [Description("Defines a telephone field type.")]
        Phone = 105,
        [Description("Defines a web address field type.")]
        Website = 106,
        [Description("Defines a company user ID field type.")]
        CompanyUserId = 107
    }

    [Description("The approval status of any request.")]
    [MetaData("2015-05-19")]
    public enum SystemApprovalStatus : byte
    {
        [Description("Defines a request currently not responded to.")]
        Pending = 0,
        [Description("Defines an approved request.")]
        Approved = 1,
        [Description("Defines a requested that has been rejected.")]
        Rejected = 2
    }

    [Description("The profile view type.")]
    [MetaData("2015-06-10", markType: 1)]
    public enum SystemProfileViewType : byte
    {
        [Description("Defines a direct profile view.")]
        Direct = 1,
        [Description("Defines an advanced search was done based on position resulting in the profile as a match.")]
        Positional = 2,
        [Description("Defines an advanced search was done based on skill resulting in the profile as a match.")]
        Skill = 3
    }

    [Description("The reception mode for user status when user is out of office.")]
    [MetaData("2015-06-18")]
    public enum SystemStatusReceptionMode : byte
    {
        [Description("Defines a request to not be disturbed.")]
        DoNotDisturb = 0,
        [Description("Defines a request to be contacted to self.")]
        Self = 1,
        [Description("Defines a delegation request to another user. Defines the delegation being for messages only.")]
        ContactMessageOnly = 2,
        [Description("Defines a delegation request to another user. Defines the delegation being for calls only.")]
        ContactCallOnly = 3,
        [Description("Defines a delegation request to another user. Defines the delegation being for both calls and messages.")]
        ContactCallAndMessage = 4
    }

    #region Old Codes
    ///// <summary>
    ///// Represents how persistent layer should handle insert/update/delete the request.
    ///// </summary>
    //public struct SystemDbRequestType
    //{
    //    /// <summary>
    //    /// Data will be inserted and/or updated without affecting other data.
    //    /// </summary>
    //    public static string Post { get { return "UPDATED"; } }
    //    /// <summary>
    //    /// Data will be inserted by first flushing existing data.
    //    /// </summary>
    //    public static string Put { get { return "FLUSHED"; } }
    //    /// <summary>
    //    /// Data will be deleted that matches with the provided one(s).
    //    /// </summary>
    //    public static string Delete { get { return "DELETED"; } }
    //    ///// <summary>
    //    ///// Data provided will perform a partial update.
    //    ///// </summary>
    //    //public static string Patch { get { return "PATCHED"; } }

    //    /// <summary>
    //    /// Generates corresponding name to be passed to persistent storage based on passed HTTP method.
    //    /// </summary>
    //    /// <param name="method">The HTTP method.</param>
    //    /// <returns>Name that persistent storage requires for processing the request.</returns>
    //    public static string GetRequest(HttpMethod method)
    //    {
    //        if (method == HttpMethod.Post)
    //            return Post;
    //        if (method == HttpMethod.Put)
    //            return Put;
    //        if (method == HttpMethod.Delete)
    //            return Delete;

    //        return string.Empty;
    //    }
    //}
    #endregion
}
