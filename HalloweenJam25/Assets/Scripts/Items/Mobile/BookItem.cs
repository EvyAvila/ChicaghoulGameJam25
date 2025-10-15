using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookItem : PluggableItem
{
    [SerializeField] private int value;
    [SerializeField] private TextMeshPro bookText;

    protected override void Start()
    {
        base.Start();
        if (bookText != null)
            bookText.text = $"{value}";
    }
    public int GetValue()
    {
        return value;
    }
}
