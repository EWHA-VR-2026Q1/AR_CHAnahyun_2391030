using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [Header("Overlay Canvas (REC)")]
    public TextMeshProUGUI recText;

    [Header("Camera Canvas (Scanner)")]
    public TextMeshProUGUI scanText;

    [Header("World Space Canvas (Password)")]
    public TextMeshProUGUI passwordText;

    // 1. Overlay 
    public void OnClickStopRecord()
    {
        recText.text = "■ STOP";
        recText.color = Color.white;
    }

    // 2. Camera 
    public void OnClickScan()
    {
        scanText.text = "[  +  ] 락온 완료";
        scanText.color = Color.red;
    }

    // 3. World Space 
    public void OnClickUnlock()
    {
        passwordText.text = "ACCESS GRANTED";
        passwordText.color = Color.green;
    }
}