using System;
using System.Collections.Generic;

namespace RazorTutorial.Models
{
    public class ExcelExtract
    {
        public List<string> Names { get; set; }
        public DateTime SessionDate { get; set; }
        public string SessionName { get; set; }
        public ExcelExtract(List<string> names, DateTime sessionDate, string sessionName)
        {
            Names = names;
            SessionDate = sessionDate;
            SessionName = sessionName;

        }
    }
}
