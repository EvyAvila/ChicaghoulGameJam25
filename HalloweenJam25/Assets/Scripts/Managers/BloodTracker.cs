using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;
using static Cinemachine.DocumentationSortingAttribute;

public class BloodTracker : MonoBehaviour
{
    public static BloodTracker Instance { get; private set; }
    private static float bloodLevel;
    public static event Action<float> updateBlood;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        bloodLevel = 0;
    }

    public static void AddToBloodLevel(float level)
    {
        bloodLevel += level;
        Debug.Log($"Add blood - Curr {bloodLevel}");
        updateBlood?.Invoke(level);
    }
    public static float GetBloodLevel()
    {
        return bloodLevel;
    }

    public static void TakeDamage()
    {
        //Take 10 percent damage
        float damage = bloodLevel * 0.1f;
        
        bloodLevel -= damage;
        updateBlood?.Invoke(-damage);
        Debug.Log($"Lost blood - Curr {bloodLevel}");


        if (bloodLevel < 0)
            bloodLevel = 0;
    }
}
