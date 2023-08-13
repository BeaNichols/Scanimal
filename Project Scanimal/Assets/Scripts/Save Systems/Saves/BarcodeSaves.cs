using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BarcodeSaves
{
    public List<string> Barcodes;

    public BarcodeSaves(PreviouslyScannedBarcodes codes)
    {
        Barcodes = codes.previousBarcodes;
    }
}
