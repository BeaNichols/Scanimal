using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
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


    void Start()
    {
        SetUpCamera();
    }

    
    void Update()
    {
        UpdateCameraRender();
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
        Scan();
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barCodeReader = new ZXing.BarcodeReader();
            Result result = barCodeReader.Decode(camTexture.GetPixels32(), camTexture.width,camTexture.height);
            if (result != null)
            {
                textOut.text = result.Text;
            }
            else 
            {
                textOut.text = "Failed To Read Code";
            }
        }
        catch
        {
            textOut.text = "Failed To Scan";
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
