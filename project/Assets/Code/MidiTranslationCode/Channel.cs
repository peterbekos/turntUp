using System;
using System.Collections.Generic;
using UnityEngine;

    public class Channel
    {
        public String ChannelName;
        public int ChannelNumber;
        public List<Note> Notes = new List<Note>();
        public List<String> TypesOfNotes = new List<String>();
        public Dictionary<string, GameObject> BeatCubeMapping = new Dictionary<string, GameObject>();
        public int lastNote = 0;
    }

