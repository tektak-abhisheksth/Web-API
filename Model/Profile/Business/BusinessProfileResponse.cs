using Model.Common;
using System;

namespace Model.Profile.Business
{
    public class CompanyEmployeeResponse
    {
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string StartDate { get; set; }
        public int PositionId { get; set; }
        public int Rating { get; set; }
        public int CompanyUserId { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public int EmployeeTypeId { get; set; }
        public bool IsExecutiveBody { get; set; }
    }

    public class DepartmentResponse
    {
        public int DepartmentId { get; set; }
    }

    public class CompanyReviewResponse
    {
        public Guid CompanyReviewGuid { get; set; }
        public UserResponse User { get; set; }
        public int Star { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime? DateCommented { get; set; }
        public bool IsApproved { get; set; }
    }

    public class EmployeeViewResponse
    {
        public UserResponse User { get; set; }
        public GeneralKvPair<int?, string> Position { get; set; }
        public long EmploymentId { get; set; }
        public int EmployeeTypeId { get; set; }
        public BeginEndDate EmploymentDate { get; set; }
        public bool IsExecutiveBody { get; set; }
        public DateTime? Added { get; set; }
        public bool IsApproved { get; set; }

    }

    public class EmployeeRatingViewResponse
    {
        public long CompanyUserRatingId { get; set; }
        public UserResponse User { get; set; }
        public int Rating { get; set; }
        public DateTime? Added { get; set; }
        public string Positon { get; set; }
    }

    public class CompanyDepartmentEmployeeViewResponse
    {
        public UserResponse User { get; set; }
        public GeneralKvPair<int?, string> Department { get; set; }
        public DateTime? Added { get; set; }
    }

    public class CompanyDepartmentViewResponse
    {
        public GeneralKvPair<int?, string> Department { get; set; }
        public int AssignedByUserId { get; set; }
        public DateTime? Added { get; set; }
        public int EmployeeCount { get; set; }
    }

    public class ResignationViewResponse
    {
        public long PersonEmploymentId { get; set; }
        public UserResponse User { get; set; }
        public string Position { get; set; }
        public DateTime? EndDate { get; set; }

    }

    public class CompanyTreeViewResponse
    {
        public int UserId { get; set; }
        public int ChildId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsRequestee { get; set; }
    }

    public class CompanyEmployeeViewResponse
    {
        public UserResponse User { get; set; }
        public GeneralKvPair<int?, string> Position { get; set; }
        public bool IsExecutiveBody { get; set; }
        public BeginEndDate EmploymentDate { get; set; }
        public int EmployeeTypeId { get; set; }
        public DateTime? Added { get; set; }
        public bool IsApproved { get; set; }
    }

    public class UpsertCompanyEmployeeResponse
    {
        public int CompanyUserId { get; set; }
    }
}
