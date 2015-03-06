    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using NAudio.Midi;
    using UnityEngine;

    public class MidiFileInspector 
    {
        #region IAudioFileInspector Members

        public string FileExtension
        {
            get { return ".mid"; }
        }

        public string FileTypeDescription
        {
            get { return "Standard MIDI File"; }
        }


        float milliSecondsPerQuartNote;
        private MidiEvent currentMidiEvent;

        public List<Channel> Channels = new List<Channel>();

        public string Describe(string fileName)
        {
            MidiFile mf = new MidiFile(fileName, false);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Format {0}, Tracks {1}, Delta Ticks Per Quarter Note {2}\r\n",
                mf.FileFormat, mf.Tracks, mf.DeltaTicksPerQuarterNote);


            var timeSignature = mf.Events[0].OfType<NAudio.Midi.TimeSignatureEvent>().FirstOrDefault();
            bool found_BPM = false;

            for (int n = 0; n < mf.Tracks; n++)
            {
                foreach (MidiEvent midiEvent in mf.Events[n])
                {
                    if(!found_BPM)
                    {
                        if(midiEvent.ToString().Contains("bpm"))
                        {
                           found_BPM = true;
                           String extractMPQ = midiEvent.ToString();
                           int index = extractMPQ.LastIndexOf('m') + 3;
                           String intermediateString = extractMPQ.Remove(0, index); 
                           int indexLastParanthesis = intermediateString.LastIndexOf(')');
                           String finalString = intermediateString.Remove(indexLastParanthesis, 1);

                           int microsecPerQuartNote = Convert.ToInt32(finalString);
                           milliSecondsPerQuartNote =  (float)((microsecPerQuartNote) / 1000.0);
                           Debug.Log(milliSecondsPerQuartNote + " Milliseconds per quarter note");
                           //String finalMPQ = extractMPQ.Remove()
                           //Console.WriteLine("Just hit BPM");
                        }
                    }
                    if (!MidiEvent.IsNoteOff(midiEvent))
                    {
                        currentMidiEvent = midiEvent;
                        sb.AppendFormat("{0} {1}\r\n", ToMBT(midiEvent.AbsoluteTime, mf.DeltaTicksPerQuarterNote, timeSignature), midiEvent);
                    }
                }
            }
            return sb.ToString();
        }


        private string ToMBT(long eventTime, int ticksPerQuarterNote, NAudio.Midi.TimeSignatureEvent timeSignature)
        {
            int beatsPerBar = timeSignature == null ? 4 : timeSignature.Numerator;
            int ticksPerBar = timeSignature == null ? ticksPerQuarterNote * 4 : (timeSignature.Numerator * ticksPerQuarterNote * 4) / (1 << timeSignature.Denominator);
            int ticksPerBeat = ticksPerBar / beatsPerBar;
            long bar = 1 + (eventTime / ticksPerBar);
            long beat = 1 + ((eventTime % ticksPerBar) / ticksPerBeat);
            long tick = eventTime % ticksPerBeat;

            String initialMidiString = currentMidiEvent.ToString();

            int currentChannel = getChannelNumber(initialMidiString);
            int noteLength = getMidiNoteLength(initialMidiString);

            if(noteLength != 0 && currentChannel > -1)
            {

                //START TIME AND DURATION TIME ARE CALCULATED HERE!!!!
                Note newNote = new Note();
                Double theStartTime = (bar * milliSecondsPerQuartNote * 4) + (milliSecondsPerQuartNote * (beat - 1));
                newNote.startTime = theStartTime; //Start time Measured in milliseconds
                String midiInfo = currentMidiEvent.ToString();
                String typeOfNote = getTypeOfNote(midiInfo, currentChannel);
                newNote.NoteType = typeOfNote;
                Double timeOfNote = noteLength;
                newNote.durationTime = ((((double)(timeOfNote))/(double)ticksPerBeat)* milliSecondsPerQuartNote);//Duration slso meseaured in milliseconds
                Channels[currentChannel].Notes.Add(newNote);

                if(!Channels[currentChannel].TypesOfNotes.Contains(typeOfNote))
                {
                    Channels[currentChannel].TypesOfNotes.Add(typeOfNote);
                }
            }

            
            string finalReturn = String.Format("{0}:{1}:{2}", bar, beat, tick);
            Console.WriteLine(finalReturn);
            return finalReturn;
            //Print out this string to see full output
            
        }

        private int getMidiNoteLength(String initialMidi)
        {
            String intermediateMidi = initialMidi.Remove(0, initialMidi.LastIndexOf(':') + 1);

            //I feel like there is a faster way of doing this
            try
            {
                return Convert.ToInt32(intermediateMidi);
            }
            catch(Exception ex)
            {
                Debug.Log(ex.ToString());
                return 0;
            }
        }

        //Bunch of string manipulation
        private String getTypeOfNote(String midiInfo, int currentClassChannel)
        {
            int currentChannel = Channels[currentClassChannel].ChannelNumber;
            String compareAgainst = "Ch: " + currentChannel;
            int firstNum = midiInfo.IndexOf(compareAgainst);
            int firstCut = firstNum + currentChannel.ToString().Length + 4;
            int secondCut = midiInfo.IndexOf("Vel") - firstCut;
            String returnValue = midiInfo.Substring(firstCut, secondCut).Trim();
            return returnValue;
        }

        private int getChannelNumber(String initialMidi)
        {
            if (initialMidi.Contains("PatchChange"))
            {

                Console.WriteLine(initialMidi);
                String intermediateMidi1 = initialMidi.Remove(0, initialMidi.LastIndexOf(':') + 2);
                String numberSub = intermediateMidi1.Substring(0, intermediateMidi1.IndexOf(' '));
                String intermediateMidi2 = intermediateMidi1.Remove(0, initialMidi.IndexOf(' '));
                int channelNumber = Convert.ToInt32(numberSub);
                Console.WriteLine(channelNumber.ToString() + " - " + intermediateMidi2);

                Channel newChannel = new Channel();
                newChannel.ChannelName = intermediateMidi2;
                newChannel.ChannelNumber = channelNumber;
                Channels.Add(newChannel);
                
            }

            return Channels.Count - 1;
        }

        private void printInformation()
        {
            for(int i = 0; i < Channels.Count; i++)
            {
                Debug.Log("Channels[" + i + "] - " + Channels[i].ChannelName);
                for(int j = 0; j < Channels[i].Notes.Count; j++)
                {
                    Debug.Log("Channels[" + i + "] - " + "Note #" + j + Channels[i].Notes[j].NoteType);
                }
            }
        }

        /// <summary>
        /// Find the number of beats per measure
        /// (for now assume just one TimeSignature per MIDI track)
        /// </summary>
        private int FindBeatsPerMeasure(IEnumerable<MidiEvent> midiEvents)
        {
            int beatsPerMeasure = 4;
            foreach (MidiEvent midiEvent in midiEvents)
            {
                TimeSignatureEvent tse = midiEvent as TimeSignatureEvent;
                if (tse != null)
                {
                    beatsPerMeasure = tse.Numerator;
                }
            }
            return beatsPerMeasure;
        }


        #endregion
    }

