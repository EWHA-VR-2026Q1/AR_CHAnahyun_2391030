using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PatternNode : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public int nodeId; // 1~9
    public PatternManager manager;

    public void OnPointerDown(PointerEventData eventData)
    {
        manager.StartPattern(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        manager.AddNode(this);
    }
}