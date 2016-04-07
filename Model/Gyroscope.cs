using System;
using System.Threading;
using System.Collections;

using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;

namespace RobotLibrary
{
    public class Gyroscope
    {
        #region Attribute
        private int focus;

        private int refX;
        private int refY;
        private AnalogIn Xinput;
        private AnalogIn Yinput;
        private int inputValueX;
        private int inputValueY;
        private int posX;
        private int posY;
        #endregion

        #region Properties
        public int AccValueX
        {
            get { return inputValueX; }
        }
        
        public int AccValueY
        {
            get { return inputValueY; }
        }

        public int PositionX
        {
            get { return posX; }
        }

        public int PositionY
        {
            get { return posY; }
        }
        #endregion

        #region Constructor
        public Gyroscope(AnalogIn.Pin xIn, AnalogIn.Pin yIn)
        {
            focus = 7;

            Xinput = new AnalogIn(xIn);
            Yinput = new AnalogIn(yIn);
            Thread threadSensor = new Thread(new ThreadStart(getPosition));
            threadSensor.Start();
        }
        #endregion

        #region Methods Public
        public void Reset()
        {
            refX = readX();
            refY = readY();
            posX = 0;
            posY = 0;
        }
        #endregion

        #region Methods Private
        private int readX()
        {
            return readAxes(true);
        }

        private int readY()
        {
            return readAxes(false);
        }

        private int readAxes(bool X)
        {
            int count = 0;
            int val = 0;
            int moy = 0;
            int returnValue = 0;
            ArrayList valuesfirst = new ArrayList();

            for (int i = 0; i < 5000; i++)
            {
                if (X) val = Xinput.Read();
                else val = Yinput.Read();
                if (((val > focus) && (val > 0)) || ((val < (-1 * focus)) && (val < 0)))
                {
                    valuesfirst.Add(val);
                    moy += val;
                    count++;
                    if (count == 10) break;
                }
            }
            if (count > 4) moy = moy / count;
            else moy = 0;

            count = 0;
            foreach (int v in valuesfirst)
            {
                if ((v < (moy + (0.25 * moy))) && (v > (moy - (0.25 * moy))))
                {
                    returnValue += v;
                    count++;
                }
            }

            if (count > 4) returnValue = returnValue / count;
            else returnValue = 0;

            return returnValue;
        }

        private void setInputValueX()
        {
            int ret;
            ret = readX() - refX;
            inputValueX = ret;
        }

        private void setInputValueY()
        {
            int ret;
            ret = readY() - refY;
            inputValueY = ret;
        }

        private void setPositionX()
        {
            posX += inputValueX;
        }

        private void setPositionY()
        {
            posY += inputValueY;
        }

        private void getPosition()
        {
            Reset();
            // Blink board LED
            bool ledState = false;

            OutputPort led = new OutputPort((Cpu.Pin)FEZ_Pin.Digital.LED, ledState);
            while (true)
            {
                // toggle LED state
                ledState = !ledState;
                led.Write(ledState);

                setInputValueX();
                setInputValueY();
                setPositionX();
                setPositionY();
                Thread.Sleep(1);
            }
        }
        #endregion
    }
}
