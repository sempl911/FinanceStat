using System;
public class DailyData
{
    public string date;
    public int callCount;
    public int clientCount;
    public int leftDeviceCount;
    public int noLeftDeviceCount;
    public int profitPercent;
    public int spendPercent;
    public int devicesLeft;
    public float profit;
    public float spend;
    public float totalPerDay;

    public DailyData(DateTime date, int callCount, int clientCount, int leftDeviceCount, int noLeftDeviceCount, float profit, float spend, float totalPerDay, int devicesLeft, int profitPercent, int spendPercent)
	{
        this.date = date.ToString("yyyy-MM-dd"); // Преобразование даты в строку
        this.callCount = callCount;
        this.clientCount = clientCount;
        this.leftDeviceCount = leftDeviceCount;
        this.noLeftDeviceCount = noLeftDeviceCount;
        this.profit = profit;
        this.spend = spend;
        this.totalPerDay = totalPerDay;
        this.devicesLeft = devicesLeft;
        this.profitPercent = profitPercent;
        this.spendPercent = spendPercent;
    }
}

