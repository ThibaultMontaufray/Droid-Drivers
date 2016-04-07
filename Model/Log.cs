using System;
using Microsoft.SPOT;

namespace RobotLibrary
{
    public delegate LogHandler LogHandler(string text);

    public static class Log
    {
        #region Attribute
        private static bool log_console;
        private static bool log_bluetooth;
        private static bool log_sd_card;
        private static bool log_communication;
        private static string LogToWrite;
        private static SDCard sdcard;
        private static Bluetooth bluetooth;
        private static Communication communication;
        #endregion

        #region Event
        /// <summary>
        /// Event when bluetooth logs have been generated
        /// </summary>
        public static event LogHandler OnLogBluetooth;
        /// <summary>
        /// Event when console logs have been generated
        /// </summary>
        public static event LogHandler OnLogConsole;
        /// <summary>
        /// Event when SDCard logs have been generated
        /// </summary>
        public static event LogHandler OnLogSDCard;
        /// <summary>
        /// Event when communication logs on bus have been generated
        /// </summary>
        public static event LogHandler OnLogCommunication;
        #endregion

        #region Properties
        /// <summary>
        /// Access to the consol mode. If false, no console logs will be generated
        /// </summary>
        public static bool LogConsole
        {
            get
            {
                if (!log_console.Equals(null))
                    return log_console;
                else
                    return false;
            }
            set { log_console = value; }
        }
        /// <summary>
        /// Access to the bluetooth mode. If false, no bluetooth logs will be generated
        /// </summary>
        public static bool LogBluetooth
        {
            get
            {
                if (!log_bluetooth.Equals(null))
                    return log_bluetooth;
                else
                    return false;
            }
            set { log_bluetooth = value; }
        }
        /// <summary>
        /// Access to the sdcard mode. If false, no sdcard logs will be generated
        /// </summary>
        public static bool LogSDCard
        {
            get
            {
                if (!log_sd_card.Equals(null))
                    return log_sd_card;
                else
                    return false;
            }
            set { log_sd_card = value; }
        }
        /// <summary>
        /// Access to the bus mode. If false, no message on communication bus logs will be generated
        /// </summary>
        public static bool LogCommunication
        {
            get
            {
                if (!log_communication.Equals(null))
                    return log_communication;
                else
                    return false;
            }
            set { log_communication = value; }
        }
        /// <summary>
        /// Allow to parameter the sdcard periphric to logs
        /// </summary>
        public static SDCard SdCard
        {
            get { return sdcard; }
            set { sdcard = value; }
        }
        /// <summary>
        /// Allow to parameter the bluetooth periphric to logs
        /// </summary>
        public static Bluetooth BlueTooth
        {
            get { return bluetooth; }
            set { bluetooth = value; }
        }
        /// <summary>
        /// Allow to parameter the communication periphric to logs
        /// </summary>
        public static Communication Communication
        {
            get { return communication; }
            set { communication = value; }
        }
        #endregion
        
        #region Methods public
        /// <summary>
        /// Generate logs in the identified output(s)
        /// </summary>
        /// <param name="val">the text you want to generate</param>
        public static void Write(string val)
        {
            LogToWrite = val;
            if (LogConsole) GenerateConsole();
            if (LogBluetooth) GenerateBluetooth();
            if (LogSDCard) GenerateSDCard();
            if (LogCommunication) GenerateCommunication();
        }
        #endregion

        #region Methods private
        /// <summary>
        /// Event when console logs is generated
        /// </summary>
        private static void OnConsole()
        {
            OnLogConsole(LogToWrite);
        }
        /// <summary>
        /// Event when bluetooth logs is generated
        /// </summary>
        private static void OnBluetooth()
        {
            OnLogBluetooth(LogToWrite);
        }
        /// <summary>
        /// Event when SDCard logs is generated
        /// </summary>
        private static void OnSDCard()
        {
            OnLogSDCard(LogToWrite);
        }
        /// <summary>
        /// Event when Communication logs is generated
        /// </summary>
        private static void OnCommunication()
        {
            OnLogCommunication(LogToWrite);
        }
        
        /// <summary>
        /// Generate the event and the code for console loggin
        /// </summary>
        private static void GenerateConsole()
        {
            OnConsole();
            Debug.Print(LogToWrite);
        }
        /// <summary>
        /// Generate the event and the code for Bluetooth loggin
        /// </summary>
        private static void GenerateBluetooth()
        {
            OnBluetooth();
            if (bluetooth != null) bluetooth.Write(LogToWrite);
        }
        /// <summary>
        /// Generate the event and the code for SDCard loggin
        /// </summary>
        private static void GenerateSDCard()
        {
            OnSDCard();
            if (sdcard != null) sdcard.Write(LogToWrite);
        }
        /// <summary>
        /// Generate the event and the code for bus loggin
        /// </summary>
        private static void GenerateCommunication()
        {
            OnCommunication();
            communication.Write(LogToWrite);
        }
        #endregion
    }
}
