using System;

namespace RobotLibrary
{
    public class LEDMulticolor_Hydra : LEDMulticolor
    {
        #region Constructor
        public LEDMulticolor_Hydra()
        {
            _stepColor = 0;
            _valEnable = true;
            _color = COLOR.YELLOW;
        }
        #endregion

        #region Methods public
        public override void Step()
        {
            bool[] b = Step(_color, _stepColor);
            _valR = b[0];
            _valG = b[1];
            _valB = b[2];
            _stepColor = (_stepColor + 1) % 65535;
        }
        public override void SetColorFromRGB(int r, int g, int b)
        {
            bool[] ret = FromRGB(r, g, b, _stepColor);
            _valR = ret[0];
            _valG = ret[1];
            _valB = ret[2];
            _stepColor = (_stepColor + 1) % 65535;
        }
        #endregion

        #region Methods private
        private bool[] Step(COLOR col, int step)
        {
            // Table of boolean
            // bool[] = {red, green, blue}
            switch (col)
            {
                case COLOR.AZUR:
                    return Azur(step);
                case COLOR.BLUE:
                    return Blue(step);
                case COLOR.GREEN:
                    return Green(step);
                case COLOR.YELLOW:
                    return GreenLemon(step);
                case COLOR.GREEN_LIGHT:
                    return GreenLight(step);
                case COLOR.POURPRE:
                    return Orange(step);
                case COLOR.PURPLE:
                    return Purple(step);
                case COLOR.RED:
                    return Red(step);
                case COLOR.SAUMON:
                    return Yellow(step);
                default: // No color
                    bool[] b = { true, true, true };
                    return b;
            }
        }
        private bool[] Step(int red, int green, int blue, int step)
        {
            return FromRGB(red, green, blue, step);
        }

        private bool[] Red(int step = 0)
        {
            //Program.pinOutRed.Write(false);
            //Program.pinOutGreen.Write(true);
            //Program.pinOutBlue.Write(true);
            bool[] b = { false, true, true };
            return b;
        }
        private bool[] GreenLemon(int step)
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    Program.pinOutRed.Write(false);
            //    Program.pinOutGreen.Write(false);
            //    Program.pinOutBlue.Write(true);
            //    Thread.Sleep(2);
            //    Program.pinOutRed.Write(true);
            //    Program.pinOutGreen.Write(false);
            //    Program.pinOutBlue.Write(true);
            //    Thread.Sleep(1);
            //}
            bool red = (step % 3 == 0) ? true : false;
            bool[] b = { red, false, true };
            return b;
        }
        private bool[] Azur(int step)
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    Program.pinOutRed.Write(true);
            //    Program.pinOutGreen.Write(false);
            //    Program.pinOutBlue.Write(false);
            //    Thread.Sleep(1);
            //    Program.pinOutRed.Write(true);
            //    Program.pinOutGreen.Write(true);
            //    Program.pinOutBlue.Write(false);
            //    Thread.Sleep(2);
            //}
            bool green = (step % 3 == 0) ? false : true;
            bool[] b = { true, green, false };
            return b;
        }
        private bool[] Pink(int step)
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    Program.pinOutRed.Write(false);
            //    Program.pinOutGreen.Write(true);
            //    Program.pinOutBlue.Write(false);
            //    Thread.Sleep(1);
            //    Program.pinOutRed.Write(true);
            //    Program.pinOutGreen.Write(true);
            //    Program.pinOutBlue.Write(false);
            //    Thread.Sleep(3);
            //}
            bool red = (step % 4 == 0) ? false : true;
            bool[] b = { red, true, false };
            return b;
        }
        private bool[] Orange(int step)
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    Program.pinOutRed.Write(false);
            //    Program.pinOutGreen.Write(false);
            //    Program.pinOutBlue.Write(true);
            //    Thread.Sleep(1);
            //    Program.pinOutRed.Write(false);
            //    Program.pinOutGreen.Write(true);
            //    Program.pinOutBlue.Write(true);
            //    Thread.Sleep(3);
            //}
            bool green = (step % 4 == 0) ? false : true;
            bool[] b = { false, green, true };
            return b;
        }
        private bool[] Blue(int step = 0)
        {
            //Program.pinOutRed.Write(true);
            //Program.pinOutGreen.Write(true);
            //Program.pinOutBlue.Write(false);
            bool[] b = { true, true, false };
            return b;
        }
        private bool[] GreenLight(int step = 0)
        {
            //Program.pinOutRed.Write(true);
            //Program.pinOutGreen.Write(false);
            //Program.pinOutBlue.Write(true);
            bool[] b = { true, false, true };
            return b;
        }
        private bool[] Purple(int step = 0)
        {
            //Program.pinOutRed.Write(false);
            //Program.pinOutGreen.Write(true);
            //Program.pinOutBlue.Write(false);
            bool[] b = { false, true, false };
            return b;
        }
        private bool[] Green(int step = 0)
        {
            //Program.pinOutRed.Write(true);
            //Program.pinOutGreen.Write(false);
            //Program.pinOutBlue.Write(false);
            bool[] b = { true, false, false };
            return b;
        }
        private bool[] Yellow(int step = 0)
        {
            //Program.pinOutRed.Write(false);
            //Program.pinOutGreen.Write(false);
            //Program.pinOutBlue.Write(true);
            bool[] b = { false, false, true };
            return b;
        }
        private bool[] FromRGB(int r, int g, int b, int step)
        {
            bool red = (step % 16 < r) ? false : true;
            bool green = (step % 16 < g) ? false : true;
            bool blue = (step % 16 < b) ? false : true;
            bool[] final = { red, green, blue };
            return final;
        }
        #endregion
    }
}
