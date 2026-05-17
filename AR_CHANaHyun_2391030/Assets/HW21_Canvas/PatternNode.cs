using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PatternNode : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    public int nodeId;
    public PatternManager manager;

    public void OnPointerDown(PointerEventData eventData)
    {
        manager.StartPattern(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        manager.AddNode(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        manager.EndPattern();
    }
}