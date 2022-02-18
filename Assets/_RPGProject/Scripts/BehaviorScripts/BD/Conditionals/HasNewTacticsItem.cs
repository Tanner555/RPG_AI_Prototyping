using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Compares Current To Previous Tactics Item To Determine If New Tactics Item Was Obtained. If Current Tactics Equal Past Tactics, Condition Will Fail. CheckPreviousNullInstead Boolean Will Check Previous Tactics Instead.")]
	public class HasNewTacticsItem : Conditional
	{
		#region Shared
		public SharedBool CheckPreviousNullInstead;
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

		private AllyTacticsItem CurrentExecutionItem_Cached;
		private AllyMember CurrentExecutionTarget_Cached;
		private AllyTacticsItem PreviousExecutionItem_Cached;
		private AllyMember PreviousExecutionTarget_Cached;

		#endregion

		public override TaskStatus OnUpdate()
		{
			CurrentExecutionItem_Cached = CurrentExecutionItem.Value;
			CurrentExecutionTarget_Cached = CurrentExecutionTarget.Value;
			PreviousExecutionItem_Cached = PreviousExecutionItem.Value;
			PreviousExecutionTarget_Cached = PreviousExecutionTarget.Value;
			var _taskResult = behaviorActions.HasNewTacticsItem(ref CurrentExecutionItem_Cached,
				ref CurrentExecutionTarget_Cached,
				ref PreviousExecutionItem_Cached,
				ref PreviousExecutionTarget_Cached, CheckPreviousNullInstead.Value) ? TaskStatus.Success : TaskStatus.Failure;
			CurrentExecutionItem.Value = CurrentExecutionItem_Cached;
			CurrentExecutionTarget.Value = CurrentExecutionTarget_Cached;
			PreviousExecutionItem.Value = PreviousExecutionItem_Cached;
			PreviousExecutionTarget.Value = PreviousExecutionTarget_Cached;
			return _taskResult;
		}
	}
}