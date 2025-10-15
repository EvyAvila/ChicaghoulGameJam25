using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;
using DG.Tweening;

public class Cauldron : MonoBehaviour
{
    /// <summary>
    /// How far the raycast will look for the potions
    /// </summary>
    [SerializeField] private float distance;
    
    /// <summary>
    /// Looking at the direction for raycast and assign up
    /// </summary>
    private Vector3 direction;

 
    public PotionColor potionColor { get; private set; }

    public static event Action<PotionColor> selected;

    public static List<PotionColor> enteredColors;

    [SerializeField] private int waiting;
    public int wait { get { return waiting; } private set { waiting = value; } }

    public bool isSolved { get; private set; }

    public bool isPouring { get; private set; }

    private PotionItem pot;

    void Start()
    {
        distance = distance == 0 ? 3 : distance;
        direction = Vector3.up;

        CheckPotion(0);

        isSolved = false;
    }

    private void OnEnable()
    {
        Rotator.rotated += CheckPotion;
    }

    private void OnDisable()
    {
        Rotator.rotated -= CheckPotion;
    }

    public void CheckPotion(float degress)
    {
        RaycastHit info;
        Debug.DrawRay(transform.position, direction, Color.green);

        if(Physics.Raycast(transform.position, direction, out info, distance))
        {
            pot = info.collider.GetComponent<PotionItem>();

            potionColor = pot.p.color;
        }
    }

    public void GetPotion()
    {
        
        selected?.Invoke(potionColor);
        pot.PlayParticle();


        StartCoroutine(Timer(waiting));
    }

    public void Completed()
    {
        isSolved = true;
    }
   

    IEnumerator Timer(float wait)
    {
        isPouring = true;
        yield return new WaitForSeconds(wait);
        isPouring = false;
        pot.StopParticle();
        StopAllCoroutines();
    }    
}
