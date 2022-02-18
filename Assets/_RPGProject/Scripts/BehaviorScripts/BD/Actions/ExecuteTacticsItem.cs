using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
    [TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Executes The Given Tactics Item with Target. StopPerformingTask Determines If Stop Performing Task Should Be Executed Instead. Make Sure The CurrentTacticsItem and Target aren't null when executing an action. If Stopping, make sure previous tactics item isn't null either. Current will be reset if not stopping, and previous will be reset after stopping.")]
    public class ExecuteTacticsItem : Action
	{
        #region Shared
        public SharedBool StopPerformingTask;
        public SharedTacticsItem CurrentExecutionItem;
		public SharedAllyMember CurrentExecutionTarget;
        public SharedTacticsItem PreviousExecutionItem;
        public SharedAllyMember PreviousExecutionTarget;
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
        public AllyTacticsItem PreviousExecutionItem_Cached;
        public AllyMember PreviousExecutionTarget_Cached;

        #endregion

        #region Overrides
        public override TaskStatus OnUpdate()
		{
            CurrentExecutionItem_Cached = CurrentExecutionItem.Value;
            CurrentExecutionTarget_Cached = CurrentExecutionTarget.Value;
            PreviousExecutionItem_Cached = PreviousExecutionItem.Value;
            PreviousExecutionTarget_Cached = PreviousExecutionTarget.Value;
            var _taskResult = behaviorActions.ExecuteTacticsItem(ref CurrentExecutionItem_Cached,
                ref CurrentExecutionTarget_Cached,
                ref PreviousExecutionItem_Cached,
                ref PreviousExecutionTarget_Cached, StopPerformingTask.Value) ? TaskStatus.Success : TaskStatus.Failure;
            CurrentExecutionItem.Value = CurrentExecutionItem_Cached;
            CurrentExecutionTarget.Value = CurrentExecutionTarget_Cached;
            PreviousExecutionItem.Value = PreviousExecutionItem_Cached;
            PreviousExecutionTarget.Value = PreviousExecutionTarget_Cached;
            return _taskResult;
        }
        #endregion

    }
}