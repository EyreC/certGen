using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RazorTutorial.Models
{
    public class FileUpload
    {
        [Required(ErrorMessage = "A file must be provided")]
        public IFormFile FormFile { get; set; }
        public FileUpload()
        {
        }
    }
}
