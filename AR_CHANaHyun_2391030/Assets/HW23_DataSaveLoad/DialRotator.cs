using UnityEngine;

public class DialRotator : MonoBehaviour
{
    [Header("회전 설정")]
    public float rotateSpeed = 15f; // 마우스 드래그 시 회전하는 속도
    public Vector3 rotationAxis = Vector3.right; // 어떤 축으로 돌릴 것인가 (기본 X축)

    // 마우스를 다이얼에 올리고 클릭한 채로 드래그할 때마다 실행됨
    private void OnMouseDrag()
    {
        // 마우스를 위아래(Y방향)로 드래그한 변화량을 가져옴
        float dragY = Input.GetAxis("Mouse Y");

        // 제자리에서 회전 
        transform.Rotate(rotationAxis, dragY * rotateSpeed, Space.Self);
    }
}