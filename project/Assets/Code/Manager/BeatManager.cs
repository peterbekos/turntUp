using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This class is used to manage music and fire off beats
 */
public static class BeatManager {

	public static void ping() {}

    private static List<Note> NotesToPlay;
    public static string fileName;
	private static int notePosition = 0;

	static BeatManager() {
        fileName = "";
		init ();
	}

	//variables


	//initalization function
	private static void init() {
        MidiAccess myAccess = new MidiAccess();
        NotesToPlay = myAccess.getNotes(fileName);
        //foreach(Note nt in NotesToPlay)
        //{

//33        }
        
	}

	public static void checkBeats(float time) {
		//TODO - grab all beats within time range
		if (NotesToPlay != null) {
			while (NotesToPlay[notePosition].startTime <= time) {
				Note note = NotesToPlay[notePosition];
				//TODO - call beat on the note's type
				callBeat (GDMethods.getBeatType(note.InstrumentName));
				notePosition++;
			}
		}
	}



	public static void callBeat(GD type) {
		GameManager.player.onBeat (type);
		GameManager.player.onMelody ();
		/*
		List<BeatObject> beatObjects = new List<BeatObject>();// = (BeatObject) GameObject.FindGameObjectsWithTag("beatObject");
		foreach (BeatObject beatobject in beatObjects) {
			beatobject.onBeat(type);
		}
		*/
	}

}
