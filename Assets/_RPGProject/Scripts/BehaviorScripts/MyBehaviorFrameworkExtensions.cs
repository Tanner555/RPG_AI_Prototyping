using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGPrototype.Behaviors
{
    #region Enums
    public enum BehaviourFrameworkChoice
    {
        BehaviorDesigner = 0,
        UNode = 1,
        NodeCanvasFlowCanvas = 2,
        NoBehaviourTreeLoad = 3
    }

    //[Header("Behavior Framework To Use.")]
    //[SerializeField]
    //public BehaviourFrameworkChoice MyBehaviourChoice;
    #endregion

    public static class MyBehaviorFrameworkExtensions
    {
        /// <summary>
        /// Call this Method To Init Spawner Because Unity Msgs Have Been Decorated
        /// With bIsInitCustomNoUnityMsgs Boolean To Prevent Starting Before
        /// Graph Has Been Assigned.
        /// </summary>
        /// <param name="_spawner"></param>
        public static void CustomSpawnerInitNoUnityMsgs(this MaxyGames.uNode.uNodeSpawner _spawner)
        {            
            //Allows Unity Msgs Code To Run Again
            _spawner.bIsInitCustomNoUnityMsgs = true;
            _spawner.Awake();
            _spawner.OnEnable();
            _spawner.Start();
        }
    }
}