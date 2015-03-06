using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MidiAccess{


    private List<Channel> mChannels;
    private List<Note> NotesToPlay = new List<Note>();


    void sortTheNotes()
    {
        NotesToPlay.Sort(
            delegate(Note n1, Note n2)
            {
                if(n1.startTime > n2.startTime) { return 1; }
                else if (n1.startTime < n2.startTime) { return -1; }
                else { return 0; }
            }
        );
    }

    void displayTheNotes()
    {
        int i = 0;
        foreach(Note currentNote in NotesToPlay)
        {
            Debug.Log("Note #" + i);
            Debug.Log("StartTime = " + currentNote.startTime);
            Debug.Log("DurationTime = " + currentNote.durationTime);
            Debug.Log("Notetype = " + currentNote.NoteType);
            i++;
        }
    }
	
	// Creates a nice stream of notes
	public List<Note> getNotes(string _filename)
    {
        MidiFileInspector mInspector = new MidiFileInspector();
        mInspector.Describe(_filename); //sets the channels

        //My Processed version of it stored in "channels"
        mChannels = mInspector.Channels;

        //Make all the notes into one stream
        foreach (Channel currentCh in mChannels)
        {
            string intstrument = currentCh.ChannelName.Trim();
            foreach (Note currentNt in currentCh.Notes)
            {
                currentNt.InstrumentName = intstrument;
                NotesToPlay.Add(currentNt);
            }
        }

        sortTheNotes(); //Long time to calculate!

        //displayTheNotes(); //Lot of displaying
        return NotesToPlay;
    }
}
