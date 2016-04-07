using System;
using Microsoft.SPOT;
using GHIElectronics.NETMF.FEZ;

namespace RobotLibrary
{
    public class MoveManager
    {
        #region Attribute
        private MoveMotorCC motorLeft;
        private MoveMotorCC motorRight;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of Move Manager : allow you to control you robot by simple instructions
        /// </summary>
        public MoveManager()
        {
            //motorLeft = new MoveMotorCC(FEZ_Pin.Digital.Di0, FEZ_Pin.Digital.Di1, FEZ_Pin.PWM.Di8);
            //motorRight = new MoveMotorCC(FEZ_Pin.Digital.Di4, FEZ_Pin.Digital.Di5, FEZ_Pin.PWM.Di9);
            motorLeft = new MoveMotorCC(FEZ_Pin.Digital.Di6, FEZ_Pin.Digital.Di7, FEZ_Pin.PWM.Di5);
            motorRight = new MoveMotorCC(FEZ_Pin.Digital.Di9, FEZ_Pin.Digital.Di10, FEZ_Pin.PWM.Di8);
        }
        #endregion

        #region Methods Public
        /// <summary>
        /// Let all wheels free
        /// </summary>
        public void free()
        {
            motorLeft.free();
            motorRight.free();
        }
        /// <summary>
        /// Bloc all actuators like a break.
        /// </summary>
        public void block()
        {
            motorLeft.block();
            motorRight.block();
        }
        /// <summary>
        /// module rotation on right activated
        /// </summary>
        public void goRight()
        {
            motorLeft.trigo();
            motorRight.horaire();
        }
        /// <summary>
        /// module rotation on left activated
        /// </summary>
        public void goLeft()
        {
            motorLeft.horaire();
            motorRight.trigo();
        }
        /// <summary>
        /// module go ahead activated
        /// </summary>
        public void goAhead()
        {
            motorLeft.trigo();
            motorRight.trigo();
        }
        /// <summary>
        /// module go back activated
        /// </summary>
        public void goBack()
        {
            motorLeft.horaire();
            motorRight.horaire();
        }
        /// <summary>
        /// do an arc on the left and not a rotation on his own
        /// </summary>
        public void arcLeft()
        {
            motorRight.trigo();
        }
        /// <summary>
        /// do an arc on the right and not a rotation on his own
        /// </summary>
        public void arcRight()
        {
            motorLeft.trigo();
        }
        #endregion
    }
}
