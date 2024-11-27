using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    [SerializeField] private GameObject _selectAllBtn;
    [SerializeField] private GameObject _selectNoneBtn;
    [SerializeField] GameObject _calendarGenerator;
    List<GameObject> selectedDays = new List<GameObject>();
    CalendarGenerator calendarGenerator;
    private bool _isSelectAll = false; 

    private void Start()
    {
        UpdateButtonStates();
        calendarGenerator = _calendarGenerator.GetComponent<CalendarGenerator>();
    }


    public void OnSelectAll()
    {
        if (!_isSelectAll) 
        {
            _isSelectAll = true;
            UpdateButtonStates();
            AddDays();
            SelectDays();
            SoundStatic.PlayMenuSound();
        }
    }

    public void OnSelectNone()
    {
        if (_isSelectAll) 
        {
            _isSelectAll = false;
            UpdateButtonStates();
            AddDays();
            DeselectDays();
            SoundStatic.PlayMenuSound();
        }
    }

    private void UpdateButtonStates()
    {
        _selectAllBtn.SetActive(!_isSelectAll); 
        _selectNoneBtn.SetActive(_isSelectAll); 
    }

   
    void AddDays()
    {
        selectedDays.Clear();
        for (int i = 0; i < _calendarGenerator.transform.childCount; i++)
        {
            GameObject day = _calendarGenerator.transform.GetChild(i).gameObject;
            selectedDays.Add(day);
        }
    }

    void SelectDays()
    {
        if (selectedDays.Count > 0 && selectedDays!= null)
        {
            foreach (GameObject day in selectedDays)
            {
                calendarGenerator.SelectDay(day);
            }
        }
    }

    void DeselectDays()
    {
        if (selectedDays.Count > 0 && selectedDays!= null)
        {
            foreach (GameObject day in selectedDays)
            {
                calendarGenerator.DeselectDay(day);
            }
        }
    }
}
