using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodJarVisual : MonoBehaviour
{
    /// <summary>
    /// The blood material in child object
    /// </summary>
    [SerializeField] private MeshRenderer bloodRenderer;
    private Material bloodMaterial;

    /// <summary>
    /// Hashed ID for quick shader property access
    /// </summary>
    private int FillHashID;

    private void Start()
    {
        FillHashID = Shader.PropertyToID("_Fill");
    
        if (bloodRenderer != null)
        {
            bloodMaterial = bloodRenderer.material;
        }
    }

    public void SetShaderFill(float amount)
    {
        if (amount < 0.3)
            amount = 0.3f;

        bloodMaterial.SetFloat(FillHashID, amount);
    }
}
