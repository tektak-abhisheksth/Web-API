using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using Model.Types;

namespace Utility
{
    /// <summary>
    /// Performs custom and supplement validations.
    /// </summary>
    public static class Validation
    {
        #region Custom Validations
        /// <summary>
        /// Checks whether the value is null or not.
        /// </summary>
        /// <param name="variable">The variable in context.</param>
        /// <param name="getVariableName">The name of the variable that should be populated in the error context.</param>
        /// <param name="actionContext">Current action context.</param>
        /// <param name="modelState">Current model object.</param>
        /// <returns>The status of the operation.</returns>
        public static bool Required(string variable, Expression<Func<string, string>> getVariableName, HttpActionContext actionContext, ModelStateDictionary modelState)
        {
            var variableName = ((MemberExpression)getVariableName.Body).Member.Name;
            var attr = new RequiredAttribute();

            if (!attr.IsValid(variable) || variable.Equals("null", StringComparison.OrdinalIgnoreCase))
            {
                modelState.AddModelError(attr.ToString(), attr.FormatErrorMessage(variableName));

                AddErrorToResponse(actionContext, modelState);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks to see whether the provided value falls within the range or not.
        /// </summary>
        /// <param name="variable">The variable in context.</param>
        /// <param name="getVariableName">The name of the variable that should be populated in the error context.</param>
        /// <param name="minimum">The minimum value allowed in the range.</param>
        /// <param name="maximum">The maximum value allowed in the range.</param>
        /// <param name="actionContext">Current action context.</param>
        /// <param name="modelState">Current model object.</param>
        /// <returns>The status of the operation.</returns>
        public static bool Range<T>(T variable, Expression<Func<T, T>> getVariableName, T minimum, T maximum, HttpActionContext actionContext, ModelStateDictionary modelState)
        {
            var variableName = ((MemberExpression)getVariableName.Body).Member.Name;
            var attr = new RangeAttribute(Convert.ToDouble(minimum), Convert.ToDouble(maximum));

            if (!attr.IsValid(variable))
            {
                modelState.AddModelError(attr.ToString(), attr.FormatErrorMessage(variableName));

                AddErrorToResponse(actionContext, modelState);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks the length of the string to verify whether or not it falls within the permissible limits.
        /// </summary>
        /// <param name="variable">The variable in context.</param>
        /// <param name="getVariableName">The name of the variable that should be populated in the error context.</param>
        /// <param name="minimumLength">The minimum length of the variable.</param>
        /// <param name="maximumLength">The maximum length of the variable.</param>
        /// <param name="actionContext">Current action context.</param>
        /// <param name="modelState">Current model object.</param>
        /// <returns>The status of the operation.</returns>
        public static bool StringLength(string variable, Expression<Func<string, string>> getVariableName, int minimumLength, int maximumLength, HttpActionContext actionContext, ModelStateDictionary modelState)
        {
            var variableName = ((MemberExpression)getVariableName.Body).Member.Name;
            var attr = new StringLengthAttribute(maximumLength) { MinimumLength = minimumLength };

            if (!attr.IsValid(variable))
            {
                modelState.AddModelError(attr.ToString(), attr.FormatErrorMessage(variableName));

                AddErrorToResponse(actionContext, modelState);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether the value is contained in a provided list.
        /// </summary>
        /// <param name="variable">The variable in context.</param>
        /// <param name="getVariableName">The name of the variable that should be populated in the error context.</param>
        /// <param name="values">List of allowed values of any numeric or enumerable type.</param>
        /// <param name="actionContext">Current action context.</param>
        /// <param name="modelState">Current model object.</param>
        /// <param name="errorMessage">Error message to return to client.</param>
        /// <param name="addErrorWhenValueIsPresent">Flag that modifies when the error should be added.</param>
        /// <returns>The status of the operation.</returns>
        public static bool Contains<TE, TNumeric>(TNumeric variable, Expression<Func<TNumeric, TNumeric>> getVariableName, IEnumerable<TE> values, HttpActionContext actionContext, ModelStateDictionary modelState, string errorMessage = null, bool addErrorWhenValueIsPresent = false)
        {
            var variableName = getVariableName == null ? string.Empty : ((MemberExpression)getVariableName.Body).Member.Name;
            var valuesConverted = values.Select(x => Convert.ToInt32(x)).ToList();
            var result = valuesConverted.All(x => x != Convert.ToInt32(variable));
            result = addErrorWhenValueIsPresent ? !result : result;
            if (result)
            {
                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = string.Format("Allowed values for {0} are only {1}.", variableName, string.Join(", ", valuesConverted));
                modelState.AddModelError(variableName, errorMessage);

                AddErrorToResponse(actionContext, modelState);
                return addErrorWhenValueIsPresent ^ false;
            }

            return addErrorWhenValueIsPresent ^ true;
        }

        /// <summary>
        /// Checks whether the list is empty or not.
        /// </summary>
        /// <typeparam name="T">The type to which the generic type should cast to.</typeparam>
        /// <param name="variable">The variable in context.</param>
        /// <param name="getVariableName">The name of the variable that should be populated in the error context.</param>
        /// <param name="actionContext">Current action context.</param>
        /// <param name="modelState">Current model object.</param>
        /// <param name="errorMessage">Error message to return to client.</param>
        /// <returns>The status of the operation.</returns>
        public static bool IsEnumerablePopulated<T>(IList variable, Expression<Func<object, IList<T>>> getVariableName, HttpActionContext actionContext, ModelStateDictionary modelState, string errorMessage = null)
        {
            var variableName = ((MemberExpression)getVariableName.Body).Member.Name;
            if (variable == null || variable.Count == 0)
            {
                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = string.Format("{0} cannot be null or empty.", variableName);
                modelState.AddModelError(variableName, errorMessage);

                AddErrorToResponse(actionContext, modelState);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether the value matches the regular expression.
        /// </summary>
        /// <param name="variable">The variable in context.</param>
        /// <param name="getVariableName">The name of the variable that should be populated in the error context.</param>
        /// <param name="regexPattern">The pattern to satisfy.</param>
        /// <param name="actionContext">Current action context.</param>
        /// <param name="modelState">Current model object.</param>
        /// <param name="errorMessage">Error message to return to client.</param>
        /// <returns>The status of the operation.</returns>
        public static bool IsMatch(this string variable, Expression<Func<string, string>> getVariableName, string regexPattern, HttpActionContext actionContext, ModelStateDictionary modelState, string errorMessage = null)
        {
            var variableName = ((MemberExpression)getVariableName.Body).Member.Name;
            if (!Regex.IsMatch(variable, regexPattern))
            {
                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = string.Format("Invalid format for {0}.", variableName);
                modelState.AddModelError(variableName, errorMessage);

                AddErrorToResponse(actionContext, modelState);
                return false;
            }
            return true;
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Adds the first error from model dictionary to the response in context.
        /// </summary>
        /// <param name="actionContext">Current action context.</param>
        /// <param name="modelState">Current model object.</param>
        public static void AddErrorToResponse(HttpActionContext actionContext, ModelStateDictionary modelState)
        {
            foreach (var state in modelState.Keys.Select(key => modelState[key]).Where(state => state.Errors.Any()))
            {
                //Since this returns model binding specific response, any code that returns 400 will do.
                var firstMsg = state.Errors.First();
                var msg = firstMsg.ErrorMessage;
                //TODO: Change this in production.
                if (msg.Length <= 1) msg = firstMsg.Exception.Message;
                if (msg.Length <= 1) msg = firstMsg.Exception.InnerException.Message;
                if (msg.Length <= 1) msg = firstMsg.Exception.StackTrace;
                actionContext.Response = actionContext.Request.SystemResponse<string>(SystemDbStatus.NotSupported, null, false, msg);
                return;
            }
        }
        #endregion
    }
}
