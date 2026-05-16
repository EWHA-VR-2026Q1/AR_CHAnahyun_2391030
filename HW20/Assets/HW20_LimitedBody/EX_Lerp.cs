using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Lerp : MonoBehaviour, IInteractable
{
    public List<Transform> TargetPoints;
    public Transform NextPoint;
    private bool isLerping = false;
    private int index = 0;

    private void Awake()
    {
        if (NextPoint == null && TargetPoints.Count > 0)
        {
            NextPoint = TargetPoints[index];
        }
    }

    private void Update()
    {
        if (isLerping && NextPoint != null)
        {
            float dist = Vector3.Distance(transform.position, NextPoint.position);
            if (dist > 0.01f)
            {
                transform.position = Vector3.Lerp(transform.position, NextPoint.position, 0.03f);
            }
            else
            {
                transform.position = NextPoint.position;
                isLerping = false;
            }
        }
    }
    public void OnClick(GameObject sender)
    {
        if (TargetPoints.Count == 0) return;

        isLerping = true;
        index++;

        if (index >= TargetPoints.Count)
        {
            index = 0;
        }

        NextPoint = TargetPoints[index];
        Debug.Log($"Next Target: {NextPoint.name}");
    }

    public void OnEnter() { }
    public void OnExit() { }
    public void OnStay() { }
    public void OnClick() { } 
    public void OnEnter(GameObject sender) { }
    public void OnExit(GameObject sender) { }
    public void OnStay(GameObject sender) { }
}