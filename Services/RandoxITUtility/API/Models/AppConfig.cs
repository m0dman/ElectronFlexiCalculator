

using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace RandoxITUtilityAPI.Models
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// AppSettings class to control what is provided during development as configuration
    /// </summary>
    public class AppConfig
    {
        public string Region { get; set; }
        public string Environment { get; set; }
    }
}