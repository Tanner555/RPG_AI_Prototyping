using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class UnityMsgManager : BaseSingleton<UnityMsgManager>
    {
        #region FieldsAndProperties

        private bool bUpdateActionList = true;
        [SerializeField] private bool bRemoveUpdateActionOnErrno = true;
        [SerializeField] private bool bRemoveFixedUpdateActionOnErrno = true;
        private List<System.Action> UpdateActionList = new List<System.Action>();
        private List<System.Action> FixedUpdateActionList = new List<System.Action>();

        private System.Action currentUpdateAction = null;
        private System.Action currentFixedUpdateAction = null;
        #endregion

        #region UnityMessages

        // Update is called once per frame
        void Update()
        {
            if (bUpdateActionList && UpdateActionList != null && UpdateActionList.Count > 0)
            {
                for (int i = 0; i < UpdateActionList.Count; i++)
                {
                    currentUpdateAction = null;
                    currentUpdateAction = UpdateActionList[i];
                    if (currentUpdateAction == null) 
                    { 
                        RemoveUnusableUpdateAction(currentUpdateAction, true, i); 
                        break;
                    }
                    if (bRemoveUpdateActionOnErrno)
                    {
                        try
                        {
                            currentUpdateAction();
                        }
                        catch (System.Exception _updateErrno)
                        {
                            RemoveUnusableUpdateAction(currentUpdateAction, false, i, _updateErrno.Message);
                            break;
                        }
                    }
                    else
                    {
                        currentUpdateAction();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (bUpdateActionList && FixedUpdateActionList != null && FixedUpdateActionList.Count > 0)
            {
                for (int i = 0; i < FixedUpdateActionList.Count; i++)
                {
                    currentFixedUpdateAction = null;
                    currentFixedUpdateAction = FixedUpdateActionList[i];
                    if (currentFixedUpdateAction == null) 
                    { 
                        RemoveUnusableFixedUpdateAction(currentFixedUpdateAction, true, i); 
                        break;
                    }
                    if (bRemoveFixedUpdateActionOnErrno)
                    {
                        try
                        {
                            currentFixedUpdateAction();
                        }
                        catch (System.Exception _updateErrno)
                        {
                            RemoveUnusableFixedUpdateAction(currentFixedUpdateAction, false, i, _updateErrno.Message);
                            break;
                        }
                    }
                    else
                    {
                        currentFixedUpdateAction();
                    }
                }
            }
        }

        void OnDisable()
        {
            bUpdateActionList = false;
            UpdateActionList.Clear();
            FixedUpdateActionList.Clear();
        }

        #endregion

        #region RegisterUnityEvents

        public void RegisterOnUpdate(System.Action updateMethodAction)
        {
            UpdateActionList.Add(updateMethodAction);
        }

        public void DeregisterOnUpdate(System.Action updateMethodAction, bool bFromIndex = false, int updateMethodIndex = -1)
        {
            if (bFromIndex)
            {
                UpdateActionList.RemoveAt(updateMethodIndex);
            }
            else
            {
                UpdateActionList.Remove(updateMethodAction);
            }            
        }

        public void RegisterOnFixedUpdate(System.Action fixedUpdateMethodAction)
        {
            FixedUpdateActionList.Add(fixedUpdateMethodAction);         
        }

        public void DeregisterOnFixedUpdate(System.Action fixedUpdateMethodAction, bool bFromIndex = false, int fixedUpdateMethodIndex = -1)
        {
            if (bFromIndex)
            {
                FixedUpdateActionList.RemoveAt(fixedUpdateMethodIndex);
            }
            else
            {
                FixedUpdateActionList.Remove(fixedUpdateMethodAction);
            }            
        }

        #endregion

        #region HelperMethods

        private void RemoveUnusableUpdateAction(System.Action updateMethodAction, bool bWasNull, int updateMethodIndex, string errnoMsg = "")
        {
            string _errormsg = bWasNull ? $"Update Method {updateMethodAction} was Null, Removing..." : 
                $"Update Method Error From {updateMethodAction}: {errnoMsg}. Removing...";
            Debug.LogWarning(_errormsg);
            //If Null, we need to remove from index. Otherwise use value to remove.
            DeregisterOnUpdate(updateMethodAction, bWasNull, updateMethodIndex);       
        }

        private void RemoveUnusableFixedUpdateAction(System.Action fixedUpdateMethodAction, bool bWasNull, int fixedUpdateMethodIndex, string errnoMsg = "")
        {
            string _errormsg = bWasNull ? $"Fixed Update Method {fixedUpdateMethodAction} was Null, Removing..." :
                $"Fixed Update Method Error From {fixedUpdateMethodAction}: {errnoMsg}. Removing...";
            Debug.LogWarning(_errormsg);
            //If Null, we need to remove from index. Otherwise use value to remove.
            DeregisterOnFixedUpdate(fixedUpdateMethodAction, bWasNull, fixedUpdateMethodIndex);
        }

        #endregion
    }
}