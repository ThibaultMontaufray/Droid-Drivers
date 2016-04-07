using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.Hardware;
using GHIElectronics.NETMF.FEZ;

namespace RobotLibrary
{
    class I2C_PCF8574P
    {
        #region Attribute
        I2CDevice.I2CTransaction[] xActions;
        I2CDevice I2C_Port;
        I2CDevice.Configuration config;
        byte[] RegisterNum;
        #endregion

        #region Constructor
        public I2C_PCF8574P(Int16 specific_adress)
        {
            // 0100XXX0
            // XXX : 210
            // 0 = écriture
            // donc 01000000

            //Commande des Leds de la carte SISI
            // Création d'un objet I2C (Acces aux Leds @ = 20h, F = 400kHz
            //I2CDevice.Configuration config = new I2CDevice.Configuration(0x70, 400);
            config = new I2CDevice.Configuration(0x20, 400);
            I2C_Port = new I2CDevice(config);
            //Création des transactions (1 seule ici)
            xActions = new I2CDevice.I2CTransaction[1];

        }
        #endregion

        #region Methods
        public void writeBit(int bit)
        {
            // Création d'un buffer d'écriture  (un seul octet)
            RegisterNum = new byte[1] { 0x01 }; // Eclairage des Leds
            // Chargement de la transaction
            xActions[0] = I2CDevice.CreateWriteTransaction(RegisterNum);

            // Acces au bus I2C avec un timeout de 1s si les LEDs ne répondent pas !
            I2C_Port.Execute(xActions, 1000);
            Thread.Sleep(500);
        }
        #endregion
    }
}
