using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionReqUI : MonoBehaviour
{
    //public static PotionReqUI instance;

    [SerializeField] private List<Image> icons;
    [SerializeField] private List<TextMeshProUGUI> textNames;
    
    private void Start()
    {
        
    }

    public void SetIcons(int pos, Color c)
    {
        icons[pos].color = c;
    }

    public void SetName(int pos, string name)
    {
        textNames[pos].text = name;
    }

}
