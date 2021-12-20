using System;
namespace RazorTutorial.Models
{
    public class PdfInput
    {
        public string TemplatePath { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string SessionName { get; set; }
        public PdfInput()
        {
        }
        public PdfInput(string templatePath, string name, DateTime dateTime, string sessionName)
        {
            TemplatePath = templatePath;
            Name = name;
            DateTime = dateTime;
            SessionName = sessionName;
        }
    }
}
