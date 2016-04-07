using System;
using System.Threading;

using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace RobotLibrary
{
    public class MoveMotorCC
    {
        #region Attribute
        private OutputPort input1;
        private OutputPort input2;
        private PWM pwm;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for a motor drive
        /// </summary>
        /// <param name="in1">pin 1 for the sens</param>
        /// <param name="in2">pin 2 for the sens</param>
        /// <param name="inPWM">control the speed of the motor</param>
        public MoveMotorCC(FEZ_Pin.Digital in1, FEZ_Pin.Digital in2, FEZ_Pin.PWM inPWM)
        {
            // initial to free mode (no instrustion, motor not blocked)
            input1 = new OutputPort((Cpu.Pin)in1, false);
            input2 = new OutputPort((Cpu.Pin)in2, false);
            pwm =  new PWM((PWM.Pin)inPWM);
        }
        #endregion

        #region Methods
        /// <summary>
        /// run the motor in horaire / trigo and let free rotor at the end
        /// </summary>
        public void check()
        {
            horaire();
            Thread.Sleep(500);
            trigo();
            Thread.Sleep(500);
            free();
        }
        /// <summary>
        /// Bloc the rotor of the motor
        /// </summary>
        public void block()
        {
            pwm.Set(false);
            input1.Write(true);
            input2.Write(true);
        }
        /// <summary>
        /// free wheel mode : to allow rotor to do anything
        /// </summary>
        public void free()
        {
            pwm.Set(false);
            input1.Write(false);
            input2.Write(false);
        }
        /// <summary>
        /// horaire rotation of the rotor
        /// </summary>
        public void horaire()
        {
            pwm.Set(true);
            input1.Write(false);
            input2.Write(true);
        }
        /// <summary>
        /// trigo rotation of the rotor
        /// </summary>
        public void trigo()
        {
            pwm.Set(true);
            input1.Write(true);
            input2.Write(false);
        }
        #endregion
    }
}
