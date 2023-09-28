using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;

    public string[] text;

    public float textspeed;

    private int index;
    private Boolean courotineRunning = false;
    
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (textMeshProUGUI.text == text[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            courotineRunning = false;
            textMeshProUGUI.text = text[index];
        }
    }

    public void startDialogue()
    {
        if (courotineRunning) return;
        courotineRunning = true;
        index = 0;
        textMeshProUGUI.text = String.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (var c in text[index].ToCharArray())
        {
            textMeshProUGUI.text += c;
            yield return new WaitForSeconds(textspeed);
        }
    }

    void NextLine()
    {
        if (index < text.Length - 1)
        {
            index++;
            textMeshProUGUI.text = String.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
