using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotLibrary
{
    public static class ComponentBuilder
    {
        private enum BuildMode
        {
            PHYSICAL,
            SIMULATION,
            MONITORING
        }
        private const BuildMode _buildMode = BuildMode.PHYSICAL;

        //public static Servomotor GetServomotor(int id)
        //{
        //    Servomotor s = null;
        //    switch (_buildMode)
        //    {
        //        case BuildMode.MONITORING :
        //            break;
        //        case BuildMode.PHYSICAL :
        //            break;
        //        case BuildMode.SIMULATION :
        //            break;
        //    }
        //    return s;
        //}
    }
}
