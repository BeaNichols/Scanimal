using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviouslyScannedBarcodes : MonoBehaviour
{
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

    private bool CheckCodeList(string codeToCheck)
    {
        foreach (var barcode in previousBarcodes)
        {
            if (barcode == codeToCheck)
            {
                previousBarcodes.Add(codeToCheck);
                return true;
            }
        }
        return false;
    }

}
