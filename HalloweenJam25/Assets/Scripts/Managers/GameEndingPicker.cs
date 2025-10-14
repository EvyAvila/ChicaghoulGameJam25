using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// [FAILURE - Loss by Sun Timer]
/// [BAD - Very low blood count ]
/// [NORMAL - More than 1/3 blood filled]
/// [SUPER - More than 2/3 blood filled]
/// </summary>
public enum EndingType { FAILURE, BAD, NORMAL, SUPER}
public enum FailureType { SUN, SOULS}

/// <summary>
/// Sets the proper game ending parameters based off of player data
/// </summary>
public class GameEndingPicker : MonoBehaviour
{
    public static GameEndingPicker Instance { get; private set; }
    public static EndingType ending { get; private set; }
    public static FailureType failType { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        GameCurator.OnLoseGameSouls += DeclareFailureSouls;
        GameCurator.OnLoseGameSun += DeclareFailureTimer;
        GameCurator.OnDecideEnding += DeclareStandardEndingType;
    }

    private void OnDisable()
    {
        GameCurator.OnLoseGameSouls -= DeclareFailureSouls;
        GameCurator.OnLoseGameSun -= DeclareFailureTimer;
        GameCurator.OnDecideEnding -= DeclareStandardEndingType;
    }
    public static void DeclareStandardEndingType()
    {
        if (BloodTracker.Instance == null)
            return;

        float blood = BloodTracker.GetBloodLevel();
            
        if (blood < 3)
        {
            Debug.Log("BAD Ending, low blood");
            ending = EndingType.BAD;
        }
        else if (blood >= 3 && blood <=6)
        {
            Debug.Log("Normal ending, regular blood");
            ending = EndingType.NORMAL;
        }
        else if (blood > 6)
        {
            Debug.Log("SUPER ending");
            ending = EndingType.SUPER;
        }
    }

    public static void DeclareFailureTimer()
    {
        Debug.Log("Failure Timer");
        ending = EndingType.FAILURE;
        failType = FailureType.SUN;
    }

    public static void DeclareFailureSouls()
    {
        Debug.Log("Failure SOULS");
        ending = EndingType.FAILURE;
        failType = FailureType.SOULS;
    }
}
