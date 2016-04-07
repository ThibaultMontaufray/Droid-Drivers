using System;
using System.Text;

namespace RobotLibrary
{
    public delegate delegateHandler delegateHandler(int x, int y, bool val);

    public class Eyes
    {
        #region Attributes
        private bool[][] matriceNew;
        private bool[][] matriceCurrent;

        public event delegateHandler EyesChanged;
        #endregion

        #region Properties
        public bool[][] MatriceNew
        {
            get { return matriceNew; }
            set { matriceNew = value; }
        }
        public bool[][] MatriceCurrent
        {
            get { return matriceCurrent; }
            set { matriceCurrent = value; }
        }
        #endregion

        #region Constructor
        public Eyes()
        {
            BuildMatrice(8, 24);
            InitMatrice();
        }
        #endregion

        #region Methods publics
        public void animation(string designation)
        {
            switch (designation)
            {
                case "WAKEUP": WAKEUP(); break;
                case "NORMAL": NORMAL(); break;
                case "SLEEPY": SLEEPY(); break;
                case "HAPPPY": HAPPY(); break;
                case "HUNGRY": HUNGRY(); break;
                case "PERPLEXE": PERPLEXE(); break;
                case "ETONNE": ETONNE(); break;
            }
        }
        #endregion

        #region Methods protected
        protected void OnEyesChanged(int x, int y, bool val)
        {
            if (EyesChanged != null)
            {
                EyesChanged(x, y, val);
            }
        }
        #endregion

        #region Methods private
        private void SetValue(int x, int y, bool val)
        {
            matriceCurrent[x][y] = val;
            OnEyesChanged(x, y, val);
        }
        private bool GetValue(int x, int y)
        {
            return matriceCurrent[x][y];
        }
        private void BuildMatrice(int heigh, int width)
        {
            matriceNew = new bool[heigh][];
            matriceCurrent = new bool[heigh][];
            for (int i = 0; i < heigh; i++)
            {
                bool[] l_new = new bool[width];
                bool[] l_current = new bool[width];
                for(int j=0 ; j<width ; j++)
                {
                    l_new[j] = false;
                    l_current[j] = false;
                }
                matriceNew[i] = l_new;
                matriceCurrent[i] = l_current;
            }
        }
        private void InitMatrice()
        {
            for (int i = 0; i < matriceNew.Length; i++)
            {
                for (int j = 0; j < matriceNew[i].Length; j++)
                {
                    matriceNew[i][j] = false;
                }
            }
        }
        private void RefreshMatrice()
        {
            for (int i = 0; i < matriceNew.Length; i++)
            {
                for (int j = 0; j < matriceNew[i].Length; j++)
                {
                    if (GetValue(i, j) != matriceNew[i][j])
                    {
                        SetValue(i, j, matriceNew[i][j]);
                    }
                }
            }
        }
        private void NORMAL()
        {
            InitMatrice();
            for (int i = 0; i < 3; i++)
            {
                matriceNew[3 + i][5] = true;
                matriceNew[3 + i][6] = true;
                matriceNew[3 + i][7] = true;
                matriceNew[3 + i][8] = true;
                matriceNew[3 + i][9] = true;

                matriceNew[3 + i][16] = true;
                matriceNew[3 + i][17] = true;
                matriceNew[3 + i][18] = true;
                matriceNew[3 + i][19] = true;
                matriceNew[3 + i][20] = true;
            }
            RefreshMatrice();
        }
        private void WAKEUP()
        {
            InitMatrice();
            for (int i = 2; i < 3; i++)
            {
                matriceNew[i][4] = true;
                matriceNew[i][5] = true;
                matriceNew[i][6] = true;
                matriceNew[i][7] = true;
                matriceNew[i][8] = true;
                matriceNew[i][15] = true;
                matriceNew[i][16] = true;
                matriceNew[i][17] = true;
                matriceNew[i][18] = true;
                matriceNew[i][19] = true;
            }
            RefreshMatrice();
        }
        private void SLEEPY()
        {
            InitMatrice();
            matriceNew[4][4] = true;
            matriceNew[4][5] = true;
            matriceNew[4][6] = true;
            matriceNew[4][7] = true;
            matriceNew[4][8] = true;
            matriceNew[4][15] = true;
            matriceNew[4][16] = true;
            matriceNew[4][17] = true;
            matriceNew[4][18] = true;
            matriceNew[4][19] = true;
        }
        private void HAPPY()
        {
            InitMatrice();
        }
        private void HUNGRY()
        {
            InitMatrice();
        }
        private void PERPLEXE()
        {
            InitMatrice();
        }
        private void ETONNE()
        {
            InitMatrice();
        }
        #endregion
    }
}
