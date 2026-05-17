using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Header("캔버스 오브젝트 연결 (ON/OFF 용)")]
    public GameObject overlayCanvas;
    public GameObject cameraCanvas;
    public GameObject worldCanvas;

    [Header("텍스트 연결")]
    public TextMeshProUGUI recText;
    public TextMeshProUGUI scanText;

    [Header("World Space Canvas (Password)")]
    public TextMeshProUGUI passwordText;

    void Start()
    {
        if (worldCanvas != null)
        {
            worldCanvas.SetActive(false);
        }
    }

    // 1. Overlay 
    public void OnClickStop()
    {
        if (overlayCanvas != null) overlayCanvas.SetActive(false);
        if (cameraCanvas != null) cameraCanvas.SetActive(false);
        if (worldCanvas != null) worldCanvas.SetActive(false);
    }

    // 2. Camera 
    public void OnClickScan()
    {
        scanText.text = "[  +  ]";
        scanText.color = Color.red;

        if (worldCanvas != null)
        {
            worldCanvas.SetActive(true);
        }

        Invoke("HideScanText", 1.5f);
    }
    void HideScanText()
    {
        if (cameraCanvas != null) cameraCanvas.SetActive(false);
    }
    // 3. World Space 
    public void OnClickUnlock()
    {
        passwordText.text = "ACCESS GRANTED";
        passwordText.color = Color.green;
    }
}