using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasDate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dateField;
    private float _updateInterval = 1.0f; // Time update in seconds
    string currentDate;

    private void Start()
    {
        StartCoroutine(TimeUpdateCoroutine());
    }

    private void Update()
    {
            if (_dateField != null && currentDate != null)
            {
                _dateField.text = currentDate;
            }
    }
    public void OnSaveData()
    {
        SQL_Loader sQL_Loader = gameObject.transform.parent.GetComponent<SQL_Loader>();
        sQL_Loader.UpdateCurrentRecord();
    }

    IEnumerator TimeUpdateCoroutine()
    {
        while (true)
        {
            currentDate = System.DateTime.Now.ToString("dd-MM-yy HH:mm:ss");

            yield return new WaitForSeconds(_updateInterval);
        }
    }
}
