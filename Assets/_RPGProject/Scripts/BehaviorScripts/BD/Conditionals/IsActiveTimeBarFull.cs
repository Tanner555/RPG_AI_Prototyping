using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Returns True if The Active Time Bar Is Full. Will Return False if AllyMember doesn't exist.")]
	public class IsActiveTimeBarFull : Conditional
	{
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
			var _taskStatus = behaviorActions.IsActiveTimeBarFull() ?
				TaskStatus.Success : TaskStatus.Failure;
			return _taskStatus;
		}
		#endregion
	}
}