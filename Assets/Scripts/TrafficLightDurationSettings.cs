using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TrafficLightDurationSettings", menuName = "ScriptableObjects/TrafficLightDurationSettings", order = 1)]
public class TrafficLightDurationSettings : ScriptableObject
{
    [SerializeField] private List<TrafficLightStateData> data;

    public TrafficLightStateData GetLightStateData(TrafficLightState lightState)
    {
        return data.First(i => i.TrafficLightState == lightState);
    }
}

[Serializable]
public class TrafficLightStateData
{
    [field: SerializeField] public TrafficLightState TrafficLightState { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public bool CanDrive { get; private set; }
    [field: SerializeField, TextArea] public string Description { get; private set; }
}