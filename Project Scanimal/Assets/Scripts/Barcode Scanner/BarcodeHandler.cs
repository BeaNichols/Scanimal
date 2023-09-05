using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PlayerStateManager;

public class BarcodeHandler : MonoBehaviour
{
    #region Events
    public delegate void ScanPass();
    public static event ScanPass OnScanPass;
    #endregion

    public TextMeshProUGUI TextHeader;
    public ItemSO item;

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
        OnScanPass?.Invoke();
    }

}
