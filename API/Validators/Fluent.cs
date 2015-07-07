using FluentValidation;
using Model.Category;
using Model.Friend;
using Model.Inbox;
using Model.Profile;
using Model.Profile.Personal;
using Model.Setting;
using Model.Types;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Utility;

namespace API.Validators
{
    #region Profile
    public class AddAcademicRequestValidator : AbstractValidator<AddAcademicRequest>
    {
        public AddAcademicRequestValidator()
        {
            RuleFor(x => x.GraduatedYear)
                .GreaterThanOrEqualTo(x => x.JoinedYear)
                .When(x => x.GraduatedYear.HasValue);
        }
    }

    public class ContactRequestBaseValidator : AbstractValidator<ContactRequestBase>
    {
        public ContactRequestBaseValidator()
        {
            RuleFor(x => x.TemporaryContact)
                .SetValidator(new TemporaryContactRequestValidator());
        }
    }

    public class TemporaryContactRequestValidator : AbstractValidator<TemporaryContactRequest>
    {
        public TemporaryContactRequestValidator()
        {
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate);
        }
    }

    public class StatusSetRequestValidator : AbstractValidator<StatusSetRequest>
    {
        public StatusSetRequestValidator()
        {
            When(x => x.StatusTypeId == SystemUserAvailabilityCode.OutOfOffice, () =>
            {
                RuleFor(x => x.OutOfOfficeRequest)
                    .NotNull().WithMessage("Out of office details must be provided when status is set as out of office.")
                    .DependentRules(
                    d => d.When(x => x.OutOfOfficeRequest.BeginDateAndTime != null && x.OutOfOfficeRequest.EndDateAndTime != null,
                    () =>
                    {
                        RuleFor(x => x.OutOfOfficeRequest.EndDateAndTime.Date)
                            .GreaterThanOrEqualTo(x => x.OutOfOfficeRequest.BeginDateAndTime.Date)
                            .When(x => x.OutOfOfficeRequest.BeginDateAndTime.Date.HasValue && x.OutOfOfficeRequest.EndDateAndTime.Date.HasValue)
                            .DependentRules(
                            di => di.When(x => x.OutOfOfficeRequest.BeginDateAndTime.Date == x.OutOfOfficeRequest.EndDateAndTime.Date,
                            () =>
                            {
                                RuleFor(x => x.OutOfOfficeRequest.EndDateAndTime.Time)
                                    .GreaterThan(x => x.OutOfOfficeRequest.BeginDateAndTime.Time)
                                    .When(x => x.OutOfOfficeRequest.BeginDateAndTime.Time.HasValue && x.OutOfOfficeRequest.EndDateAndTime.Time.HasValue);
                            }));
                    }))
                    .DependentRules(
                    da => da.RuleFor(x => x.OutOfOfficeRequest.AssigneeUserId)
                        .NotNull().WithMessage("Assignee must be specified when reception mode is set to delegate either or both call and message.")
                        .When(x => (int)x.OutOfOfficeRequest.ReceptionMode > 1));
            });
        }
    }

    public class AddEmployeeRequestValidator : AbstractValidator<AddEmployeeRequest>
    {
        public AddEmployeeRequestValidator()
        {
            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .When(x => x.EndDate.HasValue)
                .DependentRules(
                d => d.RuleFor(x => x.WorkSchedule.Schedules)
                .SetCollectionValidator(new WorkScheduleValidator())
                .When(x => x.WorkSchedule != null && x.WorkSchedule.Schedules != null));
        }
    }

    public class WorkScheduleValidator : AbstractValidator<WorkSchedule>
    {
        public WorkScheduleValidator()
        {
            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime);
        }
    }
    #endregion

    #region Category
    public class CopyCategoryValidator : AbstractValidator<CopyCategory>
    {
        public CopyCategoryValidator()
        {
            RuleFor(x => x.TargetCategories)
                .NotNull()
                .Must(x => x.Any()).WithMessage("Target categories cannot be empty.")
                .Must(x => x.All(y => y > 0)).WithMessage("Invalid category specified.")
            .DependentRules(
            d => d.RuleFor(x => x.Friends)
                .NotNull()
                .Must(x => x.Any()).WithMessage("Friends cannot be empty.")
                .Must(x => x.All(y => y > 0)).WithMessage("Invalid friend specified."))
            .DependentRules(
            d => d.RuleFor(x => x)
                .Must(x => !x.Friends.Contains(x.UserId)).WithMessage("Invalid entry in friend list."));
        }
    }

    public class CategoryFriendsValidator : AbstractValidator<CategoryFriends>
    {
        public CategoryFriendsValidator()
        {
            RuleFor(x => x.Friends)
                .NotNull()
                .Must(x => x.Any()).WithMessage("Friends cannot be empty.")
                .DependentRules(
                d => d.RuleFor(x => x)
                    .Must(x => !x.Friends.Contains(x.UserId)).WithMessage("Invalid entry in friend list."));
        }
    }
    #endregion

    #region Friend
    public class FriendshipRequestValidator : AbstractValidator<FriendshipRequest>
    {
        public FriendshipRequestValidator()
        {
            RuleFor(x => x.FriendId)
                .Matches(RegexPattern.UserName).WithMessage("Invalid friend name.")
                .Must(x => x.Length >= 6 && x.Length <= 30).WithMessage("Length of the friend name must be between 6 and 30.")
                .When(x => !Regex.IsMatch(x.FriendId, RegexPattern.Numeric))
                .DependentRules(
                d => d.RuleFor(x => x.FriendId)
                    .Matches(RegexPattern.Numeric).WithMessage("Invalid friend ID specified.")
                    .When(x => !Regex.IsMatch(x.FriendId, RegexPattern.UserName)));
        }
    }
    #endregion


    #region Inbox
    public class InboxRequestValidator : AbstractValidator<InboxRequest>
    {
        public InboxRequestValidator()
        {
            When((x => x.Rule != null && x.Rule.Any()), () =>
            {
                RuleFor(x => x.Rule)
                    .Must(x => x.Count == x.Distinct().Count()).WithMessage("Duplicate rules found.")
                    .SetCollectionValidator(new InboxRuleRequestValidator())
                    .DependentRules(
                    d => d.RuleForEach(x => x.Rule)
                    .Must((rules, rule) => new[] { SystemRuleTypeUser.Matches, SystemRuleTypeUser.Contains, SystemRuleTypeUser.Any }.Contains(rule.RuleTypeUser)));
            });
        }
    }

    public class RuleAddRequestValidator : AbstractValidator<RuleAddRequest>
    {
        public RuleAddRequestValidator()
        {
            When(x => x.Rule != null, () =>
            {
                RuleFor(x => x)
                    .Must(x => x.Rule.UserSelection != SystemUserSelection.None || x.Rule.RuleTypeSubject != SystemRuleTypeSubject.None).WithMessage("Empty rule is not allowed.").DependentRules(
                    d => d.RuleFor(x => x.Rule)
                        .SetValidator(new InboxRuleRequestValidator()));
            });
        }
    }

    public class RuleUpdateRequestValidator : RuleAddRequestValidator
    { }

    public class InboxRuleRequestValidator : AbstractValidator<InboxRuleRequest>
    {
        public InboxRuleRequestValidator()
        {
            When(x => x.UserSelection == SystemUserSelection.Contacts, () =>
            {
                RuleFor(x => x)
                    .Must(x => new[] { SystemRuleTypeUser.Matches, SystemRuleTypeUser.Contains, SystemRuleTypeUser.Any }.Contains(x.RuleTypeUser)).WithMessage("Invalid rule type user specified.")
                    .DependentRules(
                    d => RuleFor(x => x.GroupList)
                        .Must(x => x == null || !x.Any()).WithMessage("Group(s) not expected."));
            });
            When(x => x.UserSelection == SystemUserSelection.Groups, () =>
            {
                RuleFor(x => x.ContactList)
                    .Must(x => x == null || !x.Any()).WithMessage("Contact(s) not expected.");
            });
        }
    }
    #endregion

    #region Setting
    public class UserSettingRequestValidator : AbstractValidator<UserSettingRequest>
    {
        public UserSettingRequestValidator()
        {
            RuleFor(x => x)
                .Must(x => new[] { SystemSettingValue.NoOne, SystemSettingValue.Friends, SystemSettingValue.Category, SystemSettingValue.Contact }.Select(e => Convert.ToInt32(e)).Contains(x.Value)).WithMessage("Invalid value specified.")
                .When(x => x.SettingGroupId == 1)
                .DependentRules(
            d => d.RuleFor(x => x)
                .Must(x => new[] { SystemSettingValue.NoOne, SystemSettingValue.Public, SystemSettingValue.Friends, SystemSettingValue.Category, SystemSettingValue.Contact }.Select(e => Convert.ToInt32(e)).Contains(x.Value)).WithMessage("Invalid value specified.")
                .When(x => x.SettingGroupId == 2))
                .DependentRules(
            d => d.RuleFor(x => x)
                .Must(x => new[] { SystemSettingValue.Public, SystemSettingValue.Friends, SystemSettingValue.Category, SystemSettingValue.Contact, SystemSettingValue.Private }.Select(e => Convert.ToInt32(e)).Contains(x.Value)).WithMessage("Invalid value specified.")
                .When(x => x.SettingGroupId == 3))
                .DependentRules(
            d => d.RuleFor(x => x)
                .Must(x => new[] { SystemSettingValue.Category, SystemSettingValue.Contact }.Select(e => Convert.ToInt32(e)).Contains(x.Value)).WithMessage("Invalid value specified.")
                .When(x => x.SettingGroupId == 4))
                .DependentRules(
            d => d.RuleFor(x => x)
                .Must(x => new[] { SystemSettingValue.No, SystemSettingValue.Yes }.Select(e => Convert.ToInt32(e)).Contains(x.Value)).WithMessage("Invalid value specified.")
                .When(x => x.SettingGroupId == 10))
                .DependentRules(
            d => d.RuleFor(x => x)
                .Must(x => new[] { 20, 21, 22 }.Contains(x.Value)).WithMessage("Invalid value specified.")
                .When(x => x.SettingGroupId == 20))
                 .DependentRules(
            d => d.When(x => x.SettingGroupId == 50, () =>
            {
                RuleFor(x => x)
                    .Must(x => new[] { 1, 2, 3 }.Contains(x.Value)).WithMessage("Invalid value specified.")
                    .When(x => x.SettingTypeId == (int)SystemSettingPerson.ViewCount)
                    .DependentRules(
                    di => di.RuleFor(x => x)
                        .Must(x => Enumerable.Range(10, 120).Contains(x.Value)).WithMessage("Invalid value specified.")
                    .When(x => x.SettingTypeId == (int)SystemSettingPerson.ViewDuration))
                    .DependentRules(
                    di => di.RuleFor(x => x)
                        .Must(x => Enumerable.Range(0, 10).Contains(x.Value)).WithMessage("Invalid value specified.")
                    .When(x => x.SettingTypeId == (int)SystemSettingPerson.PopUpDisplayDuration));
            }));
        }
    }
    #endregion

    #region Old coldes
    //private IEnumerable<string> AllRules()
    //    {
    //        var rules = new List<string>();
    //        var descriptor = CreateDescriptor();
    //        foreach (var member in descriptor.GetMembersWithValidators())
    //            foreach (var validationRule in descriptor.GetRulesForMember(member.Key))
    //            {
    //                var rule = (PropertyRule)validationRule; // cast 
    //                foreach (var validator in rule.Validators)
    //                    rules.Add(string.Format("validator:{0} | display:{1} | property:{2} | member:{3} | expression:{4}",
    //                                            validator.ToString().Replace("FluentValidation.Validators.", ""),
    //                                            rule.PropertyName,
    //                                            rule.GetDisplayName(),
    //                                            rule.Member,
    //                                            rule.Expression));
    //            }
    //        return rules;
    //    }
    #endregion
}
