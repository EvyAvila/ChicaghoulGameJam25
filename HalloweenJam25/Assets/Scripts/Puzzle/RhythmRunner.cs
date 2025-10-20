using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Cinemachine;
using UnityEngine;

//References
//RHythm Help From reference b3agz - https://youtu.be/gIjajeyjRfE?si=Eke4NzpUOlaCn9tu
//Rhythm Help - https://rhythmquestgame.com/devlog/04.html

[RequireComponent(typeof(AudioSource))]
public class RhythmRunner : InteractableObject
{
    /// <summary>
    /// BPM of the track, manually
    /// </summary>
    [SerializeField] private int BPM;
    private double dspTime;
    private double songPos;
    private float beatInterval;

    /// <summary>
    /// The represented notes to move
    /// </summary>
    [SerializeField] private VisibleNotes notes;

    [SerializeField] private AudioClip CountInClip;
    [SerializeField] private AudioClip Song;

    private MappedNotes mappedNotes;

    /// <summary>
    /// Note divisions to track: Quarter, Eighth, etc.
    /// </summary>
    [SerializeField] private NoteDivision intervals;

    /// <summary>
    /// POV Camera
    /// </summary>
    [SerializeField] private CinemachineVirtualCamera organCamera;

    /// <summary>
    /// Toggle to count beats
    /// </summary>
    [SerializeField]private bool countBeats;

    /// <summary>
    /// Current beat counted on Eigths
    /// </summary>
    private double dspBeat;
    private double currentBeat;
    private double totalAudioLength;
    private float totalBeats;

    /// <summary>
    /// Audio source of the music
    /// </summary>
    private AudioSource source;

    /// <summary>
    /// If the puzzle has been activated or not
    /// </summary>
    private bool activated;

    /// <summary>
    /// Toggle if item can be interacted with
    /// </summary>
    private bool canBeUsed = true;
     
    //Events
    public event Action OnSongStarted;
    public event Action OnBeatIncrement;
    public event Action OnSongFinished;
    protected override void Start()
    {
        currentBeat = 0.5f;
        totalBeats = Song.length * (BPM / 60.0f);

        source = GetComponent<AudioSource>();
        source.clip = CountInClip;

        mappedNotes = GetComponent<MappedNotes>();

        beatInterval = intervals.GetIntervalLength(BPM) * 2;
        
        //Subscriptions
        intervals.IntervalEvent += IncrementBeat;
        mappedNotes.OnSolved += Deactivate;

    }

    private void Deactivate()
    {
        canBeUsed = false;
    }

    public void StartIntro()
    {
        //countBeats = true;
        //source.clip = CountInClip;
        //source.PlayOneShot(CountInClip);
        //source.clip = Song;
        //source.PlayDelayed(CountInClip.length);

        totalAudioLength = CountInClip.length + Song.length;
        if (notes != null)
        {
            notes.ResetNotes();
            notes.SetSongLength(totalBeats);
        }

        StartCoroutine(IntroSequence());
    }

    private IEnumerator IntroSequence()
    {
        float delay = CountInClip.length - 0.0f;
        source.clip = Song;
        source.PlayOneShot(CountInClip);
        source.PlayDelayed(delay);
        
        if (notes != null)
            notes.MoveNotes();
        
        yield return new WaitForSeconds(delay);
        OnSongStarted?.Invoke();
        countBeats = true;
        dspTime = AudioSettings.dspTime;
        yield return null;
    }
    public void StopCounting()
    {
        countBeats = false;
        OnSongFinished?.Invoke();

        if (organCamera != null)
            organCamera.Priority = 0;

        if (notes != null)
            notes.ResetNotes();

        InputTogglerEvents.EnablePlayerInputs();
        activated = false;
    }
    private void CountOnDivisions()
    {
        songPos = (AudioSettings.dspTime - dspTime);
        dspBeat = (songPos / beatInterval)+1;

        float elapsed = (source.timeSamples /
                (source.clip.frequency * intervals.GetIntervalLength(BPM)));

        intervals.CheckForNewInterval(elapsed);
    }

    //Is triggered from an Event
    public void IncrementBeat()
    {
        currentBeat += 0.5;
        OnBeatIncrement?.Invoke();
    }

    public void ResetBeat()
    {
        if (currentBeat <= 1)
            return;

        currentBeat = .5f;

        Debug.Log("Song restarted");
    }

    public double GetCurrentDspBeat()
    {
        return dspBeat;
    }
    public double GetCurrentBeat()
    {
        return currentBeat;
    }
    protected override void Update()
    {
        if (countBeats)
        {
            CountOnDivisions();

            if (!source.isPlaying)
            {
                StopCounting();
                ResetBeat();
            }
        }
    }

    private void OnDisable()
    {
        intervals.IntervalEvent -= IncrementBeat;
        mappedNotes.OnSolved -= Deactivate;
    }

    //Overrides
    public override void Interact()
    {
        if (!canBeUsed)
            return;

        if (activated)
            return;

        activated = true;
        StartIntro();

        if (organCamera != null)
            organCamera.Priority = 10;

        InputTogglerEvents.DisablePlayerInputs();
    }
}