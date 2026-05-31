using UnityEngine;
using UnityEngine.InputSystem;

public class PC_RaycastInteractor : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Camera.main == null) return;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                InterfaceBase_IInteractable interactable = hit.collider.GetComponent<InterfaceBase_IInteractable>();

                if (interactable != null)
                {
                    interactable.OnClick(gameObject);
                }
            }
        }
    }
}