using System;

namespace pro.DTOs.Inside
{
    public class ErrorLogDTO
    {
        public string ScreenName { get; set; }


        public string ErrorName { get; set; }


        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
