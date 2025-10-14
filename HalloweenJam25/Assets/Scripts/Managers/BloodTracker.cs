using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloodTracker : MonoBehaviour
{
    public static BloodTracker Instance { get; private set; }
    private static float bloodLevel;

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

        Debug.Log($"Lost blood - Curr {bloodLevel}");


        if (bloodLevel < 0)
            bloodLevel = 0;
    }
}
