using UnityEngine;
using UnityEngine.InputSystem;

public class Actor_LockManager : MonoBehaviour
{
    [Header("다이얼 3개 연결")]
    public Transform dial1;
    public Transform dial2;
    public Transform dial3;

    [Header("걸쇠(Shackle) 연결")]
    public Transform shackle;

    [Header("정답 세팅")]
    [Tooltip("검사할 회전 축을 선택하세요")]
    public Axis checkAxis = Axis.Z;
    public enum Axis { X, Y, Z }

    [Tooltip("마커가 일자로 맞을 때의 다이얼 각도")]
    public float targetAngle = 0f;

    [Tooltip("열릴 때 걸쇠가 위로 튀어오를 거리")]
    public Vector3 openOffset = new Vector3(0, 0.5f, 0);

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isUnlocked = false;

    private void Start()
    {
        if (shackle != null)
        {
            closedPosition = shackle.localPosition;
            openPosition = closedPosition + openOffset;
        }
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log($"[목표 정답 각도]: {targetAngle}도");
            Debug.Log($"[1번 다이얼] 현재: {GetAngle(dial1):F1}도 -> 일치함? {CheckDial(dial1)}");
            Debug.Log($"[2번 다이얼] 현재: {GetAngle(dial2):F1}도 -> 일치함? {CheckDial(dial2)}");
            Debug.Log($"[3번 다이얼] 현재: {GetAngle(dial3):F1}도 -> 일치함? {CheckDial(dial3)}");

            if (shackle == null) Debug.LogWarning("경고: Shackle(걸쇠) 오브젝트가 인스펙터에 연결되지 않았습니다!");
        }

        if (isUnlocked) return;

        if (CheckDial(dial1) && CheckDial(dial2) && CheckDial(dial3))
        {
            isUnlocked = true;
            if (shackle != null) shackle.localPosition = openPosition;
            Debug.Log("비밀번호 일치! 걸쇠가 열렸습니다!");
        }
    }

    private float GetAngle(Transform dial)
    {
        if (dial == null) return 0f;

        if (checkAxis == Axis.X) return dial.localEulerAngles.x;
        else if (checkAxis == Axis.Y) return dial.localEulerAngles.y;
        else return dial.localEulerAngles.z;
    }

    private bool CheckDial(Transform dial)
    {
        if (dial == null) return false;

        float currentAngle = GetAngle(dial);
        return Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) < 5f;
    }
}