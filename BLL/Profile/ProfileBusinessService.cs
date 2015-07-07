using System.Threading.Tasks;
using DAL.DbEntity;
using Model.Common;
using Model.Profile.Business;
using Model.Types;

namespace BLL.Profile
{
    public class ProfileBusinessService : IProfileBusinessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TekTak.iLoop.UOW.IUnitOfWork _jUnitOfWork;

        public ProfileBusinessService(IUnitOfWork unitOfWork, TekTak.iLoop.UOW.IUnitOfWork jUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jUnitOfWork = jUnitOfWork;
        }

        public Task<StatusData<UpsertCompanyEmployeeResponse>> UpsertCompanyEmployee(UpsertCompanyEmployeeRequest request, byte mode, SystemSession session)
        {
            return _jUnitOfWork.ProfileBusiness.UpsertCompanyEmployee(request, mode, session);
        }
    }
}
