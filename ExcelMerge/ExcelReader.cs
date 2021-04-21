using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;

namespace ExcelMerge
{
    internal class ExcelReader
    {
        internal static IEnumerable<ExcelRow> Read(ISheet sheet)
        {
            var actualRowIndex = 0;
            var filtrationIndex = 0;

            for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);
                if (row == null)
                    continue;

                var cells = new List<ExcelCell>();
                

                for (int columnIndex = 0; columnIndex < row.LastCellNum; columnIndex++)
                {
                    var cell = row.GetCell(columnIndex);
                    var stringValue = ExcelUtility.GetCellStringValue(cell);
                    
                    if (stringValue == "")
                    {
                        filtrationIndex++;
                    }
                    else
                    {
                        filtrationIndex = 0;
                    }
                    if (filtrationIndex < 100)
                    {
                        cells.Add(new ExcelCell(stringValue, columnIndex, rowIndex));
                    }
                    else
                    {
                        filtrationIndex = 0;
                        break;
                    }
                }
                filtrationIndex = 0;
                yield return new ExcelRow(actualRowIndex++, cells);
            }
        }
    }
}
