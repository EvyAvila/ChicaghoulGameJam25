using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using static Yarn.Compiler.BasicBlock;

public class BasketCondition : PuzzleCondition
{
    [SerializeField] public List<ItemIdentifier> itemsEntered;
    //[SerializeField] private List<ItemIdentifier> requiredItems;

    [SerializeField] private int requiredAmount;
  
    private float getYPosition;

    private Vector3 targetPos;
    private float moveSpeed = 0.5f;

    private void Start()
    {
        requiredAmount = requiredAmount == 0 ? 2 : requiredAmount;
        getYPosition = transform.position.y;
    }

    public override bool isCorrect()
    {
        for(int i = 0; i < itemsEntered.Count; i++)
        {
            if(itemsEntered.Count < requiredAmount)
                return false;

            
            //bool hasItem = itemsEntered.Where(x => x.itemName == requiredItems[i].itemName).Any();            
            if (itemsEntered[i].itemWorth != item.Wealth) // || !hasItem)
            {
                itemsEntered.Clear();
                DumpItemsOut();
                MoveBasket(getYPosition);
                return false;
            }
        }

        return true;
    }

    

    public bool CanFitIntoBasket(ItemIdentifier i)
    {
        if(itemsEntered.Count < requiredAmount)
        {
            i.gameObject.GetComponent<GrabbableItem>().StopInteraction();
            itemsEntered.Add(i);
            MoveBasket(-0.65f);
            return true;
        }

        return false;
    }

    private void MoveBasket(float y)
    {
        targetPos = new Vector3(transform.position.x, y, transform.position.z);
        InvokeRepeating(nameof(MoveStep), 0f, Time.deltaTime);
        Invoke(nameof(StopMove), 1f);

    }

    private void MoveStep() 
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    private void StopMove() 
    {
        CancelInvoke(nameof(MoveStep));
    }

    private void DumpItemsOut()
    {
        Vector3 v = new Vector3(150f, 0, 0);
        transform.DOLocalRotate(v, 2f).OnComplete(() =>
        {
            transform.DOLocalRotate(Vector3.zero, 2f);
        });
    }
}
