using System;
//using Microsoft.SPOT;

namespace RobotLibrary
{
    public class Mover
    {
        #region Enum
        public enum ActionList
        {
            NONE,
            STARTER,
            SHUTDOWN,
            RIGHT,
            LEFT,
            MIDDLE
        }
        #endregion

        #region Attribute
        //private Servomotor _servoUpDown;
        //private Servomotor _servoTurnArround;
        //private Servomotor _servoBorder;
        //private Servomotor _servoTopBottom;
        private ActionList _actionList;
        private bool _actionCompleted;
        private DateTime _nextBingo;
        #endregion

        #region Properties
        public ActionList Action
        {
            get { return _actionList; }
            set 
            {
                _nextBingo = DateTime.Now.AddSeconds(1);
                _actionCompleted = false;
                _actionList = value; 
            }
        }
        #endregion

        #region Constructor
        public Mover()
        {
            _nextBingo = DateTime.Now;
            _actionCompleted = true;
            _servoUpDown = new Servomotor_PandaII(6);
            _servoTurnArround = new Servomotor_PandaII(8);
            _servoBorder = new Servomotor_PandaII(9);
            _servoTopBottom = new Servomotor_PandaII(10);
        }
        #endregion

        #region Methods public
        public void Process()
        {
            switch (_actionList)
            {
                case ActionList.NONE:
                    break;
                case ActionList.LEFT:
                    break;
                case ActionList.MIDDLE:
                    break;
                case ActionList.RIGHT:
                    break;
                case ActionList.SHUTDOWN:
                    break;
                case ActionList.STARTER:
                    ActionStarter();
                    break;
            }
        }
        #endregion

        #region Methods private
        private void ActionStarter()
        {
            _servoUpDown.GoAngle(10);
            if (_nextBingo < DateTime.Now)
            {
                _actionCompleted = true;
            }
        }
        private void ActionShutdown()
        {
            _servoUpDown.GoAngle(0);
            if (_nextBingo < DateTime.Now)
            {
                _actionCompleted = true;
            }
        }
        #endregion
    }
}
