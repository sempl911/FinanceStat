using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class SQL_Loader : MonoBehaviour
{
    [SerializeField] DatabaseConnector databaseConnector;
    DailyStats_SQl currentDay;

    void Awake()
    {

        DailyDataManager.SetDate(System.DateTime.Now.Date);

        databaseConnector = new DatabaseConnector();
        databaseConnector.OpenDatabase();
        databaseConnector.GetAllDailyStats();

        LoadDataFromDb();
        UpdateCurrentRecord();
        Debug.Log(databaseConnector.GetAllDailyStats().Count);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UpdateCurrentRecord();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadDataFromDb();
        }
        

    }

    public void OnResetAllBtn()
    {
        databaseConnector.RESET_DB();
        DailyDataManager.AddCall(0);
        DailyDataManager.AddClient(0);
        DailyDataManager.AddDevicesLeft(0);
        DailyDataManager.AddLeftDevice(0);
        DailyDataManager.AddNoLeftDevice(0);
        DailyDataManager.AddProfit(0);
        DailyDataManager.AddSpend(0);
        DailyDataManager.AddProfitPercent(0);
        DailyDataManager.AddSpendPercent(0);
        DailyDataManager.AddTotalMoney(0);
        ReloadScene();
    }

    public void UpdateCurrentRecord()
    {
        if (currentDay != null)
        {
            currentDay.Date = DailyDataManager.SavedDate.ToString("yyyy-MM-dd");
            currentDay.CallsCount = DailyDataManager.CallsCount;
            currentDay.ClientCount = DailyDataManager.ClientCount;
            currentDay.LeftDeviceCount = DailyDataManager.LeftDeviceCount;
            currentDay.NoLeftDeviceCount = DailyDataManager.NoLeftDeviceCount;
            currentDay.Profit = DailyDataManager.Profit;
            currentDay.Spend = DailyDataManager.Spend;
            currentDay.TotalPerDay = DailyDataManager.TotalPerDay;
            currentDay.DevicesLeft = DailyDataManager.DevicesLeft;
            currentDay.ProfitPercent = DailyDataManager.ProfitInPercent;
            currentDay.SpendPercent = DailyDataManager.SpendInPercent;
            SetData(currentDay);
            databaseConnector.UpdateDailyStats(currentDay);
        }
    }


   public void LoadDataFromDb()
    {
        List<DailyStats_SQl> allStats = databaseConnector.GetAllDailyStats();
        if (allStats != null)
        {
            string currentDate = System.DateTime.Now.Date.ToString("yyyy-MM-dd");

            bool dateFound = false;

            foreach (var stat in allStats)
            {
                if (stat.Date == currentDate)
                {
                    dateFound = true;
                    currentDay = stat;

                    DailyDataManager.SetDate(DateTime.Parse(currentDate));
                    DailyDataManager.AddCall(stat.CallsCount);
                    DailyDataManager.AddClient(stat.ClientCount);
                    DailyDataManager.AddDevicesLeft(stat.DevicesLeft);
                    DailyDataManager.AddLeftDevice(stat.LeftDeviceCount);
                    DailyDataManager.AddNoLeftDevice(stat.NoLeftDeviceCount);
                    DailyDataManager.AddProfit(stat.Profit);
                    DailyDataManager.AddSpend(stat.Spend);
                    DailyDataManager.AddProfitPercent(stat.ProfitPercent);
                    DailyDataManager.AddSpendPercent(stat.SpendPercent);
                    DailyDataManager.AddTotalMoney(stat.TotalPerDay);

                    databaseConnector.UpdateDailyStats(stat);
                    Debug.Log("Data is update, calls count " + DailyDataManager.CallsCount);
                    break;
                }
            }

            if (!dateFound)
            {
                CreateNewDay();
                Debug.Log("Создана новая запись т к дата другая");
            }
        }
        else
        {
            Debug.LogWarning("List of db data is null");
        }
    }
    
    void CreateNewDay()
    {
        DailyStats_SQl dailyData = new DailyStats_SQl()
        {
            Date = DailyDataManager.SavedDate.ToString("yyyy-MM-dd"),
            CallsCount = DailyDataManager.CallsCount,
            ClientCount = DailyDataManager.ClientCount,
            LeftDeviceCount = DailyDataManager.LeftDeviceCount,
            NoLeftDeviceCount = DailyDataManager.NoLeftDeviceCount,
            Profit = DailyDataManager.Profit,
            Spend = DailyDataManager.Spend,
            TotalPerDay = DailyDataManager.TotalPerDay,
            DevicesLeft = DailyDataManager.DevicesLeft,
            ProfitPercent = DailyDataManager.ProfitInPercent,
            SpendPercent =  DailyDataManager.SpendInPercent
        };

        currentDay = dailyData;

        // Сохранение данных и проверка результата
        int result = databaseConnector.SaveDailyStats(dailyData);

        if (result > 0)
        {
            Debug.Log("Данные успешно сохранены в базу данных.");
        }
        else
        {
            Debug.LogError("Ошибка при сохранении данных в базу данных.");
        }
    }

    void SetData(DailyStats_SQl stat)
    {
        DailyDataManager.SetDate(DateTime.Parse(stat.Date));
        DailyDataManager.AddCall(stat.CallsCount);
        DailyDataManager.AddClient(stat.ClientCount);
        DailyDataManager.AddDevicesLeft(stat.DevicesLeft);
        DailyDataManager.AddLeftDevice(stat.LeftDeviceCount);
        DailyDataManager.AddNoLeftDevice(stat.NoLeftDeviceCount);
        DailyDataManager.AddProfit(stat.Profit);
        DailyDataManager.AddSpend(stat.Spend);
        DailyDataManager.AddProfitPercent(stat.ProfitPercent);
        DailyDataManager.AddSpendPercent(stat.SpendPercent);
        DailyDataManager.AddTotalMoney(stat.TotalPerDay);
    }
   
    private void OnDestroy()
    {
        UpdateCurrentRecord();
        databaseConnector.UpdateDailyStats(currentDay);
    }
    private void ReloadScene()
    {
    // Получаем активную сцену и её имя
        string currentSceneName = SceneManager.GetActiveScene().name;
    
    // Перезагружаем сцену
        SceneManager.LoadScene(currentSceneName);
    }

     private void OnApplicationQuit()
    {
        UpdateCurrentRecord();
        databaseConnector.UpdateDailyStats(currentDay);
    }

     private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            UpdateCurrentRecord();
            databaseConnector.UpdateDailyStats(currentDay);
        }
    }

    private void OnApplicationFocus(bool focusStatus) 
    {
        if (!focusStatus)
        {
            UpdateCurrentRecord();
            databaseConnector.UpdateDailyStats(currentDay);
        }
    }
}
