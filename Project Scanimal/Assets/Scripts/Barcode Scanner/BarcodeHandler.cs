using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarcodeHandler : MonoBehaviour
{
    public TextMeshProUGUI TextHeader;

    private void OnEnable()
    {
        PreviouslyScannedBarcodes.OnScanFail += ScanFailed;
        PreviouslyScannedBarcodes.OnScanPass += ScanPassed;
    }

    private void OnDisable()
    {
        PreviouslyScannedBarcodes.OnScanFail -= ScanFailed;
        PreviouslyScannedBarcodes.OnScanPass -= ScanPassed;
    }

    private void ScanFailed()
    {
        TextHeader.text = "Invalid Scan";
    }

    private void ScanPassed()
    {
        TextHeader.text = "Found Scan";
    }

}
