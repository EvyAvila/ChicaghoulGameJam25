using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class GameMenu : BaseMenu
{
    private List<ProgressBar> bottles = new List<ProgressBar>();
    private int currentBottlePos = 0;
    [SerializeField] private float maxValue;

    //[SerializeField] private float setMaxValue;

    //public static event Action<int> OnValueChanged;

    protected override void Awake()
    {
        scriptName = SceneScript.GameMenu;
    }

    protected override void SetProperties()
    {
        bottles = root.Query<ProgressBar>(className: "RotatedObj").ToList();

        maxValue = maxValue == 0 ? 4 : maxValue;

        foreach (var b in bottles)
        {
            b.value = 0;
            b.highValue = maxValue;
        }
        

        BloodToggle.OnCollectBlood += OnBottleClicked;
    }

    protected override void UnSetProperties()
    {
        BloodToggle.OnCollectBlood -= OnBottleClicked;
    }

    private void OnBottleClicked(float amount)
    {
        if (currentBottlePos >= bottles.Count)
            return;

        
        for(int i = 0; i < amount; i++)
        {
            if (bottles[bottles.Count - 1].value >= maxValue) //Ensures that if the last bottle is full, don't add any more and just leave
                return;

            if (bottles[currentBottlePos].value < maxValue)
            {
                bottles[currentBottlePos].value += 1;
            }
            else
            {
                currentBottlePos++;

                if (currentBottlePos < bottles.Count)
                {
                    bottles[currentBottlePos].value += 1;
                }

            }
        }
       
    }
}
