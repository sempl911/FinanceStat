using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasCalls : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _callsTxt;
    [SerializeField] Animator _callsTxtAnim;
    int _dailyCallsCount = 0;
    public int DailyCallsCount { get => _dailyCallsCount; set => _dailyCallsCount = value; }

    private void Awake()
    {
    }

    private void Start()
    {
        _dailyCallsCount = DailyDataManager.CallsCount;
        _callsTxtAnim = _callsTxtAnim.GetComponent<Animator>();
    }

    private void Update()
    {
        if (_dailyCallsCount <= 0)
        {
            _dailyCallsCount = 0;
        }
        ShowCalls();
    }
    public void OnAddCallBtn()
    {
        _dailyCallsCount++;
        ShowTxtAnim();
        DailyDataManager.AddCall(_dailyCallsCount);
        SaveChange();
        SoundStatic.PlayBtnSound();
    }
    public void OnDellCall()
    {
        _dailyCallsCount--;
        ShowTxtAnim();
        if (_dailyCallsCount <= 0)
        {
            ShowTxtAnim();
            _dailyCallsCount = 0;
        }

        DailyDataManager.AddCall(_dailyCallsCount);
        SaveChange();
        SoundStatic.PlayBtnSound();
    }

    void ShowCalls()
    {
        _callsTxt.text = _dailyCallsCount.ToString();
    }
    void ShowTxtAnim()
    {
        if (_callsTxtAnim != null)
        {
            _callsTxtAnim.SetTrigger("MinValue");
        }
    }

    void SaveChange()
    {
        // DailyDataManager.SaveDataToJSON(DailyDataManager.GetFileName(gameObject));
        SQL_Loader sQL_Loader = gameObject.transform.parent.GetComponent<SQL_Loader>();
        sQL_Loader.UpdateCurrentRecord();
    }
}
