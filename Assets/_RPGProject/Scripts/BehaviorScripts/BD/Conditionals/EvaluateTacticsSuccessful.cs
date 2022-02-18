using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
    [TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Returns True if Evaluating Tactics Successfully Returns A Tactics Item.")]
    public class EvaluateTacticsSuccessful : Conditional
	{
        #region Shared
        public SharedTacticsItem CurrentExecutionItem;
        public SharedAllyMember CurrentExecutionTarget;
        #endregion

        #region BehaviorActions
        RPGBehaviorActions behaviorActions
        {
            get
            {
                if (_behaviorActions == null)
                {
                    _behaviorActions = GetComponent<AIControllerRPG>().BehaviorActionsInstance as RPGBehaviorActions;
                }
                return _behaviorActions;
            }
        }
        RPGBehaviorActions _behaviorActions = null;

        public AllyTacticsItem CurrentExecutionItem_Cached;
        public AllyMember CurrentExecutionTarget_Cached;

        #endregion

        #region Overrides
        public override TaskStatus OnUpdate()
        {
            CurrentExecutionItem_Cached = CurrentExecutionItem.Value;
            CurrentExecutionTarget_Cached = CurrentExecutionTarget.Value;
            var _taskResult = behaviorActions.EvaluateTacticsSuccessful(ref CurrentExecutionItem_Cached,
                ref CurrentExecutionTarget_Cached) ? 
                TaskStatus.Success : TaskStatus.Failure;
            CurrentExecutionItem.Value = CurrentExecutionItem_Cached;
            CurrentExecutionTarget.Value = CurrentExecutionTarget_Cached;
            return _taskResult;
        }
        #endregion

    }
}