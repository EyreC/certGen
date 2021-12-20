using System;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;

using RazorTutorial.Models;


namespace RazorTutorial.Services
{
    public class PdfGenerator
    {
        public PdfGenerator()
        {
        }


        public static CertResult GeneratePDF(PdfInput pdfInput)
        {
            string templatePath = pdfInput.TemplatePath;
            string name = pdfInput.Name;

            string dest = System.IO.Path.GetTempFileName();
            using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(templatePath), new PdfWriter(dest)))
            using (Document doc = new Document(pdfDoc))
            {

                // modify the location of the rectangle for the name
                AddText(-290, name, StandardFonts.HELVETICA_BOLD, 16, doc, pdfDoc);
                AddText(-335, pdfInput.SessionName, StandardFonts.HELVETICA, 13, doc, pdfDoc);
                AddText(-340, $"On {pdfInput.DateTime.ToLongDateString()}", StandardFonts.HELVETICA, 13, doc, pdfDoc);
                return new CertResult(pdfInput.Name, dest);
            }
        }

        private static void AddText(int verticalPosition, string text,
                                    string fontName, int fontSize, Document doc, PdfDocument pdfDoc)
        {
            PdfFormXObject nameBox = new PdfFormXObject(new Rectangle(0, verticalPosition, 523, 20)); //523
            using (Canvas templateCanvas = new Canvas(nameBox, pdfDoc))
            {
                // In order to add a formXObject to the document, one can wrap it with an image
                var img = new Image(nameBox);
                img.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

                Text textBox = new Text(text);
                PdfFont font = PdfFontFactory.CreateFont(fontName);
                textBox.SetFont(font);
                textBox.SetFontSize(fontSize);
                textBox.SetFontColor(iText.Kernel.Colors.ColorConstants.WHITE);
                var nameParagraph = new Paragraph(textBox);
                nameParagraph.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                doc.Add(img);
                templateCanvas.Add(nameParagraph);
                var nameWidht = nameParagraph.GetWidth();
                templateCanvas.Close();
            }
        }
    }
}
