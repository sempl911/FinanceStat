using System;
using SQLite4Unity3d;

public class DailyStats_SQl
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Date { get; set; }
    public int CallsCount { get; set; }
    public int LeftDeviceCount { get; set; }
    public int ClientCount { get; set; }
    public int NoLeftDeviceCount { get; set; }
    public int ProfitPercent { get; set; }
    public int SpendPercent { get; set; }
    public int DevicesLeft { get; set; }
    public float Profit { get; set; }
    public float Spend { get; set; }
    public float TotalPerDay { get; set; }


    public DailyStats_SQl(){}
}

