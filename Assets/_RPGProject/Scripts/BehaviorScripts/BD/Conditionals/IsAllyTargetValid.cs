using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype {
	[TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Simple Valid Target Check using bTargetEnemy and CurrentTargettedEnemy. CheckAllyAndResetTargetIfFail Checkbox Will Check Ally (Not NULL and isAlive) And Reset Target if Fails")]
	public class IsAllyTargetValid : Conditional
	{
		#region Shared
		public SharedBool bTargetEnemy;
		public SharedTransform CurrentTargettedEnemy;
		[BehaviorDesigner.Runtime.Tasks.Tooltip("Check Ally And Will Reset Target if Fails")]
		public SharedBool CheckAllyAndResetTargetIfFail = false;
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

		bool bTargetEnemy_Cached;
		Transform CurrentTargettedEnemy_Cached;
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			bTargetEnemy_Cached = bTargetEnemy.Value;
			CurrentTargettedEnemy_Cached = CurrentTargettedEnemy.Value;
			var _taskStatus = behaviorActions.IsAllyTargetValid(
				ref bTargetEnemy_Cached, ref CurrentTargettedEnemy_Cached,
				CheckAllyAndResetTargetIfFail.Value) ?
				TaskStatus.Success : TaskStatus.Failure;
			bTargetEnemy.Value = bTargetEnemy_Cached;
			CurrentTargettedEnemy.Value = CurrentTargettedEnemy_Cached;
			return _taskStatus;
		}

		public override void OnReset()
		{
			//_CurrentTargettedEnemyAlly = null;
		}
		#endregion
	}
}