using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype {
    [TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Resets bTargetEnemy and CurrentTargettedEnemy If Character Is Free Moving. AlsoResetsIfUsingAbility Checkbox Will Also Reset Target if Using Ability.")]
	public class ResetHasTargetAndTransform : Action
	{
		#region Shared
		public SharedBool bIsFreeMoving;
		public SharedBool bTargetEnemy;
		public SharedTransform CurrentTargettedEnemy;
		public SharedBool AlsoResetsIfUsingAbility;
		public SharedBool bTryUseAbility;
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

		bool bIsFreeMoving_Cached;
		bool bTargetEnemy_Cached;
		Transform CurrentTargettedEnemy_Cached;
		bool AlsoResetsIfUsingAbility_Cached;
		bool bTryUseAbility_Cached;
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			bIsFreeMoving_Cached = bIsFreeMoving.Value;
			bTargetEnemy_Cached = bTargetEnemy.Value;
			CurrentTargettedEnemy_Cached = CurrentTargettedEnemy.Value;
			AlsoResetsIfUsingAbility_Cached = AlsoResetsIfUsingAbility.Value;
			bTryUseAbility_Cached = bTryUseAbility.Value;
			var _taskStatus = behaviorActions.ResetHasTargetAndTransform(
				ref bIsFreeMoving_Cached, ref bTargetEnemy_Cached, 
				ref CurrentTargettedEnemy_Cached, ref AlsoResetsIfUsingAbility_Cached, 
				ref bTryUseAbility_Cached) ?
				TaskStatus.Success : TaskStatus.Failure;
			bIsFreeMoving.Value = bIsFreeMoving_Cached;
			bTargetEnemy.Value = bTargetEnemy_Cached;
			CurrentTargettedEnemy.Value = CurrentTargettedEnemy_Cached;
			AlsoResetsIfUsingAbility.Value = AlsoResetsIfUsingAbility_Cached;
			bTryUseAbility.Value = bTryUseAbility_Cached;
			return _taskStatus;
		}
		#endregion

	} 
}