using System;
using System.Threading;

using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;

namespace RobotLibrary
{
    public class Servomotor
    {
        #region Attributes
        private PWM servo;
        private int currentAngle;
        #endregion

        #region Properties
        public int CurrentAngle
        {
            get { return currentAngle; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Init the servomotor pin and doesn't change the initial position
        /// </summary>
        /// <param name="servopin">PWM Servomotor pin</param>
        public Servomotor(FEZ_Pin.PWM servopin)
        {
            currentAngle = 0;
            servo = new PWM((PWM.Pin)servopin);
        }        
        /// <summary>
        /// Init the servomotor pin and allow you to choose the init position
        /// </summary>
        /// <param name="servopin">PWM Servomotor pin</param>
        /// <param name="position">Init position</param>
        public Servomotor(FEZ_Pin.PWM servopin, int position)
        {
            currentAngle = 0;
            servo = new PWM((PWM.Pin)servopin);
        }
        #endregion

        #region Methods public
        /// <summary>
        /// Check module, set to minimum, maximum and middle position
        /// </summary>
        public void Check()
        {
            SetMin();
            SetMax();
            SetMiddle();
        }
        /// <summary>
        /// Set the rotor to the position you need
        /// </summary>
        /// <param name="angle">position of rotor from 0 to 100</param>
        public void GoAngle(int angle)
        {
            //int cible = (angle * x) + 50;

        }
        /// <summary>
        /// Set the position of the rotor to minimum position
        /// </summary>
        public void SetMin()
        {
            servo.SetPulse(20 * 1000 * 1000, 500 * 1000);
        }
        /// <summary>
        /// Set the position of the rotor to maximum position
        /// </summary>
        public void SetMax()
        {
            servo.SetPulse(20 * 1000 * 1000, 1250 * 1000);
        }
        /// <summary>
        /// Set the position of the rotor to middle position
        /// </summary>
        public void SetMiddle()
        {
            servo.SetPulse(20 * 1000 * 1000, 2500 * 1000);
        }
        /// <summary>
        /// moving the rotor from minimum to maximum and maximum to minimum slowly and one time
        /// </summary>
        public void Scanning()
        {
            Thread.Sleep(100);//wait for a second

            for (uint i = 500; i < 600; i += 5)
            {
                servo.SetPulse(20 * 1000 * 1000, i * 1000);
                Thread.Sleep(3);
            }

            for (uint i = 600; i < 700; i += 5)
            {
                servo.SetPulse(20 * 1000 * 1000, i * 1000);
                Thread.Sleep(2);
            }

            for (uint i = 700; i < 2300; i += 5)
            {
                servo.SetPulse(20 * 1000 * 1000, i * 1000);
                Thread.Sleep(1);
            }

            for (uint i = 2300; i < 2400; i += 5)
            {
                servo.SetPulse(20 * 1000 * 1000, i * 1000);
                Thread.Sleep(2);
            }

            for (uint i = 2400; i < 2500; i += 5)
            {
                servo.SetPulse(20 * 1000 * 1000, i * 1000);
                Thread.Sleep(3);
            }

            Thread.Sleep(100);//wait for a second
                        
            for (uint i = 2500; i > 2400; i -= 5)
            {
                servo.SetPulse(20 * 1000 * 1000, i * 1000);
                Thread.Sleep(3);
            }

            for (uint i = 2400; i > 2300; i -= 5)
            {
                servo.SetPulse(20 * 1000 * 1000, i * 1000);
                Thread.Sleep(2);
            }

            for (uint i = 2300; i > 700; i -= 5)
            {
                servo.SetPulse(20 * 1000 * 1000, i * 1000);
                Thread.Sleep(1);
            }

            for (uint i = 700; i > 600; i -= 5)
            {
                servo.SetPulse(20 * 1000 * 1000, i * 1000);
                Thread.Sleep(2);
            }

            for (uint i = 600; i > 500; i -= 5)
            {
                servo.SetPulse(20 * 1000 * 1000, i * 1000);
                Thread.Sleep(3);
            }

            Thread.Sleep(100);//wait for a second
        }
        #endregion

        #region Methods private
        #endregion
    }
}
