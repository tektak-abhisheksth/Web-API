using System;
using System.Collections.Generic;
using System.Linq;
using Model.Common;
using Model.Types;
using Newtonsoft.Json.Linq;

namespace TekTak.iLoop.Helper
{
    public class Helper
    {
        public static void ExtractData(StatusData<object> result, string data, string errorFlag, string elementToExtract = null, ICollection<string> elementsToBypass = null)
        {
            var response = JObject.Parse(data);
            ExtractData(result, response, errorFlag, elementToExtract, elementsToBypass);
        }

        public static void ExtractData(StatusData<object> result, JObject data, string errorFlag, string elementToExtract = null, ICollection<string> elementsToBypass = null)
        {
            JToken token;
            if (data.TryGetValue(errorFlag, StringComparison.OrdinalIgnoreCase, out token) && !Convert.ToBoolean(token))
            {
                if (elementToExtract != null &&
                    data.TryGetValue(elementToExtract, StringComparison.OrdinalIgnoreCase, out token))
                    result.Data = token.ToObject<object>();
                else
                {
                    data.Descendants().OfType<JProperty>()
                        .Where(p => elementsToBypass != null && elementsToBypass.Contains(p.Name))
                        .ToList()
                        .ForEach(att => att.Remove());
                    result.Data = data;
                }
            }
            else
                result.Status = SystemDbStatus.GeneralError;
        }
    }
}
