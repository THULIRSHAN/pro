using Microsoft.VisualBasic;
using System;

namespace pro.Models.Version
{
    public class ApiVersionInfo
    {
        public string Framework { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
