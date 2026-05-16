using UnityEngine;
using UnityEngine.InputSystem;

public class LerpPoints : MonoBehaviour, IInteractable
{
    private Camera mainCamera;
    private bool isDragging = false;
    private float zDistance;
    private Vector3 offset;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnClick(GameObject sender)
    {
        var pointer = Pointer.current;
        if (pointer == null) return;

        Vector2 screenPos = pointer.position.ReadValue();
        isDragging = true;

        zDistance = mainCamera.WorldToScreenPoint(transform.position).z;
        Vector3 clickWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, zDistance));

        if (transform.parent != null)
            offset = transform.localPosition - transform.parent.InverseTransformPoint(clickWorldPos);
        else
            offset = transform.position - clickWorldPos;

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
            Vector3 mousePoint = new Vector3(screenPos.x, screenPos.y, zDistance);
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(mousePoint);

            transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
        }
    }

    public void OnEnter() { }
    public void OnExit() { }
    public void OnStay() { }
    public void OnClick() { }
    public void OnEnter(GameObject sender) { }
    public void OnExit(GameObject sender) { }
    public void OnStay(GameObject sender) { }
}