using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonObjects : MonoBehaviour
{
    private static SingletonObjects instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


}
