using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Attempts To Use Weapon And Attack Enemy. Bind HalfWeaponAttackRate Variable For Wait Node. Currently Doesn't Apply Damage To Target. Use DamageTarget Task To Apply Damage.")]
	public class TryAttackTarget : Action
	{
		#region Shared
		public SharedFloat HalfWeaponAttackRate;
		public SharedTransform CurrentTargettedEnemy;
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

		float HalfWeaponAttackRate_Cached;
		Transform CurrentTargettedEnemy_Cached;
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			HalfWeaponAttackRate_Cached = HalfWeaponAttackRate.Value;
			CurrentTargettedEnemy_Cached = CurrentTargettedEnemy.Value;
			var _taskStatus = behaviorActions.TryAttackTarget(
				ref HalfWeaponAttackRate_Cached, 
				ref CurrentTargettedEnemy_Cached) ?
				TaskStatus.Success : TaskStatus.Failure;
			HalfWeaponAttackRate.Value = HalfWeaponAttackRate_Cached;
			CurrentTargettedEnemy.Value = CurrentTargettedEnemy_Cached;
			return _taskStatus;
		}
		#endregion

	}
}