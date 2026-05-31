using UnityEngine;

public class DialRotator : MonoBehaviour
{
    [Header("다이얼 회전 설정")]
    public Vector3 rotationAxis = Vector3.right; // 돌릴 축 (기본 X축)
    public float rotationStep = 36f; // 한 번 클릭 시 돌아갈 각도

    // 인터페이스의 OnClickEvent에 연결할 public 함수입니다.
    public void Act_RotateDial(GameObject sender)
    {
        transform.Rotate(rotationAxis, rotationStep, Space.Self);
        Debug.Log($"{gameObject.name} 다이얼이 {sender?.name}에 의해 회전했습니다!");
    }
}