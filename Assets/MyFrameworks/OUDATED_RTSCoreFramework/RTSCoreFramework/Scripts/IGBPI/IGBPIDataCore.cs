﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OUTDATED_RTSCoreFramework
{
    [CreateAssetMenu(menuName = "RTSPrototype/IGBPIData")]
    public class IGBPIDataCore : ScriptableObject
    {
        public List<IGBPIPanelValue> IGBPIPanelData = new List<IGBPIPanelValue>();
    }
}