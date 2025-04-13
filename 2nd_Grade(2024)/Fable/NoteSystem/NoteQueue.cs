using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteQueue : MonoBehaviour
{
    private List<Note> NoteList = new List<Note>();

    public void AddNote(Note note)
    {
        NoteList.Add(note);
    }

    public void RemoveNote(Note note)
    {
        NoteList.Remove(note);
    }

    public Note GetNote(int index)
    {
        if(NoteList.Count <= index) return null;
        return NoteList[index];
    }

    public int ListCount
    {
        get { return NoteList.Count; }
    }
}