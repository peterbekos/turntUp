using UnityEngine;
using System.Collections;

public class NoteBar : PlayObject
{

    // Use this for initialization
    new void Start()
    {
		GameManager.notebar = this;
    }

    new public void onBeat(GD type, float interp)
    {
        //base.onBeat();
        switch (type)
        {
            case GD.HAT:
                onHat(interp);
                break; // =)
            case GD.BASS:
                onBass(interp);
                break; // =)
            case GD.MELODY:
                onMelody(interp);
                break; // =)
            case GD.KICK:
                onKick(interp);
                break; // =)
            case GD.HARMONY:
                onHarmony(interp);
                break; // =)
            case GD.SNARE:
                onSnare(interp);
                break; // =(
            case GD.TREBLE:
            	onTreble (interp);
            	break;
        }
    }

    new public void onMelody(float interp)
    {
        base.onMelody();
        NoteOrigin[] notes = transform.GetComponentsInChildren<NoteOrigin>();
        foreach (NoteOrigin n in notes)
        {
            n.SendMessage("onMelody", interp);
        }
    }

    new public void onHarmony(float interp)
    {
        base.onHarmony();
        NoteOrigin[] notes = transform.GetComponentsInChildren<NoteOrigin>();
        foreach (NoteOrigin n in notes)
        {
            n.SendMessage("onHarmony", interp);
        }
    }

    new public void onKick(float interp)
    {
        base.onKick();
        NoteOrigin[] notes = transform.GetComponentsInChildren<NoteOrigin>();
        foreach (NoteOrigin n in notes)
        {
            n.SendMessage("onKick", interp);
        }
    }

    new public void onHat(float interp)
    {
        base.onHat();
        NoteOrigin[] notes = transform.GetComponentsInChildren<NoteOrigin>();
        foreach (NoteOrigin n in notes)
        {
            n.SendMessage("onHat", interp);
        }
    }

    new public void onSnare(float interp)
    {
        base.onSnare();
        NoteOrigin[] notes = transform.GetComponentsInChildren<NoteOrigin>();
        foreach (NoteOrigin n in notes)
        {
            n.SendMessage("onSnare", interp);
        }
    }

    new public void onBass(float interp)
    {
        base.onBass();
        NoteOrigin[] notes = transform.GetComponentsInChildren<NoteOrigin>();
        foreach (NoteOrigin n in notes)
        {
            n.SendMessage("onBass", interp);
        }
    }

    new public void onTreble(float interp)
    {
        base.onTreble();
        NoteOrigin[] notes = transform.GetComponentsInChildren<NoteOrigin>();
        foreach (NoteOrigin n in notes)
        {
            n.SendMessage("onTreble", interp);
        }
    }
}
