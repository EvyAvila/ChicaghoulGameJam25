using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//https://rhythmquestgame.com/devlog/04.html
public class VisibleNotes : MonoBehaviour
{
    /// <summary>
    /// The end position of the moving notes
    /// </summary>
    [SerializeField] private float maxDistance;
    private Vector3 endPosition;

    /// <summary>
    /// How fast the notes will move
    /// </summary>
    [SerializeField] private float moveSpeed;

    private bool movingNotes;
    private float songLength;
    public void ResetNotes()
    {
        movingNotes = false;
        transform.localPosition = Vector3.zero;
        endPosition = transform.localPosition + new Vector3(-maxDistance, 0,0);
    }
    public void SetSongLength(float length)
    {
        this.songLength = length;           
    }
    public void MoveNotes()
    {
        movingNotes=true;
    }

    private void Update()
    {
        if (movingNotes)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPosition, moveSpeed * Time.deltaTime);
        }   
    
    }
}
