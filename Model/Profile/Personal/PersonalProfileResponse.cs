using Model.Base;
using Model.Common;
using Model.CountryInfo;
using Model.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Model.Profile.Personal
{
    #region For Offline pull of all profiles
    public class UserInformation
    {
        [Description("The unique user ID of the user.")]
        public int UserId { get; set; }

        [Description("The first name of the user.")]
        public string FirstName { get; set; }

        [Description("The last name of the user.")]
        public string LastName { get; set; }

        [Description("Any additional information about the user.")]
        public string Title { get; set; }

        [Description("The friendship status with the user.")]
        public SystemFriendshipStatus FriendShipStatus { get; set; }

        [Description("The mobile number of the user.")]
        public string PrimaryContactNumber { get; set; }

        [Description("The last profile updated timestamp of the user.")]
        public long LastUpdated { get; set; }
    }

    public class ProfileOfflineResponse
    {
        [Description("The basic information about the user.")]
        public UserInformation BasicInformation { get; set; }

        [Description("The contact information of the user.")]
        public Contact Contacts { get; set; }
    }
    #endregion

    public class PersonalProfileResponse
    {
        [Description("The basic information about the user.")]
        public BasicInformation BasicInformation { get; set; }

        [Description("The contact information of the user.")]
        public Contact Contacts { get; set; }

        [Description("Information about employment history of the user.")]
        public IEnumerable<EmploymentHistoryResponse> EmploymentHistory { get; set; }
    }

    #region Basic information
    public class BasicInformation : UserInformationBaseExtendedResponse
    {
        [Description("The current work-availability status of the user.")]
        public SystemUserAvailabilityCode AvailableStatus { get; set; }

        [Description("The friendship status with the user.")]
        public SystemFriendshipStatus FriendShipStatus { get; set; }

        [Description("The mobile number of the user.")]
        public string PrimaryContactNumber { get; set; }

        [Description("The email address of the user.")]
        public string Email { get; set; }

        [Description("The profile information updated date of the user.")]
        public long LastUpdated { get; set; }
    }
    #endregion

    #region Employment history
    public class EmploymentHistoryResponse
    {
        [Description("A unique identifier uniquely representing the entry.")]
        public long PersonEmploymentId { get; set; }

        [Description("The company's unique user name.")]
        public string CompanyUserName { get; set; }

        [Description("The user ID and display name of the company.")]
        public GeneralKvPair<int?, string> Company { get; set; }

        [Description("The position ID and position name of the user in the company.")]
        public GeneralKvPair<int?, string> Position { get; set; }

        [Description("The location information.")]
        public UserCity Location { get; set; }

        [Description("The employment period.")]
        public BeginEndDate EmploymentDate { get; set; }

        [Description("The employee's employement type.")]
        public SystemEmployeeType EmployeeType { get; set; }

        //[Description("The name of employee type.")]
        //public string EmployeeTypeName { get; set; }

        [Description("An indication as to whether or not the employment information has been approved by the target company, if any.")]
        public bool IsApproved { get; set; }

        [Description("The picture related to the employment/company.")]
        public string Picture { get; set; }

        [IgnoreDataMember]
        [Description("An indication as to whether the work schedule is flexible or fixed.")]
        public SystemWorkSchedule IsFlexibleWorkingSchedule { get; set; }

        [Description("The fixed work day schedules.")]
        public WorkingDayFixed WorkDayFixed { get; set; }

        [Description("The flexible work day schedules.")]
        public List<WorkingDayFlexible> WorkDayFlexible { get; set; }
    }

    //public class EmployeeHistoryResponse
    //{
    //    [Description("A unique identifier uniquely representing the entry.")]
    //    public long PersonEmploymentId { get; set; }

    //    [Description("A unique identifier uniquely representing the request.")]
    //    public long RequestId { get; set; }
    //}

    public class EmployeeWorkScheduleResponse
    {
        [Description("The employee type.")]
        public SystemEmployeeType EmployeeType { get; set; }

        [Description("The day of work.")]
        public SystemDayOfWeek Day { get; set; }

        [Description("The start time.")]
        public TimeSpan StartTime { get; set; }

        [Description("The end time.")]
        public TimeSpan EndTime { get; set; }

        [Description("The type of work schedule.")]
        public SystemWorkSchedule ScheduleType { get; set; }
    }

    public class WorkingDayFixed
    {
        public WorkingDayFixed()
        {
            //WeekDay = Enumerable.Range(0, 7).Select(x => (SystemDayOfWeek)x).ToList();
            WeekDay = new List<SystemDayOfWeek>();
        }

        [Description("The time details.")]
        public WorkingTime WorkTime { get; set; }

        [Description("The days of work.")]
        public List<SystemDayOfWeek> WeekDay { get; set; }
    }

    public class WorkingTime
    {
        [Description("The start time.")]
        public string From { get; set; }

        [Description("The end time.")]
        public string To { get; set; }
    }

    public class WorkingDayFlexible
    {
        [Description("The day of work.")]
        public SystemDayOfWeek WeekDay { get; set; }

        [Description("The time details.")]
        public WorkingTime WorkTime { get; set; }
    }
    #endregion

    public class BaseInfoResponse
    {
        [Description("The unique user ID of the user.")]
        public int UserId { get; set; }

        [Description("The unique login user name.")]
        public string UserName { get; set; }

        [Description("The type ID specifying the type of the user.")]
        public SystemUserType UserTypeId { get; set; }

        [Description("The first name of the user.")]
        public string FirstName { get; set; }

        [Description("The last name of the user.")]
        public string LastName { get; set; }

        [Description("The title of the user.")]
        public string Title { get; set; }

        [Description("The email of the user.")]
        public string Email { get; set; }

        [Description("The picture of the user.")]
        public string Picture { get; set; }

        //[Description("The friendship status with the user.")]
        //public SystemFriendshipStatus FriendShipStatus { get; set; }
    }

}
