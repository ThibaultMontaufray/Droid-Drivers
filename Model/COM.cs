using System;
using System.Collections;
using System.IO.Ports;

using Microsoft.SPOT;

namespace RobotLibrary
{
    public class COM
    {
        #region Attribute
        private DateTime lastReceived = DateTime.MaxValue.AddMilliseconds(-500);
        private SerialPort UART;
        private Queue data = new Queue();
        private TYPE_PACKET typePacketEnCours = TYPE_PACKET.NONE;
        private TYPE_PACKET lastPacket;
        private byte[] rx_data = new byte[1];
        private byte[] receivedQueue = new byte[8];
        private byte[] tb_writebuffer = new byte[8];
        private byte[] lastbuffer = new byte[8];
        private int delay = 200;
        private short receivedData;
        #endregion

        #region Properties
        public TYPE_PACKET LastPackage
        {
            get { return lastPacket; }
        }
        public int ReceivedCommand
        {
            get 
            {
                if (receivedQueue.Length > 7) return receivedQueue[7];
                else return 0;
            }
        }
        public short Data
        {
            get { return receivedData; }
        }
        #endregion

        #region Constructor
        public COM(string port)
        {
            UART = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
            UART.Open();
            UART.DiscardInBuffer();
            UART.DiscardOutBuffer();
        }
        #endregion

        #region Methods public
        public void SendLastCommunication()
        {
            UART.Write(lastbuffer, 0, 8);
        }
        public void Send(byte p, TYPE_PACKET typePacket)
        {
            if (isOpen)
            {
                BitConverter.GetBytes((byte)typePacket).CopyTo(tb_writebuffer, 0);
                BitConverter.GetBytes(p).CopyTo(tb_writebuffer, 1);
                Write3(tb_writebuffer);
            }
        }
        public void Send(byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, TYPE_PACKET typePacket)
        {
            if (isOpen)
            {
                BitConverter.GetBytes((byte)typePacket).CopyTo(tb_writebuffer, 0);
                BitConverter.GetBytes(b1).CopyTo(tb_writebuffer, 1);
                BitConverter.GetBytes(b2).CopyTo(tb_writebuffer, 2);
                BitConverter.GetBytes(b3).CopyTo(tb_writebuffer, 3);
                BitConverter.GetBytes(b4).CopyTo(tb_writebuffer, 4);
                BitConverter.GetBytes(b5).CopyTo(tb_writebuffer, 5);
                BitConverter.GetBytes(b6).CopyTo(tb_writebuffer, 6);
                Write(tb_writebuffer, 0, 8);
            }
        }
        public void Send(TYPE_PACKET typePacket)
        {
            if (isOpen)
            {
                BitConverter.GetBytes((byte)typePacket).CopyTo(tb_writebuffer, 0);
                Write(tb_writebuffer, 0, 8);
            }
        }
        public void Process()
        {
            try
            {
                if (UART.BytesToRead > 0)
                {
                    // lecture d'un octet recu
                    if (UART.Read(rx_data, 0, 1) > 0)
                    {
                        if (rx_data[0] == 255) data.Clear();
                        else data.Enqueue(rx_data[0]);
                        lastReceived = DateTime.Now;
                        //Log.Write("data received: " + rx_data[0].ToString());
                    }
                }
                if (data.Count > 0 && DateTime.Now > lastReceived.AddMilliseconds(200))
                {
                    data.Clear();
                }
            }
            catch (Exception ex)
            {
                Debug.Print("COM Exception in process method : " + ex.Message);
            }
            if ((typePacketEnCours == TYPE_PACKET.NONE) && (data.Count == 8))
            {
                data.CopyTo(receivedQueue, 0);
                typePacketEnCours = (TYPE_PACKET)(byte)data.Dequeue();

                if ((byte)(receivedQueue[0] ^ receivedQueue[1] ^ receivedQueue[2] ^ receivedQueue[3] ^ receivedQueue[4] ^ receivedQueue[5] ^ receivedQueue[6]) != receivedQueue[7])
                {
                    Send(TYPE_PACKET.ERREUR_COMMUNICATION);
                }
                else
                {
                    switch (typePacketEnCours)
                    {
                        case TYPE_PACKET.ERREUR_COMMUNICATION: lastPacket = TYPE_PACKET.ERREUR_COMMUNICATION; break;
                        case TYPE_PACKET.RequestIR1: lastPacket = TYPE_PACKET.RequestIR1; break;
                        case TYPE_PACKET.AnswerIR2: lastPacket = TYPE_PACKET.AnswerIR2; break;
                        default: break;
                    }
                    typePacketEnCours = TYPE_PACKET.NONE;
                    data.Clear();
                }
            }
        }
        public bool isOpen
        {
            get { return UART.IsOpen; }
        }
        #endregion

        #region Methods private
        private byte[] dataToSend = new byte[9];
        private void Write(byte[] tb_writebuffer, int p, int p_2)
        {
            //tb_writebuffer[7] = (byte)(tb_writebuffer[0] ^ tb_writebuffer[1] ^ tb_writebuffer[2] ^ tb_writebuffer[3] ^ tb_writebuffer[4] ^ tb_writebuffer[5] ^ tb_writebuffer[6]);
            //lastbuffer = tb_writebuffer;
            //UART.Write(tb_writebuffer, 0, 8);

            dataToSend[0] = 255;
            tb_writebuffer[7] = (byte)(tb_writebuffer[0] ^ tb_writebuffer[1] ^ tb_writebuffer[2] ^ tb_writebuffer[3] ^ tb_writebuffer[4] ^ tb_writebuffer[5] ^ tb_writebuffer[6]);

            for (int i = 0; i <= 6; i++)
            {
                if (tb_writebuffer[i] == 255) return;
            }

            lastbuffer = tb_writebuffer;
            tb_writebuffer.CopyTo(dataToSend, 1);
            UART.Write(dataToSend, 0, 9);
        }
        private void Write3(byte[] tb_writebuffer)
        {
            //tb_writebuffer[7] = (byte)(tb_writebuffer[0] ^ tb_writebuffer[1] ^ tb_writebuffer[2] ^ tb_writebuffer[3] ^ tb_writebuffer[4] ^ tb_writebuffer[5] ^ tb_writebuffer[6]);
            //lastbuffer = tb_writebuffer;
            //UART.Write(tb_writebuffer, 0, 8);

            dataToSend[0] = 255;
            tb_writebuffer.CopyTo(dataToSend, 1);
            UART.Write(dataToSend, 0, 3);
        }
        private short GetShort(Queue data)
        {
            return (short)((short)(byte)data.Dequeue() + (short)(byte)data.Dequeue() * 256);
        }
        private ushort GetUShort(Queue data)
        {
            return (ushort)((ushort)(byte)data.Dequeue() + (ushort)(byte)data.Dequeue() * 256);
        }
        private double GetDoubleData(Queue data)
        {
            return (double)((int)(byte)data.Dequeue() + (int)(byte)data.Dequeue() * 256);
        }
        #endregion
    }
}
