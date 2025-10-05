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

    [SerializeField] private GameObject PartSystem;
    private ParticleSystem ps; // temp

    public PotionColor potionColor { get; private set; }

    public static event Action<PotionColor> selected;

    //[SerializeField] 
    public static List<PotionColor> enteredColors;

    [SerializeField] private int waiting;
    public int wait { get { return waiting; } private set { waiting = value; } }

    public bool isSolved { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        distance = distance == 0 ? 3 : distance;
        direction = Vector3.up;

        CheckPotion(0);

        ps = PartSystem.GetComponent<ParticleSystem>();

        ps.Stop();

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
            PotionItem pot = info.collider.GetComponent<PotionItem>();

            potionColor = pot.p.color;

            //This is meant to get the name 
            //Debug.Log(pot.p.potionName);
        }
    }

    public void GetPotion()
    {
        
        selected?.Invoke(potionColor);
        ps.Play();

        StartCoroutine(Timer(waiting));
    }

    public void Completed()
    {
        isSolved = true;
        Debug.Log("The puzzle was solved correctly!");
    }
   

    IEnumerator Timer(float wait)
    {
        yield return new WaitForSeconds(wait);

        ps.Stop();
        StopAllCoroutines();
    }    
}
