using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSCoreFramework;
using RPGPrototype;
using MaxyGames.uNode;

namespace RPGPrototype.Behaviors
{
    [NodeMenu("RPGPrototype/AllyMember", "GetRPGBehaviorActions")] //Add the node to the menu
    [MaxyGames.Events.BlockMenu("RPGPrototype/AllyMember", "GetRPGBehaviorActions")] //Add the node to the block menu
    public class GetRPGBehaviorActionsNode : DataNode<RPGBehaviorActions>
    {
        public Transform myTransform;

        private RPGBehaviorActions _behaviorActions = null;

        public override RPGBehaviorActions GetValue(object graph)
        {
            if (_behaviorActions == null)
                _behaviorActions = myTransform.GetComponent<AIControllerRPG>()
                    .BehaviorActionsInstance as RPGBehaviorActions;

            return _behaviorActions;
        }
    }
}