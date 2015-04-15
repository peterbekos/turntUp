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
	
	private static float delayToScores = 5f;

	static BeatManager() {
		delayToScores = 5f;
		//fileName = "Assets/Art/Music/ColorsMIDI(Unfinished).mid";
		//fileName = "Assets/Art/Music/sandstorm.mid";
		//init ();
		//Debug.Log ("init called");
	}
	
	public static void loadFile(string path){
		notePosition = 0;
		noteBarPosition = 0;
		fileName = path;
		init ();
	}

	//variables


	//initalization function
	private static void init() {
        MidiAccess myAccess = new MidiAccess();
        NotesToPlay = myAccess.getNotes(fileName);
        
		Camera.main.GetComponent<GameTimer>().initTime();
		Camera.main.GetComponent<AudioSource>().Play();
		GameManager.realTimeStageStarted = Time.realtimeSinceStartup;
	}

	public static int getshit() {
		return 5;
	}

	public static Note getLastNote() {
		if (notePosition > 0) {
			return NotesToPlay [notePosition - 1];
		} else {
			return null;
		}
	}
	public static Note getCurrentNote() {
		return NotesToPlay [notePosition];
	}
	public static Note getNextNote() {
		return NotesToPlay[notePosition+1];
	}

	public static void checkBeats(float time) {
		if (NotesToPlay != null) {
			while (notePosition < NotesToPlay.Count && NotesToPlay[notePosition].startTime <= time) {
				Note note = NotesToPlay[notePosition];
				GD noteType = GDMethods.getBeatType(note.instrumentName);
				
				float interp = (float)(time - note.startTime)/1000;
				
				if(note.durationTime >= GameManager.durationForHold ){
					callBeat(noteType, interp, (float)note.durationTime);
				}
				else {
					callBeat (noteType, interp);
				}
				notePosition++;
			}
			
			while (noteBarPosition < NotesToPlay.Count && NotesToPlay[noteBarPosition].startTime <= (time + 4500f)) {
				//Debug.Log ("" + noteBarPosition + ":" + NotesToPlay[noteBarPosition].startTime);
				Note note = NotesToPlay[noteBarPosition];
				GD noteType = GDMethods.getBeatType(note.instrumentName);
				
				float interp = (float)(time + 4500f - note.startTime)/1000;
				callNoteBarBeat (noteType, interp);
				noteBarPosition++;
			}
		}
		
		if(notePosition >= NotesToPlay.Count) {
			delayToScores -= Time.deltaTime;
			
			if(delayToScores <= 0) {
				GameManager.endLevel();
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
        if (GameManager.player != null)
        {
            GameManager.player.onBeat(type, interp);
        }
        
		handleShake(type);
	}
	
	public static void callBeat(GD type, float interp, float duration) {
		if (GameManager.player != null)
		{
			GameManager.player.onBeat(type, interp, duration);
		}
		
		handleShake(type);
	}
	
	private static void handleShake(GD type){
		if(type == GD.KICK){
			//Debug.Log("BASSY");
			GameManager.gameTimer.startHorizShake();
		}
		else if(type == GD.SNARE){
			GameManager.gameTimer.startVertShake();
		}
	}

}
