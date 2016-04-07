using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace ManagerMover
{
    public partial class Program
    {
        private double valX;
        private double valY;

        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/


            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");
            GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
            timer.Tick += timer_Tick;

            gyro.MeasurementInterval = new System.TimeSpan(0, 0, 0, 0, 100);
            gyro.Calibrate();
            gyro.MeasurementComplete += gyro_MeasurementComplete;

            timer.Start();
        }

        private void timer_Tick(GT.Timer timer)
        {
            gyro.StartTakingMeasurements();
            Thread.Sleep(500);
            gyro.StopTakingMeasurements();
            

            this.motor DriverL298.SetSpeed(MotorDriverL298.Motor.Motor1, 1);
            Thread.Sleep(500);
            this.motorDriverL298.SetSpeed(MotorDriverL298.Motor.Motor1, -1);
            Thread.Sleep(500);
            this.motorDriverL298.StopAll();
        }

        private void gyro_MeasurementComplete(Gyro sender, Gyro.MeasurementCompleteEventArgs e)
        {
            valX = e.X;
            valY = e.Y;

            Debug.Print("valX;" + DateTime.Now.ToString() + ";" + valX.ToString());
            Debug.Print("valY;" + DateTime.Now.ToString() + ";" + valY.ToString());
        }
    }
}
