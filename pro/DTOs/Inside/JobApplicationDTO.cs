using System;

namespace pro.DTOs.Inside
{
    public class JobApplicationDTO
    {
        public string DesiredLocation { get; set; }
        public bool IsFullTimePosition { get; set; }
        public DateTime? StartDate { get; set; }
        public string Source { get; set; }
        public string PreferredContactMethod { get; set; }
    }
}
