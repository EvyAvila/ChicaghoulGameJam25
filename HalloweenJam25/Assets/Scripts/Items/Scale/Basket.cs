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

        yield return null; // wait 1 frame

        Scanner.itemObj += OnUpdateItem;
    }

    private void OnUpdateItem(ItemIdentifier i)
    {
        condition.CanFitIntoBasket(i);
        if (solveCondition(condition))
        {
            SolvePuzzle();
        }
        else if(condition.isIncorrect)
        {
            //Dump out
            Vector3 v = new Vector3(120f, 0, 0);
            transform.DOLocalRotate(v, 2f).OnComplete(() =>
            {
                transform.DOLocalRotate(Vector3.zero, 2f).OnComplete(() => 
                { condition.isIncorrect = false; });
            });            
        }    
    }

    private void OnDisable()
    {
        Scanner.itemObj -= OnUpdateItem;
    }

    public void Complete()
    {
        Cover.SetActive(true);
        Debug.Log("Basket is fulled and correct");
    }    



}
