using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfCondition : PuzzleCondition
{
    [SerializeField] private int[] valueOrder;
    [SerializeField] private BookBadgeSetter[] BookBadges;
    [SerializeField] private PuzzleSocket[] sockets;

    private void Start()
    {
        for (int i = 0; i < valueOrder.Length; i++)
        {
            BookBadges[i].SetBadgeText(valueOrder[i]);
        }
    }
    public override bool isCorrect()
    {
        for (int i = 0; i < valueOrder.Length; i++)
        {
            GameObject obj = sockets[i].GetPluggedItem();
            if (obj == null)
                return false;

            if (obj.GetComponent<BookItem>().GetValue() != valueOrder[i])
                return false;
        }

        return true;
    }
}
