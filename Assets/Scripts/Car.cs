using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public TrafficLight UpcomingTrafficLight { get; private set; }
    
    private readonly List<TrafficLight> _lights = new();

    private void OnTriggerEnter(Collider other)
    {
        var trafficLight = other.GetComponentInParent<TrafficLight>();
        if (trafficLight == null) return;

        if (!_lights.Contains(trafficLight)) _lights.Add(trafficLight);

        UpdateUpcomingTrafficLight();
    }

    private void OnTriggerExit(Collider other)
    {
        var trafficLight = other.GetComponentInParent<TrafficLight>();
        _lights.Remove(trafficLight);

        UpdateUpcomingTrafficLight();
    }

    private void UpdateUpcomingTrafficLight()
    {
        if (_lights.Count == 0)
            UpcomingTrafficLight = null;
        else
            UpcomingTrafficLight = CalculateClosestTrafficLight(_lights);
    }

    private TrafficLight CalculateClosestTrafficLight(List<TrafficLight> trafficLights)
    {
        TrafficLight closestLight = null;
        var minDistance = Mathf.Infinity;

        foreach (var trafficLight in trafficLights)
        {
            if (trafficLight == null) continue;
            var distance = Vector3.Distance(transform.position, trafficLight.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestLight = trafficLight;
            }
        }

        return closestLight;
    }
}