using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.Hardware;
using GHIElectronics.NETMF.FEZ;


namespace RobotLibrary
{
    public class MoveMotorSBS
    {
        #region Attribute
        private OutputPort enable;      // 0 active 1 desactive
        private OutputPort direction;   // -1 horaire 1 trigo
        private OutputPort standby;     // standby the card 0 fr rrunning 1 to shutdown
        private PWM command;
        private bool standby_state;
        private int position;
        #endregion

        #region Properties
        /// <summary>
        /// activate or disable the standbye mode if exist on the command card.
        /// </summary>
        public bool Standby
        {
            get { return standby_state; }
            set 
            { 
                standby_state = value;
                standby.Write(standby_state);
            }
        }
        /// <summary>
        /// Give the position of the step by step motor from the last initialisation
        /// </summary>
        public int Position
        {
            get { return position; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to control a step by step motor
        /// </summary>
        /// <param name="ena">pin to enable / disable the motor</param>
        /// <param name="dir">pin to determine the direction of the motor</param>
        /// <param name="stb">standbye mode for the card</param>
        /// <param name="step">PWM pin for the speed of the motor</param>
        public MoveMotorSBS(FEZ_Pin.Digital ena, FEZ_Pin.Digital dir, FEZ_Pin.Digital stb, PWM.Pin step)
        {
            enable = new OutputPort((Cpu.Pin)ena, false);
            direction = new OutputPort((Cpu.Pin)dir, false);
            standby = new OutputPort((Cpu.Pin)stb, true);   // first the card is no activate
            position = 0;
            command = new PWM((PWM.Pin)step);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reinitialisation of the position
        /// </summary>
        public void resetPosition()
        {
            position = 0;
        }
        /// <summary>
        /// check the motor by horaire / trigo rotation. Let the rotor free at the end.
        /// </summary>
        public void check()
        {
            stepHoraire(10, 10);
            stepTrigo(10, 10);
            free();
        }
        /// <summary>
        /// Bloc the rotor
        /// </summary>
        public void block()
        {
            command.Set(false);
            enable.Write(false);
            direction.Write(true);
        }
        /// <summary>
        /// Let the rotor free
        /// </summary>
        public void free()
        {   
            command.Set(false);
            enable.Write(true);
            direction.Write(false);
        }
        /// <summary>
        /// to activate rotation in horaire mode 
        /// </summary>
        public void horaire()
        {
            command.Set(3000, 50);
            enable.Write(false);
            direction.Write(true);
        }
        /// <summary>
        /// to activate rotation in trigo mode
        /// </summary>
        public void trigo()
        {
            command.Set(3000, 50);
            enable.Write(false);
            direction.Write(false);
        }
        /// <summary>
        /// determined step in horaire mode
        /// </summary>
        /// <param name="nbStep">the number of step you want to do</param>
        /// <param name="speed">the speed of the pwm</param>
        public void stepHoraire(int nbStep, int speed)
        {
            enable.Write(false);
            direction.Write(true);
            for (int i = 0; i < nbStep; i++)
            {
                position++;
                command.Set(true);
                waitUs(speed);
                command.Set(false);
                waitUs(speed);
            }
        }
        /// <summary>
        /// determined step in trigo mode
        /// </summary>
        /// <param name="nbStep">the number of step you want to do</param>
        /// <param name="speed">the speed of the pwm</param>
        public void stepTrigo(int nbStep, int speed)
        {
            enable.Write(false);
            direction.Write(false);
            for (int i = 0; i < nbStep; i++)
            {
                position--;
                command.Set(true);
                waitUs(speed);
                command.Set(false);
                waitUs(speed);
            }
        }
        #endregion

        #region Methods Private
        /// <summary>
        /// if you have to wait less than 1 ms (not in basic librarys)
        /// </summary>
        /// <param name="delay">the delay you want to wait in micro second</param>
        private static void waitUs(int delay)
        {
            for (int i = 0; i < delay; i++)
            {
            }
        }
        #endregion
    }
}
