using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using  TMPro;
using System;

public class CalendarCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currentDate;
    [SerializeField] TextMeshProUGUI _selectedDate;

    private void Start()
    {
        _currentDate.text = "Now: " + System.DateTime.Now.ToString("dd-MM-yyyy");
        SetSelectedDate(System.DateTime.Now); // Current month
    }

    public void SetSelectedDate(DateTime selectedDate)
    {
        string formatedDate = selectedDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture);
        _selectedDate.text = formatedDate;
    }
}
