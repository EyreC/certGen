using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using RazorTutorial.Models;

namespace RazorTutorial.Services
{
    public class ExcelParser
    {
        public ExcelParser()
        {
        }
        public static ExcelExtract ExtractNames(string path)
        {
            List<string> names = new List<string>();
            using (XLWorkbook wb = new XLWorkbook(path))
            {
                var ws = wb.Worksheet(1);
                var firstRowUsed = ws.FirstRowUsed();
                firstRowUsed = firstRowUsed.RowBelow();
                var nameCell = firstRowUsed.Cell(2);
                var sessionNameCell = firstRowUsed.Cell(4);
                var dateCell = firstRowUsed.Cell(5);

                string sessionName = sessionNameCell.GetString();
                DateTime date = dateCell.GetDateTime();

                while (!nameCell.IsEmpty())
                {
                    string name = nameCell.GetString();
                    names.Add(name);
                    nameCell = nameCell.CellBelow();
                }
                return new ExcelExtract(names, date, sessionName);
            }
        }
    }
}
