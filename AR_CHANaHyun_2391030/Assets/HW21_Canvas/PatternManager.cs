using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class PatternManager : MonoBehaviour
{
    public TextMeshProUGUI statusText; 
    public List<int> correctPattern = new List<int> { 1, 2, 3, 6, 9 }; // 설정할 비밀번호

    private List<int> currentPattern = new List<int>();
    private bool isDrawing = false;
    private List<Image> coloredNodes = new List<Image>();

    public void StartPattern(PatternNode node)
    {
        isDrawing = true;
        currentPattern.Clear();
        ResetNodesColor();
        AddNode(node);

        statusText.text = "입력 중..."; 
        statusText.color = Color.yellow;
    }

    public void AddNode(PatternNode node)
    {
        if (isDrawing && !currentPattern.Contains(node.nodeId))
        {
            currentPattern.Add(node.nodeId);

            Image nodeImage = node.GetComponent<Image>();
            nodeImage.color = Color.red;
            coloredNodes.Add(nodeImage);
        }
    }

    public void EndPattern()
    {
        isDrawing = false;
        CheckPattern();
    }

    void CheckPattern()
    {
        if (currentPattern.Count != correctPattern.Count)
        {
            Fail();
            return;
        }

        for (int i = 0; i < correctPattern.Count; i++)
        {
            if (currentPattern[i] != correctPattern[i])
            {
                Fail();
                return;
            }
        }

        statusText.text = "ACCESS \n COMPLETE";
        statusText.color = Color.green;
    }

    void Fail()
    {
        statusText.text = "ERROR: \n WRONG PATTERN";
        statusText.color = Color.red;
        Invoke("ResetNodesColor", 1f); 
    }

    void ResetNodesColor()
    {
        foreach (Image img in coloredNodes)
        {
            img.color = Color.white;
        }
        coloredNodes.Clear();
    }
}