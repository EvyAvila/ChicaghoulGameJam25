using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Basket : PuzzleBase
{
    [SerializeField] private Scanner scan;

    //[SerializeField] private List<GameObject> items;

    private BasketCondition condition;

    [SerializeField] private GameObject Cover;

    private Rigidbody rb;

    /*void Start()
    {
        condition = GetComponent<BasketCondition>();
        rb = GetComponent<Rigidbody>();

        solveCondition = s => s.isCorrect();

        Scanner.itemObj += OnUpdateItem;

        Cover.SetActive(false);
    }*/

    IEnumerator Start()
    {
        condition = GetComponent<BasketCondition>();
        rb = GetComponent<Rigidbody>();
        solveCondition = s => s.isCorrect();
        Cover.SetActive(false);

        yield return null; 

        scan.itemObj += OnUpdateItem;
    }

    private void OnUpdateItem(ItemIdentifier i)
    {
        if (condition.AlreadyContainsItem(i))
            return;

        if(condition.CanFitIntoBasket(i))
        {
            if (solveCondition(condition))
            {
                SolvePuzzle();
            }
        }

        //condition.CanFitIntoBasket(i);
       
        
    }

    private void OnDisable()
    {
        scan.itemObj -= OnUpdateItem;
    }

    public void Complete()
    {
        Cover.SetActive(true);
    }    



}
