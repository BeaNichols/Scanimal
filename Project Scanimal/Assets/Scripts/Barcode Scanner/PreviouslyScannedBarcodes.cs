using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviouslyScannedBarcodes : MonoBehaviour
{
    #region Events
    public delegate void FailedScan();
    public static event FailedScan OnScanFail;

    public delegate void PassScan();
    public static event PassScan OnScanPass;
    #endregion

    public List<string> previousBarcodes;

    private void OnEnable()
    {
        ContinuousDemo.OnBarcodeScan += CheckCodeList;
    }

    private void OnDisable()
    {
        ContinuousDemo.OnBarcodeScan -= CheckCodeList;
    }

    private void Start()
    {
        Debug.Log(Application.persistentDataPath + "/Barcodes.data");
        BarcodeSaves settings = LocalSaveManager.LoadBarcodeSettings();
        if (settings != null)
        {
            foreach (var barcode in settings.Barcodes)
            {
                previousBarcodes.Add(barcode);
            }
        }
        else
        {
            LocalSaveManager.SaveBarcodeSettings(this);
        }
    }

    private void CheckCodeList(string codeToCheck)
    {
        if (!previousBarcodes.Contains(codeToCheck))
        {
            previousBarcodes.Add(codeToCheck);
            OnScanPass?.Invoke();
        }
        else if (previousBarcodes.Contains(codeToCheck))
        {
            OnScanFail?.Invoke();
        }

        LocalSaveManager.SaveBarcodeSettings(this);
    }
}
