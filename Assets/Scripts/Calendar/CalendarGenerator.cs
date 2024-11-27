using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CalendarGenerator : MonoBehaviour
{
    [SerializeField] GameObject _cellNoActivePref;
    [SerializeField] RectTransform _calendarPanel;
    [SerializeField] Button _nextBtn;
    [SerializeField] Button _prevBtn;
    [SerializeField] CalendarCanvas calendarCanvas;
    [SerializeField] DataWindow dataWindow;

    private TextMeshProUGUI _dayInCell;
    private DateTime _currentDate;

    // Храним выбранные дни как даты
    private HashSet<DateTime> _selectedDays = new HashSet<DateTime>();
    bool _isCurrentDaySelected = false;

    private void Start()
    {
        _currentDate = DateTime.Now;
        GenerateCalendar();
    }

    void GenerateCalendar()
    {
        DateTime currentDate = _currentDate;
        int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

        // Очищаем календарь перед генерацией
        foreach (Transform child in _calendarPanel)
        {
            Destroy(child.gameObject);
        }

        // Генерация ячеек для дней месяца
        for (int day = 1; day <= daysInMonth; day++)
        {
            GameObject newDayCell = Instantiate(_cellNoActivePref, _calendarPanel);

            _dayInCell = newDayCell.GetComponentInChildren<TextMeshProUGUI>();
            if (_dayInCell != null)
            {
                _dayInCell.text = day.ToString();
            }

            DateTime dayDate = new DateTime(currentDate.Year, month: currentDate.Month, day);
            Button newDayBtn = newDayCell.GetComponent<Button>();

            if (newDayBtn != null)
            {
                newDayBtn.onClick.AddListener(() => ToggleDaySelection(dayDate, newDayCell));
            }
            newDayCell.GetComponent<CallendarCellProp>().CurrentDayDate = dayDate.ToString("yyyy-MM-dd");
            // Проверка, был ли этот день ранее выбран
            if (_selectedDays.Contains(dayDate))
            {
                SelectDay(newDayCell);
            }

            FindeCurrentDay(day, DateTime.Now, newDayCell);
        }
    }

    // Метод для переключения выделения дня
    void ToggleDaySelection(DateTime day, GameObject dayCell)
{
    // Проверяем, если день уже в выбранных
    if (_selectedDays.Contains(day))
    {
        // Если день уже выбран, снимаем выделение и убираем из списка
        DeselectDay(dayCell);  // Убираем выделение с визуальной части
        _selectedDays.Remove(day);  // Удаляем из списка выбранных
    }
    else
    {
        // Если день не выбран, выделяем его и добавляем в список
        _selectedDays.Add(day);   // Добавляем в список выбранных
        SelectDay(dayCell);   // Выделяем визуально
    }
}

    // Метод для выделения дня
   public void SelectDay(GameObject selectedDay)
    {
        CallendarCellProp newDayScript = selectedDay.GetComponent<CallendarCellProp>();
        if (newDayScript != null)
        {
            newDayScript.SetSelection(true); // Устанавливаем выделение
        }
        else
        {
            Debug.LogWarning("Can't find CallendarCellProp on selected day.");
        }

        SoundStatic.PlayMenuSound();
    }

    // Метод для снятия выделения
    public void DeselectDay(GameObject selectedDay)
    {
        CallendarCellProp newDayScript = selectedDay.GetComponent<CallendarCellProp>();
        if (newDayScript != null)
        {
            newDayScript.SetSelection(false); // Снимаем выделение
        }
        else
        {
            Debug.LogWarning("Can't find CallendarCellProp on deselected day.");
        }
        SoundStatic.PlayMenuSound();
    }

    // Проверка и выделение текущего дня
    void FindeCurrentDay(int day, DateTime dateTime, GameObject newDay)
    {
        if (!_isCurrentDaySelected)
        {
            if (_currentDate.Year == dateTime.Year && _currentDate.Month == dateTime.Month && day == dateTime.Day)
            {
                SelectDay(newDay); // Выделяем текущий день, если год, месяц и день совпадают
                _isCurrentDaySelected = true;
            }
        }
        else
        {
            return;
        }
    }

    // Метод для переключения на следующий месяц
    public void OnNextMonth()
    {
        _currentDate = _currentDate.AddMonths(1);
        GenerateCalendar();
        SetDateInTopPanel();
        SoundStatic.PlayMenuSound();
    }

    // Метод для переключения на предыдущий месяц
    public void OnPreviousMonth()
    {
        _currentDate = _currentDate.AddMonths(-1);
        GenerateCalendar();
        SetDateInTopPanel();
        SoundStatic.PlayMenuSound();
    }

    // Обновление информации в верхней панели календаря
    void SetDateInTopPanel()
    {
        if (calendarCanvas != null)
        {
            calendarCanvas.SetSelectedDate(_currentDate);
        }
        else
        {
            Debug.Log("Can't find CalendarCanvas.");
        }
    }
}