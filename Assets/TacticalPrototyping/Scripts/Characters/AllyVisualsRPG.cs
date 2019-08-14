using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTSCoreFramework;

namespace RPGPrototype
{
    public class AllyVisualsRPG : AllyVisuals
    {
        #region Fields
        //[SerializeField]
        //Image myHealthBar;
        #endregion

        #region UnityMessages
        protected override void Start()
        {
            base.Start();
            myEventHandler.OnHealthChanged += OnHealthUpdate;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            myEventHandler.OnHealthChanged -= OnHealthUpdate;
        }
        #endregion

        #region Handlers
        //void OnHealthUpdate(int _current, int _max)
        //{
        //    if(myHealthBar != null)
        //    {
        //        float _healthAsPercentage = ((float)_current / (float)_max);
        //        myHealthBar.fillAmount = _healthAsPercentage;
        //    }
        //}
        #endregion
    }
}