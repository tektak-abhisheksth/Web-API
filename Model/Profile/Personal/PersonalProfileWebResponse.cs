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
    #region Basic Info
    public class BasicInformationWeb : BasicInformation
    {
        [Description("The relationship status of the personal person.")]
        public SystemRelationshipStatus RelationshipStatusId { get; set; }

        [Description("The gender of the personal user.")]
        public SystemGender Gender { get; set; }

        [Description("The ID and name of the religion.")]
        public GeneralKvPair<int, string> Religion { get; set; }

        [Description("Interests of the personal user.")]
        public string Interests { get; set; }

        [Description("The ID and name of the nationality.")]
        public GeneralKvPair<int, string> Nationality { get; set; }

        [Description("The day and month of date of birth of the personal user (ignore current year).")]
        public DateTime DateOfBirthDayAndMonth { get; set; }

        [Description("The year of date of birth of the personal user.")]
        public int? DateOfBirthYear { get; set; }
    }
    #endregion

    #region Education history
    public class EducationViewResponse
    {
        [Description("A unique identifier uniquely representing the entry.")]
        public long AcademicInstituteId { get; set; }

        [IgnoreDataMember]
        [Description("The position of the entry in the list.")]
        public int DisplayOrderId { get; set; }

        [Description("The ID and name of the academic institute.")]
        public GeneralKvPair<int, string> AcademicInstitute { get; set; }

        [Description("The location information.")]
        public UserCity Location { get; set; }

        [Description("The concentration of the academic education.")]
        public GeneralKvPair<int, string> Concentration { get; set; }

        [Description("The year of enrollment.")]
        public int JoinedYear { get; set; }

        [Description("The passed out year.")]
        public int? GraduatedYear { get; set; }

        [Description("An indication as to whether or not the personal user has completed his course.")]
        public bool HasGraduated { get; set; }

        [Description("The degree ID and name.")]
        public GeneralKvPair<int, string> Degree { get; set; }
    }
    #endregion

    #region Award and honour history
    public class AwardAndHonorResponse
    {
        [Description("A unique identifier uniquely representing the entry.")]
        public long AwardAndHonorId { get; set; }

        [Description("The title of the award.")]
        public string Title { get; set; }

        [Description("The person or organization who issued the award.")]
        public string Issuer { get; set; }

        [Description("The date the award was entitled.")]
        public DateTime? AwardedDate { get; set; }

        [Description("The details of the award.")]
        public string Description { get; set; }

        [Description("The picture related to the award.")]
        public string Picture { get; set; }
    }
    #endregion

    #region Language
    public class LanguageResponse
    {
        [Description("The language ID and name.")]
        public GeneralKvPair<int, string> UserLanguage { get; set; }

        [Description("The proficiency level for the language.")]
        public int? ProficiencyId { get; set; }
        // public GeneralKvPair<byte, string> Proficiency { get; set; }
    }
    #endregion

    #region Skill
    public class ThumbsForSkillResponse
    {
        [Description("The count of thumb ups received.")]
        public int ThumbsUp { get; set; }

        [Description("The count of thumb downs received.")]
        public int ThumbsDown { get; set; }

        [Description("Your thumbs up/down.")]
        public int YourThumb { get; set; }
    }

    public class ThumbsForSkillDetailResponse
    {
        [Description("The user information.")]
        public UserResponse User { get; set; }

        [Description("The user's thumb up/down.")]
        public int UporDown { get; set; }
    }

    public class UserSkillResponse
    {
        [Description("A unique primary key representing a skill.")]
        public int SkillTypeId { get; set; }

        [Description("The skill name.")]
        public string Name { get; set; }

        [Description("The count of thumbs received.")]
        public int ThumbsCount { get; set; }

        [Description("An indication as to whether or not current user has given thumbs.")]
        public bool YourThumb { get; set; }

        [Description("Users who have provided thumbs for the skill.")]
        public IEnumerable<UserInformationBaseExtendedResponse> Users { get; set; }

        [IgnoreDataMember]
        public string UsersString { get; set; }
    }

    public class UnApprovedSkillSuggestionResponse
    {
        [Description("A GUID uniquely representing the skill, to associate with thumb providers.")]
        public Guid SkillGuid { get; set; }

        [Description("A unique primary key representing a skill.")]
        public int SkillTypeId { get; set; }

        [Description("The skill name.")]
        public string Skill { get; set; }

        [Description("The user information.")]
        public UserResponse User { get; set; }
    }
    #endregion

    #region EmploymentHistory
    public class EmploymentWebResponse
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
    #endregion
}
