using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Game Dictionary
public enum GD {
	//Beats
	MELODY,
	HARMONY,
	KICK,
	SNARE,
	HAT,
	BASS,
	TREBLE,
	//Action KeyMappings
	LEFT,
	RIGHT,
	//Modes
	STANDARD,
	TURNT,
	CHILL
}

public static class GDMethods
{

	private static Dictionary<string, GD> instrumentMappings = new Dictionary<string, GD>();
	private static bool isInit = false;

	public static void init() {
		mapInstruments ();
	}

	//To be called only once!
	private static void mapInstruments()
	{
		instrumentMappings.Add("Acoustic Grand Piano", GD.MELODY);
		instrumentMappings.Add("Bright Acoustic Piano", GD.HARMONY);
		instrumentMappings.Add("Electric Grand Piano", GD.HARMONY);
		instrumentMappings.Add("Honky-tonk Piano", GD.HARMONY);
		instrumentMappings.Add("Electric Piano 1", GD.HARMONY);
		instrumentMappings.Add("Electric Piano 2", GD.HARMONY);
		instrumentMappings.Add("Harpsichord", GD.HARMONY);
		instrumentMappings.Add("Clavi", GD.HARMONY);
		instrumentMappings.Add("Celesta", GD.HARMONY);
		instrumentMappings.Add("Glockenspiel", GD.HARMONY);
		instrumentMappings.Add("Music Box", GD.HARMONY);
		instrumentMappings.Add("Vibraphone", GD.HARMONY);
		instrumentMappings.Add("Marimba", GD.HARMONY);
		instrumentMappings.Add("Xylophone", GD.HARMONY);
		instrumentMappings.Add("Tubular Bells", GD.HARMONY);
		instrumentMappings.Add("Dulcimer", GD.HARMONY);
		instrumentMappings.Add("Drawbar Organ", GD.HARMONY);
		instrumentMappings.Add("Percussive Organ", GD.HARMONY);
		instrumentMappings.Add("Rock Organ", GD.HARMONY);
		instrumentMappings.Add("Church Organ", GD.HARMONY);
		instrumentMappings.Add("Reed Organ", GD.HARMONY);
		instrumentMappings.Add("Accordion", GD.HARMONY);
		instrumentMappings.Add("Harmonica", GD.HARMONY);
		instrumentMappings.Add("Tango Accordion", GD.HARMONY);
		instrumentMappings.Add("Acoustic Guitar (nylon)", GD.HARMONY);
		instrumentMappings.Add("Acoustic Guitar (steel)", GD.HARMONY);
		instrumentMappings.Add("Electric Guitar (jazz)", GD.HARMONY);
		instrumentMappings.Add("Electric Guitar (clean)", GD.HARMONY);
		instrumentMappings.Add("Electric Guitar (muted)", GD.HARMONY);
		instrumentMappings.Add("Overdriven Guitar", GD.HARMONY);
		instrumentMappings.Add("Distortion Guitar", GD.HARMONY);
		instrumentMappings.Add("Guitar harmonics", GD.HARMONY);
		instrumentMappings.Add("Acoustic Bass", GD.BASS);
		instrumentMappings.Add("Electric Bass (finger)", GD.HARMONY);
		instrumentMappings.Add("Electric Bass (pick)", GD.HARMONY);
		instrumentMappings.Add("Fretless Bass", GD.HARMONY);
		instrumentMappings.Add("Slap Bass 1", GD.HARMONY);
		instrumentMappings.Add("Slap Bass 2", GD.HARMONY);
		instrumentMappings.Add("Synth Bass 1", GD.HARMONY);
		instrumentMappings.Add("Synth Bass 2", GD.HARMONY);
		instrumentMappings.Add("Violin", GD.TREBLE);
		instrumentMappings.Add("Viola", GD.HARMONY);
		instrumentMappings.Add("Cello", GD.HARMONY);
		instrumentMappings.Add("Contrabass", GD.HARMONY);
		instrumentMappings.Add("Tremolo Strings", GD.HARMONY);
		instrumentMappings.Add("Pizzicato Strings", GD.HARMONY);
		instrumentMappings.Add("Orchestral Harp", GD.HARMONY);
		instrumentMappings.Add("Timpani", GD.HARMONY);
		instrumentMappings.Add("String Ensemble 1", GD.HARMONY);
		instrumentMappings.Add("String Ensemble 2", GD.HARMONY);
		instrumentMappings.Add("SynthStrings 1", GD.HARMONY);
		instrumentMappings.Add("SynthStrings 2", GD.HARMONY);
		instrumentMappings.Add("Choir Aahs", GD.HARMONY);
		instrumentMappings.Add("Voice Oohs", GD.HARMONY);
		instrumentMappings.Add("Synth Voice", GD.HARMONY);
		instrumentMappings.Add("Orchestra Hit", GD.HARMONY);
		instrumentMappings.Add("Trumpet", GD.HARMONY);
		instrumentMappings.Add("Trombone", GD.HARMONY);
		instrumentMappings.Add("Tuba", GD.HARMONY);
		instrumentMappings.Add("Muted Trumpet", GD.HARMONY);
		instrumentMappings.Add("French Horn", GD.HARMONY);
		instrumentMappings.Add("Brass Section", GD.HARMONY);
		instrumentMappings.Add("SynthBrass 1", GD.HARMONY);
		instrumentMappings.Add("SynthBrass 2", GD.HARMONY);
		instrumentMappings.Add("Soprano Sax", GD.HARMONY);
		instrumentMappings.Add("Alto Sax", GD.HARMONY);
		instrumentMappings.Add("Tenor Sax", GD.HARMONY);
		instrumentMappings.Add("Baritone Sax", GD.HARMONY);
		instrumentMappings.Add("Oboe", GD.HARMONY);
		instrumentMappings.Add("English Horn", GD.HARMONY);
		instrumentMappings.Add("Bassoon", GD.HARMONY);
		instrumentMappings.Add("Clarinet", GD.HARMONY);
		instrumentMappings.Add("Piccolo", GD.HARMONY);
		instrumentMappings.Add("Flute", GD.HARMONY);
		instrumentMappings.Add("Recorder", GD.HARMONY);
		instrumentMappings.Add("Pan Flute", GD.HARMONY);
		instrumentMappings.Add("Blown Bottle", GD.HARMONY);
		instrumentMappings.Add("Shakuhachi", GD.HARMONY);
		instrumentMappings.Add("Whistle", GD.HARMONY);
		instrumentMappings.Add("Ocarina", GD.HARMONY);
		instrumentMappings.Add("Lead 1 (square)", GD.HARMONY);
		instrumentMappings.Add("Lead 2 (sawtooth)", GD.HARMONY);
		instrumentMappings.Add("Lead 3 (calliope)", GD.HARMONY);
		instrumentMappings.Add("Lead 4 (chiff)", GD.HARMONY);
		instrumentMappings.Add("Lead 5 (charang)", GD.HARMONY);
		instrumentMappings.Add("Lead 6 (voice)", GD.HARMONY);
		instrumentMappings.Add("Lead 7 (fifths)", GD.HARMONY);
		instrumentMappings.Add("Lead 8 (bass + lead)", GD.HARMONY);
		instrumentMappings.Add("Pad 1 (new age)", GD.HARMONY);
		instrumentMappings.Add("Pad 2 (warm)", GD.HARMONY);
		instrumentMappings.Add("Pad 3 (polysynth)", GD.HARMONY);
		instrumentMappings.Add("Pad 4 (choir)", GD.HARMONY);
		instrumentMappings.Add("Pad 5 (bowed)", GD.HARMONY);
		instrumentMappings.Add("Pad 6 (metallic)", GD.HARMONY);
		instrumentMappings.Add("Pad 7 (halo)", GD.HARMONY);
		instrumentMappings.Add("Pad 8 (sweep)", GD.HARMONY);
		instrumentMappings.Add("FX 1 (rain)", GD.HARMONY);
		instrumentMappings.Add("FX 2 (soundtrack)", GD.HARMONY);
		instrumentMappings.Add("FX 3 (crystal)", GD.HARMONY);
		instrumentMappings.Add("FX 4 (atmosphere)", GD.HARMONY);
		instrumentMappings.Add("FX 5 (brightness)", GD.HARMONY);
		instrumentMappings.Add("FX 6 (goblins)", GD.HARMONY);
		instrumentMappings.Add("FX 7 (echoes)", GD.HARMONY);
		instrumentMappings.Add("FX 8 (sci-fi)", GD.HARMONY);
		instrumentMappings.Add("Sitar", GD.HARMONY);
		instrumentMappings.Add("Banjo", GD.HARMONY);
		instrumentMappings.Add("Shamisen", GD.HARMONY);
		instrumentMappings.Add("Koto", GD.HARMONY);
		instrumentMappings.Add("Kalimba", GD.HARMONY);
		instrumentMappings.Add("Bag pipe", GD.HARMONY);
		instrumentMappings.Add("Fiddle", GD.HARMONY);
		instrumentMappings.Add("Shanai", GD.HARMONY);
		instrumentMappings.Add("Tinkle Bell", GD.HAT);
		instrumentMappings.Add("Agogo", GD.HARMONY);
		instrumentMappings.Add("Steel Drums", GD.HARMONY);
		instrumentMappings.Add("Woodblock", GD.HARMONY);
		instrumentMappings.Add("Taiko Drum", GD.KICK);
		instrumentMappings.Add("Melodic Tom", GD.SNARE);
		instrumentMappings.Add("Synth Drum", GD.HARMONY);
		instrumentMappings.Add("Reverse Cymbal", GD.HARMONY);
		instrumentMappings.Add("Guitar Fret Noise", GD.HARMONY);
		instrumentMappings.Add("Breath Noise", GD.HARMONY);
		instrumentMappings.Add("Seashore", GD.HARMONY);
		instrumentMappings.Add("Bird Tweet", GD.HARMONY);
		instrumentMappings.Add("Telephone Ring", GD.HARMONY);
		instrumentMappings.Add("Helicopter", GD.HARMONY);
		instrumentMappings.Add("Applause", GD.HARMONY);
		instrumentMappings.Add("Gunshot", GD.HARMONY);
	}
	
	public static GD getBeatType(string _instrument)
	{
		if (!isInit) {
			init();
			isInit = true;
		}

		if (instrumentMappings.ContainsKey(_instrument))
		{
			return instrumentMappings[_instrument];
		}
		else
		{
			//Returns harmony if instrument doesn't exist in mappings
			return GD.MELODY;
		}
	}
	
}