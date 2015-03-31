using System;

    public class Note
    {
        public double startTime; //In Milliseconds
        public double durationTime; //In Milliseconds
        public String instrumentName;
        public int noteVelocity;
        public String notePitch; //Will return -99 if there is no pitch
        public int notePitchNumber;
        public bool active = false;
		public bool done = false;
    }

