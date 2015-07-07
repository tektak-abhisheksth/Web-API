using BLL.Profile;
using System.Web.Http;
using Model.Attribute;

namespace API.Controllers.Profile
{
    /// <summary>
    /// Provides APIs to handle requests related to personal user profiles.
    /// </summary>
    // [ApiExplorerSettings(IgnoreApi = true)]
    [MetaData]
    public partial class ProfilePersonalController : ApiController
    {
        private readonly IProfilePersonalService _profilePersonalService;

        /// <summary>
        /// Provides APIs to handle requests related to personal user profiles.
        /// </summary>
        /// <param name="profilePersonalService"></param>
        public ProfilePersonalController(IProfilePersonalService profilePersonalService)
        {
            _profilePersonalService = profilePersonalService;
        }
    }
}
