using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

public class PotionItem : MonoBehaviour
{
    [SerializeField]
    private Potion potion;
    
    public Potion p { get { return potion; } set { potion = value; } }

    [SerializeField]
    private GameObject Liquid;

    [SerializeField]
    private ParticleSystem currentParticle;

    // Start is called before the first frame update
    void Start()
    {
        Liquid.GetComponent<MeshRenderer>().material.SetColor("_SideColor", potion.sideLiquid);
        Liquid.GetComponent<MeshRenderer>().material.SetColor("_TopColor", potion.topLiquid);

        StopParticle();

    }

    public void PlayParticle()
    {
        currentParticle.Play();
    }

    public void StopParticle()
    {
        currentParticle.Stop();
    }
}
