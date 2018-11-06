using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HearstMappingsEditor.Common
{
    public class ExcelExportHelper
    {
        public static string GetFilePath(Guid fileGuid, string storagePath)
        {
            return Path.Combine(storagePath, fileGuid.ToString() + ".xlsx");
        }

        public static Guid? SaveToExcel<T>(IList<T> list, string storagePath)
        {
            if (list == null || list.Count == 0)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(storagePath) && !Directory.Exists(storagePath))
            {
                try
                {
                    Directory.CreateDirectory(storagePath);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to create directory: {storagePath}", ex);
                }
            }

            var guid = Guid.NewGuid();  
            var path = GetFilePath(guid, storagePath);

            using (var document = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                var dataHeaders = typeof(T)
                    .GetProperties()
                    .Where(p => Attribute.IsDefined(p, typeof(ExcelExportAttribute)))
                    .Select(p => new
                    {
                        Property = p,
                        Attribute = (ExcelExportAttribute)Attribute.GetCustomAttribute(p, typeof(ExcelExportAttribute), true)
                    })
                    .OrderBy(p => p.Attribute.Order)
                    .ToDictionary(p => p.Property.Name, p => string.IsNullOrEmpty(p.Attribute.Name) ? p.Property.Name : p.Attribute.Name);

                var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                var sheet = new Sheet()
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Mapping"
                };
                var sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                var headerRow = new Row
                {
                    RowIndex = 1
                };
                foreach (var key in dataHeaders.Keys)
                {
                    headerRow.AppendChild(AddTextCell(dataHeaders[key]));
                }
                sheetData.AppendChild(headerRow);

                var rowIndex = 2;
                foreach (var item in list)
                {
                    var row = new Row
                    {
                        RowIndex = (UInt32)rowIndex
                    };

                    foreach (KeyValuePair<string, string> entry in dataHeaders)
                    {
                        var y = typeof(T).InvokeMember(entry.Key, BindingFlags.GetProperty, null, item, null);
                        row.AppendChild(AddTextCell(y == null ? "" : y.ToString()));
                    }

                    sheetData.AppendChild(row);
                    rowIndex++;
                }

                sheets.Append(sheet);
                workbookPart.Workbook.Save();
            }

            return guid;
        }

        private static Cell AddTextCell(string text)
        {
            var textObject = new Text
            {
                Text = text
            };
            var inlineString = new InlineString(textObject);

            return new Cell(inlineString)
            {
                DataType = CellValues.InlineString,
            };
        }
    }
}
