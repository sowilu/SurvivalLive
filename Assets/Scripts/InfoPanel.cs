using System;
using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public static InfoPanel instance;
    
    private TextMeshProUGUI[] textBoxes;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Debug.LogWarning("More than one InfoPanel in scene.");
    }

    void Start()
    {
        textBoxes = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in textBoxes)
        {
            text.text = "";
        }
    }

    public void ShowInfo(string infoText)
    {
        foreach (var t in textBoxes)
        {
            if (t.text == "")
            {
                t.text = infoText;
                //t.transform.SetSiblingIndex(0);
                t.transform.SetSiblingIndex(transform.childCount - 1);
                return;
            }
        }
    }
}
