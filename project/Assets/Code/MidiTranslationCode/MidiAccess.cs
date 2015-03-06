using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MidiAccess : MonoBehaviour {

    public string fileName;
    private List<Channel> mChannels;
    private List<Note> NotesToPlay = new List<Note>();

    public GameObject gun;
    private ShotOrigin gunScript;

    float milliseconds = 0;
    int i = 0;

	// Use this for initialization
	void Start () {
        fileName = "NumbMidi.mid"; //Default for now
        MidiFileInspector mInspector = new MidiFileInspector();
        mInspector.Describe(fileName); //sets the channels

        //My Processed version of it stored in "channels"
        mChannels = mInspector.Channels;

        foreach(Channel currentCh in mChannels)
        {
            foreach(Note currentNt in currentCh.Notes)
            {
                NotesToPlay.Add(currentNt);
            }
        }

        sortTheNotes(); //Long time to calculate!

        gunScript = (ShotOrigin)gun.GetComponent(typeof(ShotOrigin));
        //displayTheNotes(); //Lot of displaying
        gunScript.onMelody();

	}

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
	
	// Update is called once per frame
	void Update () {
        milliseconds += Time.deltaTime * 1000;
        i++;
        if(i % 100 == 0)
        {
            gunScript.onMelody();
        }
	}
}
