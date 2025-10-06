using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PotionCondition : PuzzleCondition
{
    [SerializeField] public List<PotionColor> enteredColors;

    [SerializeField] private List<PotionColor> requiredColors;

    private int potionSize;

    private void Start()
    {
        potionSize = Enum.GetNames(typeof(PotionColor)).Length;

        for (int i = 0; i < requiredColors.Count; i++)
        {
            int ran = Random.Range(0, potionSize);
           
            requiredColors[i] = (PotionColor)ran;
            CheckColors(i, requiredColors[i]);
        }
    }

    private void CheckColors(int i, PotionColor color)
    {
        Color c = Color.white;
        // Blue, Yellow, Green, Red, Brown, Purple
        switch (color)
        {
            case PotionColor.Blue:
                c = Color.blue;
                break;
            case PotionColor.Yellow:
                c = Color.yellow;
                break;
            case PotionColor.Green:
                c = Color.green;
                break;
            case PotionColor.Red:
                c = Color.red;
                break;
            case PotionColor.Brown:
                c = new Color(0.5f, 0.25f, 0.0f);
                break;
            case PotionColor.Purple:
                c = new Color(0.5f, 0.0f, 0.5f);
                break;

        }


        PotionReqUI.instance.SetIcons(i, c);
    }

    public override bool isCorrect()
    {
        for(int i = 0; i < requiredColors.Count; i++)
        {
            
            if (enteredColors.Count < requiredColors.Count)
            {
                Debug.Log("Size not matched");
                return false;
            }
            if (enteredColors[i] != requiredColors[i])
            {
                Debug.Log("Incorrect order");
                enteredColors.Clear();
                return false;
            }
        }

        return true;
    }

    public bool CanEnterAnswer(PotionColor color)
    {
        if(enteredColors.Count < requiredColors.Count)
        {
            enteredColors.Add(color);
            return true;
        }

        return false;
    }
}
