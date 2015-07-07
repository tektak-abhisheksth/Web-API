using Model.Attribute;
using Model.Base;
using Model.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Profile.Personal
{
    public class BasicContactPersonWebRequest : BasicContactPersonRequest
    {
        [Range(typeof(DateTime), "1/1/1917", "1/1/2017")]
        [Description("The birth date of the user.")]
        public DateTime? BirthDate { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Description("The religion of the user.")]
        public string Religion { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Description("The nationality of the user.")]
        public string Nationality { get; set; }

        [StringLength(2000, MinimumLength = 3)]
        [Description("The interests of the user.")]
        public string Interests { get; set; }

        [Required, ValidEnum]
        [Description("The relationship status of the user.")]
        public SystemRelationshipStatus RelationshipStatusId { get; set; }

        [Required, ValidEnum]
        [Description("The gender of the user.")]
        public SystemGender Gender { get; set; }
    }

    public class AddAcademicRequest : RequestBase
    {
        [Required, StringLength(200)]
        [Description("The name of the academic institute.")]
        public string AcademicInstitute { get; set; }

        [Description("The city ID.")]
        public int CityId { get; set; }

        [Required, Range(1900, 2017)]
        [Description("The year of enrollment.")]
        public int JoinedYear { get; set; }

        [Range(1900, 2022)]
        [Description("The passed out year.")]
        public int? GraduatedYear { get; set; }

        [Required]
        [Description("An indication as to whether or not the personal user has completed his course.")]
        public bool HasGraduated { get; set; }

        [Required, StringLength(100, MinimumLength = 5)]
        [Description("The concentration of the academic education.")]
        public string Concentration { get; set; }
    }

    public class UpdateAcademicRequest : AddAcademicRequest
    {
        [Required, Description("A unique identifier uniquely representing the entry.")]
        public long AcademicId { get; set; }
    }

    public class LanguageRequest
    {
        [Range(1, 5)]
        [Description("The proficiency level for the language.")]
        public SystemProficiencyType? ProficiencyId { get; set; }

        [Required, StringLength(30), RegularExpression(@"^[a-zA-Z]+[\S]$")]
        [Description("The name of the language.")]
        public string Language { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Language, ProficiencyId.GetValueOrDefault());
        }
    }

    public class AddAwardAndHonorRequest : RequestBase
    {
        [Required, StringLength(50)]
        [Description("The title of the award.")]
        public string Title { get; set; }

        [StringLength(50)]
        [Description("The person or organization who issued the award.")]
        public string Issuer { get; set; }

        [Required, Description("The date the award was entitled.")]
        public DateTime? Date { get; set; }

        [StringLength(1000)]
        [Description("The details of the award.")]
        public string Description { get; set; }
    }

    public class UpdateAwardAndHonorRequest : AddAwardAndHonorRequest
    {
        [Required, Range(1, long.MaxValue)]
        [Description("A unique identifier uniquely representing the entry.")]
        public long AwardAndHonorId { get; set; }
    }

    public class SkillRequest : RequestBase
    {
        [Required]
        [Description("List of skills.")]
        public List<string> Skills { get; set; }
        //[Required]
        //public int ProfileUserId { get; set; }
    }

    //public class UpsertSkillRequest : SkillRequest
    //{
    //    public int TargetUserId { get; set; }
    //}

    public class SkillSuggestionRequest : SkillRequest
    {
        [Required]
        [Description("The user ID or user name of the suggestor.")]
        public string Suggestor { get; set; }
    }

    public class SkillAcceptanceRequest : SkillRequest
    {
        [Required]
        [Description("An indication as to whether the skill suggestion was accepted or rejected.")]
        public bool IsAccepted { get; set; }

        [Required]
        [Description("The user ID or user name of the suggestor.")]
        public string Suggestor { get; set; }
    }

    public class ThumbsForSkillRequest : RequestBase
    {
        [Required]
        [Description("The user ID or user name of the target user.")]
        public string ProfileUser { get; set; }

        [Required]
        [Description("A unique identifier uniquely representing the entry.")]
        public int SkillTypeId { get; set; }

        [Required]
        [Description("The user's thumb.")]
        public int ThumbsUporDown { get; set; }
    }

    public class ThumbsForSkillDetailRequest
    {
        [Required]
        [Description("The user ID or user name of the target user.")]
        public string TargetUser { get; set; }

        [Required]
        [Description("A unique identifier uniquely representing the entry.")]
        public int SkillTypeId { get; set; }
    }
}
