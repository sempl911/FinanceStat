using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;

public static class DailyDataManager
{
    private static DateTime date;
    private static int callCount;
    private static int clientCount;
    private static int leftDeviceCount; //Percentage
    private static int noLeftDeviceCount; //Percentage
    private static int _profitInPercent;
    private static int _spendInPercent;
    private static int _devicesLeft;
    private static float _profit;
    private static float _spend;
    private static float totalPerDay;

    public static DateTime SavedDate { get => date; }
    public static int CallsCount { get => callCount; }
    public static int ClientCount { get => clientCount; }
    public static int LeftDeviceCount { get => leftDeviceCount; }
    public static int NoLeftDeviceCount { get => noLeftDeviceCount; }
    public static int DevicesLeft { get => _devicesLeft; }
    public static int ProfitInPercent { get => _profitInPercent; }
    public static int SpendInPercent { get => _spendInPercent; }
    public static float Profit { get => _profit; }
    public static float Spend { get => _spend; }
    public static float TotalPerDay { get => totalPerDay; }



    public static void SetDate(DateTime newDate)
    {
        date = newDate;
    }

    public static void AddCall(int callsCount)
    {
        callCount = callsCount;
    }

    public static void AddClient(int totalClients)
    {
        clientCount = totalClients;
    }

    public static void AddLeftDevice(double percentLeft)
    {
        leftDeviceCount = Mathf.FloorToInt((float)percentLeft);
    }

    public static void AddNoLeftDevice(double percentNoLeft)
    {
        noLeftDeviceCount = Mathf.FloorToInt((float)percentNoLeft);
    }

    public static void AddDevicesLeft(int devicesLeft)
    {
        _devicesLeft = devicesLeft;
    }

    public static void AddProfit(float profit)
    {
        _profit = profit;
    }

    public static void AddSpend(float spend)
    {
        _spend = spend;
    }

    public static void AddTotalMoney(float summary)
    {
        totalPerDay = summary;
    }

    public static void AddProfitPercent(int profitPercent)
    {
        _profitInPercent = profitPercent;
    }

    public static void AddSpendPercent(int spendPercent)
    {
        _spendInPercent = spendPercent;
    }
    
        // Метод для загрузки данных из JSON файла
        public static void LoadDataFromJSON(string fileName)
        {
            string json = LoadJSONFromFile(fileName);
            if (json != null)
            {
                DailyData loadedData = JsonUtility.FromJson<DailyData>(json);
                SetDataFromLoaded(loadedData);
            }
        }

        // Установка данных из загруженного JSON
        private static void SetDataFromLoaded(DailyData loadedData)
        {
            date = DateTime.Parse(loadedData.date);
            callCount = loadedData.callCount;
            clientCount = loadedData.clientCount;
            leftDeviceCount = loadedData.leftDeviceCount;
            noLeftDeviceCount = loadedData.noLeftDeviceCount;
            _profit = loadedData.profit;
            _spend = loadedData.spend;
            totalPerDay = loadedData.totalPerDay;
            _devicesLeft = loadedData.devicesLeft;
            _profitInPercent = loadedData.profitPercent;
            _spendInPercent = loadedData.spendPercent;
           // Debug.Log("Data was loaded " + date);
        }

        // Загрузка JSON из файла
        private static string LoadJSONFromFile(string fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            Debug.LogWarning("Файл не найден: " + filePath);
            return null;
        }

        // Метод для сохранения данных в JSON файл
        public static void SaveDataToJSON(string fileName)
        {
            DailyData dailyData = new DailyData(date, callCount, clientCount, leftDeviceCount, noLeftDeviceCount, _profit, _spend, totalPerDay, _devicesLeft, _profitInPercent, _spendInPercent);
            string json = JsonUtility.ToJson(dailyData, true); // true для форматированного JSON
            SaveJSONToFile(json, fileName);
        }

        // Сохранение JSON в файл
        private static void SaveJSONToFile(string json, string fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllText(filePath, json);
            //Debug.Log("Данные сохранены в " + filePath);
        }


    public static bool CheckDate()
    {
        // Сравниваем только даты (без времени)
        DateTime savedDate = date;
        DateTime currentDate = DateTime.Now.Date;

        // Сравниваем, совпадает ли сохранённая дата с текущей
        if (savedDate == currentDate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckDate(DateTime savedDate)
    {
        // Сравниваем только даты (без времени)
        savedDate = date;
        DateTime currentDate = DateTime.Now.Date;

        // Сравниваем, совпадает ли сохранённая дата с текущей
        if (savedDate == currentDate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
