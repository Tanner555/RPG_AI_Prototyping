using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Updates Active Time Bar if bUpdateActiveTimeBar is Set. ActiveTimeBarRefillRate Determines How Much the Bar is Filled On Each Update.")]
	public class UpdateActiveTimeBar : Action
	{
		#region Shared
		public SharedBool bUpdateActiveTimeBar;
		public SharedInt ActiveTimeBarRefillRate;
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
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			return behaviorActions.UpdateActiveTimeBar(bUpdateActiveTimeBar.Value,
				ActiveTimeBarRefillRate.Value) ? TaskStatus.Success : TaskStatus.Failure;
		}
		#endregion
	}
}