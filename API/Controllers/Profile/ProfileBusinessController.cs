using BLL.Profile;
using System.Web.Http;
using Model.Attribute;

namespace API.Controllers.Profile
{
    /// <summary>
    /// Provides APIs to handle requests related to business user profiles.
    /// </summary>
    // [ApiExplorerSettings(IgnoreApi = true)]
    [MetaData]
    public partial class ProfileBusinessController : ApiController
    {
        private readonly IProfileBusinessService _profileBusinessService;

        /// <summary>
        /// Provides APIs to handle requests related to business user profiles.
        /// </summary>
        /// <param name="profileBusinessService"></param>
        public ProfileBusinessController(IProfileBusinessService profileBusinessService)
        {
            _profileBusinessService = profileBusinessService;
        }
    }
}
