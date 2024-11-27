using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClientDiagram : MonoBehaviour
{
    [SerializeField] Image _greenCircle;
    [SerializeField] Image _redCircle;
    [SerializeField] private float lerpSpeed = 2f;

    CanvasClients canvasClients;

    private float targetGreenFill;
    private float targetRedFill;
    private float leftPercentage;

    int _clientsCount = 0;
    int _devicesLeft = 0;
    bool _isLeftDevice;

    private void Awake()
    {

    }

    private void Start()
    {
        LoadCurrentClientsAndPercent();

        canvasClients = gameObject.transform.parent.GetComponent<CanvasClients>();

    }
    private void Update()
    {
        if (_clientsCount <= 0 && !DailyDataManager.CheckDate())
        {
            StartCircleConditions();
        }

        // Плавное заполнение зеленого круга
        _greenCircle.fillAmount = Mathf.Lerp(_greenCircle.fillAmount, targetGreenFill, Time.deltaTime * lerpSpeed);

        // Плавное заполнение красного круга (остаток)
        _redCircle.fillAmount = Mathf.Lerp(_redCircle.fillAmount, targetRedFill, Time.deltaTime * lerpSpeed);

    }
    public void OnLeftDevice()
    {
        _isLeftDevice=true;
        AddClient(_isLeftDevice);
        UpdateDiagram();
    }
    public void OnNoLeftDevice()
    {
       _isLeftDevice=false;
        AddClient(_isLeftDevice);
        UpdateDiagram();
    }

    void StartCircleConditions()
    {
        Image greenCircle = _greenCircle.GetComponent<Image>();
        Image redCircle = _redCircle.GetComponent<Image>();

        if (greenCircle != null && redCircle != null ) 
        {
            greenCircle.fillAmount = 0;
            redCircle.fillAmount = 0;
        }
    }


    void UpdateDiagram()
    {
        _clientsCount = DailyDataManager.ClientCount;

        if (_clientsCount > 0)
        {

            leftPercentage = (float)ClientTracker.GetLeftPercentage(_clientsCount, _devicesLeft) / 100;
            targetGreenFill = leftPercentage;
            targetRedFill = 1;
        }
       
    }

    void AddClient(bool isLeft)
    {
        _clientsCount++;

        if (isLeft)
        {
            _devicesLeft++;
        }

        ClientTracker.GetLeftPercentage(_clientsCount, _devicesLeft);
        ClientTracker.GetNoLeftPercentage(_clientsCount, _devicesLeft);
        DailyDataManager.AddClient(_clientsCount);
        DailyDataManager.AddDevicesLeft(_devicesLeft);
        canvasClients.CurrentClients = _clientsCount;
        canvasClients.ClientsLeftPercent = Mathf.FloorToInt((float) ClientTracker.GetLeftPercentage(_clientsCount, _devicesLeft));
    }

   public void LoadCurrentClientsAndPercent()
    {
        _clientsCount = DailyDataManager.ClientCount;
        _devicesLeft = DailyDataManager.DevicesLeft;
        leftPercentage = DailyDataManager.LeftDeviceCount;
        UpdateDiagram();
    }
}
