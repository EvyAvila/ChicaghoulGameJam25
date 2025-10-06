using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteKey { A,S,D,SPACE,J,K,L}
public enum Division { QUARTER, EIGTH}

[Serializable]
public class RhythmNote
{
    public NoteKey Key;
    public Division Division;
    public bool Sustained;
}
