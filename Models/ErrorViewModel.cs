using System;

namespace HAWK_v.Models
{
    /// <summary>
    /// This Class Manages the Errors
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
