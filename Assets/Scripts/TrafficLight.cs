using System;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private GameObject greenLight;
    [SerializeField] private GameObject yellowLight;
    [SerializeField] private GameObject redLight;

    public TrafficLightState TrafficLightState { get; private set; }

    public void SetState(TrafficLightState state)
    {
        TrafficLightState = state;
        switch (TrafficLightState)
        {
            case TrafficLightState.Green:
                greenLight.gameObject.SetActive(true);
                yellowLight.gameObject.SetActive(false);
                redLight.gameObject.SetActive(false);
                break;
            case TrafficLightState.Yellow:
                greenLight.gameObject.SetActive(false);
                yellowLight.gameObject.SetActive(true);
                redLight.gameObject.SetActive(false);
                break;
            case TrafficLightState.Red:
                greenLight.gameObject.SetActive(false);
                yellowLight.gameObject.SetActive(false);
                redLight.gameObject.SetActive(true);
                break;
            case TrafficLightState.RedYellow:
                greenLight.gameObject.SetActive(false);
                yellowLight.gameObject.SetActive(true);
                redLight.gameObject.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum TrafficLightState
{
    Green,
    Yellow,
    Red,
    RedYellow
}