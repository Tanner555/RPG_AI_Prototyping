using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype {
	[TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Checks If Target is In Range Using RPG Weapon Attack Range.")]
	public class IsAllyTargetInRange : Conditional
	{
		#region Shared
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

		Transform CurrentTargettedEnemy_Cached;
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			CurrentTargettedEnemy_Cached = CurrentTargettedEnemy.Value;
			var _taskStatus = behaviorActions.IsAllyTargetInRange(
				ref CurrentTargettedEnemy_Cached) ?
				TaskStatus.Success : TaskStatus.Failure;
			CurrentTargettedEnemy.Value = CurrentTargettedEnemy_Cached;
			return _taskStatus;
		}
		#endregion
	} 
}