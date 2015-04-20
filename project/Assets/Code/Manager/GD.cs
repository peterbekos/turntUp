using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

[Serializable]
public class HighScoreTable{
	
	public int[] scores = new int[10];
	public string[] names = new string[10];
	
	public HighScoreTable(){
	
	}
	
	public HighScoreTable(string name){
		loadScores(name);
	}
	
	public int getScore(int i){
		return scores[i-1];
	}
	
	public string getName(int i){
		return names[i-1];
	}
	
	public void addScore(int s, string n){
		int i = 10; //check lowest score first
		
		while(i >=1 && s > scores[i-1]){ //check each score if you beat it
			i--;
		}
		
		if( i == 10 ) return; //if you didn't beat the lowest score, gtfo
		
		for(int j = 9; j > i; j--){
			if(j == 0) break;
			scores[j] = scores[j-1];
			names[j] = names[j-1];
		}
		scores[i] = s;
		names[i] = n;
	}
	
	public void saveScores(string filename){
		  BinaryFormatter bf = new BinaryFormatter();
		  FileStream fs = File.Create (Application.persistentDataPath + "/" + GameManager.gameTimer.levelName + ".scores");
		  
		  bf.Serialize(fs, this);
		  fs.Close();
	}
	
	public void loadScores(string filename){
		//Debug.Log(Application.persistentDataPath);
		if(File.Exists(Application.persistentDataPath + "/" + GameManager.gameTimer.levelName + ".scores")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = File.Open(Application.persistentDataPath + "/" + GameManager.gameTimer.levelName + ".scores", FileMode.Open);
			
			HighScoreTable hst = (HighScoreTable)bf.Deserialize(fs);
			scores = hst.scores;
			names = hst.names;
			
			fs.Close();
		}
		else{ //set defaults
			scores = new int[] { 5000000, 4500000, 3500000, 2000000, 1000000, 500000, 100000, 50000, 25000, 10000 };  //if you get under 10,000 points you need to consider your life choices
			names = new string[] { "TREVOR", "ADAM", "LUKASZ", "PETER", "VINCE", "JONATHAN", "TIAN", "SAD TREVOR", "CJ", "SUP" }; //sup
			saveScores(filename);
		}
	}
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

    //If X returns -99, there is no pitch
    public static int getPitchNumber(string _pitch)
    {
        int x = -99;
        if (_pitch.Length <= 2)
        {
            char firstChar = char.ToUpper(_pitch[0]);

            switch (firstChar)
            {
                case 'C':
                    x = 0;
                    break;
                case 'D':
                    x = 2;
                    break;
                case 'E': //There is no E#
                    x = 4;
                    break;
                case 'F':
                    x = 5;
                    break;
                case 'G':
                    x = 7;
                    break;
                case 'A':
                    x = 9;
                    break;
                case 'B': //There is no B#
                    x = 11;
                    break;
                default:
                    break;
            }
        }

        
        if (_pitch.Length == 2)
        {
            char secondChar = _pitch[1];

            switch (secondChar)
            {
                case '1':
                    x =  x + (1 * 12);
                    break;
                case '2':
                    x = x + (2 * 12);
                    break;
                case '3':
                    x = x + (3 * 12);
                    break;
                case '4':
                    x = x + (4 * 12);
                    break;
                case '5':
                    x = x + (5 * 12);
                    break;
                case '6':
                    x = x + (6 * 12);
                    break;
                case '7':
                    x = x + (7 * 12);
                    break;
                case '8':
                    x = x + (8 * 12);
                    break;
                case '9':
                    x = x + (9 * 12);
                    break;
                case '#':
                    x++;
                    break;
                default:
                    break;

            }          
        }
        return x;
    }


	
}