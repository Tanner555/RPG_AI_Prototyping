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
        //Extra
        bool bUseAStarPath = false;
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
        protected override void OnAllyInitComponents(RTSAllyComponentSpecificFields _specific, RTSAllyComponentsAllCharacterFields _allFields)
        {
            base.OnAllyInitComponents(_specific, _allFields);
            var _RPGallAllyComps = (AllyComponentsAllCharacterFieldsRPG)_allFields;
            this.bUseAStarPath = _RPGallAllyComps.bUseAStarPath;
        }
        //void OnHealthUpdate(int _current, int _max)
        //{
        //    if(myHealthBar != null)
        //    {
        //        float _healthAsPercentage = ((float)_current / (float)_max);
        //        myHealthBar.fillAmount = _healthAsPercentage;
        //    }
        //}
        #endregion

        protected override void UpdateWaypointRenderer()
        {
            if (bUseAStarPath == false)
            {
                base.UpdateWaypointRenderer();
            }
        }
    }
}