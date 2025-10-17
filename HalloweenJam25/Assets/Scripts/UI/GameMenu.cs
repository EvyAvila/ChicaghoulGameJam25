using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class GameMenu : BaseMenu
{
    private List<ProgressBar> bottles = new List<ProgressBar>();
    private int currentBottlePos = 0;
    [SerializeField] private float maxValue, speed;

    private VisualElement rotatingImage;
    private float currentAngle = 0; //-180f;
    private bool isActive;

    //[SerializeField] private ClockTimer clock;

    protected override void Awake()
    {
        scriptName = SceneScript.GameMenu;
        speed = speed == 0 || speed < 5 ? 6 : speed;
    }

    private void Update()
    {
        if (isActive)
        {
            RotateClock();
        }
        
    }

    protected override void SetProperties()
    {
        bottles = root.Query<ProgressBar>(className: "RotatedObj").ToList();
        rotatingImage = root.Q<VisualElement>("RotatingImage");
        
        maxValue = maxValue == 0 ? 4 : maxValue;

        foreach (var b in bottles)
        {
            b.value = 0;
            b.highValue = maxValue;
        }
        

        BloodToggle.OnCollectBlood += OnBottleClicked;

        isActive = true;
    }

    protected override void UnSetProperties()
    {
        BloodToggle.OnCollectBlood -= OnBottleClicked;
        isActive = false;
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

    private void RotateClock()
    {
        currentAngle += speed * Time.deltaTime; // accumulate
        rotatingImage.style.rotate = new Rotate(new Angle(currentAngle, AngleUnit.Degree));
    }
}
