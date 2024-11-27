using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Globalization;

public class DataWindow : MonoBehaviour
{

    [SerializeField] DatabaseConnector databaseConnector;
    List<GameObject> dataList = new List<GameObject>();  
    public List<GameObject> DailyDatas { get => dataList;  set { dataList = value; } }

    [Header("Report params")]
    [SerializeField] GameObject _reportDatas;
    [SerializeField] GameObject _calendarGrid;
    [SerializeField] TextMeshProUGUI _noDataTxt;
    [SerializeField] TextMeshProUGUI _profitTxt;
    [SerializeField] TextMeshProUGUI _spendTxt;
    [SerializeField] TextMeshProUGUI _callsTxt;
    [SerializeField] TextMeshProUGUI _clientsCountTxt;
    [SerializeField] TextMeshProUGUI _leftsCount;
    DateTime _currentDate;

    int repCalls = 0;
    int repClients = 0;
    int repLeftClients = 0;
    float repProfit = 0;
    float repSpend = 0;

    private void Start()
    {
        databaseConnector = new DatabaseConnector();
        databaseConnector.OpenDatabase();
    }

    void Update()
    {
        FindeAllSelectedDays();
        LoadFromDbStats();
    }

    void FindeAllSelectedDays()
    {
        dataList.Clear();

        for(int i = 0; i < _calendarGrid.transform.childCount; i++) {

            GameObject child = _calendarGrid.transform.GetChild(i).gameObject;
            CallendarCellProp callendarCellProp = child.GetComponent<CallendarCellProp>();
            if(callendarCellProp != null && callendarCellProp.IsSelected)
            {
                dataList.Add(child);
            }            
        }
    }

    void LoadFromDbStats()
    {
        ResetReportValues();

        List<DailyStats_SQl> all_Stats = databaseConnector.GetAllDailyStats();
        foreach (GameObject selectedDay in dataList)  
        {
            CallendarCellProp callendarCellProp = selectedDay.GetComponent<CallendarCellProp>();
            if(callendarCellProp != null)
            {
                foreach (DailyStats_SQl dailyStats in all_Stats)
                {
                    if (callendarCellProp.CurrentDayDate == dailyStats.Date)
                    {
                        CalculateReport(dailyStats.CallsCount, dailyStats.ClientCount, dailyStats.DevicesLeft, dailyStats.Profit, dailyStats.Spend);
                    }
                }
            }
        }

        ShowReport();
    }

    void ResetReportValues()
    {
        repCalls = 0;
        repClients = 0;
        repLeftClients = 0;
        repProfit = 0;
        repSpend = 0;
    }

    void CalculateReport(int calls, int clients, int leftClients, float profit, float spend)
    {
         repCalls += calls;
         repClients += clients;
         repLeftClients += leftClients;
         repProfit += profit;
         repSpend += spend;

        
    }

    void ShowReport()
    {
        _callsTxt.text = "Calls: " + repCalls.ToString();
        _clientsCountTxt.text = "Clients: " + repClients.ToString();
        _leftsCount.text = "Lefts: " + repLeftClients.ToString();
        _profitTxt.text = "Profit: " + repProfit.ToString();
        _spendTxt.text = "Spend: " + repSpend.ToString();

        // Отображаем текст "Нет данных" в зависимости от наличия данных
        if (repCalls == 0 && repClients == 0 && repLeftClients == 0)
        {
            _noDataTxt.gameObject.SetActive(true);
            _reportDatas.SetActive(false);
        }
        else
        {
            _noDataTxt.gameObject.SetActive(false);
            _reportDatas.SetActive(true);
        }
    }
}
