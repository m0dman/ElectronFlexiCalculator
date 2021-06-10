using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace RandoxITUtilityAPI.Models
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Custom model for handling Global Error Middleware
    /// </summary>
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}