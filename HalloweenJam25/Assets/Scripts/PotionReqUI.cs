using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionReqUI : MonoBehaviour
{
    //public static PotionReqUI instance;

    [SerializeField] private List<Image> icons;

    //private void Awake()
    //{
    //    if (instance != null && instance != this)
    //    {
    //        Destroy(this);
    //        return;
    //    }
    //    else
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    public void SetIcons(int pos, Color c)
    {
        icons[pos].color = c;
    }

}
