using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionColor { Blue, Yellow, Green, Red, Brown, Purple, Orange, Gray}

[CreateAssetMenu(fileName = "New_Potion", menuName = "Potion")]
public class Potion : ScriptableObject
{
    /// <summary>
    /// The color of the potion meant for the puzzle
    /// </summary>
    public PotionColor color;
    
    /// <summary>
    /// Assigning the shader color of the liquid shown on the side
    /// </summary>
    public Color sideLiquid;
    
    /// <summary>
    /// Assigning the shader color of the liquid shown on top
    /// </summary>
    public Color topLiquid;

    /// <summary>
    /// Name of the potion. I.E Dandelion for Yellow Potion
    /// </summary>
    public string potionName;
}
