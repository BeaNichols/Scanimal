using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using Wizcorp.Utils.Logger;

public class WalkingManager : MonoBehaviour
{
    #region Events
    public delegate void Move(float vel);
    public static event Move OnMove;
    #endregion

    private double previousLat;
    private double previousLon;
    private double currentLat;
    private double currentLon;

    private double distanceMoved;

    public TextMeshProUGUI distanceText;

    private void OnEnable()
    {
        PlayerGps.OnLocationChange += LocationChange;
    }

    private void OnDisable()
    {
        PlayerGps.OnLocationChange -= LocationChange;

    }

    private void Start()
    {
        previousLat = 0;
        previousLon = 0;

        currentLat = 0;
        currentLon = 0;

        OnMove?.Invoke(0.5f);
    }

    private void Update()
    {
 
    }

    private void LocationChange(double lat, double lon)
    {
        if (previousLat == 0 && previousLon == 0)
        {
            previousLat = lat;
            previousLon = lon;
        }
        else
        {
            previousLat = currentLat;
            previousLon = currentLon;
        }
        currentLat = lat;
        currentLon = lon;

        distanceMoved = distanceMoved + CalculateDistance(currentLat, currentLon, previousLat, previousLon);
        distanceText.text = distanceMoved.ToString() + " kilometers";
    }

    // Calculate the distance between two GPS coordinates using the Haversine formula
    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        double radius = 6371; // Earth's radius in kilometers

        // Convert latitude and longitude from degrees to radians
        double dLat = Mathf.Deg2Rad * (float)(lat2 - lat1);
        double dLon = Mathf.Deg2Rad * (float)(lon2 - lon1);

        // Haversine formula
        double a = Mathf.Sin((float)(dLat / 2)) * Mathf.Sin((float)(dLat / 2)) +
                   Mathf.Cos((float)(Mathf.Deg2Rad * lat1)) * Mathf.Cos((float)(Mathf.Deg2Rad * lat2)) *
                   Mathf.Sin((float)(dLon / 2)) * Mathf.Sin((float)(dLon / 2));

        double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt((float)(1 - a)));
        double distance = radius * c;

        return distance;
    }

    //private bool CalculateDifferance(double lat1, double lon1, double lat2, double lon2, float threshold)
    //{
    //    double latDifference = lat2 - lat1;
    //    double lonDifference = lon2 - lon1;

    //    // Check if the absolute differences are within the threshold
    //    if (Mathf.Abs((float)latDifference) < threshold && Mathf.Abs((float)lonDifference) < threshold)
    //    {
    //        Debug.Log("Location differences are within the threshold.");
    //        largeEnoughCheck.text = "too small";
    //        return false;
    //    }
    //    else
    //    {
    //        Debug.Log("Location differences are too large.");
    //        largeEnoughCheck.text = "Moved";
    //        return true;
    //    }
    //}
}
