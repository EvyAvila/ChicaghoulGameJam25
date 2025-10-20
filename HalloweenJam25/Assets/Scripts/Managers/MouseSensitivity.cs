using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class MouseSensitivity : MonoBehaviour
{
    public static MouseSensitivity Instance;
    public float sensitivity { get; private set; }
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
        sensitivity = 20f;
    }
    public void SetMouseSense(float t)
    {
        Debug.Log($"Set to {t}");
        sensitivity = t;
    }
}
