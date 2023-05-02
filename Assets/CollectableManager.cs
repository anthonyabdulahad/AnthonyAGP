using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CollectableManager : MonoBehaviour
{
    public TMP_Text text;
    int count = 0;

    private void Start()
    {
        UpdateText();
    }

    internal void Pickup()
    {
        count++;
        UpdateText();
    }

    void UpdateText()
    {
        text.text = $"{count}";
    }
}
