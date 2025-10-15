using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookBadgeSetter : MonoBehaviour
{
    [SerializeField] private TextMeshPro BadgeText;

    public void SetBadgeText(int num)
    {
        BadgeText.text = num.ToString();
    }
}
