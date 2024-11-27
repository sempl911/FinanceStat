using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasClients : MonoBehaviour
{
    [SerializeField] GameObject _askWindow;
    [SerializeField] TextMeshProUGUI _clientsCountTxt;
    [SerializeField] TextMeshProUGUI _clientPercentage;
    [SerializeField] Animator _currentClientsTxtAnimator;
    [SerializeField] GameObject _clientDiagram;

    Animator _askAnimator;
    int _currentClients;
    int _clientsLeftPercent;

    public int CurrentClients { get => _currentClients; set => _currentClients = value; }
    public int ClientsLeftPercent { get => _clientsLeftPercent; set => _clientsLeftPercent = value; }

    private void Awake()
    {
    }

    private void Start()
    {
        LoadCurrentData();
        _askAnimator = _askWindow.GetComponent<Animator>();
    }

    public void LoadCurrentData()
    {
        _currentClients = DailyDataManager.ClientCount;
        _clientsLeftPercent = DailyDataManager.LeftDeviceCount; //Percentage
    }

    private void Update()
    {
        ClientsTxt();
        ShowClientsLeftPercentage();
    }

    void ShowClientsLeftPercentage()
    {
        if (_clientPercentage != null)
        {
            _clientPercentage.text = _clientsLeftPercent.ToString() + "%";
        }
    }

    public void LeftBtn()
    {
        ConfirmClientAsk();
        SoundStatic.PlayBtnSound();
    }
    public void NoLeftBtn()
    {
        ConfirmClientAsk();
        SoundStatic.PlayBtnSound();
    }

    public void OnAddClientBtn()
    {
        Invoke("ShowAskWindow", .1f);
        AnimTxtCurrentClients();
        SoundStatic.PlayBtnSound();
    }

    void ShowAskWindow()
    {
        if (_askAnimator != null)
        {
            _askAnimator.SetTrigger("AnimStart");
        }
    }

    void ClientsTxt()
    {
        _clientsCountTxt.text = _currentClients.ToString();
    }

    void AnimTxtCurrentClients()
    {
        if (_currentClientsTxtAnimator)
        {
            _currentClientsTxtAnimator.SetTrigger("AddAnim");
        }
    }

    void ConfirmClientAsk()
    {
        if (_askAnimator != null)
        {
            _askAnimator.SetTrigger("AnimEnd");
            SaveChange();
        }
    }

    void SaveChange()
    {
        SQL_Loader sQL_Loader = gameObject.transform.parent.GetComponent<SQL_Loader>();
        sQL_Loader.UpdateCurrentRecord();
    }
}
