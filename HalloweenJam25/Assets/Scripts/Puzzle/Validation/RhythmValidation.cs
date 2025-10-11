using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmValidation : PuzzleCondition
{
    private int totalNotes;
    private int notesPlayedCorrectly;
    public override bool isCorrect()
    {
        //float percentCorrect = (float)notesPlayedCorrectly / (float)totalNotes;
        //return percentCorrect > 0.8f;

        return notesPlayedCorrectly > 50;
    }

    public void SetTotalNotes(int totalNotes)
    {
        this.totalNotes = totalNotes;
    }
    public void UpdatePlayedNotes(int notes)
    {
        notesPlayedCorrectly = notes;
    }
}
