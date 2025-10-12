using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

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
    }
    public static float GetBloodLevel()
    {
        return bloodLevel;
    }
}
