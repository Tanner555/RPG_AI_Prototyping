using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFramework;
using RTSCoreFramework;

namespace RPGPrototype
{
    public class RPGGameMode : RTSGameMode
    {
        #region Properties
        //Static GameMode Instance For Easy Access
        [HideInInspector]
        public static new RPGGameMode thisInstance
        {
            get { return (RPGGameMode)GameMode.thisInstance; }
        }
        #endregion
    }
}