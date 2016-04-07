using System.Threading;
using System.Text;
using Microsoft.SPOT;
using System.IO;
using Microsoft.SPOT.IO;
using GHIElectronics.NETMF.IO;
using System;

namespace RobotLibrary
{
    public class SDCard
    {
        #region Attribute
        private const string fileName = @"Log.txt";
        private static string filePath = string.Empty;
        private static System.IO.FileStream logger;
        private static Timer tiFlushAuto;
        private static bool modeSansSd = false;
        private static PersistentStorage sdPS;
        private static bool forceDebug = false;
        private static byte[] bufferByte = new byte[512];
        private static int bufferLenght = 0;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of the class
        /// </summary>
        public SDCard()
        {
            Init();
        }
        #endregion

        #region Methods public
        /// <summary>
        /// Initialisation of the SD card usage. Mount card and create a log file.
        /// </summary>
        public void Init()
        {
            try
            {
                if (GHIElectronics.NETMF.IO.PersistentStorage.DetectSDCard())
                {
                    sdPS = new PersistentStorage("SD");
                    sdPS.MountFileSystem();

                    string directory = VolumeInfo.GetVolumes()[0].RootDirectory;
                    filePath = Path.Combine(directory, fileName);

                    string file = Path.GetFileNameWithoutExtension(filePath);

                    // search an useless file name
                    for (int i = 0; File.Exists(filePath); i++)
                        filePath = Path.Combine(Path.GetDirectoryName(filePath), file + i.ToString() + Path.GetExtension(filePath));

                    logger = new FileStream(filePath, FileMode.Create);

                    try
                    {
                        byte[] data = Encoding.UTF8.GetBytes("Test d'ecriture");
                        // Ecrit les données et ferme le fichier
                        logger.Write(data, 0, data.Length);
                        ClearBuffer();
                        if (forceDebug)
                            Debug.Print("SD Card inserted. Activation of debug mode");
                        else
                            Debug.Print("Activation of debug mode");
                        
                    }
                    finally
                    {
                        logger.Close();
                    }


                    Write("Log initialisation ...");

                    // timer de flush auto
                    tiFlushAuto = new Timer(new TimerCallback(tiFlushAuto_Tick), null, 2000, 2000);
                }
                else
                {
                    modeSansSd = true;
                    Debug.Print("No SD Card detected.");
                }
            }
            catch (Exception ex)
            {
                modeSansSd = true;
                Debug.Print("SD card detected but error on mount operation : "+ex.Message);
            }

        }
        /// <summary>
        /// Write the message on SD Card
        /// </summary>
        /// <param name="strToLog">Message to write</param>
        public void Write(string strToLog)
        {
            if (modeSansSd || forceDebug)
                Debug.Print(strToLog);
            if (!modeSansSd)
            {
                // buffer locking to avoid losing data while write operation

                AppendBuffer(DateTime.Now.ToString("HH:mm:ss.fff "));
                AppendBuffer(strToLog);
                AppendBuffer("\r\n");
            }
        }
        /// <summary>
        /// Close the SD Card mount
        /// </summary>
        public void Close()
        {
            tiFlushAuto.Dispose();
        }
        #endregion

        #region Methods private
        /// <summary>
        /// Add data at the end of the buffer
        /// </summary>
        /// <param name="str">string that you want to add</param>
        private void AppendBuffer(String str)
        {
            foreach (byte c in Encoding.UTF8.GetBytes(str))
            {
                lock (bufferByte)
                {
                    bufferByte[bufferLenght] = c;
                    bufferLenght++;
                }
                if (bufferLenght >= bufferByte.Length)
                    Flush();
            }
        }
        /// <summary>
        ///  buffer d'écriture
        /// </summary>
        private void tiFlushAuto_Tick(object sender)
        {
            Flush();
        }
        /// <summary>
        /// Clean the buffer data
        /// </summary>
        private void Flush()
        {
            if (bufferLenght > 0)
            {
                if (!modeSansSd)
                {
                    lock (bufferByte)
                    {
                        logger = new FileStream(filePath, FileMode.Append);
                        lock (logger)
                        {
                            try
                            {
                                // Ecrit les données et ferme le fichier
                                logger.Write(bufferByte, 0, bufferLenght);
                                ClearBuffer();
                            }
                            finally
                            {
                                logger.Close();
                                GC.SuppressFinalize(logger);
                            }
                        }
                    }
                }
                else
                {
                    lock (bufferByte)
                    {
                        Debug.Print(new String(UTF8Encoding.UTF8.GetChars(bufferByte)));

                        ClearBuffer();
                    }
                }
            }
        }
        /// <summary>
        /// Clean the object buffer
        /// </summary>
        private void ClearBuffer()
        {
            bufferLenght = 0;
        }
        #endregion
    }
}
