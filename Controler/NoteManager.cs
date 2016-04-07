using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace RobotLibrary
{
    public static class NoteManager
    {
        #region Attributes
        private static PWM biwuit;
        #endregion

        #region Methods public
        /// <summary>
        /// Initialisation of pins to work. if not initialised, the Note Manager cannot work.
        /// </summary>
        /// <param name="pin">The PWM pin of your card.</param>
        public static void Init(PWM pin)
        {
            biwuit = pin;
            
            //n = new Note("la", 18, 100, biwuit);
            //n = new Note("la#", 18, 100, biwuit);
            //n = new Note("si", 18, 100, biwuit);
            //n = new Note("do", 18, 100, biwuit);
            //n = new Note("do#", 18, 100, biwuit);
            //n = new Note("re", 18, 100, biwuit);
            //n = new Note("re#", 18, 100, biwuit);
            //n = new Note("mi", 18, 100, biwuit);
            //n = new Note("fa", 18, 100, biwuit);
            //n = new Note("fa#", 18, 100, biwuit);
            //n = new Note("sol", 18, 100, biwuit);
            //n = new Note("sol#", 18, 100, biwuit);
            //n = new Note("la", 35, 100, biwuit);
            //n = new Note("la#", 35, 100, biwuit);
            //n = new Note("si", 35, 100, biwuit);
            //n = new Note("do", 35, 100, biwuit);
            //n = new Note("do#", 35, 100, biwuit);
            //n = new Note("re", 35, 100, biwuit);
            //n = new Note("re#", 35, 100, biwuit);
            //n = new Note("mi", 35, 100, biwuit);
            //n = new Note("fa", 35, 100, biwuit);
            //n = new Note("fa#", 35, 100, biwuit);
            //n = new Note("sol", 35, 100, biwuit);
            //n = new Note("sol#", 35, 100, biwuit);
        }
        /// <summary>
        /// Play the sequence HAPPY
        /// </summary>
        public static void HAPPY()
        {
            Note n;
            n = new Note("mi", 35, 15, biwuit);
            n = new Note("fa", 35, 15, biwuit);
            n = new Note("fa#", 35, 15, biwuit);
            n = new Note("sol", 35, 15, biwuit);
            n = new Note("sol#", 35, 25, biwuit);
            biwuit.Set(false);
            Thread.Sleep(25);
            n = new Note("mi", 35, 15, biwuit);
            n = new Note("fa", 35, 15, biwuit);
            n = new Note("fa#", 35, 15, biwuit);
            n = new Note("sol", 35, 15, biwuit);
            n = new Note("sol#", 35, 25, biwuit);
            biwuit.Set(false);
            Thread.Sleep(25);
            n = new Note("mi", 35, 15, biwuit);
            n = new Note("fa", 35, 15, biwuit);
            n = new Note("fa#", 35, 15, biwuit);
            n = new Note("sol", 35, 15, biwuit);
            n = new Note("sol#", 35, 25, biwuit);
        
            biwuit.Set(false);
        }
        /// <summary>
        /// Play the sequence QUESTION
        /// </summary>
        public static void QUESTION()
        {
            Note n;
            n = new Note("re", 35, 50, biwuit);
            n = new Note("do#", 35, 25, biwuit);
            n = new Note("do", 35, 25, biwuit);
            n = new Note("do#", 35, 25, biwuit);
            n = new Note("mi", 35, 15, biwuit);
            n = new Note("re", 35, 15, biwuit);
            n = new Note("re#", 35, 15, biwuit);
            n = new Note("mi", 35, 15, biwuit);
            n = new Note("fa", 35, 15, biwuit);

            biwuit.Set(false);
        }
        /// <summary>
        /// Play the sequence HUNGRY
        /// </summary>
        public static void HUNGRY()
        {
            Note n;
            n = new Note("la", 9, 150, biwuit);
            biwuit.Set(false);
            Thread.Sleep(50);
            n = new Note("la", 9, 150, biwuit);

            biwuit.Set(false);
        }
        /// <summary>
        /// Play the sequence DISAGREE
        /// </summary>
        public static void DISAGREE()
        {
            Note n;
            n = new Note("fa", 35, 25, biwuit);
            n = new Note("fa#", 35, 100, biwuit);
            biwuit.Set(false);
            Thread.Sleep(25);
            n = new Note("fa", 9, 100, biwuit);
            biwuit.Set(false);
            Thread.Sleep(25);
            n = new Note("fa", 9, 100, biwuit);

            biwuit.Set(false);
        }
        #endregion
    }
}
