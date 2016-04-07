using System;
using Microsoft.SPOT;
using System.IO.Ports;

namespace RobotLibrary
{
    public class Bluetooth
    {
        #region Attribute
        private COM com;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of the class
        /// </summary>
        public Bluetooth()
        {
            com = new COM("COM1");
        }
        #endregion

        #region Methods public
        /// <summary>
        /// Read the values on the peripheric.
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            com.Process();
            return com.ReceivedCommand.ToString();
        }
        /// <summary>
        /// write data on the periph
        /// </summary>
        /// <param name="val">value to send</param>
        /// <returns></returns>
        public void Write()
        {
            com.Send(TYPE_PACKET.ACK);
        }
        #endregion

        #region Methods private
        #endregion
    }
}
