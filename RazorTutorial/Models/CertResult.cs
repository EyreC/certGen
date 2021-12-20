using System;
namespace RazorTutorial.Models
{
    public class CertResult
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public CertResult(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
