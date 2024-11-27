using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallendarCellProp : MonoBehaviour
{
    public bool IsSelected { get; private set; } = false; // Состояние выделения
    private Image _image;
    String _currentDayDate;
    public String CurrentDayDate { get => _currentDayDate; set => _currentDayDate = value; }
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    // Метод для установки состояния ячейки
    public void SetSelection(bool isSelected)
    {
        IsSelected = isSelected;
        UpdateVisualState();
    }

    // Обновление визуального состояния ячейки
    private void UpdateVisualState()
    {
        if (_image != null)
        {
            if (IsSelected)
            {
                // Выделенная ячейка
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
            }
            else
            {
                // Невыделенная ячейка
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.2f);
            }
        }
    }
}
