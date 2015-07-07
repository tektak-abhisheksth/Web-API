using TekTak.iLoop.Setting;

namespace TekTak.iLoop.Sealed.Setting
{
    public sealed class SettingRepositorySealed : SettingRepository, ISettingRepositorySealed
    {
        public SettingRepositorySealed(Services client)
            : base(client)
        { }
    }
}
