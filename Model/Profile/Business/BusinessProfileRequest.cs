using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Base;

namespace Model.Profile.Business
{
    public class BasicContactBusinessRequest : RequestBase
    {
        public string About { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public int CityId { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
    }

    public class CompanyReviewRequest : RequestBase
    {
        public int CompanyId { get; set; }
        public int Star { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string CompanyReviewGuid { get; set; }
    }

    public class CompanyDepartmentEmployeeRequest : RequestBase
    {
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public List<int> EmployeeIds { get; set; }
    }

    public class CompanyDepartmentEmployeeViewRequest : RequestBase
    {
        [Required]
        public string TargetUser { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }

    public class DepartmentRequest : RequestBase
    {
        public int DepartmentId { get; set; }
        public int FromCompanyId { get; set; }
        public int ToCompanyId { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeIds { get; set; }
    }

    public class EmployeeViewRequest : RequestBase
    {
        public int TargetUserId { get; set; }
        public int FilterId { get; set; }
        public string SearchTerm { get; set; }
    }

    public class EmployeeRatingViewRequest : RequestBase
    {
        public int TargetUserId { get; set; }
        public List<int> EmployeeId { get; set; }
        public int Rating { get; set; }
    }

    public class EmployeeRatingRequest : RequestBase
    {
        public List<int> EmployeeId { get; set; }
        public int Rating { get; set; }
    }

    public class CompanyEmployeeRequest : RequestBase
    {
        public string TargetUser { get; set; }
        public string SearchTerm { get; set; }
    }

    public class CompanyTreeViewRequest : RequestBase
    {
        public bool IgnoreSisters { get; set; }
        public bool ShowChildCompaniesOnly { get; set; }
    }

    public class UpsertCompanyEmployeeRequest : RequestBase
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int EmployeeTypeId { get; set; }
        [Required]
        public int PositionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsExecutiveBody { get; set; }
        public bool IsPromoted { get; set; }
    }
}
