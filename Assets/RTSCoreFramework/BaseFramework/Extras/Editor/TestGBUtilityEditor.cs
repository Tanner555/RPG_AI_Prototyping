using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BaseFramework
{
    [CustomEditor(typeof(SimpleSelectionLocker))]
    public class TestGBUtilityEditor : Editor
    {
        private TestGBUtility myTestGBUtility;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Test GB Utility Button"))
            {
                myTestGBUtility = (TestGBUtility) target;
                if (myTestGBUtility != null)
                {
                    Debug.Log("Hello from TestGBUtility");
                }
            }
        }
    }

}