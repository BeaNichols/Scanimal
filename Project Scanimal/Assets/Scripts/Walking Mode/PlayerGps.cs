using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Android;

public class PlayerGps : MonoBehaviour
{
    public TextMeshProUGUI GPSStatus;
    public TextMeshProUGUI latitudeValue;
    public TextMeshProUGUI longitudeValue;
    public TextMeshProUGUI altitudeValue;
    public TextMeshProUGUI horizontalAccuracy;
    public TextMeshProUGUI timeStampValue;

    private void Start()
    {
        StartCoroutine(GPSLocation());
    }

    IEnumerator GPSLocation()
    {
        if (!Input.location.isEnabledByUser)
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            GPSStatus.text = "Timed Out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "Unable to find location";
            yield break;
        }
        else
        {
            //access granted
            GPSStatus.text = "Location Found";
            InvokeRepeating("UpdateGPSLocation", 0.5f, 1f);
        }
    }

    private void UpdateGPSLocation()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            //access granted 
            GPSStatus.text = "Location Found";
            latitudeValue.text = Input.location.lastData.latitude.ToString();
            longitudeValue.text = Input.location.lastData.longitude.ToString();
            altitudeValue.text = Input.location.lastData.altitude.ToString();
            horizontalAccuracy.text = Input.location.lastData.horizontalAccuracy.ToString();
            timeStampValue.text = Input.location.lastData.timestamp.ToString();
        }
        else 
        {
            //service stopped
            GPSStatus.text = "Location Stopped";
        }
    }


}
