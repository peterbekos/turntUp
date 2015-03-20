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
	private static int noteBarPosition = 0;

	static BeatManager() {
		//fileName = "Assets/Art/Music/ColorsMIDI(Unfinished).mid";
		fileName = "Assets/Art/Music/sandstorm.mid";
		init ();
		Debug.Log ("init called");
	}

	//variables


	//initalization function
	private static void init() {
        MidiAccess myAccess = new MidiAccess();
        NotesToPlay = myAccess.getNotes(fileName);
        
		Camera.main.GetComponent<GameTimer>().initTime();
		Camera.main.GetComponent<AudioSource>().Play();
	}

	public static void checkBeats(float time) {
		if (NotesToPlay != null) {
			while (NotesToPlay[notePosition].startTime <= time) {
				Note note = NotesToPlay[notePosition];
				GD noteType = GDMethods.getBeatType(note.InstrumentName);
				
				float interp = (float)(time - note.startTime)/1000;
				callBeat (noteType, interp);
				notePosition++;
			}
			
			while (NotesToPlay[noteBarPosition].startTime <= (time + 4500f)) {
				Note note = NotesToPlay[noteBarPosition];
				GD noteType = GDMethods.getBeatType(note.InstrumentName);
				
				float interp = (float)(time + 4500f - note.startTime)/1000;
				callNoteBarBeat (noteType, interp);
				noteBarPosition++;
			}
		}
	}

	public static void callNoteBarBeat(GD type, float interp){
		if (GameManager.notebar != null)
		{
			GameManager.notebar.onBeat(type, interp);
		}
	}

	public static void callBeat(GD type, float interp) {
		//Debug.Log (type);
        if (GameManager.player != null)
        {
            GameManager.player.onBeat(type, interp);
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
