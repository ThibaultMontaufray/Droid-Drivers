using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotLibrary
{
    public abstract class LEDMulticolor
    {
        #region Enum
        public enum COLOR
        {
            RED,
            POURPRE,
            SAUMON,
            YELLOW,
            GREEN,
            GREEN_LIGHT,
            AZUR,
            BLUE,
            PURPLE
        }
        #endregion

        #region Attribute
        protected int _stepColor;
        protected COLOR _color;
        protected int _addrR;
        protected int _addrG;
        protected int _addrB;
        protected int _addrEnable;
        protected bool _valR;
        protected bool _valG;
        protected bool _valB;
        protected bool _valEnable;
        #endregion

        #region Properties
        public new COLOR Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public new int AddrRed
        {
            get { return _addrR; }
            set { _addrR = value; }
        }
        public new int AddrGreen
        {
            get { return _addrG; }
            set { _addrG = value; }
        }
        public new int AddrBlue
        {
            get { return _addrB; }
            set { _addrB = value; }
        }
        public new int AddrEnable
        {
            get { return _addrEnable; }
            set { _addrEnable = value; }
        }
        public new bool ValRed
        {
            get { return _valR; }
            set { _valR = value; }
        }
        public new bool ValGreen
        {
            get { return _valG; }
            set { _valG = value; }
        }
        public new bool ValBlue
        {
            get { return _valB; }
            set { _valB = value; }
        }
        public new bool ValEnable
        {
            get { return _valEnable; }
            set { _valEnable = value; }
        }
        #endregion

        #region Methods
        public abstract void Step();
        public abstract void SetColorFromRGB(int r, int g, int b);
        #endregion
    }
}
