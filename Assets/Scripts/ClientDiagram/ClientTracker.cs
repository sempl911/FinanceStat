using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClientTracker
{

    public static double GetLeftPercentage(int totalClients, int devicesLeft) 
    {
  
        if (totalClients == 0) return 0;
           
        double result = (double) devicesLeft / totalClients * 100;
        DailyDataManager.AddLeftDevice(result);
        return result;
       
    }

    public static double GetNoLeftPercentage(int totalClients, int devicesLeft)
    {
        if (totalClients == 0) return 0;
        double result = (double)(totalClients - devicesLeft) / totalClients * 100;
        DailyDataManager.AddNoLeftDevice(result);
        return result;
    }

}
