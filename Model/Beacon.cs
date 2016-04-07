using System;
using System.Threading;

using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;

using RobotLibrary;

namespace RobotLibrary
{
    /// <summary>
    /// Class to control a classi beacon only light)
    /// Log index 90 - offset 5
    /// </summary>
    public class Beacon
    {
        #region Attributes
        private Servomotor servo;
        private static OutputPort red_light;
        private static OutputPort yellow_light;
        private static Thread t_alternative;
        private static Thread t_animation;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for balise type light
        /// </summary>
        /// <param name="_ia">the ia reference</param>
        /// <param name="_servo">servomotor to deploy balise</param>
        /// <param name="_red_light">pin for red light</param>
        /// <param name="_yellow_light">pin for yellow light</param>
        public Beacon(Servomotor _servo, OutputPort _red_light, OutputPort _yellow_light)
        {
            Log.Write("[INF][90000][RBT_LIB] Construction of the beacon");
            servo = _servo;
            red_light = _red_light;
            yellow_light = _yellow_light;

            t_alternative = new Thread(alternative);
            t_animation = new Thread(animation);
        }
        #endregion

        #region Methods public
        /// <summary>
        /// run the animation with the right color 3 times
        /// </summary>
        public void ConfirmColor()
        {
            Log.Write("[INF][90001][RBT_LIB] confirmation color");
            int time_s = 100;
            servo_top();
            lightTurnOn();
            Thread.Sleep(time_s);
            lightTurnOff();
            Thread.Sleep(time_s);
            lightTurnOn();
            Thread.Sleep(time_s);
            lightTurnOff();
            Thread.Sleep(time_s);
            lightTurnOn();
            Thread.Sleep(time_s);
            lightTurnOff();
            servo_bottom();
        }
        /// <summary>
        /// run the animation with color
        /// </summary>
        public void AnimationRun()
        {
            Log.Write("[INF][90002][RBT_LIB] Animation launching");
            if (t_alternative.IsAlive) t_alternative.Abort();
            t_animation.Start();
        }
        /// <summary>
        ///  stop the animation with color
        /// </summary>
        public static void AnimationStop()
        {
            Log.Write("[INF][90003][RBT_LIB] Animation stop");
            t_animation.Abort();
        }
        /// <summary>
        /// Launch the animation to choose color
        /// </summary>
        public void ColorChoseStart()
        {
            Log.Write("[INF][90004][RBT_LIB] choose color start");
            servo_top();
            t_alternative.Start();
        }
        /// <summary>
        /// Stop the animation to select the color
        /// </summary>
        public void ColorChoseStop()
        {
            Log.Write("[INF][90005][RBT_LIB] Color choose stop");
            t_alternative.Abort();
            servo_bottom();
        }
        #endregion

        #region Methods private
        private void alternative()
        {
            while (true)
            {
                red_light.Write(true);
                yellow_light.Write(false);
                Thread.Sleep(500);
                yellow_light.Write(true);
                red_light.Write(false);
                Thread.Sleep(500);
            }
        }
        private void animation()
        {
            while (true)
            {
                yellow_light.Write(false);
                red_light.Write(false);
                Thread.Sleep(500);
                if (Variables.TeamColor == TEAM_COLOR.RED)
                {
                    red_light.Write(true);
                }
                else if (Variables.TeamColor == TEAM_COLOR.YELLOW)
                {
                    yellow_light.Write(true);
                }
                Thread.Sleep(500);
            }
        }
        private void lightTurnOn()
        {
            if (Variables.TeamColor == TEAM_COLOR.RED)
            {
                red_light.Write(true);
                yellow_light.Write(false);
            }
            else if (Variables.TeamColor == TEAM_COLOR.YELLOW)
            {
                red_light.Write(false);
                yellow_light.Write(true);
            }
            else
            {
                red_light.Write(false);
                yellow_light.Write(false);
            }
        }
        private void lightTurnOff()
        {
            red_light.Write(false);
            yellow_light.Write(false);
        }
        private void servo_top()
        {
            servo.GoAngle(0);
        }
        private void servo_bottom()
        {
            servo.GoAngle(10);
        }
        #endregion
    }
}
