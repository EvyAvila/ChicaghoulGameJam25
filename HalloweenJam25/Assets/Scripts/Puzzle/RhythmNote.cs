using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteKey { NONE, A,S,D,SPACE,J,K,L}
public enum Division { QUARTER, EIGTH}

[Serializable]
public class RhythmNote
{
    public NoteKey Key;
    public bool InitialSustain;
    public bool Sustained;
    public bool Success;

}
