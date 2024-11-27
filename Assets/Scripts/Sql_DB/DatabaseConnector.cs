using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DatabaseConnector
{
    private SQLiteConnection _connection;

    public void OpenDatabase()
    {
        string dbPath = Path.Combine(Application.persistentDataPath, "statistics.db");
        _connection = new SQLiteConnection(dbPath);
        _connection.CreateTable<DailyStats_SQl>();  // Создайте таблицу заново
    }

    public int UpdateDailyStats(DailyStats_SQl stats)
    {
        // Обновляем запись в базе данных
        return _connection.Update(stats);
    }

    public List<DailyStats_SQl> GetAllDailyStats()
    {
        return _connection.Table<DailyStats_SQl>().ToList();
    }

    public int SaveDailyStats(DailyStats_SQl stats)
    {
        try
        {
            return _connection.Insert(stats);
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка при сохранении данных: " + ex.Message);
            return 0;  // Возвращаем 0 в случае ошибки
        }
    }

    public void RESET_DB()
    {
        //_connection.DropTable<DailyStats_SQl>();  // Удалите старую таблицу => УДАЛЯЕТ ВООБЩЕ ВСЕ!!!
        _connection.DeleteAll<DailyStats_SQl>();
    }
}
