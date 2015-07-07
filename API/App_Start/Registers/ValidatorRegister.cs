using API.Validators;
using Autofac;
using FluentValidation;
using Model.Category;
using Model.Friend;
using Model.Inbox;
using Model.Profile;
using Model.Profile.Personal;
using Model.Setting;

namespace API.Registers
{
    public static class ValidatorRegister
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<AddAcademicRequestValidator>().As<IValidator<AddAcademicRequest>>();
            builder.RegisterType<ContactRequestBaseValidator>().As<IValidator<ContactRequest>>().As<IValidator<ContactRequestBase>>();
            builder.RegisterType<TemporaryContactRequestValidator>().As<IValidator<TemporaryContactRequest>>();
            builder.RegisterType<StatusSetRequestValidator>().As<IValidator<StatusSetRequest>>();
            builder.RegisterType<AddEmployeeRequestValidator>().As<IValidator<AddEmployeeRequest>>();
            builder.RegisterType<WorkScheduleValidator>().As<IValidator<WorkSchedule>>();
            builder.RegisterType<CopyCategoryValidator>().As<IValidator<CopyCategory>>();
            builder.RegisterType<CategoryFriendsValidator>().As<IValidator<CategoryFriends>>();
            builder.RegisterType<InboxRequestValidator>().As<IValidator<InboxRequest>>();
            builder.RegisterType<RuleAddRequestValidator>().As<IValidator<RuleAddRequest>>();
            builder.RegisterType<RuleUpdateRequestValidator>().As<IValidator<RuleUpdateRequest>>();
            builder.RegisterType<FriendshipRequestValidator>().As<IValidator<FriendshipRequest>>();
            builder.RegisterType<UserSettingRequestValidator>().As<IValidator<UserSettingRequest>>();
        }
    }
}