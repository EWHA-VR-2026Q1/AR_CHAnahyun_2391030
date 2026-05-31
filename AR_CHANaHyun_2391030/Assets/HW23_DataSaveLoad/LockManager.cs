using UnityEngine;
using UnityEngine.InputSystem;

public class LockManager : MonoBehaviour
{
    [Header("다이얼 3개 연결")]
    public Transform dial1;
    public Transform dial2;
    public Transform dial3;

    [Header("걸쇠(Shackle) 연결")]
    public Transform shackle;

    [Header("정답 세팅")]
    public Axis checkAxis = Axis.Z;
    public enum Axis { X, Y, Z }

    public float targetAngle = 0f;
    public Vector3 openOffset = new Vector3(0, 0.5f, 0); 
    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool wasAllTrueLastFrame = false;

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
            Debug.Log($"[목표]: {targetAngle}도 | [현재] 1번:{GetAngle(dial1):F1} / 2번:{GetAngle(dial2):F1} / 3번:{GetAngle(dial3):F1}");
        }

        bool isAllTrueNow = CheckDial(dial1) && CheckDial(dial2) && CheckDial(dial3);

        if (isAllTrueNow && !wasAllTrueLastFrame)
        {
            if (shackle != null) shackle.localPosition = openPosition;
            Debug.Log("비밀번호 일치! 걸쇠가 위로 열렸습니다!");
        }
        else if (!isAllTrueNow && wasAllTrueLastFrame)
        {
            if (shackle != null) shackle.localPosition = closedPosition;
            Debug.Log("다이얼이 틀어져서 자물쇠가 다시 닫혔습니다!");
        }

        wasAllTrueLastFrame = isAllTrueNow;
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