using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;

namespace RobotLibrary
{
    public class Note
    {
        #region Attributes
        private string name;
        private int octave;
        private int tempo;
        private PWM biwuit;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for note playing
        /// </summary>
        /// <param name="noteName">named note if you know it</param>
        /// <param name="noteoctave">the octave to play more or less hight</param>
        /// <param name="noteTempo">the rhytm you need</param>
        /// <param name="notBiwuit">pin of your speaker</param>
        public Note(string noteName, int noteoctave, int noteTempo, PWM notBiwuit)
        {
            biwuit = notBiwuit;
            name = noteName;
            octave = noteoctave;
            tempo = noteTempo;
            playnote();
        }
        #endregion

        #region Methods public
        /// <summary>
        /// Play the configurated note.
        /// </summary>
        public void playnote()
        {
            switch (name)
            {
                case "sol#": //do
                    biwuit.SetPulse(uint.Parse((41860100/octave).ToString()), 1000000);
                    Thread.Sleep(tempo);
                    break;
                case "sol":
                    biwuit.SetPulse(uint.Parse((44349200/octave).ToString()), 1000000);
                    Thread.Sleep(tempo);
                    break;
                case "fa#":
                    biwuit.SetPulse(uint.Parse((46986400/octave).ToString()), 1000000);
                    Thread.Sleep(tempo);
                    break;
                case "fa":
                    biwuit.SetPulse(uint.Parse((49780300/octave).ToString()), 1000000);
                    Thread.Sleep(tempo);
                    break;
                case "mi":
                    biwuit.SetPulse(uint.Parse((52740400/octave).ToString()), 1000000);
                    Thread.Sleep(tempo);
                    break;
                case "re#":
                    biwuit.SetPulse(uint.Parse((55876500/octave).ToString()), 1000000);
                    Thread.Sleep(tempo);
                    break;
                case "re":
                    biwuit.SetPulse(uint.Parse((59199100/octave).ToString()), 1000000);
                    Thread.Sleep(tempo);
                    break;
                case "do#":
                    biwuit.SetPulse(uint.Parse((62719300/octave).ToString()), 1000000);
                    Thread.Sleep(tempo);
                    break;
                case "do":
                    biwuit.SetPulse(uint.Parse((66448800/octave).ToString()), 1000000);
                    Thread.Sleep(tempo);
                    break;
                case "si":
                    biwuit.SetPulse(uint.Parse((70400000/octave).ToString()), 1000000);
                    Thread.Sleep(tempo);
                    break;
                case "la#":
                    biwuit.SetPulse(uint.Parse((74586200/octave).ToString()), 1000000);
                    Thread.Sleep(tempo);
                    break;
                case "la":
                    biwuit.SetPulse(uint.Parse((79021300/octave).ToString()), 1000000);
                    Thread.Sleep(tempo);
                    break;
            }
        }
        #endregion
    }
}
