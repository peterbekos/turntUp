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
		fileName = "Assets/Art/Music/ColorsMIDI(Unfinished).mid";
		init ();
	}

	//variables


	//initalization function
	private static void init() {
        MidiAccess myAccess = new MidiAccess();
        NotesToPlay = myAccess.getNotes(fileName);
	}

	public static void checkBeats(float time) {
		if (NotesToPlay != null) {
			while (NotesToPlay[notePosition].startTime <= time) {
				Note note = NotesToPlay[notePosition];
				string instramentName = note.InstrumentName;
				GD noteType = GDMethods.getBeatType(instramentName);
				callBeat (noteType);
				notePosition++;
			}
		}
	}



	public static void callBeat(GD type) {
		//Debug.Log (type);
        if (GameManager.player != null)
        {
            GameManager.player.onBeat(type);
        }
		//GameManager.player.onMelody ();
		/*
		List<BeatObject> beatObjects = new List<BeatObject>();// = (BeatObject) GameObject.FindGameObjectsWithTag("beatObject");
		foreach (BeatObject beatobject in beatObjects) {
			beatobject.onBeat(type);
		}
		*/
	}

}
