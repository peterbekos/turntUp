using UnityEngine;
using System.Collections;

public class NoteOrigin : BeatObject
{

    //Notes
    public GameObject greenNote, yellowNote, redNote, blueNote;
    public Vector3 greenNoteSpawnPoint = new Vector3(7, -5, 0);
    public Vector3 yellowNoteSpawnPoint = new Vector3(0, 0, 0);
    public Vector3 redNoteSpawnPoint = new Vector3(0, 0, 0);
    public Vector3 blueNoteSpawnPoint = new Vector3(0, 0, 0);

    //spawn a note at a given point
    public void spanwnote(GameObject noteObject, Vector3 spawnpoint, float interp)
    {
        GameObject note = (GameObject)GameObject.Instantiate(noteObject, spawnpoint, this.transform.rotation);
        note.SendMessage("interpolate", interp);
    }

    //Override beat functions

    // melody = 1
    new public void onMelody(float interp)
    {
        if (greenNote == null) return;
		spanwnote(greenNote, transform.position + greenNoteSpawnPoint, interp);
    }

    // harmony + kick = 2
    // doesn't seem like there is any
    new public void onHarmony(float interp)
    {
        if (yellowNote == null) return;
        spanwnote(yellowNote, transform.position + yellowNoteSpawnPoint, interp);
    }
    new public void onKick(float interp)
    {
        if (redNote == null) return;
		spanwnote(redNote, transform.position + redNoteSpawnPoint, interp);
    }

    // kick/snare = 3 since harmony is not really there
    new public void onSnare(float interp)
    {
        if (blueNote == null) return;
		spanwnote(blueNote, transform.position + redNoteSpawnPoint, interp);
    }
    new public void onHat(float interp)
    {
        if (yellowNote == null) return;
		spanwnote(yellowNote, transform.position + redNoteSpawnPoint, interp);
    }

    // treble + bass = 4
    new public void onBass(float interp)
    {
        if (blueNote == null) return;
		spanwnote(blueNote, transform.position + blueNoteSpawnPoint, interp);
    }
    new public void onTreble(float interp)
    {
        if (blueNote == null) return;
		spanwnote(blueNote, transform.position + greenNoteSpawnPoint, interp);
    }
}
