using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace RobotLibrary
{
    public class I2C_ADXL345
    {
		#region Attributes
        private I2CDevice ADXL345;
        private I2CDevice.I2CTransaction[] xActions;
		#endregion

        #region Properties
		public int ReadX
		{
			get { return ReadADXL345_X(); }
		}
		public int ReadY
		{
			get { return ReadADXL345_Y(); }
		}
		public int ReadZ
		{
			get { return ReadADXL345_Z(); }
		}

        public float AngleX
        {
            get
            {
                return (ReadX * 90) / 275;
                //int ang = ReadX;
                //if (ang < 300)
                //{
                //    return (ang * (90)) / 275;
                //}
                //else
                //{
                //    return ((ang - 65535) * 90) / 275;
                //}
            }
        }
        public double AngleY
        {
            get
            {
                return (ReadY * 90) / 275;
                //int ang = ReadY;
                //if (ang < 300)
                //{
                //    return (ang * (90)) / 275;
                //}
                //else
                //{
                //    return ((ang-65535) * 90) / 275;
                //}
            }
        }
        public double AngleZ
        {
            get
            {
                return (ReadZ * 90) / 275;
                //int ang = ReadZ;
                //if (ang < 300)
                //{
                //    return (ang * (90)) / 275;
                //}
                //else
                //{
                //    return ((ang - 65535) * 90) / 275;
                //}
            }
        }
		#endregion
		
		#region Constructor
        public I2C_ADXL345()
        {
            InitialiseADXL345();
        }
		#endregion

		#region Methods public
		public string DemoRead()
		{
			int x;
			int y;
			int z;
		
			x = ReadADXL345_X();
			y = ReadADXL345_Y();
			z = ReadADXL345_Z();

			return "x=" + x.ToString() + ", y=" + y.ToString() + 30 + ", z=" + z.ToString();
			// delay to allow the device to be ready
			//Thread.Sleep(100);
		}
		#endregion
		
		#region Methods privates
		private int ReadADXL345_X()
		{
            int ret = ReadADXL345(0x32);
            if (ret < 300)
                return ret;
            else
                return ret - 65536;
		}
		
		private int ReadADXL345_Y()
        {
            int ret = ReadADXL345(0x34);
            if (ret < 300)
                return ret;
            else
                return ret - 65536;
		}
		
		private int ReadADXL345_Z()
        {
            int ret = ReadADXL345(0x36);
            if (ret < 300)
                return ret;
            else
                return ret - 65536;
		}
		
        private int ReadADXL345(byte RegisterAddress)
        {
            xActions = new I2CDevice.I2CTransaction[2];
            byte[] SendBytes = new byte[1] { RegisterAddress };
            xActions[0] = I2CDevice.CreateWriteTransaction(SendBytes);
            byte[] ReceiveBytes = new byte[2];
            xActions[1] = I2CDevice.CreateReadTransaction(ReceiveBytes);
            int response = ADXL345.Execute(xActions, 1000);

            return (ReceiveBytes[1] << 8) | ReceiveBytes[0];
        }

        private void InitialiseADXL345()
        {
             I2CDevice.Configuration ADXL345Config = new I2CDevice.Configuration(0x53, 400);
             ADXL345 = new I2CDevice(ADXL345Config);
 
            xActions = new I2CDevice.I2CTransaction[3];
            byte[] SendByte = new byte[2] { 0x2D, 0 };
            xActions[0] = I2CDevice.CreateWriteTransaction(SendByte);
            SendByte = new byte[2] { 0x2D, 1 << 4 };
            xActions[1] = I2CDevice.CreateWriteTransaction(SendByte);
            SendByte = new byte[2] { 0x2D, 1 << 3 };
            xActions[2] = I2CDevice.CreateWriteTransaction(SendByte);
            int response = ADXL345.Execute(xActions, 1000);
        }
		#endregion
    }
}