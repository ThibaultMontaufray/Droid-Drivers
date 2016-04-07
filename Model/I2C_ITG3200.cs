using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace RobotLibrary
{
    class I2C_ITG3200
    {
		#region Attributes
        private I2CDevice ITG3200;
        private I2CDevice.I2CTransaction[] xActions;
		#endregion

		#region Properties
		public int ReadX
		{
			get { return ReadITG3200_X(); }
		}
		
		public int ReadY
		{
			get { return ReadITG3200_Y(); }
		}
		
		public int ReadZ
		{
			get { return ReadITG3200_Z(); }
		}
		
		public int ReadT
		{
			get { return ReadITG3200_T(); }
		}
		#endregion
		
		#region Constructor
        public I2C_ITG3200()
        {
            InitialiseITG3200();
		}
		#endregion

		#region Methods public
		public string DemoRead()
		{
            int x;
			int y;
			int z;
			int t;
		
			x = ReadITG3200_X();
			y = ReadITG3200_Y();
			z = ReadITG3200_Z();
			t = ReadITG3200_T();

			return "x=" + x.ToString() + ", y=" + y.ToString() + ", z=" + z.ToString() + ", t=" + t.ToString();
			// delay to allow the device to be ready
			//Thread.Sleep(100);
		}
		#endregion
		
		#region Methods privates
		private int ReadITG3200_X()
		{
			return ReadITG3200(0x1D);
		}
		
		private int ReadITG3200_Y()
		{
			return ReadITG3200(0x1F);
		}
		
		private int ReadITG3200_Z()
		{
			return ReadITG3200(0x21);
		}
		
		private int ReadITG3200_T()
		{
			return ReadITG3200(0x1B);
		}
		
		private int ReadITG3200(byte RegisterAddress)
		{
			xActions = new I2CDevice.I2CTransaction[2];
			byte[] SendBytes = new byte[1] { RegisterAddress };
			xActions[0] = I2CDevice.CreateWriteTransaction(SendBytes);
			byte[] ReceiveBytes = new byte[2];
			xActions[1] = I2CDevice.CreateReadTransaction(ReceiveBytes);
			int response = ITG3200.Execute(xActions, 1000);
			return (ReceiveBytes[1] << 8) | ReceiveBytes[0];
		}
		
		private void InitialiseITG3200()
		{
			I2CDevice.Configuration ITG3200Config = new I2CDevice.Configuration(0x69, 400);
			ITG3200 = new I2CDevice(ITG3200Config);
			xActions = new I2CDevice.I2CTransaction[1];
			byte[] SendByte = new byte[2] { 0x3E, 1 << 7 }; //Reg:[PWR_MGM] Bit7 = 1 - Hardware Reset
			xActions[0] = I2CDevice.CreateWriteTransaction(SendByte);
			int bytesTransfered = ITG3200.Execute(xActions, 1000);
			Thread.Sleep(100);
			SendByte = new byte[2] { 0x16, 0x18 }; //Reg:[DLPF_FS] +-2000Deg/Sec at 8KHz with 256Hz LPF
			xActions[0] = I2CDevice.CreateWriteTransaction(SendByte);
			bytesTransfered = ITG3200.Execute(xActions, 1000);
			Thread.Sleep(100);
			SendByte = new byte[2] { 0x3E, 1 }; //Reg:[PWR_MGM] PLL with X Gyro reference - All other bits = 0
			xActions[0] = I2CDevice.CreateWriteTransaction(SendByte);
			bytesTransfered = ITG3200.Execute(xActions, 1000);
		}
		#endregion
   }
}