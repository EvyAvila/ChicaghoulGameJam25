using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SongMeasure 
{
    public RhythmNote[] eigths = new RhythmNote[8];
}

public class Beat
{
    public RhythmNote? A;
    public RhythmNote? S;
    public RhythmNote? D;
    public RhythmNote? SPACE;
    public RhythmNote? J;
    public RhythmNote? K;
    public RhythmNote? L;
}
