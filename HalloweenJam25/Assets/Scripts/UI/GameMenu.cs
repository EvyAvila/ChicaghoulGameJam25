using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Linq;

public class GameMenu : BaseMenu
{
    private List<ProgressBar> bottles = new List<ProgressBar>();
    private int currentBottlePos = 0;
    [SerializeField] private float maxValue, speed;

    private VisualElement rotatingImage;
    private float currentAngle = 0, startAngle = 0, endAngle = 215; 
    private bool isActive;

    
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
        

        BloodTracker.updateBlood += OnBottleUpdated;

        isActive = true;
    }

    protected override void UnSetProperties()
    {
        BloodTracker.updateBlood -= OnBottleUpdated;
        isActive = false;
    }

    private void OnBottleUpdated(float amount)
    {
        if (bottles == null || bottles.Count == 0)
            return;

        
        int pos = amount > 0 ? bottles.FindIndex(x => x.value < maxValue) : bottles.FindLastIndex(x => x.value > 0);

        if (pos == -1)
        {
            Debug.Log("No valid bottle to update.");
            return;
        }

        float newValue = bottles[pos].value + amount;

        
        if (newValue > maxValue) //adding
        {
            float overflow = newValue - maxValue;
            bottles[pos].value = maxValue;

            if (pos < bottles.Count - 1)
                bottles[pos + 1].value = Mathf.Min(bottles[pos + 1].value + overflow, maxValue);
        }
        else if (newValue < 0) //removing
        {
            float underflow = newValue; // negative value
            bottles[pos].value = 0;

            if (pos > 0)
                bottles[pos - 1].value = Mathf.Max(bottles[pos - 1].value + underflow, 0);
        }
        else
        {
            bottles[pos].value = newValue;
        }

        currentBottlePos = Mathf.Clamp(pos, 0, bottles.Count - 1);
    }

    private void RotateClock()
    {
        float ratio = SessionTimer.Instance.GetTimeRatio();
        currentAngle = Mathf.Lerp(startAngle, endAngle, ratio);

        rotatingImage.style.rotate = new Rotate(new Angle(currentAngle, AngleUnit.Degree));
    }
}
