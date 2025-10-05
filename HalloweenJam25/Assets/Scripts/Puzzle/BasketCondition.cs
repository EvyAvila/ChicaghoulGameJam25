using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BasketCondition : PuzzleCondition
{
    [SerializeField] public List<ItemIdentifier> itemsEntered;
    [SerializeField] private List<ItemIdentifier> requiredItems;

    private int requiredAmount;
  
    public bool isIncorrect { get; set; }

    private void Start()
    {
        requiredAmount = requiredItems.Count;
        isIncorrect = false;
    }

    public override bool isCorrect()
    {
        for(int i = 0; i < itemsEntered.Count; i++)
        {
            if(itemsEntered.Count < requiredAmount)
                return false;

            
            bool hasItem = itemsEntered.Where(x => x.itemName == requiredItems[i].itemName).Any();            
            if (itemsEntered[i].itemWorth != item.Wealth || !hasItem)
            {
                Debug.Log("something wasn't worth wealthy");
                itemsEntered.Clear();
                isIncorrect = true;

                return false;
            }
        }

        return true;
    }

    

    public bool CanFitIntoBasket(ItemIdentifier i)
    {
        if(itemsEntered.Count < requiredAmount)
        {
            itemsEntered.Add(i);
            return true;
        }

        return false;
    }
}
