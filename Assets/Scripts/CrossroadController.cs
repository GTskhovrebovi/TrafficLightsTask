using System.Collections.Generic;
using UnityEngine;

public class CrossroadController : MonoBehaviour
{
    [SerializeField] private List<TrafficLight> firstGroup;
    [SerializeField] private List<TrafficLight> secondGroup;
    [SerializeField] private TrafficLightDurationSettings trafficLightDurationSettings;

    private void Update()
    {
        UpdateGroups();
    }

    private void UpdateGroups()
    {
        var greenDuration = trafficLightDurationSettings.GetLightStateData(TrafficLightState.Green).Duration;
        var yellowDuration = trafficLightDurationSettings.GetLightStateData(TrafficLightState.Yellow).Duration;
        var redDuration = trafficLightDurationSettings.GetLightStateData(TrafficLightState.Red).Duration;
        var redYellowDuration = trafficLightDurationSettings.GetLightStateData(TrafficLightState.RedYellow).Duration;

        var cycleDuration = greenDuration + yellowDuration + redDuration + redYellowDuration;

        // shift second group by half of the cycle duration so the traffic lights show opposite states
        var firstGroupState = CalculateState(Time.time, greenDuration, yellowDuration, redDuration, cycleDuration);
        var secondGroupState = CalculateState(Time.time + cycleDuration / 2, greenDuration, yellowDuration, redDuration,
            cycleDuration);

        foreach (var trafficLight in firstGroup) trafficLight.SetState(firstGroupState);
        foreach (var trafficLight in secondGroup) trafficLight.SetState(secondGroupState);
    }

    private TrafficLightState CalculateState(float currentTime, float greenLightDuration, float yellowLightDuration,
        float redLightDuration, float cycleDuration)
    {
        var cycleTime = currentTime % cycleDuration;

        if (cycleTime < greenLightDuration)
            return TrafficLightState.Green;
        if (cycleTime < greenLightDuration + yellowLightDuration)
            return TrafficLightState.Yellow;
        if (cycleTime < greenLightDuration + yellowLightDuration + redLightDuration)
            return TrafficLightState.Red;
        return TrafficLightState.RedYellow;
    }
}