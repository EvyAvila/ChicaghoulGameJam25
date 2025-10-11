using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class RoomPuzzleInitializer : MonoBehaviour
{
    /// <summary>
    /// List of puzzles to choose from
    /// </summary>
    [SerializeField] private GameObject[] puzzlePrefabs;

    /// <summary>
    /// The rooms trigger
    /// </summary>
    [SerializeField] private RoomEnterTrigger roomTrigger;

    [SerializeField] private Transform puzzlePoint;

    private void Start()
    {
        GameObject obj = puzzlePrefabs[UnityEngine.Random.Range(0, puzzlePrefabs.Length)];

        if (obj != null)
        {
            GameObject puzzleObjInstance = Instantiate(obj, puzzlePoint);

            if (roomTrigger != null)
            {
                roomTrigger.triggeredEvent.AddListener(
                    puzzleObjInstance.GetComponent<PuzzleRewardDispenser>().OnTimerTriggered
                    );
            }
        }
    }
}
