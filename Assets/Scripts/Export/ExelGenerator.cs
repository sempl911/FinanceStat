using System.Data;
using UnityEngine;
using SQLite4Unity3d; // Это для работы с SQLite
using System.IO; // Для работы с файлами
////using NPOI.SS.UserModel; // Для работы с NPOI
//using NPOI.XSSF.UserModel;
using System.Collections.Generic; // Для работы с XLSX

public class ExelGenerator : MonoBehaviour
{
    private DatabaseConnector dbConnector;
    //private IWorkbook workBook;
    /* void Start()
    {
        dbConnector = new DatabaseConnector();
        dbConnector.OpenDatabase();
        workBook = new XSSFWorkbook();
    }
 
    public void ExportToXlsx() 
    {
        Debug.Log("Export starting");

        List<DailyStats_SQl> statsList = dbConnector.GetAllDailyStats();

        if (statsList == null || statsList.Count == 0)
        {
            Debug.LogWarning("No data for export");
            return ;
        }

        ISheet sheet = workBook.CreateSheet("Statistic");

        sheet.CreateFreezePane(0, 1);
                                // Make header font
        ICellStyle headerStyle = workBook.CreateCellStyle();
        IFont font = workBook.CreateFont();
        font.IsBold = true;
        font.FontHeightInPoints = 14;
        headerStyle.SetFont(font);

        IRow headerRow = sheet.CreateRow(0);
        ICell cell;

        cell = headerRow.CreateCell(0);
        cell.SetCellValue("Date");
        cell.CellStyle = headerStyle;

        cell = headerRow.CreateCell(1);
        cell.SetCellValue("Calls");
        cell.CellStyle = headerStyle;

        cell = headerRow.CreateCell(2);
        cell.SetCellValue("Clients");
        cell.CellStyle = headerStyle;

        cell = headerRow.CreateCell(3);
        cell.SetCellValue("Clients left");
        cell.CellStyle = headerStyle;

        cell = headerRow.CreateCell(4);
        cell.SetCellValue("Profit");
        cell.CellStyle = headerStyle;

        cell = headerRow.CreateCell(5);
        cell.SetCellValue("Spend");
        cell.CellStyle = headerStyle;

        cell = headerRow.CreateCell(6);
        cell.SetCellValue("Money per day");
        cell.CellStyle = headerStyle;

        int rowIndex = 1;

        foreach (var stats in statsList)
        {
            IRow cellsrow = sheet.CreateRow(rowIndex++);
            cellsrow.CreateCell(0).SetCellValue(stats.Date);
            cellsrow.CreateCell(1).SetCellValue((int)stats.CallsCount);
            cellsrow.CreateCell(2).SetCellValue((int)stats.ClientCount);
            cellsrow.CreateCell(3).SetCellValue((int)stats.DevicesLeft);
            cellsrow.CreateCell(4).SetCellValue((float)stats.Profit);
            cellsrow.CreateCell(5).SetCellValue((float)stats.Spend);
            cellsrow.CreateCell(6).SetCellValue((float)stats.TotalPerDay);

        } 


    }
    
    public void GetPathAndSave (string path)
    {
          string filePath = Path.Combine(path, "statisticExport.xlsx");

        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            workBook.Write(stream);
        }  

         Debug.Log("Export finished to " + path);

    }*/
}
