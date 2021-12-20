using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorTutorial.Models;
using RazorTutorial.Services;

namespace RazorTutorial.Pages
{
    public class UploadModel : PageModel
    {
        [BindProperty]

        public FileUpload ExcelWithNames { get; set; }
        [BindProperty]

        public FileUpload PdfTemplate { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostUploadAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string fileDir = Path.GetTempPath();

            var excelPath = Path.Combine(fileDir, $"{Guid.NewGuid().ToString()}.xlsx");
            var pdfPath = Path.GetTempFileName();
            List<CertResult> pdfCertPaths = new List<CertResult>();
            try
            {
                using (var stream = System.IO.File.Create(excelPath))
                {
                    await ExcelWithNames.FormFile.CopyToAsync(stream);
                }

                ExcelExtract data = ExcelParser.ExtractNames(excelPath);

                // pdf handling
                using (var stream = System.IO.File.Create(pdfPath))
                {
                    await PdfTemplate.FormFile.CopyToAsync(stream);
                }
                foreach (string name in data.Names)
                {
                    PdfInput pdfInput = new PdfInput(pdfPath, name, data.SessionDate, data.SessionName);
                    CertResult pdfDest = PdfGenerator.GeneratePDF(pdfInput);
                    pdfCertPaths.Add(pdfDest);
                }
            }
            finally
            {
                if (System.IO.File.Exists(excelPath))
                    System.IO.File.Delete(excelPath);

                if (System.IO.File.Exists(pdfPath))
                    System.IO.File.Delete(pdfPath);
            }
            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            // zip pdfs
            string zipPath = Path.Combine(fileDir, $"{Guid.NewGuid().ToString()}.pdf");
            //zipPath = "test.zip";
            using (ZipArchive zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                foreach (CertResult cert in pdfCertPaths)
                {
                    zip.CreateEntryFromFile(cert.Path, $"{cert.Name}.pdf");
                }

            }

            var fs = new FileStream(zipPath, FileMode.Open, FileAccess.Read, FileShare.None, 5096, FileOptions.DeleteOnClose);
            return File(fs, MediaTypeNames.Application.Octet, "NewName34.zip");

        }
    }
}
