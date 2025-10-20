using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MappedNotes : PuzzleBase
{
    [SerializeField] private SongMeasuresSO MeasuresSO;

    /// <summary>
    /// Attatched song runner
    /// </summary>
    private RhythmRunner rhythmRunner;

    /// <summary>
    /// Puzzle Condition
    /// </summary>
    private RhythmValidation condition;

    /// <summary>
    /// Keyboard inputs
    /// </summary>
    private PlayerInputs rhythmInputs;

    [SerializeField] private double marginOfError;

    private int totalNoteCount;

    private int noteIndex;
    private int goodCount;

    //Keyboard Notes
    public RhythmNote[] A;
    public RhythmNote[] S;
    public RhythmNote[] D;
    public RhythmNote[] SPACE;
    public RhythmNote[] J;
    public RhythmNote[] K;
    public RhythmNote[] L;

    private KeyboardNote ANote;
    private KeyboardNote SNote;
    private KeyboardNote DNote;
    private KeyboardNote SpaceNote;
    private KeyboardNote JNote;
    private KeyboardNote KNote;
    private KeyboardNote LNote;

    private Dictionary<string, KeyboardNote> inputDict;
    private Dictionary<string, RhythmNote[]> arrayDict;

    public event Action OnSolved;

    [SerializeField] private bool acceptInput;

    private void Start()
    {
        condition = GetComponent<RhythmValidation>();
        solveCondition = val => val.isCorrect();

        noteIndex = -1;

        rhythmRunner = GetComponent<RhythmRunner>();
        rhythmRunner.OnBeatIncrement += OnBeatIncrement;
        rhythmRunner.OnSongFinished += OnRhythmFinished;
        rhythmRunner.OnSongStarted += OnSongStart;

        ANote = new KeyboardNote();
        SNote = new KeyboardNote();
        DNote = new KeyboardNote();
        SpaceNote = new KeyboardNote();
        JNote = new KeyboardNote();
        KNote = new KeyboardNote();
        LNote = new KeyboardNote(); 

        if (MeasuresSO != null)
        {
            //A notes
            CopyNotes(ref A, ref MeasuresSO.A);
            SetSustains(A, NoteKey.A);

            //S Notes
            CopyNotes(ref S, ref MeasuresSO.S);
            SetSustains(S, NoteKey.S);

            //D Notes
            CopyNotes(ref D, ref MeasuresSO.D);
            SetSustains(D, NoteKey.D);

            //SPACE Notes
            CopyNotes(ref SPACE, ref MeasuresSO.SPACE);
            SetSustains(SPACE, NoteKey.SPACE);

            //J Notes
            CopyNotes(ref J, ref MeasuresSO.J);
            SetSustains(J, NoteKey.J);

            //K Notes
            CopyNotes(ref K, ref MeasuresSO.K);
            SetSustains(K, NoteKey.K);

            //L Notes
            CopyNotes(ref L, ref MeasuresSO.L);
            SetSustains(L, NoteKey.L);
        }

        condition.SetTotalNotes(totalNoteCount);

        arrayDict = new Dictionary<string, RhythmNote[]>()
        {
            { "a", A},
            { "s", S},
            { "d", D},
            { "space",SPACE},
            { "j", J},
            { "k", K},
            { "l", L}
        };

        inputDict = new Dictionary<string, KeyboardNote>()
        {
            { "a", ANote},
            { "s", SNote},
            { "d", DNote},
            { "space",SpaceNote},
            { "j", JNote},
            { "k", KNote},
            { "l", LNote}
        };
    }


    private void OnEnable()
    {
        if (rhythmInputs == null)
            rhythmInputs = new PlayerInputs();

        rhythmInputs.RhythmMap.Enable();
        rhythmInputs.RhythmMap.A.started += OnNoteDetected;
        rhythmInputs.RhythmMap.A.canceled += OnNoteLetGo;
        rhythmInputs.RhythmMap.S.started += OnNoteDetected;
        rhythmInputs.RhythmMap.S.canceled += OnNoteLetGo;
        rhythmInputs.RhythmMap.D.started += OnNoteDetected;
        rhythmInputs.RhythmMap.D.canceled += OnNoteLetGo;
        rhythmInputs.RhythmMap.SPACE.started += OnNoteDetected;
        rhythmInputs.RhythmMap.SPACE.canceled += OnNoteLetGo;
        rhythmInputs.RhythmMap.J.started += OnNoteDetected;
        rhythmInputs.RhythmMap.J.canceled += OnNoteLetGo;
        rhythmInputs.RhythmMap.K.started += OnNoteDetected;
        rhythmInputs.RhythmMap.K.canceled += OnNoteLetGo;
        rhythmInputs.RhythmMap.L.started += OnNoteDetected;
        rhythmInputs.RhythmMap.L.canceled += OnNoteLetGo;
        //rhythmInputs.RhythmMap.Leave.started += OnLeave;

    }
    private void OnNoteDetected(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!acceptInput)
            return;

        inputDict[obj.control.name].HeldDown = true;
        inputDict[obj.control.name].PreviouslyHeld = true;
        
        ValidateNonSustainNotes(arrayDict[obj.control.name]);
    }
    private void OnNoteLetGo(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!acceptInput)
            return;

        inputDict[obj.control.name].HeldDown = false;
        inputDict[obj.control.name].PreviouslyHeld = false;
    }
    private void OnLeave(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!acceptInput)
            return;
    }

    private void OnDisable()
    {
        rhythmInputs.RhythmMap.Disable();
        rhythmInputs.RhythmMap.A.started -= OnNoteDetected;
        rhythmInputs.RhythmMap.A.canceled -= OnNoteLetGo;
        rhythmInputs.RhythmMap.S.started -= OnNoteDetected;
        rhythmInputs.RhythmMap.S.canceled -= OnNoteLetGo;
        rhythmInputs.RhythmMap.D.started -= OnNoteDetected;
        rhythmInputs.RhythmMap.D.canceled -= OnNoteLetGo;
        rhythmInputs.RhythmMap.SPACE.started -= OnNoteDetected;
        rhythmInputs.RhythmMap.SPACE.canceled -= OnNoteLetGo;
        rhythmInputs.RhythmMap.J.started -= OnNoteDetected;
        rhythmInputs.RhythmMap.J.canceled -= OnNoteLetGo;
        rhythmInputs.RhythmMap.K.started -= OnNoteDetected;
        rhythmInputs.RhythmMap.K.canceled -= OnNoteLetGo;
        rhythmInputs.RhythmMap.L.started -= OnNoteDetected;
        rhythmInputs.RhythmMap.L.canceled -= OnNoteLetGo;
        //rhythmInputs.RhythmMap.Leave.started -= OnLeave;

        rhythmRunner.OnBeatIncrement -= OnBeatIncrement;
        rhythmRunner.OnSongFinished -= OnRhythmFinished;
        rhythmRunner.OnSongStarted -= OnSongStart;
    }
    private void CopyNotes(ref RhythmNote[] dest, ref RhythmNote[] src)
    {
        dest = new RhythmNote[src.Length];
        for (int i = 0; i < src.Length; i++)
        {
            dest[i] = new RhythmNote()
            {
                Key = src[i].Key,
                Sustained = src[i].Sustained,
                InitialSustain = src[i].InitialSustain
            };            
        }
    }
    private void SetSustains(RhythmNote[] notes, NoteKey key)
    {
        bool isSustained = false;
        for (int i = 0; i < notes.Length; i++)
        {
            if (notes[i].Key == NoteKey.NONE)
            {
                if (isSustained)
                {
                    notes[i].Key = key;
                    notes[i].Sustained = true;
                    totalNoteCount++;
                }
            }
            else if (notes[i].Key == key)
            {
                isSustained = notes[i].Sustained;
                totalNoteCount++;
            }
        }
    }

    public void ValidateNonSustainNotes(RhythmNote[] arr)
    {
        if (noteIndex == -1)
            return;

        if (arr[noteIndex].Sustained && !arr[noteIndex].InitialSustain)
            return;

        if (arr[noteIndex].Key == NoteKey.NONE)
        {
            //Debug.Log("No key this time");
            return;
        }

        double diff = rhythmRunner.GetCurrentDspBeat() - rhythmRunner.GetCurrentBeat();
        if (diff < marginOfError)
        {
            //Debug.Log("In Time");
            arr[noteIndex].PlaySuccess();
            goodCount++;
        }
        else
        {
            //Debug.Log("Bad Time");
            arr[noteIndex].ResetSuccess();
        }            
    }

    private void ValidateSustainedNotes(RhythmNote[] arr, KeyboardNote note)
    {
        //Verify that there is a note to play
        if (arr[noteIndex].Key == NoteKey.NONE)
        {
            if (note.HeldDown)
            {
                return;
            }
        }

        if (arr[noteIndex].Sustained && note.HeldDown)
        {
            //If there was a previous note
            if (noteIndex-1 >= 0)
            {
                //If previous note was 'Sustain' AND
                //the note was previously held 
                if (arr[noteIndex - 1].Sustained)
                {
                    if (!arr[noteIndex-1].Success)
                    {
                        //Debug.Log("Cant continue sustain");
                        return;
                    }

                    //Debug.Log("Continued sustain");
                    arr[noteIndex].PlaySuccess();
                    goodCount++;
                    return;
                }
            }

            if (note.PreviouslyHeld)
            {
                //Debug.Log("Wrong sustain");
                return;
            }

            //Debug.Log("Final - Continued sustain");
            arr[noteIndex].PlaySuccess();
            goodCount++;
            return;
        }

    }
    private void OnBeatIncrement()
    {
        if (!acceptInput)
            return;

        noteIndex++;
        noteIndex = Mathf.Clamp(noteIndex, 0, A.Length-1);

        ValidateSustainedNotes(A, ANote);
        ValidateSustainedNotes(S, SNote);
        ValidateSustainedNotes(D, DNote);
        ValidateSustainedNotes(SPACE, SpaceNote);
        ValidateSustainedNotes(J, JNote);
        ValidateSustainedNotes(K, KNote);
        ValidateSustainedNotes(L, LNote);
    }
    private void Update()
    {
        if (noteIndex >= A.Length - 1)
            acceptInput = false;
    }
    private void OnSongStart()
    {
        acceptInput = true;
    }
    private void OnRhythmFinished()
    {
        condition.UpdatePlayedNotes(goodCount);
        acceptInput = false;

        if (solveCondition(condition))
        {
            SolvePuzzle();
        }
        else
        {
            ResetComponent();
        }
    }

    private void ResetComponent()
    {
        goodCount = 0;
        noteIndex = -1;

        for (int i = 0; i < A.Length; i++)
        {
            A[i].ResetSuccess();
            S[i].ResetSuccess();
            D[i].ResetSuccess();
            SPACE[i].ResetSuccess();
            J[i].ResetSuccess();
            K[i].ResetSuccess();
            L[i].ResetSuccess();
        }

        ANote.Reset();
        SNote.Reset();
        DNote.Reset();
        SpaceNote.Reset();
        JNote.Reset();  
        KNote.Reset();
        LNote.Reset();
    }

    //Overrides--------------
    public override void SolvePuzzle()
    {
        Debug.Log("PuzzleSolved");
        OnSolved?.Invoke();
        base.SolvePuzzle();
    }
}
