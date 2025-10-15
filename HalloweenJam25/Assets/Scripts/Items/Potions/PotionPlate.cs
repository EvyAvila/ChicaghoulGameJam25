using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionPlate : MonoBehaviour
{
    private TextMeshProUGUI Nameplate;

    private PotionItem pItem;

    // Start is called before the first frame update
    void Start()
    {
        pItem = GetComponent<PotionItem>();

       
        Nameplate = this.gameObject.transform.GetChild(3).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();


        Nameplate.text = pItem.p.potionName;
    }

    

}
