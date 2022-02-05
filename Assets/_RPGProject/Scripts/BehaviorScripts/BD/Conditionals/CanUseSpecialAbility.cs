using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Checks If Ability Can Be Used.")]
	public class CanUseSpecialAbility : Conditional
	{
		#region Shared
		public SharedObject AbilityToUse;
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
			var _taskStatus = behaviorActions.CanUseSpecialAbility(AbilityToUse.Value) ?
				TaskStatus.Success : TaskStatus.Failure;
			return _taskStatus;
		}
		#endregion
	}
}