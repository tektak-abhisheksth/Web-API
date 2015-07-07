using DAL.DbEntity;
using Model.Category;
using Model.Common;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Category
{

    public partial class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TekTak.iLoop.UOW.IUnitOfWork _jUnitOfWork;

        public CategoryService(IUnitOfWork unitOfWork, TekTak.iLoop.UOW.IUnitOfWork jUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jUnitOfWork = jUnitOfWork;
        }

        public Task<IEnumerable<CategoryResponse>> GetUserCategoryList(SystemSession session)
        {
            //return _unitOfWork.Category.GetUserCategoryList(session.UserId);
            return _jUnitOfWork.Category.GetUserCategoryList(session);
        }

        public Task<StatusData<byte?>> InsertCategory(CategoryAddRequest request, SystemSession session)
        {
            return _jUnitOfWork.Category.InsertCategory(request, session);
        }

        public Task<StatusData<string>> UpdateCategory(CategoryUpdateRequest request, SystemSession session)
        {
            return _jUnitOfWork.Category.UpdateCategory(request, session);
        }

        public Task<StatusData<string>> DeleteCategory(DeleteCategory request, SystemDbStatus mode, SystemSession session)
        {
            return _jUnitOfWork.Category.DeleteCategory(request, session);
        }

        public Task<StatusData<string>> UpsertCategoryFriends(CategoryFriends request, SystemDbStatus mode, SystemSession session)
        {
            return _jUnitOfWork.Category.UpsertCategoryFriends(request, mode, session);
        }

        //public SystemDbStatus CopyCategory(CopyCategory request, SystemDatabaseOperationMode operation)
        //{
        //    return this.unitOfWork.Category.CopyCategory(request, operation);
        //}

        //public Task<PaginatedResponse<IEnumerable<int>>> GetFriendsInCategoryList(int userId,
        //     int userCategoryTypeId, int? pageIndex, int? pageSize)
        //{
        //    return this.unitOfWork.Category.GetFriendsInCategoryList(userId, userCategoryTypeId, pageIndex, pageSize);
        //}
    }
}
