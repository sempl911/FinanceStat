using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;

public class ProfitPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField _profitField;
    [SerializeField] TextMeshProUGUI _summaryMoney;
    [SerializeField] TextMeshProUGUI _profitTxt;
    [SerializeField] TextMeshProUGUI _spendTxt;
    [SerializeField] TextMeshProUGUI _profitPercentage;
    [SerializeField] TextMeshProUGUI _spendPercentage;
    [SerializeField] Image _redCircle;
    [SerializeField] Image _greenCircle;

    float totalMoney = 0;
    float addedAmount = 0;
    float profitAmount = 0;
    float spendAmount = 0;

    float currentFillAmount = 0f;  // Текущее значение fillAmount
    float targetFillAmount = 0f;   // Целевое значение fillAmount
    public float lerpSpeed = 1f;   // Скорость изменения fillAmount

    private void Start()
    {
        if (DailyDataManager.CheckDate(System.DateTime.Now.Date))
        {
            LoadProfitData(DailyDataManager.Profit, DailyDataManager.Spend, DailyDataManager.TotalPerDay);
            Debug.Log("Date is equal");
        }
        else
        {
            StartCircleConditions();
            Debug.Log("Date no equal");
        }
       
    }

    void Update()
    {
        // Плавное изменение текущего значения fillAmount в сторону целевого
        currentFillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, Time.deltaTime * lerpSpeed);

        // Применяем это значение к закраске круга
        _greenCircle.fillAmount = currentFillAmount;
        
    }

    public void OnAddMoney()
    {
        if(GetAddedAmount()) // Если значение корректное
        {
            totalMoney += addedAmount;
            profitAmount += addedAmount;
        }
        else
        {
            Debug.LogWarning("Некорректное значение прибыли");
            return;
        }
        _redCircle.fillAmount = 1;
        ShowDaily();
        ShowSummary();
        UpdateProfitCircle();
        SoundStatic.PlayBtnSound();
    }
    public void OnDelMoney()
    {
        if (GetAddedAmount()) // Если значение корректное
        {
            totalMoney -= addedAmount; // Убираем сумму из общего числа
            spendAmount += addedAmount;
        }
        else
        {
            Debug.LogWarning("Некорректное значение прибыли");
            return;
        }
        _redCircle.fillAmount = 1;

        ShowDaily();
        ShowSummary();
        UpdateProfitCircle();
        SoundStatic.PlayBtnSound();
    }

    void UpdateProfitCircle()
    {
        if (_greenCircle != null && _redCircle != null)
        {
            float spendPercentage = (spendAmount/profitAmount) * 100;

            float profitPercentage = (totalMoney / profitAmount) * 100;

            if (profitPercentage < 0)
            {
                profitPercentage = 0;
                spendPercentage = -spendPercentage;
            }

            targetFillAmount = profitPercentage / 100;

            SaveProfitDataPercent(profitPercentage, spendPercentage);
            ShowPercentage();
        }

        SaveChange();
    }
    void ShowPercentage()
    {
        _profitPercentage.text = DailyDataManager.ProfitInPercent.ToString();
        _spendPercentage.text =  DailyDataManager.SpendInPercent.ToString();
    }
    void ShowDaily()
    {
        if (_profitTxt != null && _spendTxt != null)
        {
            _profitTxt.text = "Profit " + Mathf.FloorToInt((float)profitAmount);
            _spendTxt.text = "Spend " + Mathf.FloorToInt((float)spendAmount);
            SaveProfitDataMoney(profitAmount, spendAmount);
        }
    }
    void ShowSummary()
    {
        if (_summaryMoney != null)
        {
            _summaryMoney.text = "Summary: " + totalMoney.ToString();
        }

        DailyDataManager.AddTotalMoney(totalMoney);
    }
    bool GetAddedAmount()
    {
        if (string.IsNullOrEmpty(_profitField.text))
        {
         return false; 
        }

        bool isValid = float.TryParse(_profitField.text, out addedAmount);
        _profitField.text = null; 
        return isValid; // Возвращаем результат парсинга
    }

    void StartCircleConditions()
    { 
        _redCircle.GetComponent<Image>().fillAmount = 0;
        _greenCircle.GetComponent<Image>().fillAmount = 0;
        _profitPercentage.text = "0";
        _spendPercentage.text = "0";
    }

    void SaveProfitDataPercent(float profitPercent, float spendPercent)
    {
        DailyDataManager.AddProfitPercent(Mathf.FloorToInt((float) profitPercent));
        DailyDataManager.AddSpendPercent(Mathf.FloorToInt((float) spendPercent));
    }
    void SaveProfitDataMoney(float profit, float spend)
    {
        DailyDataManager.AddProfit(profit);
        DailyDataManager.AddSpend(spend);
    }
    public void LoadProfitData(float profitCurrentAmount, float spendCurrentAmount, float moneyPerDay)
    {
        profitAmount = profitCurrentAmount;
        spendAmount = spendCurrentAmount;
        totalMoney = moneyPerDay;

        if (profitAmount == 0 && spendAmount == 0 && totalMoney == 0)
        {
            StartCircleConditions();
        }
        else
        {
            ShowSummary();
            ShowDaily();
            UpdateProfitCircle();
        }

    }

    void SaveChange()
    {
        SQL_Loader sQL_Loader = gameObject.transform.parent.GetComponent<SQL_Loader>();
        sQL_Loader.UpdateCurrentRecord();
    }
}
