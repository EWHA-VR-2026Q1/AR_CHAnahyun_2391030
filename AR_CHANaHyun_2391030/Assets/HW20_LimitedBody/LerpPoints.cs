using UnityEngine;
using UnityEngine.InputSystem;

public class LerpPoints : MonoBehaviour, IInteractable
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;
    private Plane dragPlane;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnClick(GameObject sender)
    {
        var pointer = Pointer.current;
        if (pointer == null) return;

        isDragging = true;

        dragPlane = new Plane(transform.up, transform.position);

        Vector2 screenPos = pointer.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(screenPos);

        if (dragPlane.Raycast(ray, out float enter))
        {
            Vector3 clickWorldPos = ray.GetPoint(enter);
            offset = transform.position - clickWorldPos;
        }

        Renderer rend = GetComponentInChildren<Renderer>();
        if (rend != null) rend.material.color = Color.red;
    }

    void Update()
    {
        var pointer = Pointer.current;
        if (pointer == null) return;

        if (isDragging && pointer.press.wasReleasedThisFrame)
        {
            isDragging = false;
            Renderer rend = GetComponentInChildren<Renderer>();
            if (rend != null) rend.material.color = Color.white;
        }

        if (isDragging)
        {
            Vector2 screenPos = pointer.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(screenPos);

            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 targetPos = ray.GetPoint(enter) + offset;

                transform.position = targetPos;
            }
        }
    }

    // --- 인터페이스 필수 구현부 ---
    public void OnEnter() { }
    public void OnExit() { }
    public void OnStay() { }
    public void OnClick() { }
    public void OnEnter(GameObject sender) { }
    public void OnExit(GameObject sender) { }
    public void OnStay(GameObject sender) { }
}