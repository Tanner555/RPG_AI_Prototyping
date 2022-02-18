using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Resets Execution Items And Targets, Previous and Current.")]
	public class ResetTacticsItems : Action
	{
		#region Shared
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
			var _taskResult = behaviorActions.ResetTacticsItems(ref CurrentExecutionItem_Cached,
				ref CurrentExecutionTarget_Cached,
				ref PreviousExecutionItem_Cached, 
				ref PreviousExecutionTarget_Cached) ? TaskStatus.Success : TaskStatus.Failure;
			CurrentExecutionItem.Value = CurrentExecutionItem_Cached;
			CurrentExecutionTarget.Value = CurrentExecutionTarget_Cached;
			PreviousExecutionItem.Value = PreviousExecutionItem_Cached;
			PreviousExecutionTarget.Value = PreviousExecutionTarget_Cached;
			return _taskResult;
		}
	}
}