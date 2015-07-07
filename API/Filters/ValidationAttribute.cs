using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Utility;

namespace API.Filters
{
    /// <summary>
    /// Represents an attribute that is used to handle validation post-processing after model binding.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
                Validation.AddErrorToResponse(actionContext, actionContext.ModelState);
        }
    }
}