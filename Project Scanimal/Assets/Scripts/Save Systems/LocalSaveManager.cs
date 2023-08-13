using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LocalSaveManager : MonoBehaviour
{
    public static void SaveBarcodeSettings(PreviouslyScannedBarcodes Codes)
    {
        BinaryFormatter SaveFormatter = new BinaryFormatter();
        string SavePath = Application.persistentDataPath + "/Barcodes.data";
        FileStream fileStream = new FileStream(SavePath, FileMode.Create);

        BarcodeSaves settings = new BarcodeSaves(Codes);


        SaveFormatter.Serialize(fileStream, settings);
        fileStream.Close();
    }

    public static BarcodeSaves LoadBarcodeSettings()
    {
        string SavePath = Application.persistentDataPath + "/Barcodes.data";
        if (File.Exists(SavePath))
        {
            BinaryFormatter SaveFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(SavePath, FileMode.Open);

            BarcodeSaves Settings = SaveFormatter.Deserialize(fileStream) as BarcodeSaves;
            fileStream.Close();
            return Settings;
        }
        else
        {
            // Debug.Log("Barcodes save not found");
            return null;
        }
    }

}
