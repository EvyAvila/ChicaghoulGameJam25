using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfPuzzle : PuzzleBase
{
    private BookShelfCondition condition;

    private PuzzleSocket[] sockets;
    void Start()
    {
        condition = GetComponent<BookShelfCondition>();
        solveCondition = s => s.isCorrect();

        sockets = gameObject.GetComponentsInChildren<PuzzleSocket>();
        for (int i = 0; i < sockets.Length; i++)
        {
            sockets[i].OnItemAdded += OnUpdateDetected;
        }
        
    }

    private void OnUpdateDetected()
    {
        if (solveCondition(condition))
            SolvePuzzle();
    }

    private void OnDisable()
    {
        if (sockets.Length > 0)
        {
            for (int i = 0; i < sockets.Length; i++)
            {
                sockets[i].OnItemAdded -= OnUpdateDetected;
            }
        }
    }
}
