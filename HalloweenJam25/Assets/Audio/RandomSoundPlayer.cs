using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    private AudioSource source;

    [SerializeField] private AudioClip[] clips;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    
        if (clips.Length > 0 && source != null)
        {
            source.clip = clips[UnityEngine.Random.Range(0,clips.Length)];
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (source != null) 
            source.Play();
    }
}
