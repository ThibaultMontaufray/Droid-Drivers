using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotLibrary
{
    public class LEDMatrice_Hydra : LEDMatrice
    {
        #region Attribute
        private bool[] _tableA = new bool[32];
        private bool[] _tableB = new bool[32];
        private bool[] _tableC = new bool[32];
        private bool[][] _matrice;
        private const int WIDTH = 18;
        private const int HEIGH = 6;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public LEDMatrice_Hydra()
        {
        }
        #endregion

        #region Methods public
        public void ResetTable()
        {
            for (int i = 0; i < _tableA.Length; i++)
            {
                _tableA[i] = false;
            }
            for (int i = 0; i < _tableB.Length; i++)
            {
                _tableB[i] = false;
            }
            for (int i = 0; i < _tableC.Length; i++)
            {
                _tableC[i] = false;
            }
            _matrice = new bool[WIDTH][];
            for (int i = 0; i < _matrice.Length; i++)
            {
                _matrice[i] = new bool[HEIGH];
            }
        }
        public void SetPin(int x, int y, bool val)
        {
            _matrice[x][y] = val;
        }
        public void Refresh()
        {
            RefreshMatrice();
        }
        #endregion

        #region Methods private
        private void RefreshMatrice()
        {
            // transpose global matrice into a panel matrice table
        }
        #endregion
    }
}
