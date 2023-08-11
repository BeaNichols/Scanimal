using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using TMPro;

public class BarcodeReader : MonoBehaviour
{
    [SerializeField]
    private RawImage imageBackground;
    [SerializeField]
    private AspectRatioFitter aspectRatioFitter;
    [SerializeField]
    private TextMeshProUGUI textOut;
    [SerializeField]
    private RectTransform scanZone;

    private bool isAvaible;
    private WebCamTexture camTexture;

    private ZXing.BarcodeReader barcodeReader;


    void Start()
    {
        SetUpCamera();

        barcodeReader = new ZXing.BarcodeReader();
        var barcodeReaderOptions = new DecodingOptions
        {
            TryHarder = true,          // Enable additional search algorithms for better accuracy
            PureBarcode= true,
            PossibleFormats = new[]    // Specify possible QR code formats to narrow down decoding attempts
            {
                BarcodeFormat.UPC_A,
                BarcodeFormat.UPC_E
            }
        };
        barcodeReader.Options = barcodeReaderOptions;
    }

    
    void Update()
    {
        UpdateCameraRender();
        if (isAvaible)
        {
            Scan();
        }
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        { 
            isAvaible = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                camTexture = new WebCamTexture(devices[i].name,(int) scanZone.rect.width, (int)scanZone.rect.height);
            }
            else
            {
                return;
            }

            camTexture.Play();
            imageBackground.texture = camTexture;
            isAvaible = true;
        }
    }

    public void OnClickScan()
    {
        if (isAvaible)
        {
            Scan();
        }
    }

    private void Scan()
    {
        try
        {
            if (camTexture.isPlaying)
            {
                var color32 = camTexture.GetPixels32();
                var result = barcodeReader.Decode(color32, camTexture.width, camTexture.height);
                if (result != null)
                {
                    textOut.text = result.Text;
                }
            }
        }
        catch
        {
            return;
        }
    }

    private void UpdateCameraRender()
    {
        if (isAvaible == false)
        {
            return;
        }
        float ratio = (float) camTexture.width/ (float)camTexture.height;
        aspectRatioFitter.aspectRatio = ratio;

        int orientation = -camTexture.videoRotationAngle;
        imageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }
}

