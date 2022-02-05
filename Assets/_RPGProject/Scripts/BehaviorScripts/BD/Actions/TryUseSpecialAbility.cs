using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Try Using The Ability In The Ability To Use Slot. Retrieves Ability Animation Time From Ability Config.")]
	public class TryUseSpecialAbility : Action
	{
		#region Shared
		public SharedObject AbilityToUse;
		public SharedFloat AbilityAnimationTime;
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

		private UnityEngine.Object AbilityToUse_Cached;
		private float AbilityAnimationTime_Cached;

		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			AbilityToUse_Cached = AbilityToUse.Value;
			AbilityAnimationTime_Cached = AbilityAnimationTime.Value;
			var _taskStatus = behaviorActions.TryUseSpecialAbility
				(ref AbilityToUse_Cached, ref AbilityAnimationTime_Cached) ?
				TaskStatus.Success : TaskStatus.Failure;
			AbilityToUse.Value = AbilityToUse_Cached;
			AbilityAnimationTime.Value = AbilityAnimationTime_Cached;
			return _taskStatus;
		}
		#endregion
	}
}