using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Resets Try Use Ability and Ability To Use Variables.")]
	public class ResetTryUseSpecialAbility : Action
	{
		#region Shared
		public SharedBool bTryUseAbility;
		public SharedBool bIsPerformingAbility;
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

		public bool bTryUseAbility_Cached;
		public bool bIsPerformingAbility_Cached;
		public UnityEngine.Object AbilityToUse_Cached;
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			bTryUseAbility_Cached = bTryUseAbility.Value;
			bIsPerformingAbility_Cached = bIsPerformingAbility.Value;
			AbilityToUse_Cached = AbilityToUse.Value;
			var _taskStatus = behaviorActions.ResetTryUseSpecialAbility
				(ref bTryUseAbility_Cached, ref bIsPerformingAbility_Cached, 
				ref AbilityToUse_Cached) ?
				TaskStatus.Success : TaskStatus.Failure;
			bTryUseAbility.Value = bTryUseAbility_Cached;
			bIsPerformingAbility.Value = bIsPerformingAbility_Cached;
			AbilityToUse.Value = AbilityToUse_Cached;
			return _taskStatus;
		}
		#endregion

	}
}