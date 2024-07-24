using TMPro;
using UnityEngine;

public class TrafficLightUI : MonoBehaviour
{
    [SerializeField] private Transform hud;
    [SerializeField] private Car car;
    [SerializeField] private TMP_Text signalDescription;
    [SerializeField] private GameObject resultObject;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private float resultTextDisplayDuration;
    [SerializeField] private TrafficLightDurationSettings trafficLightDurationSettings;

    private float _lastResultDisplayTime = Mathf.NegativeInfinity;

    private void Update()
    {
        UpdateUI();
        UpdateResultState();
    }

    private void UpdateResultState()
    {
        if (Time.time >= _lastResultDisplayTime + resultTextDisplayDuration) HideResult();
    }

    private void UpdateUI()
    {
        if (car.UpcomingTrafficLight == null)
        {
            hud.gameObject.SetActive(false);
        }
        else
        {
            var trafficLightStateData = trafficLightDurationSettings.GetLightStateData(car.UpcomingTrafficLight.TrafficLightState);
            if (trafficLightStateData == null) return;

            signalDescription.text = trafficLightStateData.Description;

            hud.gameObject.SetActive(true);
        }
    }

    public void HandlePress()
    {
        if (car.UpcomingTrafficLight == null) return;

        var trafficLightStateData = trafficLightDurationSettings.GetLightStateData(car.UpcomingTrafficLight.TrafficLightState);
        if (trafficLightStateData == null) return;
        
        ShowResult(trafficLightStateData.CanDrive);
    }

    private void ShowResult(bool canDrive)
    {
        resultText.text = canDrive ? "Can Drive" : "Can't Drive";
        resultText.color = canDrive ? Color.green : Color.red;
        resultObject.gameObject.SetActive(true);
        _lastResultDisplayTime = Time.time;
    }

    private void HideResult()
    {
        resultObject.gameObject.SetActive(false);
    }
}