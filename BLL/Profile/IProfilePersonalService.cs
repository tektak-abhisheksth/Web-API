using Model.Common;
using Model.Profile.Personal;
using Model.Types;
using System.Threading.Tasks;

namespace BLL.Profile
{
    public partial interface IProfilePersonalService
    {
        Task<StatusData<string>> UpdateBasicContactPerson(BasicContactPersonWebRequest request, SystemSession session);
    }
}
